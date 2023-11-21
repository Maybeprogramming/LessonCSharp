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


            for (int i = 0; i < 50; i++)
            {
                AnimalFactory animalFactory = new AnimalFactory(AnimalTypeName.Giraffes);
                Giraffe giraffe = (Giraffe)animalFactory.Create();
                Print($"{i + 1}. {giraffe.GenderToString}\n");                
            }

            PrintLine();            
            
            for (int i = 0; i < 50; i++)
            {
                AnimalFactory animalFactory = new AnimalFactory(AnimalTypeName.Tigers);
                Tiger tiger = (Tiger)animalFactory.Create();
                Print($"{i + 1}. {tiger.GenderToString}\n");                
            }

            PrintLine();


            //Zoo zoo = new Zoo();
            //zoo.Work();
        }
    }

    class Zoo
    {
        private List<Aviary> _aviaries;
        private List<AnimalTypeName> _animalTypeNames;

        public Zoo()
        {
            _animalTypeNames = new List<AnimalTypeName>()
            {
                AnimalTypeName.Gorrillas,
                AnimalTypeName.Giraffes,
                AnimalTypeName.Elephants,
                AnimalTypeName.Bears,
                AnimalTypeName.Tigers,
                AnimalTypeName.Parrots
            };

            _aviaries = new List<Aviary>();

            FillAviaries();
        }

        public void Work()
        {
            ShowMenu();
            WaitToPressKey();
        }

        private void ShowMenu()
        {
            Print($"Вам доступны следующие вальеры:");

            for (int i = 0; i < _aviaries.Count; i++)
            {
                Print($"{i + 1}. Вальер с животными вида: [{_aviaries[i].TitleName}]");
            }
        }

        private void FillAviaries()
        {
            int minAnimalCount = 2;
            int maxAnimalCount = 10;

            for (int i = 0; i < _animalTypeNames.Count; i++)
            {
                AnimalFactory animalFactory = new AnimalFactory(_animalTypeNames[i]);
                AviaryFactory aviaryFactory = new AviaryFactory(animalFactory, minAnimalCount, maxAnimalCount);
                Aviary aviary = aviaryFactory.CreateAviary();

                _aviaries.Add(aviary);
            }
        }
    }

    class Aviary
    {
        private List<Animal> _animals;

        public Aviary(List<Animal> animals)
        {
            _animals = animals;
        }

        public string TitleName => AnimalsDictionary.TryGetAnimalTypeToString(_animals.First().AnimalTypeName);

        public void ShowInfo()
        {
            int animalFirstIndex = 0;

            PrintLine();
            Print($"В вальере {_animals.Count} животных вида: [{TitleName}]:\n");

            for (int i = 0; i < _animals.Count; i++)
            {
                Print($"{i + 1}. {_animals[i].ShowInfo()}\n");
            }

            Print($"Из вальера издаётся звук:");
            _animals[animalFirstIndex].MakeSound();
            PrintLine();
        }
    }

    #region Aviary and Animal Factory Methods

    class AviaryFactory
    {
        private int _animalCount;
        private AnimalFactory _animalFactory;
        private List<Animal> _animals;

        public AviaryFactory(AnimalFactory animalFactory, int minAnimalCount, int maxAnimalCount)
        {
            _animals = new List<Animal>();
            _animalFactory = animalFactory;
            _animalCount = GenerateRandomNumber(minAnimalCount, maxAnimalCount + 1);
        }

        public Aviary CreateAviary()
        {
            for (int i = 0; i < _animalCount; i++)
            {
                _animals.Add(_animalFactory.Create());
            }

            return new Aviary(new List<Animal>(_animals));
        }
    }

    class AnimalFactory
    {
        private List<GenderType> _gendersTypes;
        private Animal _animal;

        public AnimalFactory(AnimalTypeName animalTypeName)
        {
            _gendersTypes = new List<GenderType>()
            {
                GenderType.Male,
                GenderType.Female
            };

            _animal = AnimalsDictionary.TryGetAnimal(animalTypeName);
        }

        public Animal Create()
        {
            int genderIndex = GenerateRandomNumber(0, _gendersTypes.Count);
            GenderType genderType = _gendersTypes[genderIndex];

            return _animal.Clone(genderType);
        }
    }

    #endregion

    #region Animals classes
    abstract class Animal : ISoundProvider
    {
        public Animal(GenderType genderType)
        {
            GenderType = genderType;
        }

        public AnimalTypeName AnimalTypeName { get => AnimalsDictionary.TryGetAnimalType(this.GetType()); }
        public GenderType GenderType { get; }
        public string Name { get => AnimalsDictionary.TryGetAnimalTypeToString(AnimalTypeName); }
        public abstract string GenderToString { get; }

        public abstract void MakeSound();

        public abstract Animal Clone(GenderType gender);

        public string ShowInfo()
        {
            return $"{Name}";
        }
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

        public Tiger(GenderType gender) : base(gender) { }

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
        public Wolf(GenderType gender) : base(gender) { }

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

        public Elephant(GenderType gender) : base(gender) { }

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
        public Parrot(GenderType gender) : base(gender) { }

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
        public Gorrilla(GenderType gender) : base(gender) { }

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
        public Bear(GenderType gender) : base(gender) { }

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

    #endregion

    static class AnimalsDictionary
    {
        private static Dictionary<Type, AnimalTypeName> s_animalsTypes;
        private static Dictionary<AnimalTypeName, string> s_animalTypesToString;
        private static Dictionary<AnimalTypeName, Animal> s_animals;

        static AnimalsDictionary()
        {
            s_animalsTypes = new Dictionary<Type, AnimalTypeName>()
            {
                {typeof(Giraffe), AnimalTypeName.Giraffes},
                {typeof(Tiger), AnimalTypeName.Tigers},
                {typeof(Wolf), AnimalTypeName.Wolves },
                {typeof(Elephant), AnimalTypeName.Elephants },
                {typeof(Parrot), AnimalTypeName.Parrots },
                {typeof(Bear), AnimalTypeName.Bears }
            };

            s_animalTypesToString = new Dictionary<AnimalTypeName, string>()
            {
                {AnimalTypeName.Giraffes, "Жираф"},
                {AnimalTypeName.Bears, "Медведь"},
                {AnimalTypeName.Gorrillas, "Горилла"},
                {AnimalTypeName.Elephants, "Слон"},
                {AnimalTypeName.Parrots, "Попугай"},
                {AnimalTypeName.Wolves, "Волк"}
            };

            s_animals = new Dictionary<AnimalTypeName, Animal>()
            {
                {AnimalTypeName.Giraffes, new Giraffe(GenderType.Male)},
                {AnimalTypeName.Gorrillas, new Gorrilla(GenderType.Male)},
                {AnimalTypeName.Bears, new Bear(GenderType.Male)},
                {AnimalTypeName.Elephants, new Elephant(GenderType.Male)},
                {AnimalTypeName.Parrots, new Parrot(GenderType.Male)},
                {AnimalTypeName.Wolves, new Wolf(GenderType.Male)}
            };
        }

        public static AnimalTypeName TryGetAnimalType(Type animal)
        {
            s_animalsTypes.TryGetValue(animal, out AnimalTypeName animalType);

            return animalType;
        }

        public static string TryGetAnimalTypeToString(AnimalTypeName animalTypeName)
        {
            s_animalTypesToString.TryGetValue(animalTypeName, out string animalTypeNameToString);

            return animalTypeNameToString;
        }

        public static Animal TryGetAnimal(AnimalTypeName animalTypeName)
        {
            s_animals.TryGetValue(animalTypeName, out Animal animal);

            return animal;
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

    enum AnimalTypeName
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