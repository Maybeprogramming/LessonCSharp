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
        private Seller? _seller;
        private Queue<Buyer>? _buyers;

        public Market()
        {
            _seller = new();
            _buyers = new();
        }

        public void Work()
        {

        }
    }

    class Seller
    {
        public void SellProducts(Buyer buyer)
        {

        }

        public int TotalMoney { get; private set; }
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
            bool IsPay = false;

            while (IsPay == false)
            {
                if (_money >= totalCost)
                {
                    _money -= totalCost;
                    IsPay = true;
                    return true;
                }
                else
                {
                    RemoveRandomProduct();
                    return false;
                }
            }

            return false;
        }

        public void PutProductToCart(Product product)
        {
            _cart?.AddProduct(product);
        }

        private void RemoveRandomProduct()
        {
            int minProductNumber = 0;
            int maxProductNumber = _cart.ProductsCount;

            int randomProductNumber = GenerateRandomNumber(minProductNumber, maxProductNumber);
            Product product = _cart.GetProducts()[randomProductNumber];

            _cart.RemoveProduct(product);
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

        public List<Product> GetProducts()
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