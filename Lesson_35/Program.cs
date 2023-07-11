// Динамический массив продвинутый
// Dynamic array advanced

//В массивах вы выполняли задание "Динамический массив"
//Используя всё изученное, напишите улучшенную версию динамического массива (не обязательно брать своё старое решение)
//Задание нужно, чтобы вы освоились с List и прощупали его преимущество.
//Проверка на ввод числа обязательна.
//Пользователь вводит числа, и программа их запоминает.
//Как только пользователь введёт команду sum, программа выведет сумму всех веденных чисел.
//Выход из программы должен происходить только в том случае, если пользователь введет команду exit.

namespace Lesson_35
{
    internal class Program
    {
        static void Main()
        {
            WorkProgramm();
        }

        private static void WorkProgramm()
        {
            const string sumCommand = "sum";
            const string exitProgramm = "exit";

            Console.Title = "Динамический массив продвинутый";
            List<int> numbersList = new List<int>();
            int sumNumbers = 0;
            string userInput;
            bool isRunProgramm = true;
            string requestCommandOrNumber = $"\n\nВведите команду или число: ";
            string continueMessage = $"\nНажмите любую клавишу чтобы продолжить";
            string commandMenu = $"{sumCommand} - команда для вычисления суммы всех чисел в массиве" +
                                 $"\n{exitProgramm} - выйти из приложения";
            string noElementsInArraymessage = $"\nОшибка! Вы ещё не ввели ни одного числа!";
            string errorCommandMessage = "Ошибка! Такого числа или команды нет!";
            int inputNumber;
            bool isTryParseToInt;

            while (isRunProgramm == true)
            {
                Console.WriteLine(commandMenu);
                Console.Write("Введенные числа: ");

                foreach (int number in numbersList)
                {
                    Console.Write($"{number} ");
                }

                Console.Write(requestCommandOrNumber);
                userInput = Console.ReadLine();
                isTryParseToInt = Int32.TryParse(userInput, out int result);

                switch (userInput)
                {
                    case sumCommand:
                        SumNumbers(numbersList, sumNumbers, noElementsInArraymessage);
                        break;

                    case exitProgramm:
                        isRunProgramm = false;
                        break;

                    default:
                        TryAddNumberToList(numbersList, errorCommandMessage, isTryParseToInt, result);
                        break;
                }

                Console.WriteLine(continueMessage);
                Console.ReadKey();
                Console.Clear();
            }
        }

        private static void TryAddNumberToList(List<int> numberList, string errorCommandMessage, bool isTryParseToInt, int result)
        {
            if (isTryParseToInt == true)
            {
                numberList.Add(result);
            }
            else
            {
                Console.WriteLine(errorCommandMessage);
            }
        }

        private static void SumNumbers(List<int> numberList, int sumNumbers, string noElementsInArraymessage)
        {
            if (numberList.Count > 0)
            {
                sumNumbers = numberList.Sum(number => number);
                Console.WriteLine($"Cумма всех введенных чисел равна: {sumNumbers}");
            }
            else
            {
                Console.WriteLine(noElementsInArraymessage);
            }

            sumNumbers = 0;
        }
    }
}