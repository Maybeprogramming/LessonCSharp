namespace Lesson_13
{
    internal class Program
    {
        static void Main()
        {
            const string CommandSetName = "1";
            const string CommandSetAge = "2";
            const string CommandSetPassword = "3";
            const string CommandSetBalance = "4";
            const string CommandChangeConsoleColor = "5";
            const string CommandPrintUserCard = "6";
            const string CommandToExit = "7";

            string inputCommand;
            string inputPassword;
            string inputName;
            int inputAge;
            float inputBonuses;
            string name = "Default";
            int age = 0;
            string password = "";
            int minLenthPassword = 4;
            float balanceBonuses = 0;
            bool isRunProgramm = true;
            string continueMessege = "\nНажмите клавишу чтобы продолжить...";
            string consoleMenu = $"Консольное меню учета карточки пользователя" +
                                  $"\nВам доступны следующие действия:" +
                                  $"\n{CommandSetName} - задать имя пользователя" +
                                  $"\n{CommandSetAge} - задать возраст пользователя" +
                                  $"\n{CommandSetPassword} - задать пароль пользователя" +
                                  $"\n{CommandSetBalance} - задать баланс бонусных баллов" +
                                  $"\n{CommandChangeConsoleColor} - сменить цвет фона консоли" +
                                  $"\n{CommandPrintUserCard} - вывести карточку пользователя на экран" +
                                  $"\n{CommandToExit} - для выхода из программы";
            string requestCommandMessage = $"\nВведите команду: ";

            while (isRunProgramm)
            {
                Console.Clear();
                Console.WriteLine(consoleMenu);
                Console.Write(requestCommandMessage);
                inputCommand = Console.ReadLine();

                switch (inputCommand)
                {
                    case CommandToExit:
                        isRunProgramm = false;
                        break;

                    case CommandSetName:
                        Console.Write("\nНапишите ваше имя: ");
                        inputName = Console.ReadLine();

                        if (inputName.Length > 0)
                        {
                            name = inputName;
                            Console.WriteLine($"Установлено имя: {name}");
                        }
                        else
                        {
                            Console.WriteLine("Длина имени должна быть больше 1 символа");
                        }

                        Console.WriteLine(continueMessege);
                        Console.ReadLine();
                        break;

                    case CommandSetAge:
                        Console.Write("\nУкажите ваш возраст: ");
                        inputAge = Convert.ToInt32(Console.ReadLine());

                        if (inputAge > 0)
                        {
                            age = inputAge;
                            Console.Write($"Вы указали свой возраст - {age} лет");
                        }
                        else
                        {
                            Console.Write($"Вы ввели: \"{inputAge}\", укажите возраст больше нуля");
                        }

                        Console.WriteLine(continueMessege);
                        Console.ReadLine();
                        break;

                    case CommandChangeConsoleColor:
                        const string MenuConsoleColorBlack = "1";
                        const string MenuConsoleColorGreen = "2";
                        const string MenuConsoleColorYellow = "3";

                        string inputColor;
                        string menuColor = $"\nВыберите цвет фона для консоли:" +
                                           $"\n{MenuConsoleColorBlack} - задать черный фон консоли (по умолчанию)" +
                                           $"\n{MenuConsoleColorGreen} - задать зеленый фон консоли" +
                                           $"\n{MenuConsoleColorYellow} - задать желтый фон консоли";

                        Console.WriteLine(menuColor);
                        Console.Write(requestCommandMessage);
                        inputColor = Console.ReadLine();

                        switch (inputColor)
                        {
                            case MenuConsoleColorBlack:
                                Console.BackgroundColor = ConsoleColor.Black;
                                Console.ForegroundColor = ConsoleColor.White;
                                break;

                            case MenuConsoleColorYellow:
                                Console.BackgroundColor = ConsoleColor.Yellow;
                                Console.ForegroundColor = ConsoleColor.Black;
                                break;

                            case MenuConsoleColorGreen:
                                Console.BackgroundColor = ConsoleColor.Green;
                                Console.ForegroundColor = ConsoleColor.Black;
                                break;

                            default:
                                Console.WriteLine($"{inputColor} - такого цвета нет");
                                break;
                        }
                        break;

                    case CommandSetPassword:
                        Console.Write($"\nДлина пароля должна составлять не менее {minLenthPassword} символов!");
                        Console.Write($"\nУстановите пароль: ");
                        inputPassword = Console.ReadLine();

                        if (inputPassword.Length >= minLenthPassword)
                        {
                            password = inputPassword;
                            Console.WriteLine($"Ваш пароль: {password} - запомните его!");
                        }
                        else
                        {
                            Console.WriteLine("Длина вашего пароля не соответствует условиям, попробуйте снова");
                        }

                        Console.WriteLine(continueMessege);
                        Console.ReadLine();
                        break;

                    case CommandPrintUserCard:
                        Console.Write("\nЧтобы просмотреть карточку введите пароль: ");
                        inputPassword = Console.ReadLine();

                        if (inputPassword == password && inputPassword.Length > 0)
                        {
                            Console.WriteLine("\nДанные пользователя");
                            Console.WriteLine($"Имя: {name}");
                            Console.WriteLine($"Возраст: {age}");
                            Console.WriteLine($"Баланс бонусов: {balanceBonuses}");
                        }
                        else
                        {
                            Console.WriteLine("Вы ввели неверный пароль");
                        }

                        Console.WriteLine(continueMessege);
                        Console.ReadLine();
                        break;

                    case CommandSetBalance:
                        Console.Write("\nУкажите ваш бонусный баланс: ");
                        inputBonuses = Convert.ToSingle(Console.ReadLine());

                        if (inputBonuses >= 0)
                        {
                            balanceBonuses = inputBonuses;
                            Console.Write($"Баланс бонусов составляет - {balanceBonuses}");
                        }
                        else
                        {
                            Console.Write($"Вы указали: \"{inputBonuses}\" - баланс бонусов не может быть отрицательным");
                        }

                        Console.WriteLine(continueMessege);
                        Console.ReadLine();
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(inputCommand);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(" - такой команды не существует, попробуйте ввести другую команду...");
                        Console.Write(continueMessege);
                        Console.ReadLine();
                        break;
                }
            }

            Console.WriteLine("\nРабота программы завершена.");
            Console.ReadKey();
        }
    }
}