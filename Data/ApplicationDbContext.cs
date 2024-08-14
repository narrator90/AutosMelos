using Autokereskedes_BS.Data.Models;
using Bito_Sandor_Autokereskedes.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Autokereskedes_BS.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<CarBrand> Brands { get; set; }
        public DbSet<CarModel> CarModels { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarImage> Images { get; set; }
        public DbSet<BrandSuggest> brandSuggests { get; set; }
    }
}
