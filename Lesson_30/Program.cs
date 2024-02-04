namespace Lesson_30
{
    internal class Program
    {
        private static void Main()
        {
            Console.Title = "Программа для конвертации введенной строки в число";

            StartToParse();
            PrintText($"\nРаботы программы завершена", ConsoleColor.Green);
        }

        private static void StartToParse()
        {
            bool isTryParse = false;
            string requestNumber = "Введите число: ";
            string continueMessage = "\n\nНажмите любую клавишу чтобы продолжить...";
            string userInput;
            int number;

            while (isTryParse == false)
            {
                Console.Clear();
                PrintText(requestNumber);
                userInput = Console.ReadLine();
                number = ParseStringToInt(userInput, out bool isParseToInt);

                if (isParseToInt == true)
                {
                    PrintText($"\nВы ввели число: {number}", ConsoleColor.Green);
                    isTryParse = isParseToInt;
                }
                else
                {
                    PrintText($"\nВы ввели не число: {userInput}", ConsoleColor.Red);
                }

                PrintText(continueMessage);
                Console.ReadLine();
            }
        }

        private static int ParseStringToInt(string userInput, out bool isParseToInt)
        {
            isParseToInt = int.TryParse(userInput, out int result);
            return result;
        }

        private static void PrintText(string text, ConsoleColor color = ConsoleColor.White)
        {
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = defaultColor;
        }
    }
}