﻿namespace Lesson_49
{
    using static Randomaizer;
    using static Display;
    using static UserInput;

    class Program
    {
        static void Main()
        {
            Console.Title = "Зоопарк";

            Giraffe giraffe = new Giraffe(GenderType.Male);
            Tiger tiger = new Tiger(GenderType.Female);

            Print($"{giraffe.AnimalType}, {giraffe.GenderToString}\n");
            Print($"{tiger.AnimalType}, {tiger.GenderToString}\n");
            WaitToPressKey("\n");

            Zoo zoo = new Zoo();
            zoo.Work();
        }
    }

    class Zoo
    {
        private List<Aviary> _aviaries;

        public void Work() { }
    }

    class Aviary
    {
        private List<Animal> _animals;

        public Aviary(List<Animal> animals) => _animals = animals;
    }

    class AviaryFactory
    {
        private int _minAnimalCount;
        private int _maxAnimalCount;
        private int _animalCount;
        private AnimalFactory _animalFactory;

        public AviaryFactory(AnimalFactory animalFactory)
        {
            _animalFactory = animalFactory;
            _minAnimalCount = 3;
            _maxAnimalCount = 10;
            _animalCount = GenerateRandomNumber(_minAnimalCount, _maxAnimalCount + 1);
        }

        public Aviary CreateAviary()
        {
            return new Aviary(new List<Animal>());
        }
    }

    class AnimalFactory
    {
        private List<GenderType> _gendersTypes;
        private Animal _animal;
        private Dictionary<AnimalType, Animal> _animals;
        private GenderType _gender;

        public AnimalFactory(AnimalType animalType)
        {
            _gendersTypes = new List<GenderType>()
            {
                GenderType.Male,
                GenderType.Female
            };

            _gender = GenderType.Male;

            _animals = new Dictionary<AnimalType, Animal>()
            {
                {AnimalType.Giraffes, new Giraffe(_gender)},
                {AnimalType.Gorrillas, new Gorrilla(_gender)},
                {AnimalType.Bears, new Bear(_gender)},
                {AnimalType.Elephants, new Elephant(_gender)},
                {AnimalType.Parrots, new Parrot(_gender)},
                {AnimalType.Wolves, new Wolf(_gender)}
            };

            _animals.TryGetValue(animalType, out _animal);
        }

        public Animal Create()
        {
            int genderIndex = GenerateRandomNumber(0, _gendersTypes.Count);
            GenderType genderType = _gendersTypes[genderIndex];

            return _animal.Clone(genderType);
        }
    }

    abstract class Animal : ISoundProvider
    {
        public Animal(GenderType genderType)
        {
            GenderType = genderType;
            AnimalType = AnimalDictionary.TryGetAnimalType(this.GetType());
        }

        public AnimalType AnimalType { get; }
        public GenderType GenderType { get; }
        public string Name { get; }
        public abstract string GenderToString { get; }

        public abstract void MakeSound();

        public abstract Animal Clone(GenderType gender);
    }

    class Giraffe : Animal
    {
        public Giraffe(GenderType gender) : base(gender) { }

        public override string GenderToString => GenderType == GenderType.Male ? "Жираф" : "Жирафиха";

        public override void MakeSound()
        {
            Print($"Я {GenderToString}!!! Да да я оруууу с высока!\n");
        }

        public override Animal Clone(GenderType gender)
        {
            return new Giraffe(gender);
        }
    }

    class Tiger : Animal
    {

        public Tiger(GenderType gender) : base(gender)
        {
        }

        public override string GenderToString => GenderType == GenderType.Male ? "Тигр" : "Тигрица";

        public override void MakeSound()
        {
            Print($"Я {GenderToString}!! Мууур - мяууу, Рррррр!\n");
        }

        public override Animal Clone(GenderType gender)
        {
            return new Tiger(gender);
        }
    }

    class Wolf : Animal
    {
        public Wolf(GenderType gender) : base(gender)
        {
        }

        public override string GenderToString => GenderType == GenderType.Male ? "Волк" : "Волчица";

        public override Animal Clone(GenderType gender)
        {
            return new Wolf(gender);
        }

        public override void MakeSound()
        {
            Print($"Я {GenderToString}!!! Гаф - фав - фав!!! Ррррр!!!\n");
        }
    }

    class Elephant : Animal
    {

        public Elephant(GenderType gender) : base(gender)
        {
        }
        public override string GenderToString => GenderType == GenderType.Male ? "Слон" : "Слониха";

        public override void MakeSound()
        {
            Print($"Я {GenderToString}!!! Трубит в свой длинный хобот!!!\n");
        }

        public override Animal Clone(GenderType gender)
        {
            return new Elephant(gender);
        }
    }

    class Parrot : Animal
    {
        public Parrot(GenderType gender) : base(gender)
        {
        }

        public override string GenderToString => GenderType == GenderType.Male ? "Попугай самец" : "Попугай самка";

        public override Animal Clone(GenderType gender)
        {
            return new Parrot(gender);
        }

        public override void MakeSound()
        {
            Print($"Я {GenderToString}!!! Поёт свою музыкальную песенку!!!\n");
        }
    }

    class Gorrilla : Animal
    {

        public Gorrilla(GenderType gender) : base(gender)
        {
        }

        public override string GenderToString => GenderType == GenderType.Male ? "Самец гориллы" : "Самка гориллы";

        public override void MakeSound()
        {
            Print($"Я {GenderToString}!!! У - у - у, А - а -а , уух - ух - ух!!!\n");
        }

        public override Animal Clone(GenderType gender)
        {
            return new Gorrilla(gender);
        }
    }

    class Bear : Animal
    {
        public Bear(GenderType gender) : base(gender)
        {
        }

        public override string GenderToString => GenderType == GenderType.Male ? "Медведь" : "Медведица";

        public override void MakeSound()
        {

            Print($"Я {GenderToString}!!! Арррррррыыыыырррр !!!\n");
        }

        public override Animal Clone(GenderType gender)
        {
            return new Bear(gender);
        }
    }

    static class AnimalDictionary
    {
        private static Dictionary<Type, AnimalType> s_animalsTypes;

        static AnimalDictionary()
        {
            s_animalsTypes = new Dictionary<Type, AnimalType>()
            {
                {typeof(Giraffe), AnimalType.Giraffes},
                {typeof(Tiger), AnimalType.Tigers},
                {typeof(Wolf), AnimalType.Wolves },
                {typeof(Elephant), AnimalType.Elephants },
                {typeof(Parrot), AnimalType.Parrots },
                {typeof(Bear), AnimalType.Bears }
            };
        }

        public static AnimalType TryGetAnimalType(Type animal)
        {
            s_animalsTypes.TryGetValue(animal, out AnimalType animalType);

            return animalType;
        }
    }

    #region Interfaces

    interface ISoundProvider
    {
        void MakeSound();
    }

    #endregion

    #region Enums

    enum GenderType
    {
        Male,
        Female
    }

    enum AnimalType
    {
        Giraffes,
        Tigers,
        Wolves,
        Elephants,
        Parrots,
        Gorrillas,
        Bears
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

    static class NamesDatabase
    {
        private static readonly string[] s_names;

        static NamesDatabase()
        {
            s_names = new string[]
            {
                "Жирафы",
                "Тигры",
                "Волки",
                "Слоны",
                "Попугаи",
                "Горилы",
                "Медведи",
            };
        }

        public static string[] GetNames()
        {
            return s_names;
        }
    }

    #endregion
}

//Зоопарк
//Пользователь запускает приложение и перед ним находится меню,
//в котором он может выбрать, к какому вольеру подойти.
//При приближении к вольеру,
//пользователю выводится информация о том,
//что это за вольер,
//сколько животных там обитает,
//их пол и какой звук издает животное.
//Вольеров в зоопарке может быть много, в решении нужно создать минимум 4 вольера.

//волк - волчица
//медведь - медведица
//тигр - тигрица
//слон - слониха
//жираф - жирафиха
//горилла - горилла
//попугай