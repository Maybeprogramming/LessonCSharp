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

        }
    }

    class PlayerData
    {
        private List<Player> players = new List<Player>();

        public void AddPlayer()
        {
            players.Add(new Player(0,"DarkWarrior", 10, false));
        }

        public void RemovePlayer()
        {
            players.RemoveAt(0);
        }

        public void banPlayerById()
        {
            players[0] = new Player(players[0].Id, players[0].NickName, players[0].Level, true);
        }

        public void unbanPlayeById()
        {
            players[0] = new Player(players[0].Id, players[0].NickName, players[0].Level, false);
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
    }
}