namespace Lesson_57
{
    using static Display;
    using static UserInput;
    using static Randomaizer;

    class Program
    {
        static void Main()
        {
            Console.Title = "Объединение войск";

            Barraks barraks = new Barraks();
            barraks.Work();

            Print($"\nРабота программы завершена!", ConsoleColor.Green);
        }
    }

    class Barraks
    {
        private List<Fighter> _fightersSquad1;
        private List<Fighter> _fightersSquad2;

        public Barraks()
        {
            _fightersSquad1 = new List<Fighter>();
            _fightersSquad2 = new List<Fighter>();
        }

        public void Work()
        {
            ShowFighters("Список бойцов из первого отряда\n", _fightersSquad1);
            PrintLine();
            ShowFighters("Список бойцов из второго отряда\n", _fightersSquad2);
            PrintLine();

            WaitToPressKey("\n");
        }

        private void ShowFighters(string message, List<Fighter> fighters)
        {
            Print(message);

            for (int i = 0; i < fighters.Count; i++)
            {
                Print($"{i + 1}. Боец: {fighters[i]}\n");
            }
        }
    }

    class Fighter
    {
        public Fighter(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public override string ToString()
        {
            return $"{Name}";
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

//Объединение войск
//Есть 2 списка в солдатами.
//Всех бойцов из отряда 1,
//у которых фамилия начинается на букву Б,
//требуется перевести в отряд 2.