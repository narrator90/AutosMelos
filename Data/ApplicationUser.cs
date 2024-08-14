using Bito_Sandor_Autokereskedes.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace Autokereskedes_BS.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public bool Active { get; set; }
        public bool DeleteRequested { get; set; }
        public DateTime DeleteRequestTime { get; set; }
        public virtual ICollection<Car> Cars { get; set; }
    }

}
