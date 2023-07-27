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
                        dataSheets.ShowAllPlayers();
                        break;

                    case CommandAddPlayerToDataSheets:
                        dataSheets.TryAddPlayer();
                        break;

                    case CommandRemovePlayerInDataSheets:
                        dataSheets.TryRemovePlayer();
                        break;

                    case CommandBanPlayerById:
                        dataSheets.TryBanPlayer();
                        break;

                    case CommandUnBanPlayerById:
                        dataSheets.TryUnbanPlayer();
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

        public void ShowAllPlayers()
        {
            Console.WriteLine($"Список игроков:");

            foreach (Player player in _players)
            {
                Console.WriteLine(player.GetInfo());
            }
        }

        public void TryAddPlayer()
        {
            Console.Clear();
            Console.WriteLine("Добавление нового игрока в базу");

            Console.Write("Введите ник: ");
            string userInputNickName = Console.ReadLine();

            Console.Write("Введите уровень: ");
            int userInputLevel = ReadInt();

            if (userInputLevel <= 0)
                return;

            _players.Add(new Player(userInputNickName, userInputLevel));

            string infoMessage = $"В базу успешно добавлен игрок: {userInputNickName} с уровнем: {userInputLevel}";
            Console.WriteLine(infoMessage);
        }

        public void TryRemovePlayer()
        {
            Console.Clear();
            ShowAllPlayers();

            Console.Write("Введите Id игрока для удаления с базы: ");

            if (VerificationEnterId(out var playerId)) return;

            for (int i = 0; i < _players.Count; i++)
            {
                if (_players[i].Id.Equals(playerId))
                {
                    _players.Remove(_players[i]);
                    Console.WriteLine($"Игрок {_players[i].NickName} с ID: {playerId} - успешно удалён из базы");
                    return;
                }
            }
        }

        public void TryBanPlayer()
        {
            Console.Clear();
            ShowAllPlayers();

            Console.Write("Введите Id для бана игрока: ");

            if (VerificationEnterId(out var playerId)) return;

            for (int i = 0; i < _players.Count; i++)
            {
                if (_players[i].Id.Equals(playerId))
                {
                    _players[i].Ban();
                    Console.WriteLine($"Игрок {_players[i].NickName} с ID: {playerId} - успешно забанен");
                    return;
                }
            }
        }

        public void TryUnbanPlayer()
        {
            Console.Clear();
            ShowAllPlayers();

            Console.Write("Введите Id для разбана игрока: ");

            if (VerificationEnterId(out var playerId)) return;

            for (int i = 0; i < _players.Count; i++)
            {
                if (_players[i].Id.Equals(playerId))
                {
                    _players[i].Unban();
                    Console.WriteLine($"Игрок {_players[i].NickName} с ID: {playerId} - успешно разбанен");
                    return;
                }
            }
        }

        private bool VerificationEnterId(out int playerId)
        {
            playerId = ReadInt();

            if (playerId <= 0 || playerId >= _players.Count + 1)
            {
                Console.WriteLine($"{playerId} - Ошибка! Введены некорректные данные");
                return true;
            }

            return false;
        }

        private int ReadInt()
        {
            string userInput = Console.ReadLine();

            if (Int32.TryParse(userInput, out int result))
            {
                return result;
            }

            return result;
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
            IsBanned = isBan;
        }

        public int Id { get; }
        public string NickName { get; }

        public int Level
        {
            get => _level;
            private set => SetLevel(value);
        }

        private void SetLevel(int value)
        {
            if (value > 0)
                _level = value;
            else
                _level = 0;
        }

        public bool IsBanned { get; private set; }
        public string BanStatus => IsBanned == true ? "забанен" : "не забанен";

        public string GetInfo()
        {
            return $"#{Id} | ник: {NickName} \t | уровень: {Level} \t | статус: {BanStatus}";
        }

        public void Ban()
        {
            IsBanned = true;
        }

        public void Unban()
        {
            IsBanned = false;
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

//Доработать.
//+1 - public string IsBanned => IsBan - именуется у вас как булевая переменная, но таковой не является.
//+2 - Unban - одно слово.
//+3 - public bool IsBan -точнее как раз назвать isBanned.
//+4 - public int Level и в нем сильное разветвление get/set - лучше вынесите в метод, условно, SetLevel и в нем проверяйте.
//+5 - в ShowInfo можно обойтись без переменной.
//+6 - DataSheets - точнее назвать Database - база данных.
//+7 - Add/TryRemove и т.д. - не хватает конкретики в названиях методов. С кем работаете?
//+8 - TryRemove- вы сначала просите метод GetPlayerById вернуть вам игрока,
//+а потом снова перебираете всю коллекцию, и сравниваете. Зачем?
//+Так же и в остальных случаях.
//+9 - ViewData - по названию, класс должен только знакомиться с данными,
//но не что-то добавлять/удалять и т.д. в БД.
//Логичнее будет дополнить функционал методов DataSheets.
//Например, метод по добавлению игрока будет сразу запрашивать необходимые данные и сразу его добавлять.
//Так же и с удаление/забаниванием