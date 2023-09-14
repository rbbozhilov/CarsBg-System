namespace CarsBg_System.Views.ViewModels.Cars
{
    public class CarDetailViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Model { get; set; }

        public DateTime Date { get; set; }

        public string Color { get; set; }

        public int PhoneNumber { get; set; }

        public string Region { get; set; }

        public string Status { get; set; }

        public decimal Price { get; set; }

        public int EnginePower { get; set; }

        public int HorsePower { get; set; }

        public int Mileage { get; set; }

        public string EngineType { get; set; }

        public string Category { get; set; }

        public string Transmission { get; set; }

        public string WheelDrive { get; set; }

        public IEnumerable<string> ImagesId { get; set; }

        public IEnumerable<string> Extras { get; set; }

        public IEnumerable<string> Comments { get; set; }
    }
}
