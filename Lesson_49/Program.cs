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
                AnimalTypeName.Parrots,
                AnimalTypeName.Wolves
            };

            _aviaries = new List<Aviary>();

            FillAviaries();
        }

        public void Work()
        {
            int userInput;
            bool isWork = true;
            int exitCommand = _aviaries.Count + 1;
            int additionalNumber = 2;
            int minInputNumber = 0;
            int maxInputNumber = _aviaries.Count + additionalNumber;

            while (isWork == true)
            {
                Console.Clear();
                ShowMenu();

                userInput = ReadIntRange("Введите номер вальера для перехода: ", minInputNumber, maxInputNumber);

                if (userInput == exitCommand)
                {
                    Print($"\nВы покинули зоопарк! Приходите к нам ещё!\n", ConsoleColor.Green);
                    isWork = false;
                    return;
                };

                if (userInput > 0 && userInput <= _aviaries.Count)
                {
                    _aviaries[userInput - 1].ShowInfo();
                }

                WaitToPressKey("\n");
            }
        }

        private void ShowMenu()
        {
            Print($"Вам доступны следующие вальеры:\n", ConsoleColor.Green);

            for (int i = 0; i < _aviaries.Count; i++)
            {
                Print($"{i + 1}. Вальер с животными вида: [{_aviaries[i].TitleName}]\n");
            }

            Print($"{_aviaries.Count + 1} - Выйти из зоопарка.\n", ConsoleColor.Red);
        }

        private void FillAviaries()
        {
            int minAnimalCount = 3;
            int maxAnimalCount = 8;
            AviaryFactory aviaryFactory = new AviaryFactory(minAnimalCount, maxAnimalCount);

            for (int i = 0; i < _animalTypeNames.Count; i++)
            {
                AnimalFactory animalFactory = new AnimalFactory(_animalTypeNames[i]);
                Aviary aviary = aviaryFactory.CreateAviary(animalFactory);

                _aviaries.Add(aviary);
            }
        }
    }

    class Aviary
    {
        private List<Animal> _animals;

        public Aviary(List<Animal> animals) => _animals = animals;

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

            Print($"\nИз вальера доносятся звуки:\n");
            _animals[animalFirstIndex].MakeSound();
            PrintLine();
        }
    }

    #region Aviary and Animal Factory Methods

    class AviaryFactory
    {
        private int _animalCount;
        private int _minAnimalCount;
        private int _maxAnimalCount;
        private AnimalFactory _animalFactory;
        private List<Animal> _animals;

        public AviaryFactory(int minAnimalCount, int maxAnimalCount)
        {
            _animals = new List<Animal>();
            _minAnimalCount = minAnimalCount;
            _maxAnimalCount = maxAnimalCount;            
        }

        public Aviary CreateAviary(AnimalFactory animalFactory)
        {
            _animals.Clear();
            _animalCount = GenerateRandomNumber(_minAnimalCount, _maxAnimalCount + 1);

            for (int i = 0; i < _animalCount; i++)
            {
                _animals.Add(animalFactory.Create());
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
        public string TypeName { get => AnimalsDictionary.TryGetAnimalTypeToString(AnimalTypeName); }
        public string GenderToString => GenderType == GenderType.Male ? "Самец" : "Самка";

        public abstract void MakeSound();

        public abstract Animal Clone(GenderType gender);

        public string ShowInfo()
        {
            return $"{TypeName} [{GenderToString}]";
        }
    }

    class Giraffe : Animal
    {
        public Giraffe(GenderType gender) : base(gender) { }

        public override void MakeSound()
        {
            Print($"{TypeName} [{GenderToString}]. Издаёт тихие мычащие звуки!!!!\n");
        }

        public override Animal Clone(GenderType gender)
        {
            return new Giraffe(gender);
        }
    }

    class Tiger : Animal
    {

        public Tiger(GenderType gender) : base(gender) { }

        public override void MakeSound()
        {
            Print($"{TypeName} [{GenderToString}]. Издаёт звуки Мууур - мяууу, Рррррр!\n");
        }

        public override Animal Clone(GenderType gender)
        {
            return new Tiger(gender);
        }
    }

    class Wolf : Animal
    {
        public Wolf(GenderType gender) : base(gender) { }

        public override Animal Clone(GenderType gender)
        {
            return new Wolf(gender);
        }

        public override void MakeSound()
        {
            Print($"{TypeName} [{GenderToString}]. Издаёт звуки: Гаф - фав - фав!!! Ррррр!!!\n");
        }
    }

    class Elephant : Animal
    {

        public Elephant(GenderType gender) : base(gender) { }

        public override void MakeSound()
        {
            Print($"{TypeName} [{GenderToString}]! Трубит в свой длинный хобот!!!\n");
        }

        public override Animal Clone(GenderType gender)
        {
            return new Elephant(gender);
        }
    }

    class Parrot : Animal
    {
        public Parrot(GenderType gender) : base(gender) { }

        public override Animal Clone(GenderType gender)
        {
            return new Parrot(gender);
        }

        public override void MakeSound()
        {
            Print($"{TypeName} [{GenderToString}]! Поёт свою музыкальную песенку!!!\n");
        }
    }

    class Gorrilla : Animal
    {
        public Gorrilla(GenderType gender) : base(gender) { }

        public override void MakeSound()
        {
            Print($"{TypeName} [{GenderToString}]! Издаёт звуки: У - у - у, А - а -а , уух - ух - ух!!!\n");
        }

        public override Animal Clone(GenderType gender)
        {
            return new Gorrilla(gender);
        }
    }

    class Bear : Animal
    {
        public Bear(GenderType gender) : base(gender) { }

        public override void MakeSound()
        {

            Print($"{TypeName} [{GenderToString}]! Рычит: Арррррррыыыыырррр !!!\n");
        }

        public override Animal Clone(GenderType gender)
        {
            return new Bear(gender);
        }
    }

    #endregion

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
                {typeof(Bear), AnimalTypeName.Bears },
                {typeof(Gorrilla), AnimalTypeName.Gorrillas }
            };

            s_animalTypesToString = new Dictionary<AnimalTypeName, string>()
            {
                {AnimalTypeName.Giraffes, "Жираф"},
                {AnimalTypeName.Bears, "Медведь"},
                {AnimalTypeName.Gorrillas, "Горилла"},
                {AnimalTypeName.Elephants, "Слон"},
                {AnimalTypeName.Parrots, "Попугай"},
                {AnimalTypeName.Wolves, "Волк"},
                {AnimalTypeName.Tigers, "Тигр"}
            };

            s_animals = new Dictionary<AnimalTypeName, Animal>()
            {
                {AnimalTypeName.Giraffes, new Giraffe(GenderType.Male)},
                {AnimalTypeName.Gorrillas, new Gorrilla(GenderType.Male)},
                {AnimalTypeName.Bears, new Bear(GenderType.Male)},
                {AnimalTypeName.Elephants, new Elephant(GenderType.Male)},
                {AnimalTypeName.Parrots, new Parrot(GenderType.Male)},
                {AnimalTypeName.Wolves, new Wolf(GenderType.Male)},
                {AnimalTypeName.Tigers, new Tiger(GenderType.Male)}
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

    static class Randomaizer
    {
        private static readonly Random s_random;

        static Randomaizer()
        {
            s_random = new();
        }

        public static int GenerateRandomNumber(int minValue, int maxValue)
        {
            return s_random.Next(minValue, maxValue);
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
        public static int ReadIntRange(string message, int minValue = int.MinValue, int maxValue = int.MaxValue)
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