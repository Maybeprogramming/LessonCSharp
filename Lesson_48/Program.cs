namespace Lesson_48
{
    using static Randomaizer;
    using static Display;
    using static FishNamesDictionary;
    using System.Text;

    class Program
    {
        static void Main() 
        {
            Aquarium aquarium = new Aquarium();

            Print($"{aquarium.ShowInfoFishes()}");

            aquarium.UpdateFishesLifeCicle();
            Print($"\nСледующий цикл >>>\n");

            Print($"{aquarium.ShowInfoFishes()}");
        }
    }

    class Aquarium
    {
        private List<Fish> _fishes;
        private FishFactory _fishFactory;

        public Aquarium() 
        {
            _fishFactory = new FishFactory();
            _fishes = new List<Fish>();
            _fishes.Add(_fishFactory.CreateFish());               
            _fishes.Add(_fishFactory.CreateFish());               
            _fishes.Add(_fishFactory.CreateFish());               
            _fishes.Add(_fishFactory.CreateFish());               
            _fishes.Add(_fishFactory.CreateFish());               
            _fishes.Add(_fishFactory.CreateFish());               
            _fishes.Add(_fishFactory.CreateFish());               
            _fishes.Add(_fishFactory.CreateFish());             
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

        public string ShowInfoFishes() 
        {
            StringBuilder infoFishes = new StringBuilder();

            for (int i = 0; i < _fishes.Count; i++)
            {
                infoFishes.Append($"{i + 1}. {_fishes[i].ShowInfo()}\n");
            }

            return infoFishes.ToString();
        }

        public void AddFish() 
        {
            _fishes.Add(new Fish("Форель", 5, 20));
        }

        public void RemoveFish() 
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

    class FishFactory
    {
        public Fish CreateFish()
        {
            string[] fishesNames = FishNamesDictionary.GetFishesNames();
            string fishName = GenerateRandomName(fishesNames);
            int minCurrentAge = 0;
            int maxCurrentAge = 5;
            int minLifespanAge = 10;
            int maxLifespanAge = 30;
            int currentAge = GenerateRandomNumber(minCurrentAge, maxCurrentAge);
            int lifespanAge = GenerateRandomNumber(minLifespanAge, maxLifespanAge);

            return new Fish(fishName, currentAge, lifespanAge);
        }

        public List<Fish> CreateSomeFishes(int fishesCount)
        {
            List<Fish> fishes = new List<Fish>();

            for (int i = 0; i < fishesCount; i++)
            {
                fishes.Add(CreateFish());
            }

            return fishes;
        }
    }

    class Fish
    {
        private int _age;

        public Fish(string name, int age, int lifespan) 
        { 
            Name = name;
            _age = age; 
            Lifespan = lifespan;
        }

        public string Name { get; }
        public int Age 
        { 
            get => _age; 
            private set => SetAge(value); 
        }
        public int Lifespan { get; }
        public bool IsAlive { get => Age < Lifespan; }

        public string ShowInfo() 
        {
            return $"[{Name}] возраст: [{Age}]";
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
            if (value >= Lifespan)
            {
                _age = Lifespan;
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

    static class FishNamesDictionary
    {
        private static readonly string[] s_names;

        static FishNamesDictionary()
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

        public static string[] GetFishesNames()
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