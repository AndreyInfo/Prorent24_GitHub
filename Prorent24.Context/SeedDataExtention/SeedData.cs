using Microsoft.EntityFrameworkCore;
using Prorent24.Entity;
using Prorent24.Entity.Configuration.CustomerCommunication;
using Prorent24.Entity.General;
using Prorent24.Enum.General;
using System;
using Prorent24.Enum.Configuration;
using Prorent24.Entity.Directory;
using Prorent24.Enum.Directory;
using Prorent24.Entity.CrewMember;

namespace Prorent24.Context.SeedDataExtention
{
    public static class SeedData
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
          //  var roleIds = new { Default = Guid.NewGuid(), Poweruser = Guid.NewGuid(), Freelancer = Guid.NewGuid(), Office = Guid.NewGuid() };

            // Users Seeds
            //modelBuilder.Entity<User>().HasData(
            //    new User
            //    {
            //        Id = "542df0b7-3dee-42bc-b627-8e34e30934ab",
            //        UserName = "prorent24@gmail.com",
            //        NormalizedUserName = "PRORENT24@GMAIL.COM",
            //        Email = "prorent24@gmail.com",
            //        NormalizedEmail = "PRORENT24@GMAIL.COM",
            //        EmailConfirmed = false,
            //        PasswordHash = "AQAAAAEAACcQAAAAEPKwZWZh610m0A5k7YkHWOyC3cS0h/MKJM+La/YqG1ZSLYcO24ywMuVseAd9169BJw==",
            //        SecurityStamp = "CNXQU2PS3FUVJKFUTHKLGCHX2PIEOQPT",
            //        ConcurrencyStamp = "99582450-eaf8-4dea-8415-06151a56a5fb",
            //        //PhoneNumber,
            //        PhoneNumberConfirmed = false,
            //        TwoFactorEnabled = false,
            //        // LockoutEnd,
            //        LockoutEnabled = true,
            //        AccessFailedCount = 0
            //    }
            //);

            //modelBuilder.Entity<CrewMemberEntity>().HasData(
            //    new CrewMemberEntity()
            //    {
            //        UserId = "542df0b7-3dee-42bc-b627-8e34e30934ab",
            //        CompanyName = "Company"
            //    });
            // Шире открой глаза, живи так жадно, как будто через десять секунд умрешь. Старайся увидеть мир.Он прекрасней любой мечты, созданной на фабрике и оплаченной деньгами
            // Roles Seed
            //modelBuilder.Entity<Role>().HasData(
            //    new Role() { Id = roleIds.Default.ToString(), Name = "Default user role", NormalizedName = "DEFAULT USER ROLE" },
            //    new Role() { Id = roleIds.Poweruser.ToString(), Name = "Poweruser", NormalizedName = "POWERUSER" },
            //    new Role() { Id = roleIds.Freelancer.ToString(), Name = "Freelancer", NormalizedName = "FREELANCER" },
            //    new Role() { Id = roleIds.Office.ToString(), Name = "Office", NormalizedName = "OFFICE" }
            //    );
        }
    }
}
