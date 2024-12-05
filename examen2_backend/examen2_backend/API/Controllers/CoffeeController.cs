using examen2_backend.Application.Interfaces;
using examen2_backend.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace examen2_backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoffeeController : ControllerBase
    {
        private readonly IModifyCoffeeStock _modifyCoffeeStock;
        private readonly ICoffeeRepository _coffeeRepository;

        public CoffeeController(IModifyCoffeeStock modifyCoffeeStock,
                                ICoffeeRepository coffeeRepository)
        {
            _modifyCoffeeStock = modifyCoffeeStock;
            _coffeeRepository = coffeeRepository;
        }

        [HttpGet]
        public IActionResult GetCoffees()
        {
            return Ok(_coffeeRepository.Coffees);
        }

        [HttpPost]
        public IActionResult UpdateCoffeStock(List<int> coffeeStock)
        {
            bool result = _modifyCoffeeStock.updateStock(coffeeStock);
            return Ok(result);
        }
    }
}
