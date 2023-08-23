using System.ComponentModel.DataAnnotations;

namespace CarsBg_System.Data.Models
{
    public class Status
    {

        public Status()
        {
            this.Cars = new HashSet<Car>();
        }


        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(15)]
        public string StatusName { get; set; }


        public virtual ICollection<Car> Cars { get; set; }
    }
}
