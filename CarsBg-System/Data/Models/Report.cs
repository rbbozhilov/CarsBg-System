using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarsBg_System.Data.Models
{
    public class Report
    {

        [Key]
        public int Id { get; set; }

        public string User { get; set; }

        [ForeignKey(nameof(Post))]
        public int PostId { get; set; }

        public Post Post { get; set; }
    }
}
