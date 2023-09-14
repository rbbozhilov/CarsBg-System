using System.ComponentModel.DataAnnotations;

namespace CarsBg_System.Models.Post
{
    public class PostFormModel
    {

        [Required]
        [MaxLength(1024)]
        [MinLength(2)]
        public string Comment { get; set; }


    }
}
