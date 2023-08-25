using System.ComponentModel.DataAnnotations;

namespace CarsBg_System.Areas.Admin.Models.Category
{
    public class CategoryFormModel
    {

        [Required]
        [MaxLength(50)]
        [MinLength(1)]
        public string Name { get; set; }

    }
}
