using System.ComponentModel.DataAnnotations;

namespace CarsBg_System.Areas.Admin.Models.Region
{
    public class RegionFormModel
    {

        [Required]
        [MaxLength(50)]
        [MinLength(2)]
        public string Name { get; set; }
    }
}
