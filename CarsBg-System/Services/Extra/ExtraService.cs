using CarsBg_System.Areas.Admin.Models.Car;
using CarsBg_System.Areas.Admin.Views.ViewModels;
using CarsBg_System.Data;

namespace CarsBg_System.Services.Extra
{
    public class ExtraService : IExtraService
    {

        private CarsDbContext data;

        public ExtraService(CarsDbContext data)
        {
            this.data = data;
        }


        public bool Add(ExtraFormModel extra)
        {
            bool isHave = this.data.Extras.Any(x => x.Name == extra.Name);

            if (isHave)
            {
                return false;
            }

            var currentExtra = new CarsBg_System.Data.Models.Extra() { Name = extra.Name };

            this.data.Extras.Add(currentExtra);
            this.data.SaveChanges();

            return true;
        }

        public bool Delete(int extraId)
        {
            var extra = this.data.Extras.Where(x => x.Id == extraId).FirstOrDefault();

            if (extra == null)
            {
                return false;
            }

            this.data.Extras.Remove(extra);
            this.data.SaveChanges();

            return true;
        }

        public IEnumerable<ShowExtraViewModel> ShowExtras()
        => this.data.Extras
                     .Select(x => new ShowExtraViewModel()
                     {
                         Id = x.Id,
                         Name = x.Name
                     })
                    .ToList();


    }
}
