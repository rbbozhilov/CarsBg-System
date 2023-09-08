using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarsBg_System.Data.Models
{
    public class Car
    {

        public Car()
        {
            this.StatusId = 1;
            this.Extras = new HashSet<Extra>();
            this.Images = new HashSet<ImageData>();
            this.Prices = new HashSet<Price>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Color { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        public int Mileage { get; set; }

        [Required]
        public int PhoneNumber { get; set; }

        [Required]
        public int EnginePower { get; set; }

        [Required]
        public int HorsePower { get; set; }

        public bool IsDeleted { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [ForeignKey(nameof(Engine))]
        public int EngineId { get; set; }

        public virtual Engine Engine { get; set; }

        [ForeignKey(nameof(Model))]
        public int ModelId { get; set; }

        public virtual Model Model { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        [ForeignKey(nameof(Region))]
        public int RegionId { get; set; }

        public virtual Region Region { get; set; }

        [ForeignKey(nameof(Transmission))]
        public int TransmissionId { get; set; }

        public virtual Transmission Transmission { get; set; }

        [ForeignKey(nameof(WheelDrive))]
        public int WheelDriveId { get; set; }

        public virtual WheelDrive WheelDrive { get; set; }

        [ForeignKey(nameof(Status))]
        public int StatusId { get; set; }

        public virtual Status Status { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Extra> Extras { get; set; }

        public virtual ICollection<ImageData> Images { get; set; }

        public virtual ICollection<Price> Prices { get; set; }
    }
}
