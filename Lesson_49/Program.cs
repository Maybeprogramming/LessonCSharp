namespace Lesson_49
{
    using static Randomaizer;
    using static Display;
    using static UserInput;

    class Program
    {
        static void Main()
        {
            Console.Title = "Зоопарк";

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

        public Aviary() { }
    }

    class AnimalFactory
    {

    }

    abstract class Animal : ISoundProvider
    {
        public Animal(GenderType genderType)
        {
            GenderType = genderType;
        }

        public AnimalType AnimalType { get; }
        public GenderType GenderType { get; }
        public string Name { get; }
        public abstract string Gender { get; }

        public abstract void MakeSound();

        public abstract Animal Clone(GenderType gender);
    }

    class Giraffe : Animal
    {
        public Giraffe(GenderType gender) : base(gender) { }

        public override string Gender => GenderType == GenderType.Male ? "Жираф" : "Жирафиха";

        public override void MakeSound()
        {
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

        public override string Gender => GenderType == GenderType.Male ? "Тигр" : "Тигрица";

        public override void MakeSound()
        {
        }

        public override Animal Clone(GenderType gender)
        {
            return new Tiger(gender);
        }
    }

    class Wolf : Animal
    {
        public Wolf(GenderType gender) : base( gender)
        {
        }

        public override string Gender => GenderType == GenderType.Male ? "Волк" : "Волчица";

        public override Animal Clone(GenderType gender)
        {
            return new Wolf(gender);
        }

        public override void MakeSound()
        {
        }
    }

    class Elephant : Animal
    {

        public Elephant(GenderType gender) : base(gender)
        {
        }
        public override string Gender => GenderType == GenderType.Male ? "Слон" : "Слониха";

        public override void MakeSound()
        {
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

        public override string Gender => GenderType == GenderType.Male ? "Попугай самец" : "Попугай самка";

        public override Animal Clone(GenderType gender)
        {
            return new Parrot(gender);
        }

        public override void MakeSound()
        {
        }
    }

    class Gorrilla : Animal
    {

        public Gorrilla(GenderType gender) : base( gender)
        {
        }

        public override string Gender => GenderType == GenderType.Male ? "Самец гориллы" : "Самка гориллы";

        public override void MakeSound()
        {
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

        public override string Gender => GenderType == GenderType.Male ? "Медведь" : "Медведица";

        public override void MakeSound()
        {
        }

        public override Animal Clone(GenderType gender)
        {
            return new Bear(gender);
        }
    }

    class AnimalDictionary
    {
        private Dictionary<Animal, AnimalType> _animalsTypes;

        public AnimalDictionary()
        {
            _animalsTypes = new Dictionary<Animal, AnimalType>()
            {
                {new Giraffe(GenderType.Male), AnimalType.Giraffes},
                {new Tiger(GenderType.Male), AnimalType.Tigers},
                {new Wolf(GenderType.Male), AnimalType.Wolves },
                {new Elephant(GenderType.Male), AnimalType.Elephants },
                {new Parrot(GenderType.Male), AnimalType.Parrots },
                {new Bear (GenderType.Male), AnimalType.Bears }
            };
        }

        public Dictionary<Animal, AnimalType> GetAnimalsTypes() => new Dictionary<Animal, AnimalType>(_animalsTypes);
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