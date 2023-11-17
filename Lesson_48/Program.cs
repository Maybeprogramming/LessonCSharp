﻿namespace Lesson_48
{
    using static Randomaizer;
    using static Display;
    using static UserInput;
    using System.Text;

    class Program
    {
        static void Main()
        {
            Console.Title = "Аквариум";

            FishFactory _fishFactory = new FishFactory();
            Aquarium aquarium = new Aquarium(initialNumberFishes: _fishFactory.CreateSomeFishes(10),
                                             maxFishesCount: 15,
                                             initialFoodCount: 100);
            Home home = new Home(aquarium);

            home.Work();

            for (int i = 0; i < 40; i++)
            {
                Console.Clear();
                Print($"{aquarium.ShowInfoFishes()}");
                aquarium.Simulate();
                Print($"\nСледующий цикл >>> [{i + 1}]\n");
                Task.Delay(2000).Wait();
            }

            PrintLine();
            Console.ReadKey();
        }
    }

    class Home
    {
        private Aquarium _aquarium;
        private FishFactory _fishFactory;

        public Home(Aquarium aquarium)
        {
            _aquarium = aquarium;
            _fishFactory = new FishFactory();
        }

        public void Work()
        {
            const int SwitchToNextDayMenu = 1;
            const int AddFishMenu = 2;
            const int FeedingFishMenu = 3;
            const int RemoveDeadFishMenu = 4;
            const int RemoveOneFishMenu = 5;
            const int ExitMenu = 6;

            string menuTitle = "Доступные команды:";
            string requestMessage = "Введите номер команды для продолжения: ";

            string menu = $"{menuTitle}\n" +
                $"{SwitchToNextDayMenu}. > симуляция следующего дня\n" +
                $"{AddFishMenu}. - добавить рыбку в аквариум\n" +
                $"{FeedingFishMenu}. - покормить рыбок\n" +
                $"{RemoveDeadFishMenu}. - убрать неживых рыбок\n" +
                $"{RemoveOneFishMenu}. - убрать рыбку из аквариума\n" +
                $"{ExitMenu}. - выйти из симуляции аквариума...\n\n";

            bool isRun = true;
            int userInput;

            while (isRun == true)
            {
                Console.Clear();

                Print($"{menu}");

                userInput = ReadInt($"{requestMessage}");

                switch (userInput)
                {
                    case SwitchToNextDayMenu:
                        _aquarium.Simulate();
                        break;

                    case AddFishMenu:
                        _aquarium.AddFish(_fishFactory.CreateFish());
                        break;

                    case FeedingFishMenu:
                        _aquarium.AddFood(300);
                        break;

                    case RemoveDeadFishMenu:
                        _aquarium.RemoveDeadFish();
                        break;

                    case RemoveOneFishMenu:
                        _aquarium.RemoveOneFish();
                        break;

                    case ExitMenu:
                        isRun = false;
                        break;

                    default:
                        break;
                }

                WaitToPressKey();
            }
        }
    }

    class Aquarium
    {
        private List<Fish> _fishes;

        public Aquarium(List<Fish> initialNumberFishes, int maxFishesCount, int initialFoodCount)
        {
            _fishes = initialNumberFishes;
            MaxFishesCount = maxFishesCount;
            FoodCount = initialFoodCount;
        }

        public int MaxFishesCount { get; }
        public int FoodCount { get; private set; }

        public void Simulate()
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

        public void AddFish(Fish fish)
        {
            if (_fishes.Count < MaxFishesCount)
            {
                _fishes.Add(fish);
            }
        }

        //not work, переделать
        public void RemoveDeadFish()
        {
            foreach (Fish fish in _fishes)
            {
                if (fish.IsAlive == false)
                {
                    _fishes.Remove(fish);
                }
            }
        }

        //Реализовать метод добавления еды
        public void AddFood(int foodCount)
        {
            throw new NotImplementedException();
        }

        //Реализовать метод
        internal void RemoveOneFish()
        {
            throw new NotImplementedException();
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
        private int _health;

        public Fish(string name, int age, int lifespan)
        {
            Name = name;
            _age = age;
            Lifespan = lifespan;
        }

        public string Name { get; }

        public int Health 
        { 
            get => _health; 
            private set => SetHealth(value); 
        }

        public int Age
        {
            get => _age;
            private set => SetAge(value);
        }

        public int Lifespan { get; }

        //Убрать свойсто -> сделать метод.
        //Умирает от старости или от здоровья = 0.
        //Здоровье уменьшается от голода, т.е. сытость = false

        public bool IsAlive { get => Age < Lifespan; }

        //Количество съедаемой еды за 1 день
        public int AmountOfFoodConsumedInOneDay { get; }

        //Сытость рыбки
        public bool SatietyFromFood { get; private set; }

        public string ShowInfo()
        {
            return $"[{Name}] возраст: [{Age}]. Состояние: [{IsAliveToString()}]";
        }

        public void Update()
        {
            if (IsAlive == true)
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

        private void SetHealth(int value)
        {
            throw new NotImplementedException();
        }

        private string IsAliveToString()
        {
            if (IsAlive == true)
            {
                return "живая";
            }
            else
            {
                return "мертвая";
            }
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

    static class UserInput
    {
        public static int ReadInt(string message, int minValue = int.MinValue, int maxValue = int.MaxValue)
        {
            int result;

            Console.Write(message);

            while (int.TryParse(Console.ReadLine(), out result) == false || result < minValue || result >= maxValue)
            {
                Console.Error.WriteLine("Ошибка!. Попробуйте снова!");
            }

            return result;
        }

        public static void WaitToPressKey(string message = "")
        {
            Print(message);
            Print($"Для продолжения нажмите любую клавишу...\n");
            Console.ReadKey();
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