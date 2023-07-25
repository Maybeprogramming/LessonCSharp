using System.Numerics;

namespace Lesson_40
{
    class Program
    {
        static void Main()
        {
            Console.Title = "База данных игроков";

            DataSheets playerDataSheets = new();
            ViewData view = new();
            view.Work(playerDataSheets);

            Console.ReadLine();
        }
    }

    class ViewData
    {
        private const string CommandShowPlayersData = "1";
        private const string CommandAddPlayerToDataSheets = "2";
        private const string CommandRemovePlayerInDataSheets = "3";
        private const string CommandBanPlayerById = "4";
        private const string CommandUnBanPlayerById = "5";
        private const string CommandExitProgramm = "6";

        private string _titleMenu = "Доступные команды:";
        private string _menu = $"\n{CommandShowPlayersData} - Вывести информацию обо всех игроках" +
                             $"\n{CommandAddPlayerToDataSheets} - Добавить нового игрока в базу" +
                             $"\n{CommandRemovePlayerInDataSheets} - Удалить игрока из базы" +
                             $"\n{CommandBanPlayerById} - Забанить игрока по ID" +
                             $"\n{CommandUnBanPlayerById} - Разбанить игкрока по ID" +
                             $"\n{CommandExitProgramm} - Выход из программы";
        private string _userInput;
        private string _requestMessage = "\nВведите команду: ";
        private bool _isRun = true;
        private bool _isBan;

        public void Work(DataSheets players)
        {
            DataSheets dataSheets = players;

            while (_isRun)
            {
                Console.Clear();
                Console.Write(_titleMenu);
                Console.Write(_menu);
                Console.Write(_requestMessage);

                _userInput = Console.ReadLine();

                switch (_userInput)
                {
                    case CommandShowPlayersData:
                        Print(dataSheets.ShowAllPlayers());
                        break;

                    case CommandAddPlayerToDataSheets:
                        TryAddPlayerInData(dataSheets);
                        break;

                    case CommandRemovePlayerInDataSheets:
                        TryRemovePlayerFromData(dataSheets);
                        break;

                    case CommandBanPlayerById:
                        TrySetBanToPlayer(dataSheets, _isBan = true);
                        break;

                    case CommandUnBanPlayerById:
                        TrySetBanToPlayer(dataSheets, _isBan = false);
                        break;

                    case CommandExitProgramm:
                        _isRun = false;
                        break;

                    default:
                        Print($"{_userInput} - такой команды нет! Повторите ещё раз.", ConsoleColor.DarkRed);
                        break;
                }

                PrintContinueMessage();
            }

            Print("Работа программы завершена!", ConsoleColor.DarkGreen);
        }

        private void PrintContinueMessage()
        {
            Print("\nНажмите любую клавишу чтобы продолжить...", ConsoleColor.DarkGreen);
            Console.ReadLine();
        }

        private void Print(string message, ConsoleColor consoleColor = ConsoleColor.White)
        {
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = consoleColor;
            Console.Write(message);
            Console.ForegroundColor = defaultColor;
        }

        private void TryAddPlayerInData(DataSheets players)
        {
            Console.Clear();
            Print("Добавление нового игрока в базу\n");

            Print("Введите ник: ");
            string userInputNickName = Console.ReadLine();

            Print("Введите уровень: ");
            int userInputLevel = ReadInt();

            if (userInputLevel <= 0)
                return;

            string SuccessAddPlayerMessage = players.Add(userInputNickName, userInputLevel);

            Print(SuccessAddPlayerMessage, ConsoleColor.Green);
        }

        private void TryRemovePlayerFromData(DataSheets dataSheets)
        {
            Console.Clear();
            Print(dataSheets.ShowAllPlayers());

            Print("Введите Id игрока для удаления с базы: ");
            int playerId = ReadInt();

            if (playerId <= 0)
                return;

            if (dataSheets.TryRemove(playerId))
            {
                Print($"Игрок с ID: {playerId} - успешно удалён из базы", ConsoleColor.Yellow);
            }
            else
            {
                Print($"{playerId} - игрока с таким ID нет в базе", ConsoleColor.DarkRed);
            }
        }

