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

    class Station
    {
        public void Work()
        {
            const string SetupTrainCommand = "1";
            const string ExitCommand = "2";

            Board board = new Board();


            string titleMenu = "Меню:";
            string setupTrainMenuText = "Конфигурировать пассажирский поезд";
            string exitMenuText = "Выйти из конфигуратора";
            string menu = $"{SetupTrainCommand} - {setupTrainMenuText}" +
                          $"\n{ExitCommand} - {exitMenuText}";
            string requestMessage = "Введите команду: ";
            bool isWorkStation = true;

            while (isWorkStation == true)
            {
                Console.Clear();
                board.ShowInfo();

                Console.WriteLine(titleMenu);
                Console.WriteLine(menu);
                Console.Write(requestMessage);

                switch (Console.ReadLine())
                {
                    case SetupTrainCommand:
                        SetupTrain(board);
                        break;
                    case ExitCommand:
                        isWorkStation = false;
                        break;
                    default:
                        Console.WriteLine("Такой команды нет, попробуйте снова");
                        break;
                }

                Console.WriteLine("\nНажмите любую клавишу чтобы продолжить...");
                Console.ReadLine();
            }

            Console.WriteLine("Программа завершена. Возвращайтесь снова.");
            Console.ReadLine();
        }

        private void SetupTrain(Board board)
        {
            Random random = new Random();
            TicketOffice ticketOffice = new TicketOffice(random);
            Train train = new Train(random);
            Route route = new Route();

            Console.Clear();
            Console.WriteLine("Начинаем конфигурировать поезд и маршрут следования!");

            route.AssignTo();
            ticketOffice.SellTickets();
            train.Configure(ticketOffice.TiketsSoldCount);
            board.AddInfo(route, ticketOffice);

            Console.WriteLine($"\nКонфигурирование завершено! Создан маршрут: \n" +
                              $"{route.ShowInfo()}\n" +
                              $"Продано билетов: {ticketOffice.TiketsSoldCount}\n" +
                              $"Состав поезда насчитывает {train.GetCarriageCount()} вагонов.");
            Console.WriteLine("\nПоезд отправлен!");
        }
    }

    class Train
    {
        private List<Carriage> _carriages = new List<Carriage>();
        private Random _random;

        public Train(Random random)
        {
            _random = random;
            AddCarriege();
        }

        public int Capacity { get; private set; }

        public int GetCarriageCount()
        {
            return _carriages.Count;
        }

        public void Configure(int tiketsSoldCount)
        {
            while (Capacity < tiketsSoldCount)
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
            Console.Write("Введите станцию отправления: ");
            From = Console.ReadLine();

            Console.Write("Введите станцию прибытия: ");
            To = Console.ReadLine();
        }

        public string ShowInfo()
        {
            return $"Станция отправления: {From}, станция прибытия: {To}";
        }
    }

    class TicketOffice
    {
        private Random _random;
        int minPassangers = 10;
        int maxPassangers = 1000;

        public TicketOffice(Random random)
        {
            _random = random;
        }

        public int TiketsSoldCount { get; private set; }

        public void SellTickets()
        {
            TiketsSoldCount = _random.Next(minPassangers, maxPassangers + 1);
            Console.Write($"Количество пассажиров: {TiketsSoldCount}");
        }
    }

    class Board
    {
        private List<String> _trainsInfo = new List<String>();

        public void AddInfo(Route route, TicketOffice ticketOffice)
        {
            _trainsInfo.Add($"Выезд из: {route.From} по направлению в: {route.To} (Продано: {ticketOffice.TiketsSoldCount} билетов)");
        }

        public void ShowInfo()
        {
            int number = 0;
            int leftCursorPosition = 0;
            int topCursorPosition = 0;

            Console.SetCursorPosition(leftCursorPosition, topCursorPosition);

            if (_trainsInfo.Count == 0)
            {
                Console.WriteLine($"Нет маршрутов для следования.\n");
                return;
            }

            topCursorPosition = 1;
            Console.WriteLine("Список маршрутов следования:");

            foreach (String info in _trainsInfo)
            {
                Console.WriteLine($"{++number}. {info}");
            }

            Console.WriteLine();

            topCursorPosition += _trainsInfo.Count + 1;
            Console.SetCursorPosition(leftCursorPosition, topCursorPosition);
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