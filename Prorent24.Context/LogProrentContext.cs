using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Prorent24.Context.Data;
using Prorent24.Context.Logger;
using Prorent24.Context.SeedDataExtention;
using Prorent24.Entity;
using Prorent24.Entity.Base;
using Prorent24.Entity.Configuration;
using Prorent24.Entity.Configuration.CustomerCommunication;
using Prorent24.Entity.Configuration.Financial;
using Prorent24.Entity.Configuration.Roles;
using Prorent24.Entity.Configuration.Settings;
using Prorent24.Entity.Contact;
using Prorent24.Entity.CrewMember;
using Prorent24.Entity.CrewPlanner;
using Prorent24.Entity.Directory;
using Prorent24.Entity.Equipment;
using Prorent24.Entity.General;
using Prorent24.Entity.Identity;
using Prorent24.Entity.Invoice;
using Prorent24.Entity.Maintenance;
using Prorent24.Entity.Notification;
using Prorent24.Entity.Project;
using Prorent24.Entity.Subhire;
using Prorent24.Entity.Tasks;
using Prorent24.Entity.TimeRegistration;
using Prorent24.Entity.Vehicle;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Prorent24.Context
{
    public class LogProrentContext : DbContext
    {

        public LogProrentContext(DbContextOptions<LogProrentContext> options) : base(options)
        {
            //this.Database.EnsureCreated();
        }

        public DbSet<LogMain> LogMain { get; set; }
    }

    [Table("LogMain")]
    public class LogMain
    {
        [Key]
        public int Id { get; set; }

        public string CreateDate { get; set; }
        public string InsertedEmail { get; set; }
        public bool IsSuccessAuth { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Source { get; set; }
    }

    }
