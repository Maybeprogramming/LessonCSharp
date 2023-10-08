namespace Lesson_46
{
    using static Randomaizer;
    using static UserInput;
    using static Display;

    class Program
    {
        static void Main()
        {

        }
    }

    class Market
    {
        private int BalanceMoney;
        private Seller? _seller;
        private Queue<Buyer>? _buyers;
        private Showcase _showcase;

        public Market()
        {
            _seller = new();
            _buyers = new();
            _showcase = new();
        }

        public void Work()
        {

        }
    }

    class Showcase
    {
        private List<Product> _products;

        public Showcase()
        {
            _products = new()
            {
                new Product("Апельсин", GenerateRandomNumber(10, 100)),
                new Product("Яблоко", GenerateRandomNumber(10, 100)),
                new Product("Груша", GenerateRandomNumber(10, 100)),
                new Product("Малина", GenerateRandomNumber(10, 100)),
                new Product("Клубника", GenerateRandomNumber(10, 100)),
                new Product("Смородина", GenerateRandomNumber(10, 100)),
                new Product("Манго", GenerateRandomNumber(10, 100)),
                new Product("Арбуз", GenerateRandomNumber(10, 100)),
                new Product("Дыня", GenerateRandomNumber(10, 100)),
                new Product("Абрикос", GenerateRandomNumber(10, 100))
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

                if (buyer.TryBuyProduct(totalCost) == true)
                {
                    isPay = true;
                    Money += totalCost;
                }
                else
                {
                    buyer.RemoveRandomProduct();
                }
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
            _products?.Remove(product);
        }

        public Product GetOneProduct(int index)
        {
            return _products[index].Clone();
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