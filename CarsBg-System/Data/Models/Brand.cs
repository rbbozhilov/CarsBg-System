using System.ComponentModel.DataAnnotations;

namespace CarsBg_System.Data.Models
{
    public class Brand
    {

        public Brand()
        {
            this.Models = new HashSet<Model>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(60)]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<Model> Models { get; set; }
    }
}
