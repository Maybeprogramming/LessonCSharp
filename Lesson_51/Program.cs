namespace Lesson_51
{
    using static Display;
    using static UserInput;

    class Program
    {
        static void Main()
        {
            Console.Title = "Поиск преступника";
            Console.WindowWidth = 120;

            DetectiveOffice detectiveOffice = new DetectiveOffice();
            detectiveOffice.Work();

            WaitToPressKey("Работа программы завершена.\n");
        }
    }

    class DetectiveOffice
    {
        private List<Criminal> _criminals;

        public DetectiveOffice()
        {
            _criminals = new List<Criminal>()
            {
                new Criminal("Алексей Соколов", false, 170, 65, "Русский"),
                new Criminal("Евгений Иванов", true, 170, 65, "Русский"),
                new Criminal("Александр Милохин", false, 170, 70, "Русский"),
                new Criminal("Петр Волков", false, 170, 70, "Русский"),
                new Criminal("Михаил Швецов", false, 180, 60, "Русский"),
                new Criminal("Павел Корягин", false, 180, 95, "Русский"),
                new Criminal("Сергей Жуков", true, 180, 95, "Русский"),
                new Criminal("Олег Простов", false, 170, 60, "Русский"),
                new Criminal("Валерий Михаленков", false, 170, 60, "Русский"),
                new Criminal("Ибрагим Насыбуллин", false, 185, 60, "Татарин"),
                new Criminal("Казим Замалиев", false, 185, 60, "Татарин"),
                new Criminal("Айан Галев", false, 175, 60, "Татарин"),
                new Criminal("Дохсун Жданов", true, 160, 60, "Татарин"),
                new Criminal("Заман Гаттаулин", false, 160, 60, "Татарин"),
                new Criminal("Ильдар Касимов", false, 160, 60, "Татарин"),
                new Criminal("Келач Рашид", false, 180, 60, "Азейбарджанец"),
                new Criminal("Узун Абдула", false, 180, 60, "Азейбарджанец"),
                new Criminal("Рамзан Чимиков", false, 180, 60, "Дагестанец"),
                new Criminal("Магомед Долганов", false, 170, 60, "Дагестанец"),
                new Criminal("Эльнар Атаев", false, 185, 60, "Узбек"),
                new Criminal("Багымбай Бердиев", false, 160, 60, "Узбек"),
                new Criminal("Армель Бюжо", false, 175, 60, "Француз"),
                new Criminal("Гастон Дебюсси", false, 175, 60, "Француз"),
                new Criminal("Микола Радчук", false, 175, 60, "Белорус"),
                new Criminal("Тимур Савич", false, 170, 60, "Белорус"),
                new Criminal("Анджей Любанский", false, 180, 60, "Поляк"),
                new Criminal("Бартоломей Невядомский", false, 185, 60, "Поляк"),
            };
        }

        public List<Criminal> TryGetCriminals(int heigth, int weigth, string nationaly)
        {
            return new List<Criminal>(_criminals.Where(crimainal => crimainal.Height == heigth &&
                                                                    crimainal.Weight == weigth &&
                                                                    crimainal.Nationaly.ToLower().Equals(nationaly.ToLower()) &&
                                                                    crimainal.IsImprisoned != true));
        }

        public void Work()
        {
            const int FindCriminalsByParametrsCommand = 1;
            const int ShowAllCrimanalsInfoCommand = 2;
            const int ExitCommand = 3;

            bool isWork = true;
            int userInput;

            string titleMenu = "Список доступных команд:\n";
            string menu = $"{FindCriminalsByParametrsCommand} - Найти преступника по параметрам\n" +
                          $"{ShowAllCrimanalsInfoCommand} - Показать всех преступников\n" +
                          $"{ExitCommand} - Выйти из программы.\n";
            string requestMessage = "Введите номер команды: ";

            while (isWork == true)
            {
                Console.Clear();

                Print(titleMenu, ConsoleColor.Green);
                Print(menu);

                userInput = ReadInt(requestMessage);

                switch (userInput)
                {
                    case FindCriminalsByParametrsCommand:
                        FindCrimrnalsByParametrs();
                        break;

                    case ShowAllCrimanalsInfoCommand:
                        ShowCriminalsInfo("\nСписок всех преступников в базе:\n", _criminals);
                        break;

                    case ExitCommand:
                        isWork = false;
                        break;

                    default:
                        break;
                }

                WaitToPressKey("\n");
            }
        }

        private void FindCrimrnalsByParametrs()
        {
            int height;
            int weight;
            string nationaly;
            List<Criminal> criminals;

            string requestHeightText = "Введите рост (в см): ";
            string requestWeightText = "Введите вес (в кг): ";
            string requestNationalyText = "Введите национальность: ";

            height = ReadInt(requestHeightText);
            weight = ReadInt(requestWeightText);
            nationaly = ReadString(requestNationalyText);

            criminals = TryGetCriminals(height, weight, nationaly);

            if(criminals.Count == 0)
            {
                Print($"\nПо запросу ничего не найдено!!!\n", ConsoleColor.Red);
                return;
            }

            ShowCriminalsInfo("\nСписок найденных преступников по запросу:\n", criminals);
        }

        private void ShowCriminalsInfo(string message, List<Criminal> criminals)
        {
            Print(message);

            for (int i = 0; i < criminals.Count; i++)
            {
                Print($"{i + 1}. {criminals[i].ShowInfo()}\n");
            }
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
            return $"{Name}. Рост {Height} см, Вес {Weight} кг, Национальность: {Nationaly}. Под стражей? [{IsImprisonedToString}].";
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