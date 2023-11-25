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
                new Criminal("Алексей Соколов", false, 180, 60, "Русский"),
                new Criminal("Евгений Иванов", false, 180, 60, "Русский"),
                new Criminal("Магомед Долганов", false, 180, 60, "Дагестанец"),
                new Criminal("Рамзан Чимиков", false, 180, 60, "Дагестанец"),
                new Criminal("Узун Абдула", false, 180, 60, "Азейбарджанец"),
                new Criminal("Келач Рашид", false, 180, 60, "Азейбарджанец"),
                new Criminal("Ибрагим Насыбуллин", false, 180, 60, "Татарин"),
                new Criminal("Казим Замалиев", false, 180, 60, "Татарин"),
                new Criminal("Эльнар Атаев", false, 180, 60, "Узбек"),
                new Criminal("Багымбай Бердиев", false, 180, 60, "Узбек"),
                new Criminal("Заман Гаттаулин", false, 180, 60, "Турок"),
                new Criminal("Ильдар Касимов", false, 180, 60, "Турок"),
                new Criminal("Армель Бюжо", false, 180, 60, "Француз"),
                new Criminal("Гастон Дебюсси", false, 180, 60, "Француз"),
                new Criminal("Микола Радчук", false, 180, 60, "Белорус"),
                new Criminal("Тимур Савич", false, 180, 60, "Белорус"),
                new Criminal("Анджей Любанский", false, 180, 60, "Поляк"),
                new Criminal("Бартоломей Невядомский", false, 180, 60, "Поляк"),
                new Criminal("Айан Галев", false, 180, 60, "Якут"),
                new Criminal("Дохсун Жданов", false, 180, 60, "Якут"),
            };
        }
    }

    class CrimanalFactory
    {
        public List<Criminal> CreateSomeCriminals(int criminalsCount)
        {
            return new List<Criminal>();
        }

        public Criminal CreateCriminal()
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