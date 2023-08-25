using System.ComponentModel.DataAnnotations;

namespace CarsBg_System.Areas.Admin.Models.Car
{
    public class ExtraFormModel
    {

        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        public string Name { get; set; }
    }
}
