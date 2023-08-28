using System.ComponentModel.DataAnnotations;

namespace CarsBg_System.Services.Engine
{
    public class EngineFormModel
    {
        [Required]
        [MaxLength()]
        [MinLength(1)]
        public string Name { get; set; }
    }
}