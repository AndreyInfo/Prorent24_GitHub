using Prorent24.Common.Attributes;
using Prorent24.Common.Models.Excels;
using Prorent24.Common.Models.Trees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Prorent24.Common.Models.Columns;
using Prorent24.Context;
using Microsoft.EntityFrameworkCore;

namespace Prorent24.Common.Extentions
{
    public class LogManage
    {

        /// <summary>
        /// Add log authentication
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static void AddLogAuth(LogProrentContext _context, string insertedEmail, bool isSuccessAuth, string source, string userId = null, string userName = null)
        {
            LogMain log = new LogMain();
            log.CreateDate = DateTime.Now.ToString("yyyy'-'MM'-'dd' 'HH':'mm':'ss'.'fffffff");
            log.InsertedEmail = insertedEmail;
            log.IsSuccessAuth = isSuccessAuth;
            log.Source = source;
            log.UserId = userId;
            log.UserName = userName;

            _context.Entry(log).State = EntityState.Added;
            _context.SaveChanges();
        }


    }
}
