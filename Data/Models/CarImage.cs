using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bito_Sandor_Autokereskedes.Data.Models
{
    public class CarImage
    {
        [Key]
        public int Id { get; set; }
        public byte[] ImageData { get; set; }
        [ForeignKey("Car")]
        public int CarId { get; set; }
        public virtual Car Car { get; set; }
    }
}
