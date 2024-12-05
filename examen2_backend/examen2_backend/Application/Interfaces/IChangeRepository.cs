using examen2_backend.Domain;

namespace examen2_backend.Application.Interfaces
{
    public interface IChangeRepository
    {
        List<MoneyModel> changeMoney { get; }
        public List<MoneyModel> usedChangeMoney { get; }
    }
}
