namespace Lesson_43
{
    class Program
    {
        static void Main()
        {
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

        public void BuyProducts (Product product)
        {
            _products.Add(product);
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