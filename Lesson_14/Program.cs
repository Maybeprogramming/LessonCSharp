namespace Lesson_14
{
    internal class Program
    {
        static void Main()
        {
            const string CommandStartPrint = "1";
            const string CommandExitProgramm = "2";

            string inputString;
            string inputSymbol;
            string spaceSymbol = " ";
            string middleString;
            string borderString;
            string totalString;

            bool isRunProgramm = true;
            string inputCommand;
            string continueMessege = "Нажмите клавишу чтобы продолжить...";
            string programmMenu = $"Программа для принта имени!!!" +
                                  $"\nДоступные команды: " +
                                  $"\n{CommandStartPrint} - для начала работы программы" +
                                  $"\n{CommandExitProgramm} - для выхода из программы";
            string requestCommandMessage = $"\nВведите команду: ";
            string requestInputStringMessage = $"\nВведите строку для принта: ";
            string requestInputCharMessage = $"\nВведите 1 символ для рамки: ";

            while (isRunProgramm)
            {
                Console.Clear();
                Console.WriteLine(programmMenu);
                Console.Write(requestCommandMessage);
                inputCommand = Console.ReadLine();

                switch (inputCommand)
                {
                    case CommandStartPrint:
                        Console.Write(requestInputStringMessage);
                        inputString = Console.ReadLine();
                        Console.Write(requestInputCharMessage);
                        inputSymbol = Console.ReadLine();

                        middleString = "\n" + inputSymbol + spaceSymbol + inputString + spaceSymbol + inputSymbol;
                        borderString = "\n" + inputSymbol + new string(Convert.ToChar(inputSymbol), middleString.Length) + inputSymbol;
                        totalString = borderString + middleString + borderString;

                        Console.WriteLine(totalString);
                        Console.WriteLine("\n" + continueMessege);
                        Console.ReadLine();
                        break;

                    case CommandExitProgramm:
                        isRunProgramm = false;
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(inputCommand);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(" - такой команды не существует, попробуйте ввести другую команду...\n");
                        Console.WriteLine(continueMessege);
                        Console.ReadLine();
                        break;
                }
            }

            Console.WriteLine("\nРабота программы завершена.");
            Console.ReadKey();
        }
    }
}