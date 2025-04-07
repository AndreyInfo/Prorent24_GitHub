using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.PlatformAbstractions;
using Prorent24.WebApp;
using System;
using System.IO;
using System.Net.Http;

namespace Prorent24.Test
{
    public class TestServerFixture : IDisposable
    {
        private readonly TestServer _testServer;
        public HttpClient Client { get; }

        public TestServerFixture()
        {
            var builder = new WebHostBuilder()
                    .UseEnvironment("Development")
                    .UseStartup<Startup>();

            var config = new ConfigurationBuilder()
                .AddJsonFile(String.Concat(GetContentRootPath(), "appsettings.Development.json"))
                .Build();

            builder.UseConfiguration(config);

            _testServer = new TestServer(builder);
            Client = _testServer.CreateClient();

        }

        private string GetContentRootPath()
        {
            string testProjectPath = PlatformServices.Default.Application.ApplicationBasePath;
            string rootProjectPat = testProjectPath.Replace("Prorent24.Test\\bin\\Debug\\netcoreapp2.2", "Prorent24.WebApp");
            return rootProjectPat;
        }

        public void Dispose()
        {
            Client.Dispose();
            _testServer.Dispose();
        }
    }
}
