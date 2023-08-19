﻿using CarsBg_System.Views.ViewModels.Engine;

namespace CarsBg_System.Services.Engine
{
    public interface IEngineService
    {

        bool IsHaveEngineById(int id);

        IEnumerable<EngineViewModel> GetAllEngines();

    }
}
