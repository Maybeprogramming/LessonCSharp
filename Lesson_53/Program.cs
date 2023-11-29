namespace Lesson_53
{
    using static Display;
    using static UserInput;
    using static Randomaizer;

    class Program
    {
        static void Main()
        {
            Console.Title = "Анархия в больнице";

            int pacientCount = 10;
            Hospital hospital = new Hospital(pacientCount);
            hospital.Work();

            Print($"\nРабота программы завершена!\n", ConsoleColor.Green);
        }
    }

    class Hospital
    {
        private List<Pacient> _pacients;
        private List<string> _sicknesses;

        public Hospital(int pacientCount)
        {
            _sicknesses = new List<string>()
            {
                "Язва", "Мигрень", "Анемия", "Гастрит", "Пневмония", "Тонзилит", "Фарингит"
            };

            _pacients = FillPacients(pacientCount);
        }

        public void Work()
        {
            const string PacientsSortBySickness = "1";
            const string PacientsSortByAgeCommand = "2";
            const string PacientsSortByNameCommand = "3";
            const string FindPacientsByConcretSicknessCommand = "4";
            const string ExitProgrammCommand = "5";

            bool isRun = true;
            string userInput;
            string requestMessage = "Введите номер команды: ";
            string titleMenu = "Доступные команды:\n";
            string menu = $"{PacientsSortBySickness}. - Отсортировать пациентов по заболеваниям.\n" +
                          $"{PacientsSortByAgeCommand}. - Отсортировать пациентов по годам.\n" +
                          $"{PacientsSortByNameCommand}. - Отсортировать пациентов по ФИО.\n" +
                          $"{FindPacientsByConcretSicknessCommand}. - Найти пациентов по заболеванию.\n" +
                          $"{ExitProgrammCommand}. - Выйти из программы.\n";

            while (isRun == true)
            {
                Console.Clear();

                ShowAllPacients("Список всех пациентов в больнице:\n", _pacients);

                PrintLine();
                Print(titleMenu, ConsoleColor.Green);
                Print(menu);
                PrintLine();

                userInput = ReadString(requestMessage);

                switch (userInput)
                {
                    case PacientsSortBySickness:
                        SortBySicknessPacients();
                        break;

                    case PacientsSortByAgeCommand:
                        SortByAgePacients();
                        break;

                    case PacientsSortByNameCommand:
                        SortByNamePacients();
                        break;

                    case FindPacientsByConcretSicknessCommand:
                        FindPacientsByConcretSickness();
                        break;

                    case ExitProgrammCommand:
                        isRun = false;
                        break;
                }

                WaitToPressKey("\n");
                PrintLine();
            }
        }

        private void SortByAgePacients()
        {
            _pacients = _pacients.OrderBy(pacient => pacient.Age).ToList();

            ShowAllPacients("Пациенты отсортированы по годам:\n", _pacients);
            PrintLine();
        }

        private void SortByNamePacients()
        {
            _pacients = _pacients.OrderBy(pacient => pacient.Name).ToList();

            ShowAllPacients("Пациенты отсортированы по имени:\n", _pacients);
            PrintLine();
        }

        private void SortBySicknessPacients()
        {
            _pacients = _pacients.OrderBy(pacient => pacient.Sickness).ToList();

            ShowAllPacients("Пациенты отсортированы по заболеваниям:\n", _pacients);
            PrintLine();
        }

        private void FindPacientsByConcretSickness()
        {
            string userInput;
            List<Pacient> pacientsFaund = new List<Pacient>();

            ShowAllSikness();

            userInput = ReadString($"Введите \"название\" заболевания для поиска пациентов: ");

            pacientsFaund = _sicknesses.Contains(userInput) == true ? pacientsFaund = _pacients.Where(p => p.Sickness.Equals(userInput)).ToList() : null;

            if (pacientsFaund != null && pacientsFaund.Count > 0)
            {
                PrintLine();
                ShowAllPacients("Найденные пациенты по запрашиваемому заболеванию:\n", pacientsFaund);
                PrintLine();
            }
            else
            {
                PrintLine();
                Print("С таким заболеванием пациенты в больницу не поступали.\n", ConsoleColor.Yellow);
                PrintLine();
            }

            //if (_sicknesses.Contains(userInput))
            //{
            //    pacientsFaund = _pacients.Where(pacient => pacient.Sickness.Equals(userInput)).ToList();

            //    if (pacientsFaund.Count > 0)
            //    {
            //        PrintLine();
            //        ShowAllPacients("Найденные пациенты по запрашиваемому заболеванию:\n", pacientsFaund);
            //        PrintLine();
            //    }
            //    else
            //    {
            //        PrintLine();
            //        Print("С таким заболеванием пациенты в больницу не поступали.\n", ConsoleColor.Yellow);
            //        PrintLine();
            //    }
            //}
            //else
            //{
            //    PrintLine();
            //    Print($"Вы ввели неверное заболевание. Попробуйте снова.\n", ConsoleColor.Red);
            //    PrintLine();
            //}
        }

        private void ShowAllSikness()
        {
            Print("\nСписок заболеваний:\n", ConsoleColor.Green);

            for (int i = 0; i < _sicknesses.Count; i++)
            {
                Print($"{i + 1}. {_sicknesses[i]}\n");
            }
        }

        private void ShowAllPacients(string message, List<Pacient> pacients)
        {
            Print(message);

            for (int i = 0; i < pacients.Count; i++)
            {
                Print($"{i + 1}. {pacients[i]}\n");
            }
        }

        private List<Pacient>? FillPacients(int sickPacientCount)
        {
            List<Pacient> pacients = new List<Pacient>();
            int minAgePacient = 1;
            int maxAgePacient = 100;
            string fullName;
            string sickness;
            int age;

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

            for (int i = 0; i < sickPacientCount; i++)
            {
                fullName = firstNames[GenerateRandomNumber(0, firstNames.Length)] + " " + lastNames[GenerateRandomNumber(0, lastNames.Length)];
                sickness = _sicknesses[GenerateRandomNumber(0, _sicknesses.Count)];
                age = GenerateRandomNumber(minAgePacient, maxAgePacient);
                Pacient pacient = new Pacient(fullName, sickness, age);

                pacients.Add(pacient);
            }

            return pacients;
        }
    }

    class Pacient
    {
        public Pacient(string name, string sickness, int age)
        {
            Name = name;
            Sickness = sickness;
            Age = age;
        }

        public string Name { get; }
        public string Sickness { get; }
        public int Age { get; }

        public override string ToString()
        {
            return $"{Name}, возраст: {Age}, заболевание: \"{Sickness}\"";
        }
    }

    #region UserUtils

    static class UserInput
    {
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

//Анархия в больнице
//У вас есть список больных(минимум 10 записей)
//Класс больного состоит из полей:
//ФИО,
//возраст,
//заболевание.
//Требуется написать программу больницы,
//в которой перед пользователем будет меню со следующими пунктами:
//1)Отсортировать всех больных по фио
//2)Отсортировать всех больных по возрасту
//3)Вывести больных с определенным заболеванием
//(название заболевания вводится пользователем с клавиатуры)