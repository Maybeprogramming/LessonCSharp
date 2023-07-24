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
            DataSheets playerDataSheets = new();
            ViewData view = new();
            view.Work(playerDataSheets);

            Console.ReadLine();
        }
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

    private string _titleMenu = $"Доступные команды:";
    private string _menu = $"\n{CommandShowPlayersData} - Вывести информацию обо всех игроках" +
                         $"\n{CommandAddPlayerToDataSheets} - Добавить нового игрока в базу" +
                         $"\n{CommandRemovePlayerInDataSheets} - Удалить игрока из базы" +
                         $"\n{CommandBanPlayerById} - Забанить игрока по ID" +
                         $"\n{CommandUnBanPlayerById} - Разбанить игкрока по ID" +
                         $"\n{CommandExitProgramm} - Выход из программы";
    private string _userInput;
    private string _requestMessage = $"\nВведите команду: ";
    private bool _isRun = true;
    private bool _isBan;

    public void Work(DataSheets players)
    {
        DataSheets playersDataSheets = players;

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
                    ShowAllPlayers(playersDataSheets.GetAllPlayers());
                    break;

                case CommandAddPlayerToDataSheets:
                    AddPlayerInData(playersDataSheets);
                    break;

                case CommandRemovePlayerInDataSheets:
                    RemovePlayerFromData(playersDataSheets);
                    break;

                case CommandBanPlayerById:
                    SetBanStatusToPlayer(playersDataSheets, _isBan = true);
                    break;

                case CommandUnBanPlayerById:
                    SetBanStatusToPlayer(playersDataSheets, _isBan = false);
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

    private void ShowAllPlayers(List<Player> players)
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

    private void AddPlayerInData(DataSheets players)
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

    private void RemovePlayerFromData(DataSheets players)
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

    private void SetBanStatusToPlayer(DataSheets players, bool isBan = true)
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
    public bool Ban { get; }
    public string IsBanned => Ban == true ? "забанен" : "не забанен";
}