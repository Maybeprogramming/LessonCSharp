namespace Lesson_46
{
    using static Randomaizer;
    using static UserInput;
    using static Display;

    class Program
    {
        static void Main()
        {
            Market market = new Market();
            market.Work();

            Console.ReadKey();
        }
    }

    class Market
    {
        private int BalanceMoney;
        private Seller? _seller;
        private Queue<Buyer>? _buyers;
        private Showcase _showcase;
        private int _queueBuyersCount;

        public Market()
        {
            _seller = new();
            _showcase = new();
            _buyers = new();
            _queueBuyersCount = 5;
            CreateQueueBuyers(_queueBuyersCount);
        }

        public void Work()
        {
            Buyer buyer = _buyers.Dequeue();
            Cart cart = new Cart();

            _showcase.ShowProducts();

            buyer.PutProductToCart(_showcase.GetProduct(0));
            buyer.PutProductToCart(_showcase.GetProduct(1));
            buyer.PutProductToCart(_showcase.GetProduct(2));
            buyer.PutProductToCart(_showcase.GetProduct(3));
            buyer.PutProductToCart(_showcase.GetProduct(4));

            cart = buyer.GetCart();

            Console.WriteLine($"{new string ('-', 50)}");

            foreach (var item in cart.GetAllProducts())
            {
                Console.WriteLine($"{item.GetInfo()}");
            }

            Console.WriteLine($"{new string ('-', 50)}");

            _seller.TrySellProducts(buyer);

            cart = buyer.GetCart();

            Console.WriteLine($"{new string ('-', 50)}");

            foreach (var item in cart.GetAllProducts())
            {
                Console.WriteLine($"{item.GetInfo()}");
            }

            Console.WriteLine($"{new string ('-', 50)}");
        }

        private void CreateQueueBuyers(int buyersCount)
        {
            for (int i = 0; i < buyersCount; i++)
            {
                _buyers.Enqueue(new Buyer());
            }
        }
    }

    class Showcase
    {
        private List<Product> _products;

        public Showcase()
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

            ProductsCount = _products.Count;
        }

        public int ProductsCount { get; }

        public void ShowProducts()
        {
            Print($"Список продуктов в магазине:\n");

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

        public void TrySellProducts(Buyer buyer)
        {
            int totalCost;
            bool isPay = false;

            while (isPay == false)
            {
                totalCost = CalculateProductsCost(buyer.GetCart());

                Console.WriteLine($"LOG: цена продуктов: {totalCost}");

                if (buyer.TryBuyProduct(totalCost) == true)
                {
                    isPay = true;
                    Money += totalCost;

                    Console.WriteLine($"LOG: Наконец то!");
                }
                else
                {
                    Console.WriteLine($"LOG: ты там удаляешь или нет?!");
                    buyer.RemoveRandomProduct();
                }

                Task.Delay(3000).Wait();
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

    class Buyer
    {
        private int _money;
        private Cart? _cart;

        public Buyer()
        {
            _cart = new();
            _money = GenerateRandomNumber(200, 500);
            Console.WriteLine($"Денег у покупателя: {_money}");
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

            Console.WriteLine($"LOG: случайный номер продукта: {randomIndex}");

            Product product = _cart.GetOneProduct(randomIndex);

            Console.WriteLine($"LOG: продукт: {product.GetInfo()}");

            _cart.RemoveProduct(product);
        }

        public Cart GetCart()
        {
            return _cart;
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
            Console.WriteLine($"LOG: метод удаления продукта: {product.GetInfo()}");
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
        public Product(string description, int price)
        {
            Description = description;
            Price = price;
        }

        public string Description { get; }
        public int Price { get; }

        public Product Clone()
        {
            return new Product(Description, Price);
        }

        public string GetInfo()
        {
            return $"{Description} - цена: {Price}";
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