using examen2_backend.Application.Interfaces;
using examen2_backend.Domain;

namespace examen2_backend.Application.Managers
{
    public class ModifyCoffeeStock : IModifyCoffeeStock
    {
        private readonly ICoffeeRepository _coffeeRepository;

        public ModifyCoffeeStock(ICoffeeRepository coffeeRepository)
        {
            _coffeeRepository = coffeeRepository;
        }
        public bool updateStock(List<int> stocks) 
        {
            for (int i = 0; i < stocks.Count; i++)
            {
                if (stocks[i] > _coffeeRepository.Coffees[i].Stock) return false;
            }

            for (int i = 0; i < stocks.Count; i++)
            {
                _coffeeRepository.Coffees[i].Stock -= stocks[i];
            }

            return true;
        }
    }
}
