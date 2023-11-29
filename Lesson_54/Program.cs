namespace Lesson_54
{
    using static Display;
    using static Randomaizer;

    class Program
    {
        static void Main()
        {
            Console.Title = "Топ игроков сервера";

            int playersCount = 15;
            LeaderBoard leaderBoard = new LeaderBoard(playersCount);
            leaderBoard.Work();

            Print($"\nРабота программы завершена.\n", ConsoleColor.Green);
        }
    }

    class LeaderBoard
    {
        private List<Player> _players;

        public LeaderBoard(int playersCount)
        {
            _players = FillPlayers(playersCount);
        }

        public void Work()
        {
            int topPlayersByLevelCount = 3;
            int topPlayersByStranght = 3;
            List<Player> topPlayersByLevel = new List<Player>(); ;
            List<Player> topPlayersByStrength = new List<Player>();

            topPlayersByLevel = _players.OrderByDescending(player => player.Level).Take(topPlayersByLevelCount).ToList();
            topPlayersByStrength = _players.OrderByDescending(player => player.Strength).Take(topPlayersByStranght).ToList();

            ShowPlayers("Список всех игроков на сервере:\n", _players);
            PrintLine();

            ShowPlayers($"Топ {topPlayersByLevelCount} игрока по уровню:\n", topPlayersByLevel);
            PrintLine();

            ShowPlayers($"Топ {topPlayersByStranght} игрока по силе:\n", topPlayersByStrength);
            PrintLine();
        }

        private void ShowPlayers(string message, List<Player> players)
        {
            Print(message, ConsoleColor.Green);

            for (int i = 0; i < players.Count; i++)
            {
                Print($"{i + 1}. {players[i]}\n");
            }
        }

        private List<Player> FillPlayers(int playersCount)
        {
            List<string> names = new List<string>()
            {
                "Limon4IK", "FireBlast", "MegaTrone", "AnGeL", "XrIsT", "Blade", "Vazelin", "RamPager", "VitaminC", "GreenLighter", "Vertuxan",
                "Prizma", "Shavel", "Jonatan", "FreeDomer", "EgZa", "Frezerovshik", "Kopatel", "Zoomer", "KillFisher", "Amazonka", "Jaguar4IK",
                "Vermishel", "Jumper", "TopWarrior", "Gorrilaz", "LordWaine", "Romenar", "Pivasik", "Wazgen", "LadaPriora", "JungleMen",
                "QualityMaster", "Yogist", "UmbaTumba", "Iskatel", "TurboMen", "Winstonist", "Kurilshik", "Driverist", "Pizama", "LuckyLord",
                "Azgardez", "TatuMaster", "HidroGEL", "Сrack", "ForceWood", "Virusolog", "Marmarist", "TeleGruver", "Mashinist", "Tracktorist",
                "Melonholick", "Alcoholic", "MetaFox", "GrenageForce", "Killer", "WhoAmI", "Anonymus", "TVMaker", "UnderReserver", "Frucktis"
            };

            List<Player> players = new List<Player>();

            int counter = 0;
            string postFixSymbol = "_";
            int minLevel = 0;
            int maxLevel = 121;
            int minStranght = 200;
            int maxStranght = 501;
            string name;
            int level;
            int stranght;

            for (int i = 0; i < playersCount; i++)
            {
                name = names[GenerateRandomNumber(0, names.Count)];
                Player faundPlayer = players.Find(p => p.Name.Equals(name) == true);

                if (players.Contains(faundPlayer) == true)
                {
                    name += postFixSymbol + ++counter;
                }

                level = GenerateRandomNumber(minLevel, maxLevel);
                stranght = GenerateRandomNumber(minStranght, maxStranght);
                Player player = new Player(name, level, stranght);

                players.Add(player);
            }

            return players;
        }
    }

    class Player
    {
        public Player(string name, int level, int strength)
        {
            Name = name;
            Level = level;
            Strength = strength;
        }

        public string Name { get; }
        public int Level { get; }
        public int Strength { get; }

        public override string ToString()
        {
            return $">> {Name} << Уровень: {Level}. Сила {Strength}";
        }
    }

    #region UserUtils

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

    static class Randomaizer
    {
        private static readonly Random s_random;

        static Randomaizer()
        {
            s_random = new();
        }

        public static int GenerateRandomNumber(int minValue, int maxValue)
        {
            return s_random.Next(minValue, maxValue);
        }
    }

    #endregion
}

//Топ игроков сервера
//У нас есть список всех игроков(минимум 10).
//У каждого игрока есть поля:
//имя,
//уровень,
//сила.
//Требуется написать запрос для определения
//топ 3 игроков по уровню
//и топ 3 игроков по силе,
//после чего вывести каждый топ.
//2 запроса получится.