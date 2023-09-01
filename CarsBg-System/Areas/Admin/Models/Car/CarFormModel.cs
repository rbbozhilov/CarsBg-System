using CarsBg_System.Areas.Admin.Views.ViewModels;

namespace CarsBg_System.Areas.Admin.Models.Car
{
    public class CarFormModel
    {

        public int StatusId { get; set; }
        public IEnumerable<ShowStatusViewModel> Statuses { get; set; }

    }
}
