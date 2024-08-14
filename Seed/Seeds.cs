using Autokereskedes_BS.Data;
using Microsoft.AspNetCore.Identity;

namespace Autokereskedes_BS.Seed
{
    public static class Seeds
    {
        public static IEnumerable<(ApplicationUser, string)> getBaseUsers()
        {
            return new List<(ApplicationUser,string)>()
            {
                (new ApplicationUser()
                {
                    UserName = "admin",
                    FullName = "Adminisztrá Thor",
                    Email = "a@a.com",
                    Active = true,
                    EmailConfirmed = true
                },"123456aA!"),
                (new ApplicationUser()
                {
                    UserName = "user",
                    FullName = "Use Róbert",
                    Email = "n@n.com",
                    Active = true,
                    EmailConfirmed = true
                },"123456aA!")
            };
        }
        public static IEnumerable<IdentityRole> getBaseRoles()
        {
            return new List<IdentityRole>()
            {
                new IdentityRole("Administrator")
            };
        }
        public static IEnumerable<(string, string)> BaseRolesForUsers()
        {
            return new List<(string, string)>()
            {
                ("admin","Administrator")
            };
        }
    }
}
