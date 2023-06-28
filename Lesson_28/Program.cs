namespace Lesson_28
{
    class Program
    {
        static void Main()
        {
            const string CreateCardMenu = "1";
            const string ShowCardMenu = "2";
            const string DeleteCardMenu = "3";
            const string FindBySernameMenu = "4";
            const string ExitMenu = "exit";

            string[] names = { "Василий Петрович Пупкин", "Геннадий Сергеевич Иванов", "Александр Викторович Махов", "Александр Петрович Пупкин" };
            string[] positions = { "Главный инженер", "Специалист по кадрам", "Диспетчер", "Механик" };
            bool isRunProgramm = true;
            string menu = $"Меню программы по учёту кадров организации:" +
                         $"\n{CreateCardMenu} - Добавить досье сотрудника" +
                         $"\n{ShowCardMenu} - Вывести все досье на сотрудников" +
                         $"\n{DeleteCardMenu} - Удалить досье сотрудника организации" +
                         $"\n{FindBySernameMenu} - Поиск досье по фамилии сотрудника" +
                         $"\n{ExitMenu} - Выйти из программы";
            string requestMessage = "\nВведите команду: ";
            string exitMessage = "\nРабота программы завершена\n";
            string errorMessageMenu = "Такой команды нет";
            string userInput;
            string continueMessage = "\nДля продолжения нажмите любую клавишу...";
            Console.Title = "Программа по учёту кадров организации";

            while (isRunProgramm == true)
            {
                Console.Clear();
                Console.WriteLine(menu);
                Console.Write(requestMessage);
                userInput = Console.ReadLine().ToLower();

                switch (userInput)
                {
                    case CreateCardMenu:
                        CreateCard(ref names, ref positions);
                        break;

                    case ShowCardMenu:
                        ShowAllCards(names, positions);
                        break;

                    case DeleteCardMenu:
                        DeleteCard(ref names, ref positions);
                        break;

                    case FindBySernameMenu:
                        SearchBySername(names, positions);
                        break;

                    case ExitMenu:
                        isRunProgramm = false;
                        break;

                    default:
                        PrintColorText($"\n\"{userInput}\" - {errorMessageMenu}\n", ConsoleColor.Red);
                        break;
                }

                PrintMessage(continueMessage);
            }

            PrintColorText(exitMessage, ConsoleColor.Green);
        }

        static void CreateCard(ref string[] names, ref string[] positions)
        {
            string userInput;
            Console.Clear();
            Console.WriteLine("Добавление нового досье на сотрудника");
            Console.Write("Введите ФИО сотрудника: ");
            userInput = Console.ReadLine();
            names = AddElementToArray(userInput, names);
            Console.Write($"Введите должность сотрудника {userInput}: ");
            userInput = Console.ReadLine();
            positions = AddElementToArray(userInput, positions);
            PrintColorText($"\nДосье успешно добавлено: {names[names.Length - 1]} - {positions[positions.Length - 1]}\n", ConsoleColor.Green);
        }

        static void ShowAllCards(string[] names, string[] positions)
        {
            Console.Clear();

            if (IsEmptyCard(names, positions) == false)
            {
                PrintColorText("Архив всех досье:\n", ConsoleColor.Green);

                for (int i = 0; i < names.Length; i++)
                {
                    Console.Write($"\n{i + 1}. {names[i]} - {positions[i]}");
                }

                Console.WriteLine();
            }
            else
            {
                PrintColorText($"Архив с досье пуст", ConsoleColor.Yellow);
            }
        }

        static void DeleteCard(ref string[] names, ref string[] positions)
        {
            string userInput;
            Console.Clear();
            ShowAllCards(names, positions);

            if (IsEmptyCard(names, positions) == true)
            {
                return;
            }

            Console.Write($"\nДля удаления досье введите порядковый номер сотрудника из списка: ");
            userInput = Console.ReadLine();

            if (Int32.TryParse(userInput, out int indexToRemove) == true)
            {
                --indexToRemove;

                if (indexToRemove >= 0 && indexToRemove < names.Length)
                {
                    PrintColorText($"\nДосье успешно удалено: {names[indexToRemove]} - {positions[indexToRemove]}\n", ConsoleColor.Green);
                    names = RemoveElementFromArray(indexToRemove, names);
                    positions = RemoveElementFromArray(indexToRemove, positions);
                }
                else
                {
                    PrintColorText($"\n\"{indexToRemove + 1}\" - такого индекса нет\n", ConsoleColor.Red);
                }
            }
            else
            {
                PrintColorText($"\n\"{userInput}\" - вы ввели не число\n", ConsoleColor.Red);
            }
        }

        static void SearchBySername(string[] names, string[] positions)
        {
            string userInput;
            Console.Clear();
            Console.Write("Введите фамилию сотрудника для поиска: ");
            userInput = Console.ReadLine();
            int findSurnameCount = 0;
            bool isFoundEmployee = false;
            char symbolToSplit = ' ';
            Console.WriteLine($"Совпадения по запросу \"{userInput}\":");

            for (int i = 0; i < names.Length; i++)
            {
                string[] surname = names[i].Split(symbolToSplit);

                for (int j = 0; j < surname.Length; j++)
                {
                    if (userInput.ToLower() == surname[j].ToLower())
                    {
                        Console.Write($"\n{findSurnameCount + 1} - ");
                        PrintColorText($"{names[i]}", ConsoleColor.Green);
                        Console.Write($" занимает должность: ");
                        PrintColorText($"{positions[i]}\n", ConsoleColor.Green);
                        findSurnameCount++;
                        isFoundEmployee = true;
                    }
                }
            }

            if (isFoundEmployee == false)
            {
                PrintColorText($"- Не найдены!\n", ConsoleColor.Red);
            }
        }

        static void PrintColorText(string text, ConsoleColor color)
        {
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = defaultColor;
        }

        static string[] AddElementToArray(string inputString, string[] sourceArray)
        {
            string[] tempArray = new string[sourceArray.Length + 1];

            for (int i = 0; i < sourceArray.Length; i++)
            {
                tempArray[i] = sourceArray[i];
            }

            tempArray[tempArray.Length - 1] = inputString;
            sourceArray = tempArray;
            return sourceArray;
        }

        static string[] RemoveElementFromArray(int indexToRemove, string[] sourceArray)
        {
            string[] tempArray = new string[sourceArray.Length - 1];

            for (int i = 0; i < indexToRemove; i++)
            {
                tempArray[i] = sourceArray[i];
            }

            for (int i = indexToRemove + 1; i < sourceArray.Length; i++)
            {
                tempArray[i - 1] = sourceArray[i];
            }

            sourceArray = tempArray;
            return sourceArray;
        }

        static bool IsEmptyCard(string[] names, string[] positions)
        {
            int emptyValue = 0;

            if (names.Length == emptyValue && positions.Length == emptyValue)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static void PrintMessage(string textMessage)
        {
            Console.WriteLine(textMessage);
            Console.ReadLine();
        }
    }
}