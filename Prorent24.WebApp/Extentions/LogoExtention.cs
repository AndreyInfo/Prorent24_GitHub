using Microsoft.AspNetCore.Http;
using Prorent24.Common.ApplicationSettings;
using System.IO;
using System.Linq;

namespace Prorent24.WebApp.Extentions
{
    /// <summary>
    /// 
    /// </summary>
    public static class LogoExtention
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="environmentName"></param>
        /// <returns></returns>
        public static string GetCompanyLogo(this HttpContext context, string environmentName)
        {
            string localFileDirectory = Path.Combine(
              Directory.GetCurrentDirectory(), DirectorySettings.fileDirectory);

            string pathToCompanyLogo = context.GetOldPathToCompanyLogo(environmentName);

            string imgPath = null;
            if (!string.IsNullOrWhiteSpace(pathToCompanyLogo))
            {
                pathToCompanyLogo = pathToCompanyLogo.Replace(localFileDirectory + "\\", "");

                imgPath = context.GeneratePathToCompanyLogo(environmentName, pathToCompanyLogo); 
            }

            return imgPath;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="environmentName"></param>
        /// <returns></returns>
        public static string GetOldPathToCompanyLogo(this HttpContext context, string environmentName)
        {
            string localFileDirectory = Path.Combine(
              Directory.GetCurrentDirectory(), DirectorySettings.fileDirectory);

            string companyLogoPath = Directory.GetFiles(localFileDirectory).FirstOrDefault(x => x.Contains(string.Concat("company-logo_", environmentName, "_")));

            return companyLogoPath;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="environmentName"></param>
        /// <param name="pathToFile"></param>
        /// <returns></returns>
        public static string GeneratePathToCompanyLogo(this HttpContext context, string environmentName, string pathToFile)
        {
            string imgPath = string.Concat(context.Request.Scheme, "://", context.Request.Host, $"/Files/{environmentName}/{pathToFile}");
            return imgPath;
        }
    }
}
