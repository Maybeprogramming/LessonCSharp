namespace Lesson_51
{
    using static Display;
    using static UserInput;
    using System.Text;

    class Program
    {
        static void Main()
        {
            DetectiveOffice detectiveOffice = new DetectiveOffice();
            detectiveOffice.Work();

            Console.ReadKey();
        }
    }

    class DetectiveOffice
    {
        private List<Criminal> _criminals;

        public DetectiveOffice()
        {
            _criminals = new List<Criminal>()
            {
                new Criminal("Алексей Соколов", false, 170, 60, "Русский"),
                new Criminal("Евгений Иванов", true, 170, 60, "Русский"),
                new Criminal("Магомед Долганов", false, 170, 60, "Дагестанец"),
                new Criminal("Рамзан Чимиков", false, 180, 60, "Дагестанец"),
                new Criminal("Узун Абдула", false, 180, 60, "Азейбарджанец"),
                new Criminal("Келач Рашид", false, 180, 60, "Азейбарджанец"),
                new Criminal("Ибрагим Насыбуллин", false, 185, 60, "Татарин"),
                new Criminal("Казим Замалиев", false, 185, 60, "Татарин"),
                new Criminal("Эльнар Атаев", false, 185, 60, "Узбек"),
                new Criminal("Багымбай Бердиев", false, 160, 60, "Узбек"),
                new Criminal("Заман Гаттаулин", false, 160, 60, "Татарин"),
                new Criminal("Ильдар Касимов", false, 160, 60, "Татарин"),
                new Criminal("Армель Бюжо", false, 175, 60, "Француз"),
                new Criminal("Гастон Дебюсси", false, 175, 60, "Француз"),
                new Criminal("Микола Радчук", false, 175, 60, "Белорус"),
                new Criminal("Тимур Савич", false, 170, 60, "Белорус"),
                new Criminal("Анджей Любанский", false, 180, 60, "Поляк"),
                new Criminal("Бартоломей Невядомский", false, 185, 60, "Поляк"),
                new Criminal("Айан Галев", false, 175, 60, "Татарин"),
                new Criminal("Дохсун Жданов", false, 160, 60, "Татарин")
            };
        }

        public List<Criminal> TryGetCriminals(int heigth, int weigth, string nationaly)
        {
            return new List<Criminal>(_criminals.Where(crimainal => crimainal.Height == heigth &&
                                                                    crimainal.Weight == weigth &&
                                                                    crimainal.Nationaly.Equals(nationaly) &&
                                                                    crimainal.IsImprisoned != true));
        }

        public string TryGetCriminalsInfo()
        {
            StringBuilder criminalsInfo = new StringBuilder();
            List<Criminal> criminals = TryGetCriminals(170, 60, "Русский");

            foreach (var criminal in criminals)
            {
                criminalsInfo.Append($"{criminal.ShowInfo()}\n");
            }

            return criminalsInfo.ToString();
        }

        public void Work()
        {
            Print($"{TryGetCriminalsInfo()}\n");
        }

        private void ShowAllCriminalsInfo()
        {
            Print($"Список всех преступников:\n");

            for (int i = 0; i < _criminals.Count; i++)
            {
                Print($"{i + 1}. {_criminals[i].ShowInfo()}\n");
            }
        }
    }

    class CrimanalFactory
    {
        public List<Criminal> CreateSomeCriminals(int criminalsCount)
        {
            return new List<Criminal>();
        }

        public Criminal CreateCriminal()
        {
            return new Criminal("", false, 180, 60, "Якут");
        }
    }

    class Criminal
    {
        public Criminal(string name, bool isImprisoned, int height, int weight, string nationaly)
        {
            Name = name;
            IsImprisoned = isImprisoned;
            Height = height;
            Weight = weight;
            Nationaly = nationaly;
        }

        public string Name { get; }
        public bool IsImprisoned { get; }
        public string IsImprisonedToString { get => IsImprisoned == true ? "да" : "нет"; }
        public int Height { get; }
        public int Weight { get; }
        public string Nationaly { get; }

        public string ShowInfo()
        {
            return $"{Name}. Рост [{Height}] см, Вес [{Weight}] кг, Национальность: [{Nationaly}]. Заключение под стражей: [{IsImprisonedToString}].";
        }
    }

    #region UserUtils

    static class Randomaizer
    {
        private static readonly Random s_random;

        static Randomaizer()
        {
            s_random = new();
        }

        public static string GenerateRandomName(string[] names)
        {
            return names[s_random.Next(0, names.Length)];
        }

        public static int GenerateRandomNumber(int minValue, int maxValue)
        {
            return s_random.Next(minValue, maxValue);
        }
    }

    static class UserInput
    {
        public static int ReadIntRange(string message, int minValue = int.MinValue, int maxValue = int.MaxValue)
        {
            int result;

            Console.Write(message);

            while (int.TryParse(Console.ReadLine(), out result) == false || result < minValue || result >= maxValue)
            {
                Console.Error.WriteLine("Ошибка!. Попробуйте снова!");
            }

            return result;
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

//Поиск преступника
//У нас есть список всех преступников.
//В преступнике есть поля:
//ФИО,
//заключен ли он под стражу,
//рост,
//вес,
//национальность.
//Вашей программой будут пользоваться детективы.
//У детектива запрашиваются данные (рост, вес, национальность),
//и детективу выводятся все преступники,
//которые подходят под эти параметры,
//но уже заключенные под стражу выводиться не должны.