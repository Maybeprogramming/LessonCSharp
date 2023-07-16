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
            PlayersDataSheets playersDataSheets = new PlayersDataSheets();

            playersDataSheets.ShowDataSheets();

            Console.ReadLine();
        }
    }

    class PlayersDataSheets
    {
        private List<Player> players = new List<Player>()
        {
            new Player (1, "BluBerry", 20, false),
            new Player (2, "Wiking", 30, false),
            new Player (3, "BunnyHope", 25, false),
            new Player (4, "Zirael", 45, false),
            new Player (5, "AprilOnil", 80, false)
        };

        public void AddPlayer()
        {
            players.Add(new Player(0, "DarkWarrior", 10, false));
        }

        public void RemovePlayer()
        {
            players.RemoveAt(0);
        }

        public void BanPlayerById()
        {
            players[0] = new Player(players[0].Id, players[0].NickName, players[0].Level, true);
        }

        public void UnbanPlayeById()
        {
            players[0] = new Player(players[0].Id, players[0].NickName, players[0].Level, false);
        }

        public void ShowDataSheets()
        {
            foreach (Player player in players)
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