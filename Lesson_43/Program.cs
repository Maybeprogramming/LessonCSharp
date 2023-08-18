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

            Console.ReadLine();
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

            string titleMenu = "Добро пожаловать в магазин \"Продуктовый\"!!!";
            string menu = $"{ShowSellerProductsCommand} - Показать товары продавца." +
                          $"{ShowBuyerProductsCommand} - Показать купленные товары покупателя." +
                          $"{BuyProductCommand} - Купить товар." +
                          $"{ExitCommand} - Закончить покупки и выйти из магазина.";
            string requestMessage = "Введите команду: ";
            string continueMesaage = "\nНажмите любую клавишу чтобы продолжить...";
            string errorCommandMessage = "Такой команды нет! Попробуйте снова.";

            string userInput;

            while (isWork == true)
            {
                Console.Clear();

                Display.Print(titleMenu);
                Display.Print(menu);

                Display.Print(requestMessage);
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
                        Display.Print($"{userInput} - {errorCommandMessage}");
                        break;
                }

                Display.Print(continueMesaage);
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

            foreach (Product product in _products)
            {
                Display.Print($"{++index}. {product}");
            }
        }

        public void SellProduct(Buyer buyer)
        {
            int index = 0;
            Product product = _products[index];
            _products.Remove(product);

            Display.Print($"{buyer.Name} купил {product.Name} по цене {product.Price}");

            Display.Print($"У {buyer.Name} не хватает денег чтобы купить товар {product.Name}");
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

            Display.Print($"Ваш список купленных продуктов:");

            foreach (Product product in _products)
            {
                Display.Print($"{++index}. {product}");
            }
        }

        public bool BuyProduct (Product product)
        {
            if (Money >= product.Price)
            {
                Money -= product.Price;
                _products.Add(product);                
                return true;
            }

            return false;            
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