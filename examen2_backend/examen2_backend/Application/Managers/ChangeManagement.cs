using examen2_backend.Application.Interfaces;
using examen2_backend.Domain;

namespace examen2_backend.Application.Managers
{
    public class ChangeManagement : IChangeManagement
    {
        private readonly IChangeRepository _changeRepository;

        public ChangeManagement(IChangeRepository changeRepository)
        {
            _changeRepository = changeRepository;
        }
        public bool CanGiveChange(int requestedMoney, List<int> newMoneyQuantities)
        {
            List<MoneyModel> newChange = new List<MoneyModel>();
            for (int i = 0; i < newMoneyQuantities.Count; i++) 
            {
                _changeRepository.usedChangeMoney[i].Quantity = 0;
                newChange.Add(new MoneyModel { 
                        Money = _changeRepository.changeMoney[i].Money, 
                        Quantity = _changeRepository.changeMoney[i].Quantity + newMoneyQuantities[i],
                        Type = _changeRepository.changeMoney[i].Type
                    }
                );
            }

            for (int i = 0; i < newChange.Count; i++)
            {
                while (newChange[i].Quantity > 0 && requestedMoney >= newChange[i].Money) 
                {
                    requestedMoney -= newChange[i].Money;
                    newChange[i].Quantity--;
                    _changeRepository.usedChangeMoney[i].Quantity++;
                }
            }

            if (requestedMoney == 0)
            {
                for (int i = 0; i < newChange.Count; i++)
                {
                    _changeRepository.changeMoney[i].Quantity = newChange[i].Quantity;
                }
                return true;
            }
            return false;
        }

        public List<MoneyModel> GiveChange(int requestedMoney, List<int> newMoneyQuantities) 
        {
            return CanGiveChange(requestedMoney, newMoneyQuantities) ? _changeRepository.usedChangeMoney : null;
        }
    }
}
