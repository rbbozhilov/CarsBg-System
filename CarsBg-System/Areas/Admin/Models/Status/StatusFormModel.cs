using System.ComponentModel.DataAnnotations;

namespace CarsBg_System.Areas.Admin.Models.Status
{
    public class StatusFormModel
    {

        [Required]
        [MaxLength(15)]
        [MinLength(1)]
        public string Name { get; set; }
    }
}
