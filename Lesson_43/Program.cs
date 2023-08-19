namespace Lesson_43
{
    class Program
    {
        static void Main()
        {
            Console.Title = "Магазин";
            Shop shop = new();
            shop.Work();
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

            Random random = new();
            int maxBuyerMoney = 1000;
            int sellerMoney = 0;
            string sellerName = "Галя";
            string buyerName = "Григорий";
            Seller seller = new(sellerName, sellerMoney);
            Buyer buyer = new(buyerName, random.Next(maxBuyerMoney));
            bool isWork = true;

            string welcomeMessage = "Добро пожаловать в магазин \"Продуктовый\"!!!";
            string titleMenu = "\nМеню:";
            string menu = $"\n{ShowSellerProductsCommand} - Показать товары продавца." +
                          $"\n{ShowBuyerProductsCommand} - Показать купленные товары покупателя." +
                          $"\n{BuyProductCommand} - Купить товар." +
                          $"\n{ExitCommand} - Закончить покупки и выйти из магазина.";
            string requestMessage = "\nВведите команду: ";
            string continueMesaage = "\nНажмите любую клавишу чтобы продолжить...";
            string errorCommandMessage = "Такой команды нет! Попробуйте снова.";
            string exitMessage = "\nДо свидания! Приходите к нам ещё!!!";
            string userInput;

            while (isWork == true)
            {
                Console.Clear();

                buyer.DrawInfo();
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
                        buyer.ShowAllProducts();
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

            Display.Print(exitMessage);
            Console.ReadLine();
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

    class Human
    {
        protected List<Product> _products = new();

        public string Name { get; protected set; }
        public int Money { get; protected set; }

        public Human(string name = "Anonymous", int money = 0)
        {
            Name = name;
            Money = money;
        }

        public void ShowAllProducts()
        {
            int index = 0;

            if (_products.Count <= 0)
            {
                Display.Print($"Нет товаров для отображения.", ConsoleColor.DarkRed);
                return;
            }

            Display.Print($"Список продуктов у {Name}:", ConsoleColor.Green);

            foreach (Product product in _products)
            {
                Display.Print($"\n{++index}. {product}");
            }
        }
    }

    class Seller : Human
    {
        public Seller(string name, int money) : base(name, money)
        {
            _products = new()
            {
                new Product("Апельсин", 100),
                new Product("Клубника", 120),
                new Product("Манго", 150),
                new Product("Хлеб", 120),
                new Product("Масло", 200),
                new Product("Огурцы", 90),
                new Product("Помидоры", 115),
                new Product("Петрушка", 20),
                new Product("Вода", 50)
            };
        }

        public void SellProduct(Buyer buyer)
        {
            int lastIndexProduct = _products.Count;

            Console.Clear();
            ShowAllProducts();
            buyer.DrawInfo();

            Display.Print($"\nВведите номер желаемого продукта для покупки: ");
            int inputIndex = ReadInt(lastIndexProduct) - 1;
            Product product = _products[inputIndex];

            if (buyer.BuyProduct(product) == true)
            {
                Money += product.Price;
                _products.Remove(product);
                Display.Print($"{buyer.Name} купил {product.Name} по цене {product.Price}");
            }
            else
            {
                Display.Print($"У {buyer.Name} не хватает денег чтобы купить товар {product.Name}");
            }
        }

        private int ReadInt(int collectionCount)
        {
            bool isTryParse = false;
            string userInput;
            int number = 0;

            while (isTryParse == false)
            {
                userInput = Console.ReadLine();

                if (Int32.TryParse(userInput, out int result) == true)
                {
                    if (result > 0 && result <= collectionCount)
                    {
                        number = result;
                        isTryParse = true;
                    }
                    else
                    {
                        Display.Print($"\nОшибка! Такого товара нет!\nпопробуйте снова:", ConsoleColor.DarkRed);
                    }
                }
                else
                {
                    Display.Print($"\nОшибка! Вы ввели не число: {userInput}!\nПопробуйте снова: ", ConsoleColor.DarkRed);
                }
            }

            return number;
        }
    }

    class Buyer : Human
    {
        public Buyer(string name, int money) : base(name, money) { }

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

        public void DrawInfo()
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
            Display.Print($"# Количество товаров у покупателя: {_products.Count}.", ConsoleColor.Green);

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