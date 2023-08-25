using System.ComponentModel.DataAnnotations;

namespace CarsBg_System.Areas.Admin.Models.Car
{
    public class TransmissionFormModel
    {
        [Required]
        [MaxLength(20)]
        [MinLength(1)]
        public string Name { get; set; }
    }
}
