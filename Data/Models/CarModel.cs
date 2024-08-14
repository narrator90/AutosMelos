using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bito_Sandor_Autokereskedes.Data.Models
{
    public class CarModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("CarBrand")]
        public int CarBrandId { get; set; }
        public virtual CarBrand CarBrand { get; set; }
        public virtual ICollection<Car> Cars { get; set; }
    }
}
