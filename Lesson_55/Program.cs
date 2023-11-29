namespace Lesson_55
{
    using static Display;
    using static UserInput;

    class Program
    {
        static void Main()
        {
            Console.Title = "Определение просрочки";
        }
    }

    class Stock
    {
        List<Product> _products;

        public Stock(int productsCount)
        {
            _products = FillProducts(productsCount);
        }

        public void Work()
        {

        }

        private void ShowProducts()
        {

        }

        private List<Product> FillProducts(int productsCount)
        {
            return new List<Product>();
        }
    }

    class Product
    {
        public string Name { get; }
        public int ProductionDate { get; }
        public int ExpirationDate { get; }

        public override string ToString()
        {
            return $"\"{Name}\". Дата производства: {ProductionDate}. Срок годности: {ExpirationDate} лет.";
        }
    }

    #region UserUtils

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

        public static string ReadString(string message)
        {
            Console.Write(message);

            return Console.ReadLine();
        }

        public static void WaitToPressKey(string message = "")
        {
            Print(message);
            Print($"Для продолжения нажмите любую клавишу...\n");
            Console.ReadKey();
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

        public static void PrintLine(ConsoleColor color = ConsoleColor.White)
        {
            int symbolCount = Console.WindowWidth - 1;
            Print($"{new string('-', symbolCount)}\n", color);
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

    #endregion
}

//Определение просрочки
//Есть набор тушенки.
//У тушенки есть:
//название,
//год производства
//и срок годности.
//Написать запрос для получения всех просроченных банок тушенки.
//Чтобы не заморачиваться, можете думать, что считаем только года, без месяцев.