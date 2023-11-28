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

            int sickPacientCount = 50;
            Hospital hospital = new Hospital(sickPacientCount);
            hospital.Work();

            WaitToPressKey($"\nРабота программы завершена!\n");
        }
    }

    class Hospital
    {
        private List<SickPacient> _sickPacients;
        private string[] _sicknesses;

        public Hospital(int sickPacientCount)
        {
            _sicknesses = new string[]
            {
                "Язва", "Мигрень", "Анемия", "Гастрит", "Пневмония", "Тонзилит", "Фарингит"
            };

            _sickPacients = FillSickPacient(sickPacientCount);
        }

        public void Work()
        {
            const string PacientsSortByAgeCommand = "1";
            const string PacientsSortByNameCommand = "2";
            const string FindPacientsByConcretSicknessCommand = "3";
            const string ExitProgrammCommand = "4";

            bool isRun = true;

            ShowAllSickPacients("Список всех пациентов:\n",_sickPacients);

            WaitToPressKey("\n\n");

            SickPacientsSortByAge(_sickPacients);

            ShowAllSickPacients("Сортировка по году:\n", _sickPacients);

            WaitToPressKey("\n\n");

            SickPacientsSortByName(_sickPacients);

            ShowAllSickPacients("Сортировка по имени:\n", _sickPacients);
        }

        private void SickPacientsSortByAge(List<SickPacient> sickPacients)
        {
            _sickPacients = _sickPacients.OrderBy(pacient => pacient.Age).ToList();
        }

        private void SickPacientsSortByName(List<SickPacient> sickPacients)
        {
            _sickPacients = _sickPacients.OrderBy(pacient => pacient.Name).ToList();
        }

        private void FindPacientsByConcretSickness(List<SickPacient> sickPacients)
        {
            string userInput;
            userInput = ReadString($"Введите название заболевания для поиска пациентов:\n");

            Print("Введите номер заболевания для вывода списка: ");

        }

        private void ShowAllSickPacients(string message, List<SickPacient> sickPacients)
        {
            Print(message);

            for (int i = 0; i < sickPacients.Count; i++)
            {
                Print($"{i + 1}. {sickPacients[i]}\n");
            }
        }

        private List<SickPacient>? FillSickPacient(int sickPacientCount)
        {
            List<SickPacient> sickPacients = new List<SickPacient>();
            int minAgePacient = 1;
            int maxAgePacient = 100;

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

            string fullName;
            string sickness;
            int age;

            for (int i = 0; i < sickPacientCount; i++)
            {
                fullName = firstNames[GenerateRandomNumber(0, firstNames.Length)] + " " + lastNames[GenerateRandomNumber(0, lastNames.Length)];
                sickness = _sicknesses[GenerateRandomNumber(0, _sicknesses.Length)];
                age = GenerateRandomNumber(minAgePacient, maxAgePacient);
                SickPacient sickPacient = new SickPacient(fullName, sickness, age);

                sickPacients.Add(sickPacient);
            }

            return sickPacients;
        }
    }

    class SickPacient
    {
        public SickPacient(string name, string sickness, int age)
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