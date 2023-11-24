namespace Lesson_51
{
    class Program
    {
        static void Main()
        {
        }
    }

    class Criminal
    {
        public Criminal(string name, bool isImprisoned, int height, int weight, string nationaly)
        {
            Name = name;
            IsImprisoned = isImprisoned;
            Height = height;
            Weight = weight;
            Nationaly = nationaly;
        }

        public string Name { get; }
        public bool IsImprisoned { get; }
        public int Height { get; }
        public int Weight { get; }
        public string Nationaly { get; }
    }
}

//Поиск преступника
//У нас есть список всех преступников.
//В преступнике есть поля:
//ФИО,
//заключен ли он под стражу,
//рост,
//вес,
//национальность.
//Вашей программой будут пользоваться детективы.
//У детектива запрашиваются данные (рост, вес, национальность),
//и детективу выводятся все преступники,
//которые подходят под эти параметры,
//но уже заключенные под стражу выводиться не должны.