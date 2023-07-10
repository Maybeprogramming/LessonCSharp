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
            string consoleTitle = "Задача - очередь в магазине";

            WorkShop();

            Console.Title = consoleTitle;
            Console.WriteLine("\nМагазин закрывается на техническое обслужвивание, ждём вас завтра!");
            Console.ReadLine();
        }

        private static void WorkShop()
        {
            Random random = new();
            string titleBalanceText = "Баланс магазина: ";
            string requestMessage = "Нажмите любую клавишу для продолжения...";
            int shopBalance = 0;
            int queueCustomerCount = 15;
            int minMoneyCustomer = 50;
            int maxMoneyCustomer = 500;
            int currentCustomer = 0;
            int paidMoney;
            bool isWorkShop = true;

            Queue<int> customers = new Queue<int>(queueCustomerCount);

            FillingQueueCustomers(random, queueCustomerCount, minMoneyCustomer, maxMoneyCustomer, customers);

            while (isWorkShop)
            {
                Console.Clear();

                paidMoney = customers.Dequeue();
                currentCustomer++;

                Console.WriteLine($"Обслуживается клиент №{currentCustomer}:");
                Console.WriteLine($"Совершена покупка товара на сумму {paidMoney} рублей");

                shopBalance += paidMoney;
                isWorkShop = customers.Count > 0;

                Console.Title = titleBalanceText + shopBalance;
                Console.WriteLine("\n" + requestMessage);
                Console.ReadLine();
            }

            Console.Clear();
            Console.WriteLine($"Баланс магазина от продаж за прошедший день составляет: {shopBalance} рублей");
        }

        private static void FillingQueueCustomers(Random random, int queueCustomerCount, int minMoneyCustomer, int maxMoneyCustomer, Queue<int> customers)
        {
            for (int i = 0; i < queueCustomerCount; i++)
            {
                customers.Enqueue(random.Next(minMoneyCustomer, maxMoneyCustomer + 1));
            }
        }
    }
}