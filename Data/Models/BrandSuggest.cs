using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Autokereskedes_BS.Data.Models
{
    public class BrandSuggest
    {
        [Key]
        public int Id { get; set; }
        public string SuggestedName { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
