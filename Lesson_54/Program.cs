namespace Lesson_54
{
    using static Display;
    using static UserInput;

    class Program
    {
        static void Main()
        {
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