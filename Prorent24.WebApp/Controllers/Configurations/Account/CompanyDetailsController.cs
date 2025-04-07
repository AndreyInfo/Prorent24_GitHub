using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Prorent24.BLL.Models.Configuration.AccountConfiguration;
using Prorent24.BLL.Services;
using Prorent24.BLL.Services.Configuration.AccountConfiguration.CompanyDetails;
using Prorent24.Common.ApplicationSettings;
using Prorent24.Common.Extentions;
using Prorent24.Enum.Configuration.ConfigurationRoles;
using Prorent24.Enum.General;
using Prorent24.WebApp.Extentions;
using Prorent24.WebApp.Models.Configuration.Account;
using Prorent24.WebApp.Transfers.Configurations.Account;
using Swashbuckle.AspNetCore.Annotations;

namespace Prorent24.WebApp.Controllers.Configurations.Account
{
    /// <summary>
    /// Configuration company details
    /// </summary>
    [Route("api/configuration/account/company_details")]
    [ApiController]
    [SwaggerTag("Configuration -> Account")]
    public class CompanyDetailsController : ControllerBase
    {
        private readonly ICompanyDetailsService _companyDetailsService;
        private readonly IHostingEnvironment _env;
        private IMemoryCache _cache;
        private readonly IPermissionService _permissionService;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="companyDetailsService"></param>
        /// <param name="env"></param>
        /// <param name="cache"></param>
        /// <param name="permissionService"></param>
        public CompanyDetailsController(ICompanyDetailsService companyDetailsService,
            IHostingEnvironment env,
            IMemoryCache cache,
            IPermissionService permissionService)
        {
            _companyDetailsService = companyDetailsService;
            _env = env;
            _cache = cache;
            _permissionService = permissionService;
        }
        /// <summary>
        /// Get Company Details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(CompanyDetailsViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCompanyDetails()
        {
            CompanyDetailsDto dto = await _companyDetailsService.GetAsync();
            return Ok(dto.TransferToViewModel());
        }

        /// <summary>
        /// Update Company Details
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(CompanyDetailsViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateCompanyDetails([FromBody]CompanyDetailsViewModel model)
        {
            UserRoleModulePermissionDto permission = await _permissionService.GetPermissions(ModuleTypeEnum.Configuration);
            if (permission.IsAllowed && permission.Functions?.Where(x => x.Function == PermissionFieldEnum.CompanyInfo).FirstOrDefault()?.Value?.ToLower() == "yes")
            {
                CompanyDetailsDto dto = await _companyDetailsService.Update(model.TransferToDto());
                return Ok(dto.TransferToViewModel());
            }
            else
            {
                return Forbid();
            }
        }

        /// <summary>
        /// Upload Background image
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("upload/background-image")]
        public async Task<IActionResult> UploadBackgroundImage(IFormFile file)
        {
            // your other code
            if (file.Length > 0)
            {
                string outputFileName = file.FileName.Substring(file.FileName.IndexOf('.') + 1);
                string newFileName = string.Concat("company-logo_", _env.EnvironmentName, ".", outputFileName);
                var path = Path.Combine(
                           Directory.GetCurrentDirectory(), DirectorySettings.fileDirectory,
                           newFileName);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                CompanyDetailsDto dto = await _companyDetailsService.GetAsync();
                string imgPath = string.Concat(this.HttpContext.Request.Scheme, "://", this.HttpContext.Request.Host, $"/Files/{_env.EnvironmentName}/{newFileName}");
                dto.BackgroundImage = imgPath;
                await _companyDetailsService.Update(dto);

                return Ok(dto.BackgroundImage);
            }
            else
            {
                return NoContent();
            }

        }

        /// <summary>
        /// Upload Logo image
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("upload/logo-image")]
        public async Task<IActionResult> UploadLogoImage(IFormFile file)
        {
            // your other code
            if (file.Length > 0)
            {

                string newFileName = string.Concat("company-logo_", _env.EnvironmentName, "_", file.FileName);
                var path = Path.Combine(
                           Directory.GetCurrentDirectory(), DirectorySettings.fileDirectory,
                           newFileName);

                string pathToCompanyLogo = this.HttpContext.GetOldPathToCompanyLogo(_env.EnvironmentName);

                if (!string.IsNullOrWhiteSpace(pathToCompanyLogo))
                {
                    System.IO.File.Delete(pathToCompanyLogo);
                }

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                CompanyDetailsDto dto = await _companyDetailsService.GetAsync();
                string imgPath = this.HttpContext.GeneratePathToCompanyLogo(_env.EnvironmentName, newFileName);
                dto.LogoImage = imgPath;
                await _companyDetailsService.Update(dto);
                return Ok(imgPath);
            }
            else
            {
                return NoContent();
            }

        }
    }
}