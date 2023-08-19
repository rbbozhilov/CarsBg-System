﻿using CarsBg_System.Data;
using CarsBg_System.Views.ViewModels.Engine;

namespace CarsBg_System.Services.Engine
{
    public class EngineService : IEngineService
    {

        private CarsDbContext data;

        public EngineService(CarsDbContext data)
        {
            this.data = data;
        }

        public bool IsHaveEngineById(int id)
        => this.data.Engines.Any(x => x.Id == id);

        public IEnumerable<EngineViewModel> GetAllEngines()
        => this.data.Engines.Select(x => new EngineViewModel() { Id = x.Id, EngineName = x.Name });


    }
}
