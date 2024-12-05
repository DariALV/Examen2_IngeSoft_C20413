using examen2_backend.Domain;

namespace examen2_backend.Application.Interfaces
{
    public interface ICoffeeRepository
    {
        List<CoffeeModel> Coffees { get; }
    }
}
