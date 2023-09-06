﻿using System.Diagnostics.CodeAnalysis;

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

    }

    class Station
    {
        private Random _random = new Random();
        private Route _route = new Route("Самара","Москва");
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