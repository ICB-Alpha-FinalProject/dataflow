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

            //var user = context.Users.FirstOrDefault();

            //var valueTypeSensor = new ValueTypeSensor()
            //{
            //    MeasurementType = "Temprature",
            //    MinValue = 15,
            //    Maxvalue = 28,
            //    CurrentValue = 20
            //};

            //var boolTypeSensor = new BoolTypeSensor()
            //{
            //    MeasurementType = "Open/Close",
            //    CurrentValue = true
            //};

            //var valueSensor = new Sensor()
            //{
            //    Name = "MyTempratureSensor",
            //    Description = "Sensor which measures temprature",
            //    URL = "url",
            //    PollingInterval = 30,
            //    ValueTypeSensor = valueTypeSensor,
            //    IsPublic = true,
            //    Creator = user
            //};

            //var boolSensor = new Sensor()
            //{
            //    Name = "MyDoorSensor",
            //    Description = "Sensor which indicates whether the door is open",
            //    URL = "url",
            //    PollingInterval = 30,
            //    BoolTypeSensor = boolTypeSensor,
            //    IsPublic = false,
            //    Creator = user
            //};

            //user.MySensors.Add(boolSensor);

            //context.BoolSensors.Add(boolTypeSensor);
            //context.ValueSensors.Add(valueTypeSensor);
            //context.Sensors.Add(valueSensor);
            //context.Sensors.Add(boolSensor);
            //context.SaveChanges();

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
            //            .First(u => u.UserName == "test@test.bg")
            //            .Roles.Add(new IdentityUserRole() { RoleId = role.Id });

            //        ctx.SaveChanges();
            //    }
            //}


            //if (!context.Roles.Any())
            //{
            //    var role = context.Roles.Add(new IdentityRole("Admin"));
            //    context.SaveChanges();

            //    role = context.Roles.Single();
            //    var user = context.Users.Single();
            //    user.Roles.Add(new IdentityUserRole()
            //    {
            //        RoleId = role.Id,
            //        UserId = user.Id
            //    });

            //    context.SaveChanges();
            //}

            //if (context.Roles.Any())
            //{
            //    var role = context.Roles.Single();


            //    context.Users
            //        .First(u => u.UserName == "test@test.bg")
            //        .Roles.Add(new IdentityUserRole() { RoleId = role.Id });

            //    context.SaveChanges();
            //}
        }
    }
}

