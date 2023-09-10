using System.Globalization;
using System.Net.NetworkInformation;

namespace Lesson_44
{
    internal class Program
    {
        static void Main()
        {
            Console.Title = "Конфигуратор пассажирских поездов";

            Station station = new Station();
            station.Work();
        }
    }

    static class Display
    {
        public static void Print(string text)
        {
            Console.Write(text);
        }
    }

    class Station
    {
        public void Work()
        {
            const string SetupTrainCommand = "1";
            const string ExitCommand = "2";

            Random random = new Random();
            Board board = new Board();
            string setupTrainMenuText = "Конфигурировать пассажирский поезд";
            string exitMenuText = "Выйти из конфигуратора";
            string menu = $"{SetupTrainCommand} - {setupTrainMenuText}" +
                          $"\n{ExitCommand} - {exitMenuText}";
            bool isWorkStation = true;

            while (isWorkStation == true)
            {
                Console.Clear();
                board.ShowInfo();
                Console.WriteLine();

                Console.WriteLine(menu);

                switch (Console.ReadLine())
                {
                    case SetupTrainCommand:
                        SetupTrain();
                        break;
                    case ExitCommand:
                        isWorkStation = false;
                        break;
                    default:
                        Console.WriteLine("Такой команды нет, попробуйте снова");
                        break;
                }

                Console.WriteLine();
                Console.ReadLine();
            }

            Console.WriteLine("Программа завершена. Возвращайтесь снова.");
            Console.ReadLine();
        }

        private void SetupTrain()
        {
            Console.WriteLine("Блок с конфигурированием поезда");
        }
    }

    class Train
    {
        private List<Carriage> _carriages = new List<Carriage>();
        private Random _random;

        public Train(Random random, int passangersCount)
        {
            _random = random;
            AddCarriege();
            Configure(passangersCount);
        }

        public int Capacity { get; private set; }

        public int GetCarriageCount()
        {
            return _carriages.Count;
        }

        private void Configure(int passangersCount)
        {
            while (Capacity < passangersCount)
            {
                AddCarriege();
            }
        }

        private void AddCarriege()
        {
            Carriage carriage = new Carriage(_random);
            Capacity += carriage.Capacity;
            _carriages.Add(carriage);
        }
    }

    class Carriage
    {
        private int _minCapacity = 20;
        private int _maxCapacity = 50;

        public Carriage(Random random)
        {
            Capacity = random.Next(_minCapacity, _maxCapacity + 1);
        }

        public int Capacity { get; private set; }
    }

    class Route
    {
        public string From { get; private set; }
        public string To { get; private set; }

        public void AssignTo()
        {
            string stationDeparture = "";
            string stationArrival = "";
            string requestStationDepartureMessage = "Введите станцию отправления: ";
            string requestStationArrivalMesage = "Введите станцию прибытия: ";
        }
    }

    class TicketOffice
    {
        public TicketOffice(int passangersCount)
        {
            TiketsSoldCount = passangersCount;
        }

        public int TiketsSoldCount { get; private set; }
    }

    class Board
    {
        private List<String> _trainsInfo = new List<String>();

        public void AddInfo(Route route, Train train, TicketOffice ticketOffice)
        {
            _trainsInfo.Add($"Выезд из: {route.From} по направлению в: {route.To} (Продано билетов: {ticketOffice.TiketsSoldCount}");
        }

        public void ShowInfo()
        {
            int number = 0;

            Console.SetCursorPosition(0, 0);

            if (_trainsInfo.Count == 0)
            {
                Console.WriteLine($"Нет маршрутов для следования.");
                return;
            }

            foreach (String info in _trainsInfo)
            {
                Console.WriteLine($"{++number}. {info}");
            }

            Console.SetCursorPosition (0, _trainsInfo.Count + 1);
        }
    }
}

//Конфигуратор пассажирских поездов
//У вас есть программа,
//которая помогает пользователю составить план поезда.
//Есть 4 основных шага в создании плана:
//-Создать направление - создает направление для поезда(к примеру Бийск - Барнаул)
//-Продать билеты - вы получаете рандомное кол-во пассажиров, которые купили билеты на это направление
//-Сформировать поезд - вы создаете поезд и добавляете ему столько вагонов
//(вагоны могут быть разные по вместительности),
//сколько хватит для перевозки всех пассажиров.
//-Отправить поезд - вы отправляете поезд, после чего можете снова создать направление.
//В верхней части программы должна выводиться полная информация о текущем рейсе или его отсутствии.