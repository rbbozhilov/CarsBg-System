using System.ComponentModel.DataAnnotations;

namespace CarsBg_System.Data.Models
{
    public class Extra
    {

        public Extra()
        {
            this.Cars = new HashSet<Car>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public virtual ICollection<Car> Cars { get; set; }

    }
}
