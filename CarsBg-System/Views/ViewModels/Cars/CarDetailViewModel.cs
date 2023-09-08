namespace CarsBg_System.Views.ViewModels.Cars
{
    public class CarDetailViewModel
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string EngineType { get; set; }

        public int HorsePower { get; set; }

        public int EnginePower { get; set; }

        public string Status { get; set; }

        public decimal Price { get; set; }

        public DateTime Date { get; set; }

        public int PricesChangeCount { get; set; }
    }
}
