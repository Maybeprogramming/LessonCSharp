namespace Lesson_46
{
    using static Randomaizer;
    using static UserInput;
    using static Display;

    class Program
    {
        static void Main()
        {
            const string OpenMarketCommand = "1";
            const string ExitProgrammCommand = "2";

            Console.WindowWidth = 100;
            Console.BufferHeight = 500;
            Console.Title = "Магазин > Фруктовый <";

            string menu = $"Какое действие выполнить?\n" +
                          $"{OpenMarketCommand} - Открыть магазин\n" +
                          $"{ExitProgrammCommand} - Выйти из программы\n" +
                          $"Введите номер команды для продолжения: ";

            bool isRun = true;

            while (isRun == true)
            {
                Console.Clear();
                Print($"{menu}");

                switch (Console.ReadLine())
                {
                    case OpenMarketCommand:
                        Market market = new Market(GenerateRandomNumber(1, 5));
                        market.Work();
                        break;

                    case ExitProgrammCommand:
                        isRun = false;
                        break;

                    default:
                        Print($"\n");
                        break;
                }
            }

            Print($"\nВсего доброго!!! Возвращайтесь к нам за новыми покупками!\n");
            Console.ReadKey();
        }
    }

    class Market
    {
        private int _marketBalanceMoney;
        private Seller? _seller;
        private Queue<Customer>? _customers;
        private ProductsCase _productCase;

        public Market(int customersCount)
        {
            _marketBalanceMoney = 0;
            _seller = new();
            _productCase = new();
            CreateQueueCustomers(customersCount);
        }

        public void Work()
        {
            Customer customer;
            bool isThereCustomers = true;

            ShowMarketBalance();

            while (isThereCustomers == true)
            {
                Console.Clear();

                ShowCustomers();
                customer = _customers.Dequeue();

                WaitToPressKey($"\nНачать наполнение корзины товарами\n");

                ToFillsCart(customer);

                ShowProductsInCart(customer, $"\n>----- Покупатель показывает продукты из корзины продавцу: ------<\n");

                TryToPayProducts(customer, $"\n>----- Покупатель проходит на кассу для оплаты продуктов: ------<\n");

                ShowProductsInCart(customer, $"\n>----- Покупатель купил продукты: ------<\n");
                WaitToPressKey($"\nПерейти к следующему покупателю\n");

                ShowMarketBalance();

                isThereCustomers = _customers.Count > 0;
            }
        }

        private static void WaitToPressKey(string message)
        {
            Print(message);
            Print($"Для продолжения нажмите любую клавишу...\n");
            Console.ReadKey();
        }

        private void ShowProductsInCart(Customer customer, string message)
        {
            Print(message, ConsoleColor.DarkYellow);
            customer.ShowProductsInCart();
        }

        private void ShowMarketBalance()
        {
            string MarketBalance = "Баланс магазина: " + _marketBalanceMoney.ToString() + " рублей";
            Console.Title = MarketBalance;
        }

        private void TryToPayProducts(Customer customer, string message)
        {
            Print(message, ConsoleColor.DarkYellow);
            _seller.TrySellProducts(customer);
            _marketBalanceMoney += _seller.Money;
        }

        private void ToFillsCart(Customer customer)
        {
            int ToFinishFillsCartCommand = _productCase.ProductsCount;

            bool isCustomerCompleteShopping = false;
            int userInputNumber;

            while (isCustomerCompleteShopping == false)
            {
                Console.Clear();

                ShowAllProducts();

                Print($"{ToFinishFillsCartCommand} - Пойти на кассу.\n", ConsoleColor.Green);

                userInputNumber = ReadInt("Введите номер продукта, чтобы положить его в корзину: ");

                if (userInputNumber == ToFinishFillsCartCommand)
                {
                    isCustomerCompleteShopping = true;
                }
                else if (userInputNumber >= 0 && userInputNumber < _productCase.ProductsCount)
                {
                    Product product = _productCase.GetProduct(userInputNumber);
                    customer.PutProductToCart(product);
                    Print($"Покупатель положил в корзину: {product.GetInfo()}\n", ConsoleColor.Green);
                }
                else
                {
                    Print($"Ошибка! Такого продукта нет.\n");
                }

                Print($"Нажмите любую клавишу чтобы продолжить...\n");
                Console.ReadKey();
            }
        }

        private void ShowCustomers()
        {
            int clientNumber = 0;

            Print($">----- Покупатели: ------<\n", ConsoleColor.DarkYellow);

            foreach (var client in _customers)
            {
                Print($"{++clientNumber}. Покупатель [{client}]\n");
            }
        }

        private void ShowAllProducts()
        {
            Print($">----- Продукты на витрине магазина: ------<\n", ConsoleColor.DarkYellow);
            _productCase.ShowAllProducts();
        }

        private void CreateQueueCustomers(int customerCount)
        {
            _customers = new();

            for (int i = 0; i < customerCount; i++)
            {
                _customers.Enqueue(new Customer());
            }
        }
    }

    class ProductsCase
    {
        private List<Product> _products;

        public ProductsCase()
        {
            _products = new()
            {
                new Product("Апельсин", GenerateRandomNumber(50, 100)),
                new Product("Яблоко", GenerateRandomNumber(50, 100)),
                new Product("Груша", GenerateRandomNumber(50, 100)),
                new Product("Малина", GenerateRandomNumber(50, 100)),
                new Product("Клубника", GenerateRandomNumber(100, 150)),
                new Product("Смородина", GenerateRandomNumber(50, 100)),
                new Product("Манго", GenerateRandomNumber(100, 150)),
                new Product("Арбуз", GenerateRandomNumber(50, 100)),
                new Product("Дыня", GenerateRandomNumber(50, 100)),
                new Product("Абрикос", GenerateRandomNumber(50, 100)),
                new Product ("Гранат", GenerateRandomNumber(50, 150)),
                new Product ("Помело", GenerateRandomNumber(50, 150)),
                new Product ("Виноград", GenerateRandomNumber(100, 150)),
                new Product ("Банан", GenerateRandomNumber(30, 50))
            };
        }

        public int ProductsCount { get => _products.Count; }

        public void ShowAllProducts()
        {
            for (int i = 0; i < _products.Count; i++)
            {
                Print($"{i} - {_products[i].GetInfo()}\n");
            }
        }

        public Product GetProduct(int index)
        {
            return _products[index].Clone();
        }
    }

    class Seller
    {
        public Seller()
        {
            Money = 0;
        }

        public int Money { get; private set; }

        public void TrySellProducts(Customer customer)
        {
            int totalCost;
            bool isToPay = false;

            while (isToPay == false)
            {
                totalCost = CalculateProductsCost(customer.GetCart());

                Print($"Стоимость товаров в корзине составляет: {totalCost} рублей\n");

                if (customer.TryBuyProduct(totalCost) == true)
                {
                    isToPay = true;
                    Money = totalCost;

                    Print($"Покупатель произвёл оплату товаров на сумму: {totalCost} рублей\n");
                }
                else
                {
                    Print($"Покупатель не может произвести оплату на сумму: {totalCost} рублей\n");
                    customer.RemoveRandomProduct();
                    PrintLine();
                }

                Task.Delay(1000).Wait();
            }
        }

        private int CalculateProductsCost(Cart cart)
        {
            int totalCost = 0;

            foreach (var product in cart.GetAllProducts())
            {
                totalCost += product.Price;
            }

            return totalCost;
        }
    }

    class Customer
    {
        private int _money;
        private Cart? _cart;

        public Customer()
        {
            _cart = new();
            _money = GenerateRandomNumber(200, 500);
        }

        public bool TryBuyProduct(int totalCost)
        {
            if (_money >= totalCost)
            {
                _money -= totalCost;
                return true;
            }

            return false;
        }

        public void PutProductToCart(Product product)
        {
            _cart?.AddProduct(product);
        }

        public void RemoveRandomProduct()
        {
            int minIndex = 0;
            int maxIndex = _cart.ProductsCount;
            int randomIndex = GenerateRandomNumber(minIndex, maxIndex);
            Product product = _cart.GetOneProduct(randomIndex);

            Print($"Покупатель выложил и не будет покупать: {product.GetInfo()}\n");

            _cart.RemoveProduct(product);
        }

        public Cart GetCart()
        {
            return _cart;
        }

        public void ShowProductsInCart()
        {
            List<Product> products = _cart.GetAllProducts();

            for (int i = 0; i < products.Count; i++)
            {
                Print($"{i + 1}. {products[i].GetInfo()}\n");
            }
        }

        public override string ToString()
        {
            return $"Баланс: {_money} рублей";
        }

        public int GetMoney()
        {
            return _money;
        }
    }

    class Cart
    {
        private List<Product>? _products;

        public Cart()
        {
            _products = new();
        }

        public int ProductsCount { get => _products.Count; }

        public void RemoveProduct(Product product)
        {
            _products?.Remove(product);
        }

        public List<Product> GetAllProducts()
        {
            return new List<Product>(_products);
        }

        public void AddProduct(Product product)
        {
            _products?.Add(product);
        }

        public Product GetOneProduct(int index)
        {
            return _products[index];
        }
    }

    class Product
    {
        public Product(string name, int price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; }
        public int Price { get; }

        public Product Clone()
        {
            return new Product(Name, Price);
        }

        public string GetInfo()
        {
            return $"{Name} - цена: {Price} рублей";
        }
    }

    static class Randomaizer
    {
        private static readonly Random s_random;
        private static readonly string[] s_names;

        static Randomaizer()
        {
            s_random = new();
            s_names = new string[]
            {
                "Варвар",
                "Космонафт",
                "Миледи",
                "Вульфич",
                "Страйк",
                "Герандич",
                "Фрея",
                "Крыса",
                "Нинка",
                "Царь",
                "Забота",
                "Прожариватель",
                "Овощ",
                "Имба",
                "Нагибатель",
                "Топчик",
                "Холивар",
                "Бывалый",
                "Пирожок",
                "Котейка",
                "Оливер",
                "Викрам",
                "Архидея",
                "Метрономщик",
                "Зимник",
                "Волкодав",
                "Богатырь",
                "Вафлич",
                "Вурдолакыч",
                "Зяблик",
                "Кудахта",
                "Чувиха",
                "Мордорка",
                "Куряха",
                "Смоляха",
                "Крендель",
                "Остряк",
                "Крушила",
                "Очкович",
                "Щавель",
                "Днище",
                "Нубичка",
                "Жираф",
                "Подлиза",
                "Лимурчик",
                "Попрыгун",
                "Тряпкович"
            };
        }

        public static string GenerateRandomName()
        {
            return s_names[s_random.Next(0, s_names.Length)];
        }

        public static int GenerateRandomNumber(int minValue, int maxValue)
        {
            return s_random.Next(minValue, maxValue);
        }
    }

    static class UserInput
    {
        public static int ReadInt(string message, int minValue = int.MinValue, int maxValue = int.MaxValue)
        {
            int result;

            Console.Write(message);

            while (int.TryParse(Console.ReadLine(), out result) == false || result < minValue || result >= maxValue)
            {
                Console.Error.WriteLine("Ошибка!. Попробуйте снова!");
            }

            return result;
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

        public static void PrintLine(int symbolCount = 100)
        {
            Print($"{new string('-', symbolCount)}\n");
        }
    }
}

//Супермаркет
//Написать программу администрирования супермаркетом.
//В супермаркете есть очередь клиентов.
//У каждого клиента в корзине есть товары,
//также у клиентов есть деньги.
//Клиент, когда подходит на кассу,
//получает итоговую сумму покупки и старается её оплатить.
//Если оплатить клиент не может,
//то он рандомный товар из корзины выкидывает до тех пор,
//пока его денег не хватит для оплаты.
//Клиентов можно делать ограниченное число на старте программы.
//Супермаркет содержит список товаров, из которых клиент выбирает товары для покупки.