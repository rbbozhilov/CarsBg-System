using System.ComponentModel.DataAnnotations;

namespace CarsBg_System.Data.Models
{
    public class WheelDrive
    {

        public WheelDrive()
        {
            this.Cars = new HashSet<Car>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string Name { get; set; }


        public virtual ICollection<Car> Cars { get; set; }

    }
}
