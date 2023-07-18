using System.ComponentModel.DataAnnotations;

namespace CarsBg_System.Data.Models
{
    public class Engine
    {

        public Engine()
        {
            this.Cars = new HashSet<Car>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
