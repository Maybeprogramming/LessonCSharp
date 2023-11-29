namespace Lesson_54
{
    using static Display;
    using static UserInput;

    class Program
    {
        static void Main()
        {
            Console.Title = "Топ игроков сервера";

            LeaderBoard leaderBoard = new LeaderBoard();
            leaderBoard.Work();

            Print($"\nРабота программы завершена.\n", ConsoleColor.Green);
        }
    }

    class LeaderBoard
    {
        private List<Player> _players;
        private List<Player> _topPlayersByLevel;
        private List<Player> _topPlayersByStrength;

        public LeaderBoard()
        {
            _players = new List<Player>();
            _topPlayersByLevel = new List<Player>();
            _topPlayersByStrength = new List<Player>();
        }

        public void Work()
        {

        }
    }

    class Player
    {
        public Player(string name, int level, int strength)
        {
            Name = name;
            Level = level;
            Strength = strength;
        }

        public string Name { get; }
        public int Level { get; }
        public int Strength { get; }

        public override string ToString()
        {
            return $"{Name}. Уровень: {Level}. Сила {Strength}";
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

//Топ игроков сервера
//У нас есть список всех игроков(минимум 10).
//У каждого игрока есть поля:
//имя,
//уровень,
//сила.
//Требуется написать запрос для определения
//топ 3 игроков по уровню
//и топ 3 игроков по силе,
//после чего вывести каждый топ.
//2 запроса получится.