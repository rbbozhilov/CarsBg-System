using CarsBg_System.Areas.Admin.Views.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace CarsBg_System.Areas.Admin.Models.Car
{
    public class ModelFormModel
    {

        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        public string Name { get; set; }

        [Required]
        public int BrandId { get; set; }
        public IEnumerable<ShowBrandViewModel>? Brands { get; set; }

    }
}
