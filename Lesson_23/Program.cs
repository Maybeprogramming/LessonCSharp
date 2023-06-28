namespace Lesson_23
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string sumCommand = "sum";
            const string exitProgramm = "exit";

            int[] numbers = new int[0];
            bool isRunProgramm = true;
            string requestCommandOrNumber = $"\n\nВведите команду или число: ";
            string continueMessage = $"\nНажмите любую клавишу чтобы продолжить";
            string titleText = "Программа для вычисления суммы элементов динамического массива";
            string commandMenu = $"{sumCommand} - команда для вычисления суммы всех чисел в массиве" +
                                 $"\n{exitProgramm} - выйти из приложения";
            string noElementsInArraymessage = $"\nОшибка! Вы ещё не ввели ни одного числа!";
            string errorCommandMessage = "Ошибка! Такого числа или команды нет!";
            string userInput;
            int inputNumber;
            int sumNumbers = 0;
            bool isTryParseToInt;

            Console.Title = titleText;

            while (isRunProgramm == true)
            {
                Console.WriteLine(commandMenu);
                Console.Write("Введенные числа: ");

                foreach (int number in numbers)
                {
                    Console.Write($"{number} ");
                }

                Console.Write(requestCommandOrNumber);
                userInput = Console.ReadLine();
                isTryParseToInt = Int32.TryParse(userInput, out inputNumber);

                switch (userInput)
                {
                    case sumCommand:
                        if (numbers.Length > 0)
                        {
                            foreach (int number in numbers)
                            {
                                sumNumbers += number;
                            }

                            Console.WriteLine($"Cумма всех введенных чисел равна: {sumNumbers}");
                            sumNumbers = 0;
                        }
                        else
                        {
                            Console.WriteLine(noElementsInArraymessage);
                        }
                        break;

                    case exitProgramm:
                        isRunProgramm = false;
                        break;

                    default:
                        if (isTryParseToInt == true)
                        {
                            int[] tempNumbers = new int[numbers.Length + 1];

                            for (int i = 0; i < numbers.Length; i++)
                            {
                                tempNumbers[i] = numbers[i];
                            }

                            tempNumbers[tempNumbers.Length - 1] = inputNumber;
                            numbers = tempNumbers;
                        }
                        else
                        {
                            Console.WriteLine(errorCommandMessage);
                        }
                        break;
                }

                Console.WriteLine(continueMessage);
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}