        private void TrySetBanToPlayer(DataSheets dataSheets, bool isBan = true)
        {
            Console.Clear();
            Print(dataSheets.ShowAllPlayers());

            Print("Введите Id для изменения статуса бана игрока: ");
            int playerId = ReadInt();

            if (isBan && dataSheets.TrySetBanStatus(playerId, isBan))
            {
                Print($"Игрок с ID: {playerId} - успешно забанен", ConsoleColor.Yellow);
            }
            else if (isBan == false && dataSheets.TrySetBanStatus(playerId, isBan))
            {
                Print($"Игрок с ID: {playerId} - успешно разбанен", ConsoleColor.Yellow);
            }
            else if (playerId != 0)
            {
                Print($"{playerId} - игрока с таким ID нет в базе", ConsoleColor.DarkRed);
            }

        }

        private int ReadInt()
        {
            string userInput = Console.ReadLine();

            if (Int32.TryParse(userInput, out int result))
            {
                return result;
            }

            if (result <= 0)
            {
                Print($"{userInput} - Ошибка! Введенные данные должны быть в положительном диапазоне и больше 0");
                return result;
            }

            Print($"{userInput} - Ошибка! Вы совершили неверный ввод данных!");
            return result;
        }
    }

    class DataSheets
    {
        private List<Player> _players = new()
        {
            new Player ("BluBerry", 20),
            new Player ("Wiking", 30),
            new Player ("BunnyHope", 25),
            new Player ("Zirael", 45),
            new Player ("AprilOnil", 80)
        };

        public string ShowAllPlayers()
        {
            string playersInfo = "";

            foreach (Player player in _players)
            {
                playersInfo += player.ShowInfo() + "\n";
            }

            return playersInfo;
        }


        public string Add(string nickname, int level)
        {
            _players.Add(new Player(nickname, level));
            return $"В базу успешно добавлен игрок: {nickname} с уровнем: {level}";
        }

        public bool TryRemove(int id)
        {
            Player player = GetPlayerById(id);

            if (_players.Contains(player))
            {
                _players.Remove(player);
                return true;
            }

            return false;
        }

        public bool TrySetBanStatus(int id, bool isBan)
        {
            Player playerToBan = GetPlayerById(id);

            for (int i = 0; i < _players.Count; i++)
            {
                if (_players[i].Equals(playerToBan))
                {
                    _players[i] = new Player(playerToBan.Id, playerToBan.NickName, playerToBan.Level, isBan);
                    return true;
                }
            }

            return false;
        }

        private Player GetPlayerById(int id)
        {
            foreach (Player player in _players)
            {
                if (player.Id == id)
                {
                    return player;
                }
            }

            return null;
        }
    }

    class Player
    {
        private static int _idCount = 0;
        private int _level;

        public Player(string nickName, int level, bool isBan = false)
        {
            ++_idCount;
            Id = _idCount;
            Level = level;
            NickName = nickName;
            Ban = isBan;
        }

        public Player(int id, string nickName, int level, bool isBan)
        {
            Id = id;
            Level = level;
            NickName = nickName;
            Ban = isBan;
        }

        public int Id { get; }
        public string NickName { get; }

        public int Level
        {
            get
            {
                return _level;
            }
            private set
            {
                if (value > 0)
                    _level = value;
                else
                    _level = 0;
            }
        }

        public bool Ban { get; }
        public string IsBanned => Ban == true ? "забанен" : "не забанен";

        public string ShowInfo()
        {
            return $"#{Id} | ник: {NickName} \t | уровень: {Level} \t | статус: {IsBanned}";
        }
    }
}

//Реализовать базу данных игроков и методы для работы с ней.
//У игрока может быть уникальный номер, ник, уровень, флаг – забанен ли он(флаг - bool).
//Реализовать возможность добавления игрока, бана игрока по уникальный номеру,
//разбана игрока по уникальный номеру и удаление игрока.
//Создание самой БД не требуется, задание выполняется инструментами,
//которые вы уже изучили в рамках курса.
//Но нужен класс, который содержит игроков и её можно назвать "База данных".

//База данных игроков