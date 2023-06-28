namespace Lesson_12
{
    internal class Program
    {
        static void Main()
        {
            const string CommandRubToUsd = "1";
            const string CommandRubToEur = "2";
            const string CommandUsdToRub = "3";
            const string CommandUsdToEur = "4";
            const string CommandEurToRub = "5";
            const string CommandEurToUsd = "6";
            const string CommandToExit = "7";

            float rub;
            float usd;
            float eur;
            int minRubCount = 300;
            int maxRubCount = 600;
            int minUsdCount = 20;
            int maxUsdCount = 50;
            int minEurCount = 10;
            int maxEurCount = 30;
            Random random = new Random();
            rub = random.Next(minRubCount, maxRubCount + 1);
            usd = random.Next(minUsdCount, maxUsdCount + 1);
            eur = random.Next(minEurCount, maxEurCount + 1);
            float rubToUsd = 65;
            float rubToEur = 75;
            float usdToRub = 0.02f;
            float usdToEur = 1.2f;
            float eurToRub = 0.01f;
            float eurToUsd = 0.9f;
            string userInputCommand;
            float userInputMoney;
            float convertCountMoney;
            bool isConvertMoney = true;
            string continueMessage = "\nНажмите любую клавишу чтобы продолжить...";
            string welcomeMessage = "Добро пожаловать в пункт обмена валют \"Мультиконвертер\"";
            string inncorrectInputCountMessage = "У вас недостаточно денег для выполнения этой операции";
            string userBalanceInfo = $"\nУ вас на балансе: {rub} рублей, {usd} долларов, {eur} евро";
            string programmMenu = $"\nМеню для обмена:" +
                $"\n{CommandRubToUsd} - для обмена рублей в доллары, по курсу {rubToUsd} за 1 доллар" +
                $"\n{CommandRubToEur} - для обмена рублей в евро, по курсу {rubToEur} за 1 евро" +
                $"\n{CommandUsdToRub} - для обмена долларов в рубли, по курсу {usdToRub} за 1 рубль" +
                $"\n{CommandUsdToEur} - для обмена долларов в евро, по курсу {usdToEur} за 1 евро" +
                $"\n{CommandEurToRub} - для обмена евро в рубли, по курсу {eurToRub} за 1 рубль" +
                $"\n{CommandEurToUsd} - для обмена евро в доллары, по курсу {eurToUsd} за 1 доллар" +
                $"\n{CommandToExit} - для выхода из пункта обмена";
            string requestCommandMessage = $"\nВведите команду: ";

            while (isConvertMoney)
            {
                Console.Clear();
                Console.WriteLine(welcomeMessage);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(userBalanceInfo);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(programmMenu);
                Console.Write(requestCommandMessage);
                userInputCommand = Console.ReadLine();

                switch (userInputCommand)
                {
                    case CommandToExit:
                        isConvertMoney = false;
                        Console.WriteLine("Вы ушли из пункта обмена");
                        break;

                    case CommandRubToUsd:
                        Console.Write("Сколько рублей вы хотите обменять на доллары? ");
                        userInputMoney = Convert.ToSingle(Console.ReadLine());

                        if (rub >= userInputMoney)
                        {
                            rub -= userInputMoney;
                            usd += userInputMoney / rubToUsd;
                            convertCountMoney = userInputMoney / rubToUsd;
                            Console.WriteLine($"Вы обменяли {userInputMoney} рублей на {convertCountMoney} долларов");
                        }
                        else
                        {
                            Console.WriteLine(inncorrectInputCountMessage);
                        }

                        Console.WriteLine(continueMessage);
                        Console.ReadLine();
                        break;

                    case CommandRubToEur:
                        Console.Write("Сколько рублей вы хотите обменять на евро? ");
                        userInputMoney = Convert.ToSingle(Console.ReadLine());

                        if (rub >= userInputMoney)
                        {
                            rub -= userInputMoney;
                            eur += userInputMoney / rubToEur;
                            convertCountMoney = userInputMoney / rubToEur;
                            Console.WriteLine($"Вы обменяли {userInputMoney} рублей на {convertCountMoney} евро");
                        }
                        else
                        {
                            Console.WriteLine(inncorrectInputCountMessage);
                        }

                        Console.WriteLine(continueMessage);
                        Console.ReadLine();
                        break;

                    case CommandUsdToRub:
                        Console.Write("Сколько долларов вы хотите обменять на рубли? ");
                        userInputMoney = Convert.ToSingle(Console.ReadLine());

                        if (usd >= userInputMoney)
                        {
                            usd -= userInputMoney;
                            rub += userInputMoney / usdToRub;
                            convertCountMoney = userInputMoney / usdToRub;
                            Console.WriteLine($"Вы обменяли {userInputMoney} долларов на {convertCountMoney} рублей");
                        }
                        else
                        {
                            Console.WriteLine(inncorrectInputCountMessage);
                        }

                        Console.WriteLine(continueMessage);
                        Console.ReadLine();
                        break;

                    case CommandUsdToEur:
                        Console.Write("Сколько долларов вы хотите обменять на евро? ");
                        userInputMoney = Convert.ToSingle(Console.ReadLine());

                        if (usd >= userInputMoney)
                        {
                            usd -= userInputMoney;
                            eur += userInputMoney / usdToEur;
                            convertCountMoney = userInputMoney / usdToEur;
                            Console.WriteLine($"Вы обменяли {userInputMoney} долларов на {convertCountMoney} евро");
                        }
                        else
                        {
                            Console.WriteLine(inncorrectInputCountMessage);
                        }

                        Console.WriteLine(continueMessage);
                        Console.ReadLine();
                        break;

                    case CommandEurToRub:
                        Console.Write("Сколько евро вы хотите обменять на рубли? ");
                        userInputMoney = Convert.ToSingle(Console.ReadLine());

                        if (eur >= userInputMoney)
                        {
                            eur -= userInputMoney;
                            rub += userInputMoney / eurToRub;
                            convertCountMoney = userInputMoney / eurToRub;
                            Console.WriteLine($"Вы обменяли {userInputMoney} евро на {convertCountMoney} рублей");
                        }
                        else
                        {
                            Console.WriteLine(inncorrectInputCountMessage);
                        }

                        Console.WriteLine(continueMessage);
                        Console.ReadLine();
                        break;

                    case CommandEurToUsd:
                        Console.Write("Сколько евро вы хотите обменять на доллары? ");
                        userInputMoney = Convert.ToSingle(Console.ReadLine());

                        if (eur >= userInputMoney)
                        {
                            eur -= userInputMoney;
                            usd += userInputMoney / eurToUsd;
                            convertCountMoney = userInputMoney / eurToRub;
                            Console.WriteLine($"Вы обменяли {userInputMoney} евро на {convertCountMoney} долларов");
                        }
                        else
                        {
                            Console.WriteLine(inncorrectInputCountMessage);
                        }

                        Console.WriteLine(continueMessage);
                        Console.ReadLine();
                        break;

                    default:
                        Console.Write($"Извините, ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($"\"{userInputCommand}\"");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write($" - такой команды не существует!");
                        Console.WriteLine(continueMessage);
                        Console.ReadLine();
                        break;
                }
            }

            Console.ReadKey();
        }
    }
}