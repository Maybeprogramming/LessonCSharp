namespace Lesson_56
{
    using static Display;
    using static UserInput;
    using static Randomaizer;

    class Program
    {
        static void Main()
        {
            Console.Title = "Отчет о вооружении";
        }
    }

    class Report
    {
        private List<Soldier> _soldiers;

        public Report()
        {
            _soldiers = FillSoldiers();
        }

        public void Work()
        {

        }

        private List<Soldier> FillSoldiers()
        {
            List<string> names = new List<string>() 
            { 
                "Иванов",
                "Соколов",
                "Петров",
                "Сидоров",
                "Шевчук",
                "Шапенков",
                "Простаков",
                "Монахов",
                "Михайлин",
                "Ротнов",
                "Бирюков",
                "Мищеряков",
                "Коваленко"
            };

            List<string> weapons = new List<string>() 
            { 
                "Пистолет", 
                "Автомат", 
                "Пулемет", 
                "Винтовка", 
                "Кинжал" 
            };

            List<string> ranks = new List<string>() 
            { 
                "Рядовой", 
                "Сержант", 
                "Лейтенант", 
                "Генерал" 
            };

            int minDate = 1;
            int maxDate = 60;
            string name;
            string weapon;
            string rank;
            int date;
            List<Soldier> soldiers = new List<Soldier>();

            for (int i = 0; i < names.Count; i++)
            {
                name = names[i];
                weapon = weapons[GenerateRandomNumber(0, weapons.Count)];
                rank = ranks[GenerateRandomNumber(0, ranks.Count)];
                date = GenerateRandomNumber(minDate, maxDate);
                Soldier soldier = new Soldier(name, weapon, rank, date);

                soldiers.Add(soldier);
            }

            return new List<Soldier>();
        }
    }

    class Soldier
    {
        public Soldier(string name, string weapon, string rank, int date)
        {
            Name = name;
            Weapon = weapon;
            Rank = rank;
            Date = date;
        }

        public string Name { get; }
        public string Weapon { get; }
        public string Rank { get; }
        public int Date { get; }
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

//Отчет о вооружении
//Существует класс солдата.
//В нём есть поля:
//имя,
//вооружение,
//звание,
//срок службы(в месяцах).
//Написать запрос, при помощи которого получить набор данных состоящий из имени и звания.
//Вывести все полученные данные в консоль.
//(Не менее 5 записей)