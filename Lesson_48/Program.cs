namespace Lesson_48
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
            int fishesCountOnStart = 10;
            int maxFishesCountOnAquarium = 15;
            int foodCountOnStart = 500;
            List<Fish> fishesOnStart = _fishFactory.CreateSomeRandomFishes(fishesCountOnStart);

            Aquarium aquarium = new Aquarium(fishesOnStart, maxFishesCountOnAquarium, foodCountOnStart);
            Home home = new Home(aquarium);

            home.Work();

            PrintLine();
            Print($"Программа завершена", ConsoleColor.Green);
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

            string menu =
                $"{SwitchToNextDayMenu}. > симуляция следующего дня\n" +
                $"{AddFishMenu}. - добавить случайную рыбку в аквариум\n" +
                $"{FeedingFishMenu}. - добавить корма в аквариум\n" +
                $"{RemoveDeadFishMenu}. - убрать мёртвых рыбок\n" +
                $"{RemoveOneFishMenu}. - убрать конкретную рыбку из аквариума\n" +
                $"{ExitMenu}. - выйти из симуляции аквариума...\n\n";

            bool isRun = true;
            int userInput;

            while (isRun == true)
            {
                Console.Clear();

                Print($"Корма в аквариуме: {_aquarium.FoodCount} единиц. Количество рыбок: {_aquarium.CurrentFishesCount}/{_aquarium.MaxFishesCount}\n\n", ConsoleColor.Yellow);
                Print($"Рыбки в аквариуме:\n", ConsoleColor.Green);
                Print($"{_aquarium.GetInfoFishes()}\n");
                Print($"{menuTitle}\n", ConsoleColor.Green);
                Print($"{menu}");

                userInput = ReadInt($"{requestMessage}");

                switch (userInput)
                {
                    case SwitchToNextDayMenu:
                        _aquarium.Simulate();
                        break;

                    case AddFishMenu:
                        _aquarium.AddFish(_fishFactory.CreateRandomFish());
                        break;

                    case FeedingFishMenu:
                        _aquarium.AddFood();
                        break;

                    case RemoveDeadFishMenu:
                        _aquarium.TryRemoveDeadFish();
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

    class Aquarium : IFeederProvider
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
        public int CurrentFishesCount { get => _fishes.Count; }

        public void Simulate()
        {
            foreach (Fish fish in _fishes)
            {
                TryToGiveFood(fish);
                fish.Update();
            }
        }

        public string GetInfoFishes()
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

                Print($"В аквариум добавлена рыбка: [{fish.Name}], здоровье [{fish.Health}]\n");
            }
            else
            {
                Print($"Достигнута максимальная вместимость аквариума!\n", ConsoleColor.Red);
            }
        }

        public void TryRemoveDeadFish()
        {
            List<Fish> deadFishes = _fishes.Where(fishes => fishes.IsAlive() == false).ToList();
            List<Fish> aliveFishes = _fishes.Where(fishes => fishes.IsAlive() == true).ToList();

            if (deadFishes.Count > 0)
            {
                _fishes = aliveFishes;
                Print($"Мёртвые рыбки из аквариуму убраны! ({deadFishes.Count} рыбок)\n", ConsoleColor.Green);
            }
            else
            {
                Print($"В аквариуме все рыбки живые!\n", ConsoleColor.Green);
            }
        }

        public void AddFood()
        {
            int foodCount;
            int minFoodCount = 0;
            int maxFoodCount = 1000;

            Print($"Добавить корма в аквариум. Максимум {maxFoodCount} единиц корма.\n");

            foodCount = ReadInt($"Введите количество корма: ", minFoodCount, maxFoodCount + 1);
            FoodCount += foodCount;

            Print($"Успешно добавлено {foodCount} единиц корма в аквариум.\n");
        }

        public void RemoveOneFish()
        {
            Fish fish;
            int fishIndex;

            fishIndex = ReadInt("\nВведите номер удаляемой рыбки: ", 1, _fishes.Count + 1);
            fish = _fishes[fishIndex - 1];

            _fishes.Remove(fish);
            Print($"Рыбка: {fish.Name}. Здоровье: {fish.Health} успешно убрана из аквариума!\n", ConsoleColor.Green);
        }

        public void TryToGiveFood(ISuitableForFeeding fish)
        {
            if (fish.TryToEatingFood(FoodCount, out int foodEatenAmount) == true)
            {
                FoodCount -= foodEatenAmount;
            }
        }
    }

    #region Fish Factory Method

    class FishFactory
    {
        public Fish CreateRandomFish()
        {
            string[] fishesNames = FishNamesDictionary.GetFishesNames();
            string fishName = GenerateRandomName(fishesNames);
            int minCurrentAge = 0;
            int maxCurrentAge = 5;
            int minLifespanAge = 15;
            int maxLifespanAge = 30;
            int minHealth = 80;
            int maxHealth = 150;
            int currentAge = GenerateRandomNumber(minCurrentAge, maxCurrentAge);
            int lifespanAge = GenerateRandomNumber(minLifespanAge, maxLifespanAge);
            int health = GenerateRandomNumber(minHealth, maxHealth);

            return new Fish(fishName, currentAge, lifespanAge, health);
        }

        public List<Fish> CreateSomeRandomFishes(int fishesCount)
        {
            List<Fish> fishes = new List<Fish>();

            for (int i = 0; i < fishesCount; i++)
            {
                fishes.Add(CreateRandomFish());
            }

            return fishes;
        }
    }

    #endregion

    class Fish : ISuitableForFeeding
    {
        private int _age;
        private int _health;
        private int _maxHealth;
        private int _currentFoodCount;
        private int _criticalLevelFood;
        private int _maxFoodCount;
        private int _dailyFoodIntake;
        private int _decreasedHealthWhenHungry;
        private int _increasedHealthWhenSatiety;
        private int _maxAmountFoodEatenAtDay;

        public Fish(string name, int age, int lifespan, int health)
        {
            Name = name;
            _age = age;
            Lifespan = lifespan;
            _maxHealth = health;
            Health = health;

            _criticalLevelFood = 0;
            _currentFoodCount = 15;
            _maxFoodCount = 50;

            _dailyFoodIntake = 5;
            _decreasedHealthWhenHungry = 10;
            _increasedHealthWhenSatiety = 5;
            _maxAmountFoodEatenAtDay = 20;
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

        public int CurrentFoodCount
        {
            get => _currentFoodCount;
            private set => SetCurrentFoodCount(value);
        }

        public int Lifespan { get; }
        public bool IsSatietyStatus { get => CurrentFoodCount > _criticalLevelFood; }

        public bool IsAlive()
        {
            if (Age < Lifespan && Health > 0)
            {
                return true;
            }

            Health = 0;
            return false;
        }

        public bool TryToEatingFood(int foodCount, out int foodEatenAmount)
        {
            if (IsAlive() == true)
            {
                if (foodCount >= _maxAmountFoodEatenAtDay)
                {
                    foodEatenAmount = _maxAmountFoodEatenAtDay;
                    CurrentFoodCount += _maxAmountFoodEatenAtDay;
                }
                else
                {
                    foodEatenAmount = foodCount;
                    CurrentFoodCount += foodCount;
                }

                return true;
            }

            foodEatenAmount = 0;
            return false;
        }

        public string ShowInfo()
        {
            string info = $"[{Name}] возраст: [{Age}] дней, ХР: [{Health}]. Состояние: [{SatietyToString()}] - ({CurrentFoodCount}). [{AliveToString()}].";

            if (ReasonOfDeathToString() != null && ReasonOfDeathToString() != "")
            {
                info += $" ({ReasonOfDeathToString()})";
            }

            return info;
        }

        public void Update()
        {
            if (IsAlive() == true)
            {
                ++Age;
                CurrentFoodCount -= _dailyFoodIntake;

                if (CurrentFoodCount <= _criticalLevelFood)
                {
                    Health -= _decreasedHealthWhenHungry;
                }
                else
                {
                    Health += _increasedHealthWhenSatiety;
                }
            }
        }

        private void SetCurrentFoodCount(int value)
        {
            if (value > 0 && value <= _maxFoodCount)
            {
                _currentFoodCount = value;
            }
            else if (value >= _maxFoodCount)
            {
                _currentFoodCount = _maxFoodCount;
            }
            else
            {
                _currentFoodCount = 0;
            }
        }

        private void SetAge(int value)
        {
            if (value >= Lifespan)
            {
                _age = Lifespan;
            }
            else
            {
                _age = value;
            }
        }

        private void SetHealth(int value)
        {
            if (value > 0 && value < _maxHealth)
            {
                _health = value;
            }
            else if (value >= _maxHealth)
            {
                _health = _maxHealth;
            }
            else
            {
                _health = 0;
            }
        }

        private string AliveToString()
        {
            return IsAlive() == true ? "живая" : "мертвая";
        }

        private string SatietyToString()
        {
            return IsSatietyStatus == true ? "сытая" : "голодная";
        }

        private string? ReasonOfDeathToString()
        {
            if (Age >= Lifespan)
            {
                return $"от старости";
            }
            else if (Health <= 0 && IsSatietyStatus == false)
            {
                return $"от голода";
            }

            return null;
        }
    }

    #region Interfaces

    public interface IFeederProvider
    {
        void TryToGiveFood(ISuitableForFeeding fish);
    }

    public interface ISuitableForFeeding
    {
        string Name { get; }

        bool TryToEatingFood(int foodCount, out int foodEatenAmount);
    }

    #endregion

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

//Александр Михновец
//1) class FishNamesDictionary -статические методы есть,
//а тем более классы есть смысл делать,
//когда этот метод вызывается в 2-х и более классах.
//Таким образом стараются избежать дублирования кода,
//например по созданию объектов типа Random.
//class FishNamesDictionary и единственный метод в нем
//вызывается только один раз во всем коде и
//вполне может быть объявлен как объект в классе,
//где используется. Т.о. статика ему не нужна.
//2) AliveToString(), SatietyToString(), ReasonOfDeathToString()
//- методам нужен глагол в названии.
//Часть этих методов, могут стать свойствами, если не придумаете глагол.
//3) Update() - методу больше подходит название SkipTime()
//(рекомендация, а не требование исправить).
//4) ReadInt() если метод считывает число в определенном диапазоне,
//то лучше его так и назвать ReadIntInRange()
//5) Замечания не критические, исправьте у себя,
//повторно отправлять на проверку не нужно. Принято