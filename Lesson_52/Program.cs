namespace Lesson_52
{
    using static Display;
    using static UserInput;
    using static Randomaizer;

    class Program
    {
        static void Main()
        {
            Console.Title = "Амнистия в стране Арстоцка";

            int prisonersCount = 20;
            Prison prison = new Prison(prisonersCount);
            prison.Work();

            WaitToPressKey("\nРабота программы завершена.\n");
        }
    }

    class Prison
    {
        private List<Prisoner> _prisoners;

        public Prison(int prisonersCount)
        {
            _prisoners = FillPrisoners(prisonersCount);
        }

        public void Work()
        {
            const string HoldAnAmnestyCommand = "1";
            const string ExitCommand = "2";

            string crimeToAmnesty = "Антиправительственное";
            string quastionText = $"\nВы хотите амнистировать преступников за {crimeToAmnesty}?\n";
            string menu = $"{HoldAnAmnestyCommand} - Аминистировать\n" +
                          $"{ExitCommand} - Не делать ничего и выйти из программы\n";
            string requestMessage = "Введите номер команды: ";
            string userInput;
            bool isRun = true;

            ShowPrisonersInfo("Список всех преступников:\n", _prisoners);

            Print(quastionText, ConsoleColor.Yellow);
            Print(menu);

            while (isRun == true)
            {
                userInput = ReadString(requestMessage);

                switch (userInput)
                {
                    case HoldAnAmnestyCommand:
                        ToHoldAnAmnesty(_prisoners);
                        isRun = false;
                        break;

                    case ExitCommand:
                        isRun = false;
                        break;

                    default:
                        Print("Такой команды нет, попробуйте снова...\n", ConsoleColor.Red);
                        break;
                }
            }
        }

        private void ToHoldAnAmnesty(List<Prisoner> prisoners)
        {

        }

        private List<Prisoner> FillPrisoners(int prisonersCount)
        {
            List<Prisoner> prisoners = new List<Prisoner>();

            string[] firstNames =
            {
                "Алексей", "Александр", "Вячеслав", "Всеволод", "Геннадий", "Григорий", "Дмитрий", "Даниил", "Демьян", "Михаил",
                "Леонид", "Николай", "Валерий", "Сергей", "Иван", "Олег", "Владислав", "Игорь", "Юрий", "Павел", "Пётр", "Андрей"
            };

            string[] lastNames =
            {
                "Алексеев", "Иванов", "Петров", "Павлов", "Жуков", "Михаленков", "Прудков", "Жабин", "Плотниченко", "Зайцев", "Сидоров",
                "Володченко", "Сергеев", "Бубликов", "Пирожков", "Карченко", "Пухалёв", "Рожков", "Сабельников", "Пыжиков", "Стародубцев"
            };

            string[] crimesNames =
            {
                "Антиправительственное", "Коррупция", "Грабеж", "Хулиганство"
            };

            string fullName;
            string crime;


            for (int i = 0; i < prisonersCount; i++)
            {
                fullName = firstNames[GenerateRandomNumber(0, firstNames.Length)] + " " + lastNames[GenerateRandomNumber(0, lastNames.Length)];
                crime = crimesNames[GenerateRandomNumber(0, crimesNames.Length)];

                prisoners.Add(new Prisoner(fullName, crime));
            }

            return prisoners;
        }

        private void ShowPrisonersInfo(string message, List<Prisoner> prisoners)
        {
            Print(message, ConsoleColor.Green);

            for (int i = 0; i < prisoners.Count; i++)
            {
                Print($"{i + 1}. {prisoners[i].ShowInfo()}\n");
            }
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

//Амнистия
//В нашей великой стране Арстоцка произошла амнистия!
//Всех людей, заключенных за преступление "Антиправительственное",
//следует исключить из списка заключенных.
//Есть список заключенных,
//каждый заключенный состоит из полей:
//ФИО,
//преступление.
//Вывести список до амнистии и после.