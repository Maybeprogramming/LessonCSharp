namespace Lesson_44
{
    internal class Program
    {
        static void Main()
        {

        }
    }

    static class Display
    {
        public static void Print(string text)
        {

        }
    }

    class Station
    {


        public void Work()
        {
            Random _random = new Random();

            string _stationDeparture;
            string _stationArrival;
            string _requestStationDepartureMessage = "Введите станцию отправления: ";
            string _requestStationArrivalMesage = "Введите станцию прибытия: ";
            Route _route = new Route("Самара", "Москва");
        }
    }

    class Train
    {

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