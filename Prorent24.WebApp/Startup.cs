using DinkToPdf;
using DinkToPdf.Contracts;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Prorent24.BLL;
using Prorent24.Common.ApplicationSettings;
using Prorent24.Common.Hubs;
using Prorent24.Context;
using Prorent24.DAL;
using Prorent24.Entity;
using Prorent24.UnitOfWork.Extensions;
using Prorent24.WebApp.Deployment;
using Prorent24.WebApp.Extentions;
using Prorent24.WebApp.Filters;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Prorent24.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        private AppSettings appSettings = new AppSettings();
        public IConfiguration Configuration { get; }
        private readonly IHostingEnvironment _env;
        private string connectionString = string.Empty;

        public void ConfigureServices(IServiceCollection services)
        {
            Configuration.GetSection("AppSettings").Bind(appSettings);
            services.Configure<AppSettings>(options => Configuration.GetSection("AppSettings").Bind(options));

            var jsonSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateTimeZoneHandling = DateTimeZoneHandling.Local
            };

            JsonConvert.DefaultSettings = () => jsonSettings;

            //Create config with envirement
            ConfigurationSettings.Configure(_env.EnvironmentName);

            //Create Directories
            DeploymentNewClient.CreateDirectories();

            // Access for Files
            services.AddSingleton<IFileProvider>(
            new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), DirectorySettings.fileDirectory)));

            services.AddSingleton<IFileProvider>(
            new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), DirectorySettings.backupDirectory)));

            services.AddSingleton<IFileProvider>(
            new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), DirectorySettings.logsDirectory)));

            services.AddSingleton<IFileProvider>(
            new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), DirectorySettings.dataBasesDirectory)));

            services.AddSingleton<IFileProvider>(
           new PhysicalFileProvider(
               Path.Combine(Directory.GetCurrentDirectory(), DirectorySettings.reportDirectory)));

            //connectionString = $"Filename={DirectorySettings.dataBasesDirectory}{_env.EnvironmentName}.db";
            connectionString = Configuration["ConnectionStrings:Prorent24DbConnection"];
            //string connectionStringLog = $"Filename={DirectorySettings.dataBasesDirectory}{"LogProrent"}.db";
            //string connectionStringLog = $"Filename='C:\\_SystemData\\1\\DataBases\\LogProrent.db'";
            string connectionStringLog = Configuration["ConnectionStrings:LogProrentContext"];

            // Db context
            services
                .AddEntityFrameworkSqlite()
                .AddDbContext<Prorent24DbContext>(options => options
                            .EnableSensitiveDataLogging()
                            .UseSqlite(connectionString, opt => opt.SuppressForeignKeyEnforcement()))
                .AddDbContext<LogProrentContext>(options => options
                            .UseSqlite(connectionStringLog)); 


            // Add EntityFramework UnitOfWork
            services.AddUnitOfWork<Prorent24DbContext>();

            // ASP Identity
            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<Prorent24DbContext>()
            .AddDefaultTokenProviders();

            services.AddScoped<IUserClaimsPrincipalFactory<User>, ClaimsPrincipalFactory>();
            services.AddHttpContextAccessor();

            // Cookie configure
            services.ConfigureApplicationCookie(options =>
            {
                options.Events.OnRedirectToAccessDenied = context =>
                {
                    context.Response.StatusCode = 403;
                    return Task.CompletedTask;
                };
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                };
            });

            // Add layers
            services.AddBusinessLogicLayer();
            services.AddDataAccessLayer();

            // Add SignalR
            services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();
            services.AddSignalR();
            services.AddNotifier();

            // Add converter to DI
            var assemblyContext = new CustomAssemblyLoadContext();
            assemblyContext.LoadUnmanagedLibrary(Path.Combine(DirectorySettings.currentDirectory, "libwkhtmltox.dll"));
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("ru-RU");
                options.SupportedCultures = new List<CultureInfo> { new CultureInfo("en-US"), new CultureInfo("ru-RU") };
            });

            var cultureInfo = new CultureInfo("ru-RU");
            cultureInfo.NumberFormat.CurrencySymbol = "RUB";

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            //Mediator
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddMvc(config =>
            {
                config.Filters.Add(new ExceptionLoggerAttribute());

                config.RespectBrowserAcceptHeader = true;
                config.OutputFormatters.Clear();
                config.OutputFormatters.Add(new JsonOutputFormatter(new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore,
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }, ArrayPool<char>.Shared));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info { Title = "Prorent24", Version = "v1" });
                options.DescribeAllEnumsAsStrings();
                options.EnableAnnotations();

                options.OrderActionsBy((apiDesc) => $"{apiDesc.ActionDescriptor.RouteValues["controller"]}_{apiDesc.HttpMethod}");

                var basePath = PlatformServices.Default.Application.ApplicationBasePath;

            });

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "Prorent24.App/dist";
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseAuthentication();

            app.UseSignalR((configure) =>
            {
                var desiredTransports =
                    HttpTransportType.WebSockets |
                    HttpTransportType.LongPolling;

                configure.MapHub<HubConnector>("/hub", (options) =>
                {
                    options.Transports = desiredTransports;
                });
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");

                c.DisplayOperationId();
                c.DisplayRequestDuration();
                c.DocExpansion(DocExpansion.None);
                c.EnableFilter();
                c.ShowExtensions();
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            var dbContext = serviceProvider.GetService<Prorent24DbContext>();
            PostDeployment.ExecutePostDeployment(dbContext, connectionString, env.EnvironmentName);

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "Prorent24.App";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
