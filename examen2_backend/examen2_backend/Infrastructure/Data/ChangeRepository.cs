using examen2_backend.Application.Interfaces;
using examen2_backend.Domain;

namespace examen2_backend.Infrastructure.Data
{
    public class ChangeRepository : IChangeRepository
    {
        public List<MoneyModel> changeMoney { get; private set; }
        public List<MoneyModel> usedChangeMoney { get; private set; }

        public ChangeRepository()
        {
            changeMoney = new List<MoneyModel>
            {
                new MoneyModel { Money = 1000, Quantity = 0, Type = "billete" },
                new MoneyModel { Money = 500, Quantity = 20, Type = "moneda" },
                new MoneyModel { Money = 100, Quantity = 30, Type = "moneda" },
                new MoneyModel { Money = 50, Quantity = 50, Type = "moneda" },
                new MoneyModel { Money = 25, Quantity = 25, Type = "moneda" }
            };

            usedChangeMoney = new List<MoneyModel>
            {
                new MoneyModel { Money = 1000, Quantity = 0, Type = "billete" },
                new MoneyModel { Money = 500, Quantity = 0, Type = "moneda" },
                new MoneyModel { Money = 100, Quantity = 0, Type = "moneda" },
                new MoneyModel { Money = 50, Quantity = 0, Type = "moneda" },
                new MoneyModel { Money = 25, Quantity = 0, Type = "moneda" }
            };
        }
    }
}
