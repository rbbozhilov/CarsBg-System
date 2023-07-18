﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarsBg_System.Data.Models
{
    public class Car
    {

        public Car()
        {
            this.Extras = new HashSet<Extra>();
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

        [Required]
        public int PhoneNumber { get; set; }

        [Required]
        public int EnginePower { get; set; }

        [Required]
        public int HorsePower { get; set; }

        [Required]
        public decimal Price { get; set; }

        public bool IsDeleted { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [ForeignKey(nameof(Engine))]
        public int EngineId { get; set; }

        public Engine Engine { get; set; }

        [ForeignKey(nameof(Brand))]
        public int BrandId { get; set; }

        public Brand Brand { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        [ForeignKey(nameof(Region))]
        public int RegionId { get; set; }

        public Region Region { get; set; }

        [ForeignKey(nameof(Transmission))]
        public int TransmissionId { get; set; }

        public Transmission Transmission { get; set; }

        [ForeignKey(nameof(WheelDrive))]
        public int WheelDriveId { get; set; }

        public WheelDrive WheelDrive { get; set; }

        public virtual ICollection<Extra> Extras { get; set; }
    }
}
