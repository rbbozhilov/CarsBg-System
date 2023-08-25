using System.ComponentModel.DataAnnotations;

namespace CarsBg_System.Areas.Admin.Models.Brand
{
    public class BrandFormModel
    {

        [MaxLength(60)]
        [MinLength(1)]
        [Required]
        public string Name { get; set; }


    }
}
