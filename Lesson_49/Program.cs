namespace Lesson_49
{
    class Program
    {
        static void Main()
        {
            Console.Title = "Зоопарк";
        }
    }

    class Aviary
    {
        private List<Animal> _animals;

        public Aviary() { }
    }

    abstract class Animal : ISoundProvider
    {
        private Gender _gender;
        private Sound _sound;

        public Animal(string name, Gender gender, Sound sound)
        { 
            Name = name;
            _gender = gender;
            _sound = sound;
        }

        public string Name { get; }

        public virtual void MakeSound()
        {

        }

        public string GetGender()
        {
            return _gender == Gender.Male ? "мужской" : "женский";
        }
    }

    class Sound
    {
        public Sound(string name)
        {
            Name = name;
        }

        string Name { get; }
    }

    #region Interfaces

    interface ISoundProvider
    {
        void MakeSound();
    }

    #endregion

    #region Enums

    enum Gender
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