//Реализовать базу данных игроков и методы для работы с ней.
//У игрока может быть уникальный номер, ник, уровень, флаг – забанен ли он(флаг - bool).
//Реализовать возможность добавления игрока, бана игрока по уникальный номеру,
//разбана игрока по уникальный номеру и удаление игрока.
//Создание самой БД не требуется, задание выполняется инструментами,
//которые вы уже изучили в рамках курса.
//Но нужен класс, который содержит игроков и её можно назвать "База данных".

//База данных игроков

using System;
using System.Numerics;

namespace Lesson_40
{
    class Program
    {
        static void Main()
        {
            WorkDataBasePlayers();

            Console.ReadLine();
        }

        private static void WorkDataBasePlayers()
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
            DataSheets playersDataSheets = new();

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
                        playersDataSheets.Add(1, "BluBerry", 20, false);
                        break;

                    case CommandRemovePlayerInDataSheets:
                        playersDataSheets.Remove(3);
                        break;

                    case CommandBanPlayerById:
                        playersDataSheets.BanById(1);
                        break;

                    case CommandUnBanPlayerById:
                        playersDataSheets.UnbanById(1);
                        break;

                    case CommandExitProgramm:
                        isRun = false;
                        break;

                    default:
                        Console.WriteLine($"{userInput} - такой команды нет! Повторите ещё раз.");
                        break;
                }

                Console.ReadLine();
            }
        }
    }

    class DataSheets
    {
        private List<Player> _players = new()
        {
            new Player (1, "BluBerry", 20, false),
            new Player (2, "Wiking", 30, false),
            new Player (3, "BunnyHope", 25, false),
            new Player (4, "Zirael", 45, false),
            new Player (5, "AprilOnil", 80, false)
        };
        private bool isBanPlayer;

        public void Add(int id, string nickname, int level, bool isBanned)
        {
            _players.Add(new Player(id, nickname, level, isBanned));
        }

        public void Remove(int id)
        {
            Player player = FindPlayerById(id, out int index);

            if (player != null)
            {
                _players?.Remove(player);
            }
        }

        public void BanById(int id)
        {
            isBanPlayer = true;
            TryPlayerChangeBanStatus(id, isBanPlayer);
        }

        public void UnbanById(int id)
        {
            isBanPlayer = false;
            TryPlayerChangeBanStatus(id, isBanPlayer);
        }

        private void TryPlayerChangeBanStatus(int id, bool isBan)
        {
            Player player = FindPlayerById(id, out int index);

            if (index >= 0)
            {
                _players[index] = new Player(_players[index].Id, _players[index].NickName, _players[index].Level, isBan);
            }
        }

        public void ShowDataSheets()
        {
            foreach (Player player in _players)
            {
                Console.Write($"#ID: {player.Id}. NickName: {player.NickName}. Level: {player.Level}. Статус игрока: {player.StatusBan()}\n");
            }
        }

        public List<Player> GetAllPlayers()
        {
            List<Player> players = new List<Player>(_players);
            return players;
        }

        private Player FindPlayerById(int id, out int index)
        {
            Player? PlayerById = null;
            index = -1;

            foreach (var player in _players)
            {
                if (player.Id == id)
                {
                    PlayerById = player;
                    index = _players.IndexOf(player);
                }
            }

            return PlayerById;
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