// Кадровый учет продвинутый
// HR accounting advanced

//В функциях вы выполняли задание "Кадровый учёт"
//Используя одну из изученных коллекций, вы смогли бы сильно себе упростить код выполненной программы, ведь у нас данные, это ФИО и позиция.
//Поиск в данном задании не нужен.
//1) добавить досье
//2) вывести все досье(в одну строку через “-” фио и должность)
//3) удалить досье
//4) выход

namespace Lesson_36
{
    class Program
    {
        static void Main()
        {
            Console.SetWindowSize(80, 30);
            Console.SetBufferSize(80, 30);

            Work();
        }

        private static void Work()
        {
            const string AddCardMenu = "1";
            const string ShowCardMenu = "2";
            const string DeleteCardMenu = "3";
            const string ExitMenu = "4";

            Dictionary<string, string> cardsEmployees = new() {
                {"Василий Петрович Пупкин","Главный инженер"},
                {"Геннадий Сергеевич Иванов","Специалист по кадрам"},
                {"Александр Викторович Махов", "Диспетчер" },
                {"Александр Петрович Пупкин", "Механик"},
                {"Воробьев Анатолий Сергеевич", "Техник по учёту"}};

            Console.Title = "Продвинутая программа по учёту кадров организации";
            bool isRunProgramm = true;
            string menu = $"Меню программы по учёту кадров организации:" +
                         $"\n{AddCardMenu} - Добавить досье сотрудника" +
                         $"\n{ShowCardMenu} - Вывести все досье на сотрудников" +
                         $"\n{DeleteCardMenu} - Удалить досье сотрудника организации" +
                         $"\n{ExitMenu} - Выйти из программы\n";
            string requestMessage = "\nВведите команду: ";
            string exitMessage = "\nРабота программы завершена\n";
            string errorMessageMenu = "Такой команды нет";
            string userInput;
            string continueMessage = "\nДля продолжения нажмите любую клавишу...";

            while (isRunProgramm == true)
            {
                Console.Clear();
                Console.Write(menu);
                Console.Write(requestMessage);
                userInput = Console.ReadLine().ToLower();

                switch (userInput)
                {
                    case AddCardMenu:
                        CreateCard(cardsEmployees, continueMessage);
                        break;

                    case ShowCardMenu:
                        ShowAllCards(cardsEmployees);
                        break;

                    case DeleteCardMenu:
                        DeleteCard(cardsEmployees);
                        break;

                    case ExitMenu:
                        isRunProgramm = false;
                        break;

                    default:
                        PrintText($"\n\"{userInput}\" - {errorMessageMenu}\n", ConsoleColor.Red);
                        break;
                }

                PrintText(continueMessage);
                Console.ReadLine();
            }

            PrintText(exitMessage, ConsoleColor.Green);
        }

        static void CreateCard(Dictionary<string, string> cardsEmployees, string continueMessage)
        {
            string inputNameKey;
            string inputRankValue;

            Console.Clear();
            Console.WriteLine("Добавление нового досье на сотрудника");

            Console.Write("Введите ФИО: ");
            inputNameKey = Console.ReadLine();

            if (cardsEmployees.ContainsKey(inputNameKey) == true)
            {
                PrintText($"{inputNameKey} - не допустимое значение, в базе уже есть такое досье");
                return;
            }

            Console.Write($"Введите должность {inputNameKey}: ");
            inputRankValue = Console.ReadLine();

            cardsEmployees.Add(inputNameKey,  inputRankValue);
            PrintText($"\nДосье успешно добавлено: {inputNameKey} - {inputRankValue}\n", ConsoleColor.Green);
        }

        static void ShowAllCards(Dictionary<string, string> cardsEmployeess)
        {
            Console.Clear();
            int indexPosition = 0;

            if (IsEmptyCard(cardsEmployeess) == false)
            {
                PrintText("Архив всех досье:\n", ConsoleColor.Green);

                foreach (var card in cardsEmployeess)
                {
                    indexPosition++;
                    Console.Write($"\n{indexPosition}. {card.Key} - {card.Value}");
                }

                Console.WriteLine();
            }
            else
            {
                PrintText($"Архив с досье пуст", ConsoleColor.Yellow);
            }
        }

        static void DeleteCard(Dictionary<string, string> cardsEmployeess)
        {
            string userInput;
            Console.Clear();
            ShowAllCards(cardsEmployeess);

            if (IsEmptyCard(cardsEmployeess) == true)
                return;

            Console.Write($"\nДля удаления введите ФИО сотрудника: ");
            userInput = Console.ReadLine();

            if (cardsEmployeess.ContainsKey(userInput) == true)
            {
                cardsEmployeess.Remove(userInput);
                PrintText($"\nДосье успешно удалено: {userInput}\n", ConsoleColor.Green);
            }
            else
            {
                PrintText($"\n\"{userInput}\" - такого досье нет\n", ConsoleColor.Red);
            }
        }

        private static void PrintText(string text, ConsoleColor color = ConsoleColor.White)
        {
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = defaultColor;
        }

        private static bool IsEmptyCard(Dictionary<string, string> cardsEmployeess)
        {
            if (cardsEmployeess.Count <= 0)
                return true;
            else
                return false;
        }
    }
}
