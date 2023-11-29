namespace Lesson_55
{
    using static Display;
    using static UserInput;
    using static Randomaizer;

    class Program
    {
        static void Main()
        {
            Console.Title = "Определение просрочки";

            int productsCount = 20;
            Stock stock = new Stock(productsCount);
            stock.Work();

            PrintLine();
            Print($"Работа программы завершена!\n", ConsoleColor.Green);
        }
    }

    class Stock
    {
        List<Product> _products;
        

        public Stock(int productsCount)
        {
            _products = FillProducts(productsCount);
        }

        public void Work()
        {
            int currentDate = 2023;

            ShowProducts($"Список тушенки на складе:\n", _products);
            PrintLine();

            IdentifyExpiredProducts(_products, currentDate);
            PrintLine();

            WaitToPressKey("\n");
        }

        private void IdentifyExpiredProducts(List<Product> products, int currentDate)
        {
            List<Product> expiredProducts = new List<Product>();

            expiredProducts = products.Where(product => GetExpiredDateProduct(product, currentDate) != null).ToList();

            ShowProducts("Список просроченных банок тушенки:\n", expiredProducts);
        }

        private Product GetExpiredDateProduct(Product product, int currentDate)
        {
            int storagePeriod = currentDate - product.ProductionDate;
            int remainingStorageTime = product.ExpirationDate - storagePeriod;

            return product = remainingStorageTime < 0 ? product : null;
        }

        private void ShowProducts(string message, List<Product> products)
        {
            Print(message, ConsoleColor.Green);

            for (int i = 0; i < products.Count; i++)
            {
                Print($"{i + 1}. {products[i]}\n");
            }
        }

        private List<Product> FillProducts(int productsCount)
        {
            List<string> productsNames = new List<string>() 
            { 
                "Барс Экстра", "Главпродукт", "Батькин резерв", "Кронидов", "Гродфуд", "Село Зелёное", "Совок"
            };

            string name;
            int productionDate;
            int expirationDate;
            int minProductionDate = 2000;
            int maxProductionDate = 2023;
            int minExpirationDate = 10;
            int maxExpirationDate = 20;
            List<Product> products = new List<Product>();

            for (int i = 0; i < productsCount; i++)
            {
                name = productsNames[GenerateRandomNumber(0, productsNames.Count)];
                productionDate = GenerateRandomNumber(minProductionDate, maxProductionDate + 1);
                expirationDate = GenerateRandomNumber(minExpirationDate, maxExpirationDate + 1);
                Product product = new Product(name, productionDate, expirationDate);

                products.Add(product);
            }

            return products;
        }
    }

    class Product
    {
        public Product(string name, int productionDate, int expirationDate)
        {
            Name = name;
            ProductionDate = productionDate;
            ExpirationDate = expirationDate;
        }

        public string Name { get; }
        public int ProductionDate { get; }
        public int ExpirationDate { get; }

        public override string ToString()
        {
            return $"\"{Name}\". Дата производства: {ProductionDate}. Срок годности: {ExpirationDate} лет.";
        }
    }

    #region UserUtils

    static class UserInput
    {
        public static void WaitToPressKey(string message = "")
        {
            Print(message);
            Print($"Для продолжения нажмите любую клавишу...\n");
            Console.ReadKey();
        }
    }

    static class Display
    {
        public static void Print(string message, ConsoleColor consoleColor = ConsoleColor.White)
        {
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = consoleColor;
            Console.Write(message);
            Console.ForegroundColor = defaultColor;
        }

        public static void PrintLine(ConsoleColor color = ConsoleColor.White)
        {
            int symbolCount = Console.WindowWidth - 1;
            Print($"{new string('-', symbolCount)}\n", color);
        }
    }

    static class Randomaizer
    {
        private static readonly Random s_random;

        static Randomaizer()
        {
            s_random = new();
        }

        public static int GenerateRandomNumber(int minValue, int maxValue)
        {
            return s_random.Next(minValue, maxValue);
        }
    }

    #endregion
}

//Определение просрочки
//Есть набор тушенки.
//У тушенки есть:
//название,
//год производства
//и срок годности.
//Написать запрос для получения всех просроченных банок тушенки.
//Чтобы не заморачиваться, можете думать, что считаем только года, без месяцев.