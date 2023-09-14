using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarsBg_System.Data.Models
{
    public class Post
    {

        public Post()
        {
            this.Reports = new HashSet<Report>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(1024)]
        public string Comment { get; set; }

        public string User { get; set; }

        [ForeignKey(nameof(Car))]
        public int CarId { get; set; }

        public virtual Car Car { get; set; }

        public virtual ICollection<Report> Reports { get; set; }

    }
}
