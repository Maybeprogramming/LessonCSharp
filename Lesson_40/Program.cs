//Реализовать базу данных игроков и методы для работы с ней.
//У игрока может быть уникальный номер, ник, уровень, флаг – забанен ли он(флаг - bool).
//Реализовать возможность добавления игрока, бана игрока по уникальный номеру,
//разбана игрока по уникальный номеру и удаление игрока.
//Создание самой БД не требуется, задание выполняется инструментами,
//которые вы уже изучили в рамках курса.
//Но нужен класс, который содержит игроков и её можно назвать "База данных".

//База данных игроков

using System;
using System.Net.Http.Headers;
using System.Numerics;
using System.Reflection.Emit;

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
                          $"\n{CommandBanPlayerById} - Забанить игрока по ID" +
                          $"\n{CommandUnBanPlayerById} - Разбанить игкрока по ID" +
                          $"\n{CommandExitProgramm} - Выход из программы";
            string userInput;
            string requestMessage = $"\nВведите команду: ";
            bool isRun = true;
            DataSheets playersDataSheets = new();

            while (isRun)
            {
                Console.Clear();
                Console.Write(titleMenu);
                Console.Write(menu);
                Console.Write(requestMessage);

                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandShowPlayersData:
                        playersDataSheets.ShowDataSheets();
                        break;

                    case CommandAddPlayerToDataSheets:
                        TryAddPlayerToDataSheet(playersDataSheets);
                        break;

                    case CommandRemovePlayerInDataSheets:
                        TryRemovePlayerInDataSheet(playersDataSheets);
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

                PrintContinueMessage();
            }
        }

        private static void PrintContinueMessage()
        {
            string continueMessage = "\nНажмите любую клавишу чтобы продолжить...";

            Console.Write(continueMessage);
            Console.ReadLine();
        }

        private static void TryRemovePlayerInDataSheet(DataSheets playersDataSheets)
        {
            string userInputId;
            int playerId = -1;
            bool isConvertToInt = false;

            do
            {
                Console.Clear();
                Console.WriteLine("Удаление игрока по ID из базы:");
                Console.Write("Введите ID игрока для удаления: ");
                userInputId = Console.ReadLine();

                if (Int32.TryParse(userInputId, out int result) == true)
                {
                    playerId = result;
                    isConvertToInt = true;
                }
                else
                {
                    Console.Write($"{userInputId} - Ошибка! Вы ввели не число! Попробуйте ещё раз...");
                    PrintContinueMessage();
                }
            }
            while (isConvertToInt == false);

            if (playerId >= 0)
            {
                playersDataSheets.Remove(playerId);
                Console.WriteLine($"Игрок с ID: {playerId} - успешно удалён");
            }
            else
            {
                Console.WriteLine("Ошибка! Попытка удаления завершилась ошибкой, попробуйте снова...");
            }
        }

        private static void TryAddPlayerToDataSheet(DataSheets playersDataSheets)
        {
            string nickname;
            int level = -1;
            bool isConvertToInt = false;
            string userInputLevel;

            do
            {
                Console.Clear();
                Console.WriteLine("Добавление нового игрока в базу:");
                Console.Write("Введите никнейм: ");
                nickname = Console.ReadLine();
                Console.Write("Введите уровень: ");
                userInputLevel = Console.ReadLine();

                if (Int32.TryParse(userInputLevel, out int result) == true)
                {
                    level = result;
                    isConvertToInt = true;
                }
                else
                {
                    Console.Write($"{userInputLevel} - Ошибка! Вы ввели не число! Попробуйте ещё раз...");
                    PrintContinueMessage();
                }
            }
            while (isConvertToInt == false);

            if (level >= 0)
            {
                playersDataSheets.Add(nickname, level);
                Console.WriteLine($"Игрок: {nickname} - успешно добавлен в базу!");
            }
            else
            {
                Console.WriteLine("Ошибка! Попытка добавления завершилась ошибкой, попробуйте снова...");
            }
        }
    }

    class DataSheets
    {
        private List<Player> _players = new()
        {
            new Player (0,"BluBerry", 20),
            new Player (1,"Wiking", 30),
            new Player (2,"BunnyHope", 25),
            new Player (3,"Zirael", 45),
            new Player (4,"AprilOnil", 80)
        };
        private bool isBanPlayer;

        public DataSheets()
        {

        }

        public void Add(string nickname, int level)
        {
            int playerId = _players.Count + 1;
            _players.Add(new Player(playerId, nickname, level));
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
        private static int _idCount = 0;

        private int _id;
        private string _nickName;
        private int _level;
        private bool _isBanned;

        public Player(int id, string nickName, int level, bool isBanned = false)
        {
            ++_idCount;
            _id = _idCount;
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