﻿namespace Lesson_44
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
            string setupTrainMenuText = "Конфигурировать пассажирский поезд";
            string exitMenuText = "Выйти из конфигуратора";
            string menu = $"{SetupTrainCommand} - {setupTrainMenuText}" +
                          $"\n{ExitCommand} - {exitMenuText}";
            string stationDeparture = "";
            string stationArrival = "";
            string requestStationDepartureMessage = "Введите станцию отправления: ";
            string requestStationArrivalMesage = "Введите станцию прибытия: ";
            bool isWorkStation = true;

            while (isWorkStation == true)
            {
                Console.Clear();
                PrintStatusTrainRoute();
                Console.WriteLine();

                Console.WriteLine(menu);

                switch (Console.ReadLine())
                {
                    case SetupTrainCommand:
                        SetupTrainRoute();
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

        private void PrintStatusTrainRoute()
        {
            Console.WriteLine("Блок вывода статуса маршрута поезда");
        }

        private void SetupTrainRoute()
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
        }

        private void Configure(int passangersCount)
        {

        }

        private void AddCarriege()
        {
            _carriages.Add(new Carriage(_random));
        }

        private int GetTrainCapacity()
        {
            int trainCapacity = 0;

            foreach (Carriage carriage in _carriages)
            {
                trainCapacity += carriage.Capacity;
            }

            return trainCapacity;
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
        public Route(string from, string to)
        {
            From = from;
            To = to;
        }

        public string From { get; private set; }
        public string To { get; private set; }

        public string AssignTo()
        {
            return $"Выезд из: {From} по направлению в {To}";
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