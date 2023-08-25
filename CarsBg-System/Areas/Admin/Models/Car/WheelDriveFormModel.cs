using System.ComponentModel.DataAnnotations;

namespace CarsBg_System.Areas.Admin.Models.Car
{
    public class WheelDriveFormModel
    {

        [Required]
        [MaxLength(10)]
        [MinLength(1)]
        public string Name { get; set; }

    }
}
