namespace Lesson_51
{
    class Program
    {
        static void Main()
        {
        }
    }

    class DetectiveOffice
    {
        private List<Criminal> _criminals;

        public DetectiveOffice()
        {
            _criminals= new List<Criminal>() 
            {
                new Criminal("", false, 180, 60, "Русский"),
                new Criminal("", false, 180, 60, "Русский"),
                new Criminal("", false, 180, 60, "Дагестанец"),
                new Criminal("", false, 180, 60, "Дагестанец"),
                new Criminal("", false, 180, 60, "Азейбарджанец"),
                new Criminal("", false, 180, 60, "Азейбарджанец"),
                new Criminal("", false, 180, 60, "Татарин"),
                new Criminal("", false, 180, 60, "Татарин"),
                new Criminal("", false, 180, 60, "Узбек"),
                new Criminal("", false, 180, 60, "Узбек"),
                new Criminal("", false, 180, 60, "Турок"),
                new Criminal("", false, 180, 60, "Турок"),
                new Criminal("", false, 180, 60, "Француз"),
                new Criminal("", false, 180, 60, "Француз"),
                new Criminal("", false, 180, 60, "Белорус"),
                new Criminal("", false, 180, 60, "Белорус"),
                new Criminal("", false, 180, 60, "Поляк"),
                new Criminal("", false, 180, 60, "Поляк"),
                new Criminal("", false, 180, 60, "Якут"),
                new Criminal("", false, 180, 60, "Якут"),
            };
        }
    }

    class CrimanalFactory
    {
        public List<Criminal> CreateSomeCriminals(int criminalsCount)
        {
            return new List<Criminal>();
        }

        private Criminal CreateCriminal()
        {
            return new Criminal("", false, 180, 60, "Якут");
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
        public string IsImprisonedToString { get => IsImprisoned == true ? "да": "нет"; }
        public int Height { get; }
        public int Weight { get; }
        public string Nationaly { get; }

        public string ShowInfo()
        {
            return $"{Name}. Рост [{Height}], Вес [{Weight}], Национальность: [{Nationaly}]. Заключение под стражу: [{IsImprisonedToString}].";
        }
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