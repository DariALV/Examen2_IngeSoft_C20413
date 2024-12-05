using examen2_backend.Application.Interfaces;
using examen2_backend.Domain;

namespace examen2_backend.Infrastructure.Data
{
    public class CoffeeRepository : ICoffeeRepository
    {
        public List<CoffeeModel> Coffees { get; private set; }

        public CoffeeRepository()
        {
            // Initialize with default values
            Coffees = new List<CoffeeModel>
            {
                new CoffeeModel { Name = "Americano", Stock = 100, Price = 950, ImageURL = "../assets/Cafe_Americano.png" },
                new CoffeeModel { Name = "Capuchino", Stock = 100, Price = 950, ImageURL = "../assets/Capuchino.png" },
                new CoffeeModel { Name = "Late", Stock = 100, Price = 950, ImageURL = "../assets/Late.png" },
                new CoffeeModel { Name = "Mocachino", Stock = 100, Price = 950, ImageURL = "../assets/Mocachino.png" }
            };
        }
    }
}
