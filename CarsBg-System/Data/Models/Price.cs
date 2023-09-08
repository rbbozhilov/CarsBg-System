using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarsBg_System.Data.Models
{
    public class Price
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public decimal Money { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [ForeignKey(nameof(Car))]
        public int CarId { get; set; }

        public virtual Car Car { get; set; }

    }
}
