namespace Lesson_57
{
    using static Display;
    using static UserInput;

    class Program
    {
        static void Main()
        {
            Console.Title = "Объединение войск";

            Barraks barraks = new Barraks();
            barraks.Work();

            Print($"\nРабота программы завершена!", ConsoleColor.Green);
        }
    }

    class Barraks
    {
        private List<Fighter> _fightersSquad1;
        private List<Fighter> _fightersSquad2;

        public Barraks()
        {
            _fightersSquad1 = new List<Fighter>()
            {
                new Fighter("Соколов"),
                new Fighter("Башлаев"),
                new Fighter("Иванов"),
                new Fighter("Петров"),
                new Fighter("Сидоров"),
                new Fighter("Коваленко"),
                new Fighter("Беляев"),
                new Fighter("Чижов"),
                new Fighter("Бирюков"),
                new Fighter("Чичков"),
            };

            _fightersSquad2 = new List<Fighter>()
            {
                new Fighter("Королев"),
                new Fighter("Бондаренко"),
                new Fighter("Митяев"),
                new Fighter("Назаров"),
                new Fighter("Пухалёв"),
                new Fighter("Гришин"),
                new Fighter("Рашкин"),
                new Fighter("Данилов"),
                new Fighter("Баландин"),
                new Fighter("Воронин")
            };
        }

        public void Work()
        {
            string symbolNameForTransfer = "Б";
            string messageFirstSquad = "Список бойцов из первого отряда:\n";
            string messageSecondSquad = "Список бойцов из второго отряда:\n";

            Print($"Списки бойцов >>> до перевода бойцов из отряда 1 в отряда 2.\n", ConsoleColor.Green);
            ShowFighters(messageFirstSquad, _fightersSquad1);
            PrintLine();

            ShowFighters(messageSecondSquad, _fightersSquad2);
            PrintLine();

            TransferFighters(symbolNameForTransfer);

            Print($"Списки бойцов >>> после перевода бойцов из отряда 1 в отряда 2.\n", ConsoleColor.Red);
            ShowFighters(messageFirstSquad, _fightersSquad1);
            PrintLine();

            ShowFighters(messageSecondSquad, _fightersSquad2);
            PrintLine();

            WaitToPressKey("\n");
        }

        private void TransferFighters(string symbol)
        {
            List<Fighter> tranferFighters = _fightersSquad1.Where(fighter => fighter.Name.StartsWith(symbol)).ToList();
            _fightersSquad1 = _fightersSquad1.Except(tranferFighters).ToList();
            _fightersSquad2 = _fightersSquad2.Union(tranferFighters).ToList();
        }

        private void ShowFighters(string message, List<Fighter> fighters)
        {
            Print(message);

            for (int i = 0; i < fighters.Count; i++)
            {
                Print($"{i + 1}. Боец: {fighters[i]}\n");
            }
        }
    }

    class Fighter
    {
        public Fighter(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }

    #region UserUtils

    static class UserInput
    {
        public static void WaitToPressKey(string message = "")
        {
            Print(message);
            Print($"Для продолжения нажмите любую клавишу...\n");
            Console.ReadKey();
        }
    }

    static class Display
    {
        public static void Print(string message, ConsoleColor consoleColor = ConsoleColor.White)
        {
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = consoleColor;
            Console.Write(message);
            Console.ForegroundColor = defaultColor;
        }

        public static void PrintLine(ConsoleColor color = ConsoleColor.White)
        {
            int symbolCount = Console.WindowWidth - 1;
            Print($"{new string('-', symbolCount)}\n", color);
        }
    }

    #endregion
}

//Объединение войск
//Есть 2 списка в солдатами.
//Всех бойцов из отряда 1,
//у которых фамилия начинается на букву Б,
//требуется перевести в отряд 2.