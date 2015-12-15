namespace CRIMAS.Migrations
{
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
                    AccountNo ="",
                    Name="Olumide Jegede",
                    NextOfkin="Olumide Jegede",
                    LocalGovtArea="jagsdgad",
                    OfficeAddress="dhgfjkgfabfha",
                    ResidentialAddress="dasgdfasbgfhjaf",
                    DateCreated="today",
                    VillageClan="fgfjk"
                    }
                );
            
        }
        private void SeedMembership()
        {
            WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);

            var roles = (SimpleRoleProvider)Roles.Provider;
            var membership = (SimpleMembershipProvider)Membership.Provider;

            //Create Admin role
            if (!roles.RoleExists("Admin"))
            {
                roles.CreateRole("Admin");
            }
            if (membership.GetUser("Daniel Adigun", false) == null)
            {
                membership.CreateUserAndAccount("Daniel Adigun", "morphy");
            }
            if (!roles.GetRolesForUser("Daniel Adigun").Contains("Admin"))
            {
                roles.AddUsersToRoles(new[] { "Daniel Adigun" }, new[] { "Admin" });
            }

        }
    }
}
