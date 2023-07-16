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
    }

    class Player
    {
        private int _id;
        private int _level;
        private bool _isBanned;

        public Player(int id, int level, bool isBanned)
        {
            _id = id;
            _level = level;
            _isBanned = isBanned;
        }
    }
}