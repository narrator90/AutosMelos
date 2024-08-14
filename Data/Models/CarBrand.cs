using System.ComponentModel.DataAnnotations;

namespace Bito_Sandor_Autokereskedes.Data.Models
{
    public class CarBrand
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<CarModel> CarModels { get; set; }
    }
}
