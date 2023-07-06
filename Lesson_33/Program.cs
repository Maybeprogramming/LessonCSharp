namespace Lesson_33
{
    //Создать программу, которая принимает от пользователя слово и
    //выводит его значение.
    //Если такого слова нет,
    //то следует вывести соответствующее сообщение.

    // Толковый словарь
    // Explanatory dictionary

    internal class Program
    {
        static void Main()
        {
            Console.Title = "Толковый словарь";
            Dictionary<string, string> dictionaryWords;
            dictionaryWords = InitialDictionary();
            StartWorkDictionary(dictionaryWords);
        }

        private static void StartWorkDictionary(Dictionary<string, string> dictionaryWords)
        {
            const string FindWordCommand = "1";
            const string PrintWorldsCommand = "2";
            const string PrintDictionaryCommand = "3";
            const string Exit = "4";

            ConsoleColor titleMenuColor = ConsoleColor.Green;
            ConsoleColor exitMessageColor = ConsoleColor.DarkYellow;
            ConsoleColor continueMessageColor = ConsoleColor.DarkYellow;
            ConsoleColor noSuchCommandMessageColor = ConsoleColor.Red;

            string titleMenu = "Меню \"толкового словаря\":";
            string menu = $"\n{FindWordCommand} - Ввести слово для вывода значения" +
                          $"\n{PrintWorldsCommand} - Вывести список слов из словаря" +
                          $"\n{PrintDictionaryCommand} - Вывести весь словарь" +
                          $"\n{Exit} - Выход из программы";
            string requestMessage = "\nВведите команду: ";
            string userInput;
            string exitMessage = "\nРабота программы завершена! Ждём вашего возвращения!";
            string continueMessage = "\nНажмите любую клавишу чтобы продолжить...";
            string noSuchCommandMessage = "Такой команды нет!";
            int delayExitMiliseconds = 1500;
            bool isRun = true;

            while (isRun)
            {
                Console.Clear();
                Print(titleMenu, titleMenuColor);
                Print(menu);
                Print(requestMessage);
                userInput = Console.ReadLine();
                Console.WriteLine();

                switch (userInput)
                {
                    case FindWordCommand:
                        TryFindWord(dictionaryWords, userInput);
                        break;

                    case PrintWorldsCommand:
                        PrintDictionary(dictionaryWords, userInput, PrintWorldsCommand, PrintDictionaryCommand);
                        break;

                    case PrintDictionaryCommand:
                        PrintDictionary(dictionaryWords, userInput, PrintWorldsCommand, PrintDictionaryCommand);
                        break;

                    case Exit:
                        isRun = false;
                        break;

                    default:
                        Print($"\"{userInput}\" - {noSuchCommandMessage}", noSuchCommandMessageColor);
                        break;
                }

                Print(continueMessage, continueMessageColor);
                Console.ReadLine();
            }

            Print(exitMessage, exitMessageColor);
            Task.Delay(delayExitMiliseconds).Wait();
        }

        static void TryFindWord(Dictionary<string, string> dictionary, string userInput)
        {
            string noSuchWordMessage = "Такого слова нет в данном словаре, попробуйте снова";
            string requestWordMessage = "\nВведите слово: ";
            ConsoleColor noSuchWordMessageColor = ConsoleColor.Red;
            bool isWordFound = false;

            while (isWordFound == false)
            {
                Print(requestWordMessage);
                userInput = Console.ReadLine();

                if (dictionary.ContainsKey(userInput))
                {
                    Print($"\n{userInput} - {dictionary[userInput]}\n");
                    isWordFound = true;
                }
                else
                {
                    Print($"\n\"{userInput}\" - {noSuchWordMessage}\n", noSuchWordMessageColor);
                }
            }
        }

        static void Print(string text, ConsoleColor consoleColor = ConsoleColor.White)
        {
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = consoleColor;
            Console.Write(text);
            Console.ForegroundColor = defaultColor;
        }

        static void PrintDictionary(Dictionary<string, string> dictionaryWords, string userInput, string PrintWorldsCommand, string PrintDictionaryCommand)
        {
            int indexElement = 1;            

            if (userInput == PrintWorldsCommand)
            {
                Print("Перечень слов в словаре: ");

                foreach (var word in dictionaryWords.Keys)
                {
                    Print($"\n{indexElement}. \"{word}\"");
                    indexElement++;
                }
            }
            else if (userInput == PrintDictionaryCommand)
            {
                Print("Перечень слов в словаре: ");

                foreach (var word in dictionaryWords)
                {
                    Print($"\n{indexElement}. \"{word.Key}\" - {word.Value}");
                    indexElement++;
                }
            }
        }

        static Dictionary<string, string> InitialDictionary()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>()
            {
                {"Программист", "Специалист по программированию" },
                {"Комрьютер", "Электронная вычислительная машина (ЭВМ). Персональный компьютер" },
                {"Слово", "Единица языка, служащая для наименования понятий, предметов, лиц, действий, состояний, признаков, связей, отношений, оценок" },
                {"Бит", "Единица измерения количества информации" },
                {"Кремний", "Химический элемент, тёмно-серые кристаллы с металлическим блеском, одна из главных составных частей горных пород" },
                {"Лазер", "Оптический квантовый генератор, устройство для получения мощных узконаправленных пучков света" },
                {"Логика", "Ход рассуждений, умозаключений. Разумность, внутренняя закономерность вещей, событий" },
                {"Алгоритм","Совокупность действий, правил для решения данной задачи" },
                {"Атом","Мельчайшая частица химического элемента, состоящая из ядра и электронов" },
                {"Сила","Величина, являющаяся мерой механического взаимодействия тел, вызывающего их ускорение или деформацию; характеристика интенсивности физических процессов" },
            };

            return dictionary;
        }
    }
}