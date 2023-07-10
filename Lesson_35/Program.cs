namespace Lesson_35
{
    // Динамический массив продвинутый
    // Dynamic array advanced

    //В массивах вы выполняли задание "Динамический массив"
    //Используя всё изученное, напишите улучшенную версию динамического массива (не обязательно брать своё старое решение)
    //Задание нужно, чтобы вы освоились с List и прощупали его преимущество.
    //Проверка на ввод числа обязательна.
    //Пользователь вводит числа, и программа их запоминает.
    //Как только пользователь введёт команду sum, программа выведет сумму всех веденных чисел.
    //Выход из программы должен происходить только в том случае, если пользователь введет команду exit.

    internal class Program
    {
        static void Main()
        {
            const string sumCommand = "sum";
            const string exitProgramm = "exit";

            Console.Title = "Динамический массив продвинутый";
            List<int> numberList = new List<int>();
            int sumNumbers = 0;
            string userInput;
            string requestEnterNumber = "Пожалуйста, введите число";
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

                foreach (int number in numberList)
                {
                    Console.Write($"{number} ");
                }

                Console.Write(requestCommandOrNumber);
                userInput = Console.ReadLine();
                isTryParseToInt = Int32.TryParse(userInput, out int result);

                switch (userInput)
                {
                    case sumCommand:
                        if (numberList.Count > 0)
                        {
                            foreach (int number in numberList)
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
                            numberList.Add(result);
                        }
                        else
                        {
                            Console.WriteLine(errorCommandMessage);
                        }
                        break;
                }
            }

            Console.WriteLine(continueMessage);
            Console.ReadKey();
            Console.Clear();
        }
    }
}