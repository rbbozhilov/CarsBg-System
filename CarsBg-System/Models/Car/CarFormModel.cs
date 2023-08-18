﻿using CarsBg_System.Views.ViewModels.Cars;
using CarsBg_System.Views.ViewModels.Category;
using CarsBg_System.Views.ViewModels.Engine;
using CarsBg_System.Views.ViewModels.Region;
using CarsBg_System.Views.ViewModels.Transmission;
using CarsBg_System.Views.ViewModels.WheelDrive;

namespace CarsBg_System.Models.Car
{
    public class CarFormModel
    {

        public string Color { get; set; }

        public int FromHorsePower { get; set; }

        public int ToHorsePower { get; set; }

        public decimal FromPrice { get; set; }

        public decimal ToPrice { get; set; }

        public int FromYear { get; set; }

        public int ToYear { get; set; }

        public int BrandId { get; set; }
        public IEnumerable<BrandViewModel>? Brands { get; set; }

        public int ModelId { get; set; }
        public IEnumerable<ModelViewModel>? Models { get; set; }

        public int EngineId { get; set; }
        public IEnumerable<EngineViewModel> Engines { get; set; }

        public int CategoryId { get; set; }
        public IEnumerable<CategoryViewModel> Categories { get; set; }

        public int RegionId { get; set; }
        public IEnumerable<RegionViewModel> Regions { get; set; }

        public int TransmissionId { get; set; }
        public IEnumerable<TransmissionViewModel> Transmissions { get; set; }

        public int WheelDriveId { get; set; }
        public IEnumerable<WheelDriveViewModel> WheelDrives { get; set; }


    }
}
