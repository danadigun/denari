namespace CRIMAS.Migrations
{
    using CRIMAS.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;
    using WebMatrix.WebData;

    internal sealed class Configuration : DbMigrationsConfiguration<CRIMAS.Models.CrimasDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(CRIMAS.Models.CrimasDb context)
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
            SeedCustomer();

            SeedMembership();
        }

        private void SeedCustomer()
        {
            var db = new CRIMAS.Models.CrimasDb();

            db.Customers.AddOrUpdate(
                 p => p.Name,
                new CRIMAS.Models.Customer {
                    AccountNo =new Random().Next().ToString(),
                    Name="Olumide Jegede",
                    NextOfkin="Olumide Jegede",
                    StateOfOrigin="jagsdgad",
                    OfficeAddress="dhgfjkgfabfha",
                    ResidentialAddress="dasgdfasbgfhjaf",
                    DateCreated=DateTime.Now.ToLongDateString(),
                    Email = "olumide@yahoo.com",
                    phone = "070387273812"
                    //VillageClan="fgfjk"
                    }
                );
            db.SaveChanges();
        }
        private void SeedMembership()
        {
            WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);

            var roles = (SimpleRoleProvider)Roles.Provider;
            var membership = (SimpleMembershipProvider)Membership.Provider;
            var _db = new CrimasDb();

            //Create Admin role
            if (!roles.RoleExists("Admin"))
            {
                roles.CreateRole("Admin");
            }
            if (membership.GetUser("admin@digitalforte.ng", false) == null)
            {
                membership.CreateUserAndAccount("admin@digitalforte.ng", "pass119");
            }
            if (!roles.GetRolesForUser("admin@digitalforte.ng").Contains("Admin"))
            {
                roles.AddUsersToRoles(new[] { "admin@digitalforte.ng" }, new[] { "Admin" });
            }

        }
    }
}
