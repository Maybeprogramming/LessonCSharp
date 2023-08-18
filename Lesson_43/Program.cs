using System.Xml.Linq;

namespace Lesson_43
{
    class Program
    {
        static void Main()
        {
            Console.Title = "Магазин";
            Shop shop = new Shop();
            shop.Work();

            Console.ReadKey(true);
        }
    }

    static class Display
    {
        public static void Print<T>(T message)
        {
            Console.Write(message.ToString());
        }

        public static void Print<T>(T message, ConsoleColor consoleColor = ConsoleColor.White)
        {
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = consoleColor;
            Print(message);
            Console.ForegroundColor = defaultColor;
        }


    }

    class Shop
    {
        public void Work()
        {
            const string ShowSellerProductsCommand = "1";
            const string ShowBuyerProductsCommand = "2";
            const string BuyProductCommand = "3";
            const string ExitCommand = "4";

            Random random = new Random();
            int maxBuyerMoney = 1000;
            Seller seller = new Seller();
            Buyer buyer = new Buyer("Григорий", random.Next(maxBuyerMoney));
            bool isWork = true;

            string welcomeMessage = "Добро пожаловать в магазин \"Продуктовый\"!!!";
            string titleMenu = "\n\nМеню:";
            string menu = $"\n{ShowSellerProductsCommand} - Показать товары продавца." +
                          $"\n{ShowBuyerProductsCommand} - Показать купленные товары покупателя." +
                          $"\n{BuyProductCommand} - Купить товар." +
                          $"\n{ExitCommand} - Закончить покупки и выйти из магазина.";
            string requestMessage = "\nВведите команду: ";
            string continueMesaage = "\nНажмите любую клавишу чтобы продолжить...";
            string errorCommandMessage = "Такой команды нет! Попробуйте снова.";

            string userInput;

            while (isWork == true)
            {
                Console.Clear();

                buyer.ShowBuyerInfo();
                Display.Print(welcomeMessage, ConsoleColor.DarkYellow);
                Display.Print(titleMenu);
                Display.Print(menu);

                Display.Print(requestMessage, ConsoleColor.Green);
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case ShowSellerProductsCommand:
                        seller.ShowAllProducts();
                        break;

                    case ShowBuyerProductsCommand:
                        buyer.ShowPurchasedProducts();
                        break;

                    case BuyProductCommand:
                        seller.SellProduct(buyer);
                        break;

                    case ExitCommand:
                        isWork = false;
                        break;

                    default:
                        Display.Print($"{userInput} - {errorCommandMessage}", ConsoleColor.DarkRed);
                        break;
                }

                Display.Print(continueMesaage, ConsoleColor.DarkYellow);
                Console.ReadLine();
            }
        }
    }

    class Product
    {
        public Product(string name, int price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; private set; }
        public int Price { get; private set; }

        public override string ToString()
        {
            return $"Товар: {Name}, цена: {Price}";
        }
    }

    class Seller
    {
        private List<Product> _products = new()
        {
            new Product("Апельсин", 100),
            new Product("Манго", 150),
            new Product("Хлеб", 120),
            new Product("Масло", 200),
            new Product("Вода", 50)
        };

        public void ShowAllProducts()
        {
            int index = 0;
            Display.Print("Доступный ассортимент продуктов:", ConsoleColor.Green);

            foreach (Product product in _products)
            {
                Display.Print($"\n{++index}. {product}");
            }
        }

        public void SellProduct(Buyer buyer)
        {
            Console.Clear();
            ShowAllProducts();
            buyer.ShowBuyerInfo();

            Display.Print($"\nВведите номер желаемого продукта для покупки: ");
            int inputIndex = ReadInt() - 1;
            Product product = _products[inputIndex];

            if (buyer.BuyProduct(product) == true)
            {
                _products.Remove(product);
                Display.Print($"{buyer.Name} купил {product.Name} по цене {product.Price}");
            }
            else
            {
                Display.Print($"У {buyer.Name} не хватает денег чтобы купить товар {product.Name}");
            }
        }

        private int ReadInt(int firstNumber = 0, int lastNumber = int.MaxValue)
        {
            bool isTryParse = false;
            string userInput;
            int number = 0;

            while (isTryParse == false)
            {
                userInput = Console.ReadLine();

                if (Int32.TryParse(userInput, out int result) == true)
                {
                    if (result > firstNumber && result <= lastNumber)
                    {
                        number = result;
                        isTryParse = true;
                    }
                    else
                    {
                        Display.Print($"\nОшибка! Введеное число [{userInput}] должно быть больше: [{firstNumber}] и меньше, либо равным: [{lastNumber}]!\nПопробуйте снова: ");
                    }
                }
                else
                {
                    Display.Print($"\nОшибка! Вы ввели не число: {userInput}!\nПопробуйте снова: ");
                }
            }

            return number;
        }
    }

    class Buyer
    {
        private List<Product> _products = new();

        public Buyer(string name, int money)
        {
            Name = name;
            Money = money;
        }

        public string Name { get; private set; }
        public int Money { get; private set; }

        public void ShowPurchasedProducts()
        {
            int index = 0;

            if (_products.Count <= 0)
            {
                Display.Print($"Список покупок пуст");
                return;
            }

            Display.Print($"\nВаш список купленных продуктов:");

            foreach (Product product in _products)
            {
                Display.Print($"\n{++index}. {product}");
            }
        }

        public bool BuyProduct(Product product)
        {
            if (Money >= product.Price)
            {
                Money -= product.Price;
                _products.Add(product);
                return true;
            }

            return false;
        }

        public int GetProductsCount()
        {

            if (_products.Count != 0)
            {
                return _products.Count;
            }

            return 0;
        }

        public void ShowBuyerInfo()
        {
            int currentPositionLeft = Console.CursorLeft;
            int currentPositionTop = Console.CursorTop;
            int positionLeft = 60;
            int positionTopName = 0;
            int positionTopBalance = 1;
            int positionTopCountProducts = 2;

            Console.SetCursorPosition(positionLeft, positionTopName);
            Display.Print($"# Имя покупателя: {Name}", ConsoleColor.Green);
            Console.SetCursorPosition(positionLeft, positionTopBalance);
            Display.Print($"# Баланс покупателя: {Money} рублей.", ConsoleColor.Green);
            Console.SetCursorPosition(positionLeft, positionTopCountProducts);
            Display.Print($"# Количество товаров у покупателя: {GetProductsCount()}.", ConsoleColor.Green);

            Console.SetCursorPosition(currentPositionLeft, currentPositionTop);
        }
    }
}

//Магазин
//Существует продавец, он имеет у себя список товаров,
//и при нужде, может вам его показать,
//также продавец может продать вам товар.
//После продажи товар переходит к вам,
//и вы можете также посмотреть свои вещи.
//Возможные классы – игрок, продавец, товар.
//Вы можете сделать так, как вы видите это.