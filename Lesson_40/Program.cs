//Реализовать базу данных игроков и методы для работы с ней.
//У игрока может быть уникальный номер, ник, уровень, флаг – забанен ли он(флаг - bool).
//Реализовать возможность добавления игрока, бана игрока по уникальный номеру,
//разбана игрока по уникальный номеру и удаление игрока.
//Создание самой БД не требуется, задание выполняется инструментами,
//которые вы уже изучили в рамках курса.
//Но нужен класс, который содержит игроков и её можно назвать "База данных".

//База данных игроков

namespace Lesson_40
{
    class Program
    {
        static void Main()
        {
            WorkDataBasePlayersProgramm();

            Console.ReadLine();
        }

        private static void WorkDataBasePlayersProgramm()
        {
            const string CommandShowPlayersData = "1";
            const string CommandAddPlayerToDataSheets = "2";
            const string CommandRemovePlayerInDataSheets = "3";
            const string CommandBanPlayerById = "4";
            const string CommandUnBanPlayerById = "5";
            const string CommandExitProgramm = "6";

            string titleMenu = $"Доступные команды:";
            string menu = $"\n{CommandShowPlayersData} - Вывести информацию обо всех игроках" +
                          $"\n{CommandAddPlayerToDataSheets} - Добавить нового игрока в базу" +
                          $"\n{CommandRemovePlayerInDataSheets} - Удалить игрока из базы" +
                          $"\n{CommandBanPlayerById} - Забанить игрока по уникальному ID" +
                          $"\n{CommandUnBanPlayerById} - Разбанить игкрока по уникальному ID" +
                          $"\n{CommandExitProgramm} - Выход из программы";
            string userInput;
            bool isRun = true;
            PlayersDataSheets playersDataSheets = new PlayersDataSheets();

            while (isRun)
            {
                Console.Clear();
                Console.Write(titleMenu);
                Console.Write(menu);

                Console.WriteLine();
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandShowPlayersData:
                        playersDataSheets.ShowDataSheets();
                        break;

                    case CommandAddPlayerToDataSheets:
                        playersDataSheets.AddPlayer();
                        break;

                    case CommandRemovePlayerInDataSheets:
                        playersDataSheets.RemovePlayer();
                        break;

                    case CommandBanPlayerById:
                        playersDataSheets.BanPlayerById();
                        break;

                    case CommandUnBanPlayerById:
                        playersDataSheets.UnbanPlayeById();
                        break;

                    case CommandExitProgramm:
                        isRun = false;
                        break;
                }

                Console.ReadLine();
            }
        }
    }

    class PlayersDataSheets
    {
        private List<Player> _players = new List<Player>()
        {
            new Player (1, "BluBerry", 20, false),
            new Player (2, "Wiking", 30, false),
            new Player (3, "BunnyHope", 25, false),
            new Player (4, "Zirael", 45, false),
            new Player (5, "AprilOnil", 80, false)
        };

        public void AddPlayer()
        {
            Player newPlayer = new Player(0, "DarkWarrior", 10, true);

            _players.Add(newPlayer);
        }

        public void RemovePlayer()
        {
            _players.RemoveAt(0);
        }

        public void BanPlayerById()
        {
            _players[0] = new Player(_players[0].Id, _players[0].NickName, _players[0].Level, true);
        }

        public void UnbanPlayeById()
        {
            _players[0] = new Player(_players[0].Id, _players[0].NickName, _players[0].Level, false);
        }

        public void ShowDataSheets()
        {
            foreach (Player player in _players)
            {
                Console.Write($"#ID: {player.Id}. NickName: {player.NickName}. Level: {player.Level}. Статус игрока: {player.StatusBan()}\n");
            }
        }
    }

    class Player
    {
        private int _id;
        private string _nickName;
        private int _level;
        private bool _isBanned;

        public Player(int id, string nickName, int level, bool isBanned)
        {
            _id = id;
            _level = level;
            _isBanned = isBanned;
            _nickName = nickName;
        }

        public int Id => _id;
        public string NickName => _nickName;
        public int Level => _level;
        public bool IsBanned => _isBanned;

        public string StatusBan()
        {
            return _isBanned == true ? "забанен" : "не забанен";
        }
    }
}