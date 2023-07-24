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
            WorkDataBase();

            Console.ReadLine();
        }

        private static void WorkDataBase()
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
            bool isBan;
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
                        ShowAllPlayers(playersDataSheets.GetAllPlayers());
                        break;

                    case CommandAddPlayerToDataSheets:
                        AddPlayerInData(playersDataSheets);
                        break;

                    case CommandRemovePlayerInDataSheets:
                        RemovePlayerFromData(playersDataSheets);
                        break;

                    case CommandBanPlayerById:
                        SetBanStatusToPlayer(playersDataSheets, isBan = true);
                        break;

                    case CommandUnBanPlayerById:
                        SetBanStatusToPlayer(playersDataSheets, isBan = false);
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

        private static void Print(string message, ConsoleColor consoleColor = ConsoleColor.White)
        {
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = consoleColor;
            Console.Write(message);
            Console.ForegroundColor = defaultColor;
        }

        private static void ShowAllPlayers(List<Player> players)
        {
            Print("Список игроков:\n");

            foreach (var player in players)
            {
                Print($"#{player.Id} | ник: {player.NickName} \t | уровень: {player.Level} \t | статус:");

                if (player.Ban == true)
                {
                    Print($" {player.IsBanned}\n", ConsoleColor.DarkRed);
                }
                else
                {
                    Print($" {player.IsBanned}\n", ConsoleColor.DarkGreen);
                }
            }
        }

        private static void AddPlayerInData(DataSheets players)
        {
            Console.Clear();
            string userInputNickName;
            string userInputLevel;

            Print("Добавление нового игрока в базу\n");
            Print("Введите ник: ");
            userInputNickName = Console.ReadLine();
            Print("Введите уровень: ");
            userInputLevel = Console.ReadLine();

            if (Int32.TryParse(userInputLevel, out int result))
            {
                players.Add(userInputNickName, result);
                Print($"В базу успешно добавлен игрок: {userInputNickName} с уровнем: {result}", ConsoleColor.Green);
            }
            else
            {
                Print($"{userInputLevel} - Вы ввели не число!");
            }
        }

        private static void RemovePlayerFromData(DataSheets players)
        {
            Console.Clear();
            ShowAllPlayers(players.GetAllPlayers());

            string userInputId;
            Print("Введите Id игрока для удаления с базы: ");
            userInputId = Console.ReadLine();

            if (Int32.TryParse(userInputId, out int resultId))
            {
                if (players.TryRemove(resultId) == true)
                {
                    Print($"Игрок с ID: {resultId} - успешно удалён из базы", ConsoleColor.Yellow);
                }
                else
                {
                    Print($"{resultId} - игрока с таким ID нет в базе", ConsoleColor.DarkRed);
                }
            }
            else
            {
                Print($"{userInputId} - Вы ввели не число!");
            }
        }

        private static void SetBanStatusToPlayer(DataSheets players, bool isBan = true)
        {
            Console.Clear();
            ShowAllPlayers(players.GetAllPlayers());

            string userInputId;
            Print("Введите Id для бана игрока: ");
            userInputId = Console.ReadLine();

            if (Int32.TryParse(userInputId, out int resultId))
            {
                if (isBan == true && players.TrySetBanStatus(resultId, isBan) == true)
                {
                    Print($"Игрок с ID: {resultId} - успешно забанен", ConsoleColor.Yellow);
                }
                else if (isBan == false && players.TrySetBanStatus(resultId, isBan) == true)
                {
                    Print($"Игрок с ID: {resultId} - успешно разбанен", ConsoleColor.Yellow);
                }
                else
                {
                    Print($"{resultId} - игрока с таким ID нет в базе", ConsoleColor.DarkRed);
                }
            }
            else
            {
                Print($"{userInputId} - Вы ввели не число!");
            }
        }

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

    public void Add(string nickname, int level)
    {
        _players.Add(new Player(nickname, level));
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

    public List<Player> GetAllPlayers()
    {
        return new List<Player>(_players);
    }
}

class Player
{
    private static int _idCount = 0;

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
    public int Level { get; }
    public bool Ban { get;}
    public string IsBanned => Ban == true ? "забанен" : "не забанен";
}