namespace Lesson_52
{
    using static Display;
    using static UserInput;

    class Program
    {
        static void Main()
        {
            Console.Title = "Амнистия";
        }
    }

    class Prison
    {
        List<Prisoner> _prisoners;

        public Prison()
        {
            _prisoners = new List<Prisoner>()
            {
                new Prisoner("", ""),
                new Prisoner("", ""),
                new Prisoner("", ""),
            };
        }
    }

    class Prisoner
    {
        public Prisoner(string name, string crime)
        {
            Name = name;
            Crime = crime;
        }

        string Name { get; }
        string Crime { get; }

        public string ShowInfo()
        {
            return $"{Name}. Статья: {Crime}";
        }
    }

    enum Crimes
    {

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

    #endregion
}

//Амнистия
//В нашей великой стране Арстоцка произошла амнистия!
//Всех людей, заключенных за преступление "Антиправительственное",
//следует исключить из списка заключенных.
//Есть список заключенных,
//каждый заключенный состоит из полей:
//ФИО,
//преступление.
//Вывести список до амнистии и после.