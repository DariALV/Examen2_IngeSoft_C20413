using examen2_backend.Domain;

namespace examen2_backend.Application.Interfaces
{
    public interface IChangeManagement
    {
        public bool CanGiveChange(int requestedMoney, List<int> newMoneyQuantities);
        public List<MoneyModel> GiveChange(int requestedMoney, List<int> newMoneyQuantities);
    }
}
