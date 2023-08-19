﻿using CarsBg_System.Data;
using CarsBg_System.Views.ViewModels.WheelDrive;

namespace CarsBg_System.Services.WheelDrive
{
    public class WheelDriveService : IWheelDriveService
    {

        private CarsDbContext data;

        public WheelDriveService(CarsDbContext data)
        {
            this.data = data;
        }

        public bool IsHaveWheelDriveById(int id)
        => this.data.WheelDrives.Any(x => x.Id == id);

        public IEnumerable<WheelDriveViewModel> GetAllWheelDrives()
        => this.data.WheelDrives.Select(x => new WheelDriveViewModel() { Id = x.Id, WheelDriveName = x.Name });

    }
}
