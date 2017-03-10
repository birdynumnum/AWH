using Domain;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Linq;

namespace Data
{
    public class AWHDbConfiguration : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            CreateInitial(context);
        }

        public void CreateInitial(ApplicationDbContext context)
        {

            string Adminrole = "admin";

            string[] nonadminroles = new string[2] { "invitee", "parent" };

            string Inviteerole = "invitee";
            string Parentrole = "parent";

            if (!(context.Users.Any(u => u.UserName == "Admin@admin.com")))
            {
                //Create Admin and assign to admin role
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

                var userToInsert = new ApplicationUser { UserName = "admin", PhoneNumber = "0229853193" };
                userManager.Create(userToInsert, "Password@123");

                if (!RoleManager.RoleExists(Adminrole))
                {
                    var roleresult = RoleManager.Create(new IdentityRole(Adminrole));
                }

                var result = userManager.AddToRole(userToInsert.Id, Adminrole);

                //Create Invitee and Parent roles
                if (!RoleManager.RoleExists(Inviteerole))
                {
                    var roleresult = RoleManager.Create(new IdentityRole(Inviteerole));
                }

                if (!RoleManager.RoleExists(Parentrole))
                {
                    var roleresult = RoleManager.Create(new IdentityRole(Parentrole));
                }

                foreach (string role in nonadminroles)
                {
                    if (!RoleManager.RoleExists(role))
                    {
                        var roleresult = RoleManager.Create(new IdentityRole(role));
                    }
                }
            }
        }
    }
}