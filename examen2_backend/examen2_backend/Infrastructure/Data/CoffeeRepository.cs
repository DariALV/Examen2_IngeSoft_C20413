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
                new CoffeeModel { Name = "Americano", Stock = 10, Price = 950},
                new CoffeeModel { Name = "Capuchino", Stock = 8, Price = 1200},
                new CoffeeModel { Name = "Late", Stock = 10, Price = 1350},
                new CoffeeModel { Name = "Mocachino", Stock = 15, Price = 1500}
            };
        }
    }
}
