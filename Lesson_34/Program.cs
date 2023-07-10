    // Очередь в магазине
    // Queue at the store

    //У вас есть множество целых чисел.
    //Каждое целое число - это сумма покупки.
    //Вам нужно обслуживать клиентов до тех пор, пока очередь не станет пуста.
    //После каждого обслуженного клиента деньги нужно добавлять на наш счёт и выводить его в консоль.
    //После обслуживания каждого клиента программа ожидает нажатия любой клавиши,
    //после чего затирает консоль и по новой выводит всю информацию, только уже со следующим клиентом

namespace Lesson_34
{
    class Program
    {
        static void Main()
        {
            Console.Title = "Очередь в магазине";
            StartWorkShop();
        }

        private static void StartWorkShop()
        {
            Random random = new();

            int startBalanceMoney = 0;
            Seller seller = new(startBalanceMoney);

            int queueCustomerCount = 15;
            int minMoneyCustomer = 40;
            int maxMoneyCustomer = 200;
            int numberCustomer = 0;
            Queue<Customer> customers = new Queue<Customer>(queueCustomerCount);
            Customer customer;

            int lowPriceProduct = 50;
            int highPriceProduct = 100;
            string[] productsNames = { "Булочка", "Кефир", "Творожок", "Картофель", "Помидор", "Хлебушек", "Капуста", "Вишня", "Паштет" };
            int firstIndexProduct = 0;
            int lastIndexProduct = productsNames.Length;
            Product product;

            string requestMessage = "Нажмите любую клавишу для продолжения...";
            int delayOperationMiliseconds = 200;
            bool isWorkShop = true;

            for (int i = 0; i < queueCustomerCount; i++)
            {
                customers.Enqueue(new Customer(random.Next(minMoneyCustomer, maxMoneyCustomer + 1)));
            }

            while (isWorkShop)
            {
                Console.Clear();

                customer = customers.Dequeue();
                numberCustomer++;
                product = new Product(productsNames[random.Next(firstIndexProduct, lastIndexProduct)], random.Next(lowPriceProduct, highPriceProduct + 1));
                seller.ShowCurrentBalance();
                Console.WriteLine($"Обслуживается клиент №{numberCustomer}:");
                seller.TrySellProduct(customer.TryToBuyProduct(product));
                isWorkShop = customers.Count > 0;
                Task.Delay(delayOperationMiliseconds).Wait();

                Console.WriteLine("\n" + requestMessage);
                Console.ReadLine();
            }

            Console.Clear();
            seller.ShowCurrentBalance();
            Console.WriteLine("Магазин закрывается на техническое обслужвивание, ждём вас завтра!");
            Console.ReadLine();
        }
    }

    class Customer
    {
        private const int ZeroMoney = 0;

        private int _money;

        public Customer(int money)
        {
            _money = money;
        }

        public int TryToBuyProduct(Product product)
        {
            if (_money > ZeroMoney && _money >= product.Price)
            {
                _money -= product.Price;
                Console.WriteLine($"Клиент сделал покупку продукта: {product.Name} по цене: {product.Price} у.е.");
                return product.Price;
            }
            else
            {
                Console.WriteLine("У клиента не хватило денег =( и он уходит...");
                return ZeroMoney;
            }
        }
    }

    class Seller
    {
        private int _balanceMoney;

        public Seller(int balanceMoney)
        {
            _balanceMoney = balanceMoney;
        }

        public void TrySellProduct(int money)
        {
            if (money > 0)
            {
                _balanceMoney += money;
                Console.WriteLine($"Чек выписан на {money} у.е. Спасибо за покупку!");
            }
            else
            {
                Console.Error.WriteLine($"Данная операция не доступна! Извините за предоставленные неудобства!");
            }
        }

        public void ShowCurrentBalance()
        {
            Console.WriteLine($"Текущий баланс магазина составляет: {_balanceMoney} у.е.");
        }
    }

    class Product
    {
        private string _name;
        private int _price;

        public Product(string name, int price)
        {
            _name = name;
            _price = price;
        }

        public string Name => _name;
        public int Price => _price;
    }
}