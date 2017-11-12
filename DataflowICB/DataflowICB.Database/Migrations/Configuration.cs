namespace DataflowICB.Database.Migrations
{
    using DataflowICB.Database.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<DataflowICB.Database.ApplicationDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
            this.AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(DataflowICB.Database.ApplicationDbContext context)
        {

          
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            //using (var ctx = new ApplicationDbContext())
            //{
            //    if (ctx.Users.Any())
            //    {
            //        var role = ctx.Roles.Add(new IdentityRole("Admin"));

            //        ctx.Users
            //            .First(u => u.UserName == "admin")
            //            .Roles.Add(new IdentityUserRole() { RoleId = role.Id });

            //        ctx.SaveChanges();
            //    }
            //}


            if (!context.Roles.Any())
            {
                var role = context.Roles.Add(new IdentityRole("Admin"));
                context.SaveChanges();

                role = context.Roles.Single();
                var user = context.Users
                    .Single(u => u.UserName == "user4");

                user.Roles.Add(new IdentityUserRole()
                {
                    RoleId = role.Id,
                    UserId = user.Id
                });

                context.SaveChanges();
            }

        }
    }
}

