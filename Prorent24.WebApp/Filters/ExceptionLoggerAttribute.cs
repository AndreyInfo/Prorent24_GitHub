using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Prorent24.WebApp.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class ExceptionLoggerAttribute : IExceptionFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            OkObjectResult objectResult = new OkObjectResult(context.Exception.Message);

            bool? existsNewStatusCode = context.Exception.InnerException?.Data?.Contains("StatusCode");

            if (existsNewStatusCode.HasValue && existsNewStatusCode.Value)
            {
                object newStatusCode = context.Exception.InnerException.Data["StatusCode"];
                objectResult.StatusCode = Convert.ToInt32(newStatusCode);
            }
            else
            {
                if(context.Exception.Message.Contains("Object reference not set to an instance of an object."))
                {
                    object newStatusCode = 400;
                    objectResult.StatusCode = Convert.ToInt32(newStatusCode);
                    objectResult.Value = "errors.module_loading_error_has_occurred";
                }
                else
                {
                    objectResult.StatusCode = 428;
                }
            }

            context.Result = objectResult;
        }
    }
}
