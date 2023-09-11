using CarsBg_System.Views.ViewModels.Cars;
using CarsBg_System.Views.ViewModels.Category;
using CarsBg_System.Views.ViewModels.Engine;
using CarsBg_System.Views.ViewModels.Extras;
using CarsBg_System.Views.ViewModels.Region;
using CarsBg_System.Views.ViewModels.Transmission;
using CarsBg_System.Views.ViewModels.WheelDrive;
using System.ComponentModel.DataAnnotations;

namespace CarsBg_System.Models.Car
{
    public class AddCarFormModel
    {

        [Required]
        [MaxLength(200)]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        [MaxLength(10000)]
        [MinLength(3)]
        public string Description { get; set; }

        [Required]
        public int PhoneNumber { get; set; }

        [Required]
        [Range(1,10000)]
        public int EnginePower { get; set; }

        [Required]
        [Range(1,1500)]
        public int HorsePower { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(2)]
        public string Color { get; set; }

        [Required]
        [Range(1,5000000)]
        public decimal Price { get; set; }

        [Required]
        [Range(0, 3000000)]
        public int Mileage { get; set; }

        [Required]
        public DateTime Year { get; set; }

        [Required]
        public int BrandId { get; set; }
        public IEnumerable<BrandViewModel>? Brands { get; set; }

        [Required]
        public int ModelId { get; set; }
        public IEnumerable<ModelViewModel>? Models { get; set; }

        [Required]
        public int EngineId { get; set; }
        public IEnumerable<EngineViewModel>? Engines { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public IEnumerable<CategoryViewModel>? Categories { get; set; }

        [Required]
        public int RegionId { get; set; }
        public IEnumerable<RegionViewModel>? Regions { get; set; }

        [Required]
        public int TransmissionId { get; set; }
        public IEnumerable<TransmissionViewModel>? Transmissions { get; set; }

        [Required]
        public int WheelDriveId { get; set; }
        public IEnumerable<WheelDriveViewModel>? WheelDrives { get; set; }

        public IList<ExtrasViewModel>? Extras { get; set; }


    }
}
