using examen2_backend.Domain;

namespace examen2_backend.Infrastructure.Data
{
    public class ChangeRepository
    {
        public List<MoneyModel> changeMoney { get; private set; }

        public ChangeRepository()
        {
            // Initialize with default values
            changeMoney = new List<MoneyModel>
            {
                new MoneyModel { Money = 1000, Quantity = 10, Type = "billete" },
                new MoneyModel { Money = 500, Quantity = 10, Type = "moneda" },
                new MoneyModel { Money = 100, Quantity = 10, Type = "moneda" },
                new MoneyModel { Money = 50, Quantity = 10, Type = "moneda" },
                new MoneyModel { Money = 25, Quantity = 10, Type = "moneda" }
            };
        }
    }
}
