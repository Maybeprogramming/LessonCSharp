namespace Lesson_29
{
    internal class Program
    {
        static void Main()
        {
            const string TakeDamageCommand = "1";
            const string GiveHealingCommand = "2";
            const string TakeManaCommand = "3";
            const string GiveManaCommand = "4";
            const string SetPositionBarsCommand = "5";
            const string ExitCommand = "6";

            int currentHealth = 4;
            int maxHealth = 10;
            string nameHealthBar = "HP";
            int leftPositionHealth = 0;
            int topPositionHealth = 0;
            int leftPositionMana = 0;
            int toptPositionMana = 1;
            int currentMana = 6;
            int maxMana = 10;
            string nameManaBar = "MP";
            int leftPositionMenu = 0;
            int topPositionMenu = 3;
            bool isRunProgramm = true;
            string userInput;
            string continueMessage = "\nДля продолжения нажмите любую клавишу...";
            string errorMessageMenu = "Такой команды нет";
            string menu = $"Выберите действие:" +
                          $"\n{TakeDamageCommand} - для нанесения урона" +
                          $"\n{GiveHealingCommand} - для восполнения здоровья" +
                          $"\n{TakeManaCommand} - для сжигания маны" +
                          $"\n{GiveManaCommand} - для восстановления маны" +
                          $"\n{SetPositionBarsCommand} - для установки позиции баров здоровья и маны" +
                          $"\n{ExitCommand} - для выхода из программы";
            string requestMessage = "\nВведите команду: ";
            string requestMessageDamage = "Введите сколько вы нанесёте урона";
            string requestMessageHealing = "Введите количество исцеляемого здоровья";
            string requestMessageBurnMana = "Введите сколько вы сожгёте маны";
            string requestMessageGiveMana = "Введите количество маны для восполнения";
            string lowHealthMessage = "У цели нет здоровья";
            string lowManaMessage = "У цели нет маны";
            string maxHealthMessage = "Цель не нуждается в лечении";
            string maxManaMessage = "Цель не нуждается в пополнении маны";
            string exitMessage = "Выход из программы";

            while (isRunProgramm == true)
            {
                Console.Clear();
                DrowBar(nameHealthBar, currentHealth, maxHealth, ConsoleColor.Red, topPositionHealth, leftPositionHealth);
                DrowBar(nameManaBar, currentMana, maxMana, ConsoleColor.Blue, toptPositionMana, leftPositionMana);
                Console.WriteLine("\n");
                DrowMenu(menu, leftPositionMenu, topPositionMenu);
                Console.Write(requestMessage);
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case TakeDamageCommand:
                        currentHealth -= TakeParametrValue(currentHealth, requestMessageDamage, lowHealthMessage, continueMessage);
                        break;

                    case GiveHealingCommand:
                        currentHealth += GiveParametrValue(currentHealth, maxHealth, requestMessageHealing, maxHealthMessage, continueMessage);
                        break;

                    case TakeManaCommand:
                        currentMana -= TakeParametrValue(currentMana, requestMessageBurnMana, lowManaMessage, continueMessage);
                        break;

                    case GiveManaCommand:
                        currentMana += GiveParametrValue(currentMana, maxMana, requestMessageGiveMana, maxManaMessage, continueMessage);
                        break;

                    case SetPositionBarsCommand:
                        SetPositionUserInterface(ref leftPositionHealth, ref topPositionHealth, ref leftPositionMana, ref toptPositionMana, ref leftPositionMenu, ref topPositionMenu);
                        break;

                    case ExitCommand:
                        isRunProgramm = false;
                        break;

                    default:
                        PrintText($"\n\"{userInput}\" - {errorMessageMenu}\n", ConsoleColor.Red);
                        break;
                }

                PrintText(continueMessage);
            }

            PrintText("\n" + exitMessage);
            Console.ReadLine();
        }

        static void DrowMenu(string menuText, int leftPositionMenu, int topPositionMenu)
        {
            Console.SetCursorPosition(leftPositionMenu, topPositionMenu);
            Console.WriteLine(menuText);
        }

        static void DrowBar(string nameBar, int value, int maxValue, ConsoleColor color, int topPosition, int leftPosition, char valueSymbol = '#', char remainingSymbol = '_')
        {
            ConsoleColor defaultColor = Console.BackgroundColor;
            string bar = "";
            int fullScalePercent = 100;
            int maxDrowSymbols = 10;
            int valueForDraw = value * fullScalePercent / maxValue / maxDrowSymbols;
            float remainderOfDivision = ((float)value * fullScalePercent) / maxValue - (valueForDraw * maxDrowSymbols);

            if (remainderOfDivision > 0)
            {
                valueForDraw++;
            }

            for (int i = 0; i < valueForDraw; i++)
            {
                bar += valueSymbol;
            }

            Console.SetCursorPosition(leftPosition, topPosition);
            Console.Write('[');
            Console.BackgroundColor = color;
            Console.Write(bar);
            Console.BackgroundColor = defaultColor;

            bar = "";

            for (int i = valueForDraw; i < maxDrowSymbols; i++)
            {
                bar += remainingSymbol;
            }

            Console.Write($"{bar}]");
            Console.Write($" {nameBar}");
        }

        static int TakeParametrValue(int currentStat, string requestText, string warningText, string continueText = "")
        {
            Console.Write($"{requestText}: ");
            int userInput = Convert.ToInt32(Console.ReadLine());
            int minTakeValue = 0;

            if (currentStat <= 0)
            {
                PrintText($"{warningText} ({currentStat})", ConsoleColor.Red);
                return minTakeValue;
            }

            if (currentStat >= userInput)
            {
                return userInput;
            }
            else
            {
                userInput = currentStat;
                return userInput;
            }
        }

        static int GiveParametrValue(int currentStat, int maxStat, string requestText, string warningText, string continueText = "")
        {
            Console.Write($"{requestText}: ");
            int userInput = Convert.ToInt32(Console.ReadLine());
            int remainingValue = maxStat - currentStat;
            int minGiveValue = 0;

            if (currentStat >= maxStat)
            {
                PrintText($"{warningText} ({currentStat})", ConsoleColor.Green);
                return minGiveValue;
            }

            if (userInput <= remainingValue)
            {
                return userInput;
            }
            else
            {
                userInput = remainingValue;
                return userInput;
            }
        }

        static void PrintText(string text)
        {
            Console.WriteLine(text);
            Console.ReadLine();
        }

        static void PrintText(string text, ConsoleColor color)
        {
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = defaultColor;
        }

        static void SetPositionUserInterface(ref int leftPositionHealth, ref int topPositionHealth, ref int leftPositionMana, ref int topPositionMana, ref int leftPositionMenu, ref int topPositionMenu)
        {
            int userInput;
            int marginTopPosition = 1;
            int topMenuSize = 15;
            int leftDefailtPosition = 0;
            int topDefailtSize = 0;
            Console.WriteLine("Изменение позиции бара здоровья и маны");
            Console.Write("Введите позицию по столбцу: ");
            userInput = Convert.ToInt32(Console.ReadLine());
            leftPositionHealth = userInput;
            Console.Write("Введите позицию по строке: ");
            userInput = Convert.ToInt32(Console.ReadLine());
            topPositionHealth = userInput;
            leftPositionMana = leftPositionHealth;
            topPositionMana = topPositionHealth + marginTopPosition;

            if (topPositionHealth >= topMenuSize)
            {
                leftPositionMenu = leftDefailtPosition;
                topPositionMenu = topDefailtSize;
            }
            else
            {
                leftPositionMenu = leftPositionMana + marginTopPosition;
                topPositionMenu = topPositionMana + marginTopPosition;
            }
        }
    }
}