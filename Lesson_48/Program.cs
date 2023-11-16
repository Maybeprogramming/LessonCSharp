namespace Lesson_48
{
    class Program
    {
        static void Main() 
        {
            Aquarium aquarium = new Aquarium();

            aquarium.GetInfoFishes();

            aquarium.UpdateFishesLifeCicle();
            Console.WriteLine($"\nСледующий цикл >>>\n");
            aquarium.GetInfoFishes();
        }
    }

    class Aquarium
    {
        private List<Fish> _fishes;

        public Aquarium() 
        {
            _fishes = new List<Fish>() 
            { 
                new Fish(),
                new Fish(),
                new Fish(),
                new Fish(),
                new Fish(),
                new Fish()
            };
        }

        public int MaxFishesCount { get; }

        public void Work() { }

        public void UpdateFishesLifeCicle()
        {
            foreach (Fish fish in _fishes)
            {
                fish.Update();
            }
        }

        public void GetInfoFishes() 
        {
            for (int i = 0; i < _fishes.Count; i++)
            {
                Console.Write($"{i + 1}. {_fishes[i].ShowInfo()}\n");
            }
        }

        public void AddFish() 
        {
            _fishes.Add(new Fish());
        }

        public void GetFish() 
        { 
            foreach(Fish fish in _fishes)
            {
                if (fish.IsAlive == false)
                {
                    _fishes.Remove(fish);
                }
            }
        }
    }

    class Fish
    {
        private int _age;

        public Fish() 
        { 
            _age = 0; 
            DeadAge = 20;
            Name = "Окунь";
        }

        public string Name { get; }
        public int Age 
        { 
            get => _age; 
            private set => SetAge(value); 
        }
        public int DeadAge { get; private set; }
        public bool IsAlive { get => Age < DeadAge; }

        public string ShowInfo() 
        {
            return $"Рыба: [{Name}] возраст: [{Age}]";
        }

        public void Update() 
        { 
            if(IsAlive == true)
            {
                ++Age;
            }
        }

        private int SetAge(int value)
        {
            if (value >= DeadAge)
            {
                _age = DeadAge;
            }
            else
            {
                _age = value;
            }

            return _age;
        }
    }

    #region UserUtils

    static class Randomaizer
    {
        private static readonly Random s_random;

        static Randomaizer()
        {
            s_random = new();
        }

        public static string GenerateRandomName(string[] names)
        {
            return names[s_random.Next(0, names.Length)];
        }

        public static int GenerateRandomNumber(int minValue, int maxValue)
        {
            return s_random.Next(minValue, maxValue);
        }

        public static List<T> Shuffle<T>(List<T> array)
        {
            int elementIndex;
            T tempElement;

            for (int i = 0; i < array.Count; i++)
            {
                elementIndex = GenerateRandomNumber(i, array.Count);

                tempElement = array[i];
                array[i] = array[elementIndex];
                array[elementIndex] = tempElement;
            }

            return array;
        }

        public static bool IsСhanceNow(int chancePercent)
        {
            int minPercent = 0;
            int maxPercent = 100;
            int randomValue = GenerateRandomNumber(minPercent, maxPercent);

            return randomValue < chancePercent;
        }
    }

    static class Display
    {
        public static void Print(string message, ConsoleColor consoleColor = ConsoleColor.White)
        {
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = consoleColor;
            Console.Write(message);
            Console.ForegroundColor = defaultColor;
        }

        public static void PrintLine(ConsoleColor color = ConsoleColor.White)
        {
            int symbolCount = Console.WindowWidth - 1;
            Print($"{new string('-', symbolCount)}\n", color);
        }
    }

    static class FishNamesDatabase
    {
        private static readonly string[] s_names;

        static FishNamesDatabase()
        {
            s_names = new string[]
            {
                "Окунь",
                "Лещ",
                "Осётр",
                "Форель",
                "Щука",
                "Сом",
                "Анчоус",
                "Сельдь",
                "Лосось",
                "Камбала",
                "Карп",
            };
        }

        public static string[] GetNames()
        {
            return s_names;
        }
    }

    #endregion
}

//Аквариум
//Есть аквариум, в котором плавают рыбы.
//В этом аквариуме может быть максимум определенное кол-во рыб.
//Рыб можно добавить в аквариум или рыб можно достать из аквариума.
//(программу делать в цикле для того, чтобы рыбы могли “жить”)
//Все рыбы отображаются списком,
//у рыб также есть возраст.
//За 1 итерацию рыбы стареют на определенное кол-во жизней и могут умереть.
//Рыб также вывести в консоль, чтобы можно было мониторить показатели.


//Чаво ещё:
//1. Добавить меню с:
//  - добавить случайную рыбу;
//  - добавить пользовательскую рыбу (ввести тип, возраст);
//  - убрать мёртвую рыбу;
//  - убрать 1 рыбу на выбор;
//  - следующий цикл (Обновить возраст рыбы в аквариуме)