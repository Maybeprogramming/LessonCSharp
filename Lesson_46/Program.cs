namespace Lesson_46
{
    using static Randomaizer;
    using static UserInput;
    using static Display;

    class Program
    {
        static void Main()
        {
            Console.WindowWidth = 100;

            Market market = new Market(customersCount: 5);
            market.Work();

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
            _seller = new();
            _productCase = new();
            _customers = new();
            CreateQueueBuyers(customersCount);
        }

        public void Work()
        {
            Customer customer = _customers.Dequeue();
            Cart cartCurrentCustomer;

            //  Показать покупателей
            ShowCustomers();

            //  Покупатель кладёт продукты в корзину
            ToFillsCart(customer);

            // Показать продукты из корзины покупателя
            cartCurrentCustomer = customer.GetCart();

            PrintLine();

            foreach (var item in cartCurrentCustomer.GetAllProducts())
            {
                Print($"{item.GetInfo()}\n");
            }

            // Продавец продаёт продукты покупателю
            PrintLine();

            _seller.TrySellProducts(customer);

            cartCurrentCustomer = customer.GetCart();

            // Показать оплаченные продукты у покупателя
            PrintLine();

            foreach (var item in cartCurrentCustomer.GetAllProducts())
            {
                Print($"{item.GetInfo()}\n");
            }

            PrintLine();
        }

        private void ToFillsCart(Customer customer)
        {
            int exitCommdand = _productCase.ProductsCount;

            bool isCustomerCompleteShopping = false;
            int userInputNumber;

            while (isCustomerCompleteShopping == false)
            {
                ShowAllProducts();

                Print($"{exitCommdand} - Пойти на кассу.\n", ConsoleColor.Green);

                userInputNumber = ReadInt("Введите номер: ");

                if (userInputNumber == exitCommdand)
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

            Print($">----- Очередь покупателей: ------<\n");

            foreach (var client in _customers)
            {
                Print($"{++clientNumber}. {client}\n");
            }
        }

        private void ShowAllProducts()
        {
            Print($">----- Список продуктов в магазине: ------<\n");
            _productCase.ShowAllProducts();
        }

        private void CreateQueueBuyers(int buyersCount)
        {
            for (int i = 0; i < buyersCount + 1; i++)
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
                new Product("Клубника", GenerateRandomNumber(50, 100)),
                new Product("Смородина", GenerateRandomNumber(50, 100)),
                new Product("Манго", GenerateRandomNumber(50, 100)),
                new Product("Арбуз", GenerateRandomNumber(50, 100)),
                new Product("Дыня", GenerateRandomNumber(50, 100)),
                new Product("Абрикос", GenerateRandomNumber(50, 100))
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
            bool isPay = false;

            while (isPay == false)
            {
                totalCost = CalculateProductsCost(customer.GetCart());

                //Console.WriteLine($"LOG: цена продуктов: {totalCost}");

                if (customer.TryBuyProduct(totalCost) == true)
                {
                    isPay = true;
                    Money += totalCost;

                    //Console.WriteLine($"LOG: Наконец то!");
                }
                else
                {
                    //Console.WriteLine($"LOG: ты там удаляешь или нет?!");
                    customer.RemoveRandomProduct();
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

            //Console.WriteLine($"LOG: случайный номер продукта: {randomIndex}");

            Product product = _cart.GetOneProduct(randomIndex);

            //Console.WriteLine($"LOG: продукт: {product.GetInfo()}");
            Print($"Покупатель выложил и не будет покупать: {product.GetInfo()}\n");

            _cart.RemoveProduct(product);
        }

        public Cart GetCart()
        {
            return _cart;
        }

        public override string ToString()
        {
            return $"Баланс: {_money}.";
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

        public void AddProduct(Product product)
        {
            _products?.Add(product);
        }

        public void RemoveProduct(Product product)
        {
            //Console.WriteLine($"LOG: метод удаления продукта: {product.GetInfo()}");
            _products?.Remove(product);
        }

        public Product GetOneProduct(int index)
        {
            return _products[index];
        }

        public List<Product> GetAllProducts()
        {
            return new List<Product>(_products);
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
            return $"{Name} - цена: {Price}";
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
            Print($"{new string('-', symbolCount)}");
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