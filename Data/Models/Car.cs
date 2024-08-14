using Autokereskedes_BS.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bito_Sandor_Autokereskedes.Data.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public string FuelType { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        [ForeignKey("CarModel")]
        public int CarModelId { get; set; }
        public virtual ICollection<CarImage> CarImages { get; set; }
        public virtual CarModel CarModel { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}