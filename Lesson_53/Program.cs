namespace Lesson_53
{
    using System.Linq;
    using static Display;
    using static UserInput;

    class Program
    {
        static void Main()
        {
            Console.Title = "Анархия в больнице";

            int sickPacientCount = 10;
            Hospital hospital = new Hospital(sickPacientCount);
            hospital.Work();

            WaitToPressKey($"\nРабота программы завершена!\n");
        }
    }

    class Hospital
    {
        private List<SickPacient> _sickPacients;

        public Hospital(int sickPacientCount)
        {
            _sickPacients = FillSickPacient(sickPacientCount);
        }

        public void Work()
        {

        }

        private void SickPacientsSortByAge(List<SickPacient> sickPacients)
        {
            _sickPacients.OrderBy(pacient => pacient.Age);
        }

        private void SickPacientsSortByName(List<SickPacient> sickPacients)
        {
            _sickPacients.OrderBy(pacient => pacient.Name);
        }

        private void FindPacientsByConcretSickness(List<SickPacient> sickPacients)
        {

        }

        private List<SickPacient>? FillSickPacient(int sickPacientCount)
        {
            return new List<SickPacient>();
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