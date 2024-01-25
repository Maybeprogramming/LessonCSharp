namespace Lesson_50_v2
{
    using static UserInput;
    using static Randomaizer;
    using static Display;

    class Program
    {
        static void Main()
        {
            PartFactory partFactory = new PartFactory();

            List<Part> partList = partFactory.CreateSeveral();

            foreach (var item in partList)
            {
                Print($"\n{item.Name} - {item.HealhtyStatus}");
            }

            Console.ReadKey();
        }
    }

    class Cell
    {
        private readonly Part _part;
        private int _count;

        public Cell(Part part, int count)
        {
            _part = part;
            _count = count;
        }

        public int Count => _count;
        public string Name => _part.Name;

        public void SetValue(int value) => _count = value > 0 ? value : 0;

        public void ShowInfo(string index = "")
        {
            Print($"{index}");
            Print($". Деталь: <");
            Print($"{Name}", ConsoleColor.Green);
            Print($">. Количество: <");
            Print($"{Count}", _count > 0 ? ConsoleColor.Green : ConsoleColor.Red);
            Print($">.");
        }
    }

    class PartFactory
    {
        private List<string> _partsNames;

        public PartFactory()
        {
            _partsNames = new(PartsDictionary.GetPartsNames());
        }

        public Part CreateSingle(string partName)
        {
            return new Part(partName, false);
        }

        public List<Part> CreateSeveral(int minCount = 5, int maxCount = 10)
        {
            maxCount = maxCount > _partsNames.Count ? _partsNames.Count : maxCount;

            List<Part> parts = new List<Part>();
            int countParts = GenerateRandomNumber(minCount, maxCount + 1);

            for (int i = 0; i < countParts; i++)
            {
                int indexNumber = GenerateRandomNumber(0, _partsNames.Count - i);
                string somePartName = _partsNames[indexNumber];
                bool isBroken = false;

                parts.Add(new Part(somePartName, isBroken));

                MovePartNameToEnd(somePartName, indexNumber);
            }

            parts = CreateBrokenPart(parts);

            return parts;
        }

        private List<Part> CreateBrokenPart(List<Part> parts)
        {
            int indexBrokenPart = GenerateRandomNumber(0, parts.Count);
            Part brokenPart = parts[indexBrokenPart].Clone(true);
            parts[indexBrokenPart] = brokenPart;

            return parts;
        }

        private void MovePartNameToEnd(string somePartName, int currentIndex)
        {
            int lastIndex = _partsNames.Count - 1;
            string tempName = _partsNames[lastIndex];

            _partsNames[lastIndex] = somePartName;
            _partsNames[currentIndex] = tempName;
        }
    }

    class Part : ICloneable
    {
        public Part(string name, bool isBroken)
        {
            Name = name;
            IsBroken = isBroken;
        }

        public string Name { get; }
        public bool IsBroken { get; }
        public string HealhtyStatus => IsBroken == true ? "не исправно" : "исправно";

        public Part Clone(bool isBroken)
        {
            return new Part(Name, isBroken);
        }
    }

    struct Price
    {
        private readonly int _value;

        public Price(int value)
        {
            _value = value;
        }

        public int Value => _value;

        public override string ToString()
        {
            return $"{_value}";
        }
    }

    class Stock
    {
        private Cell _parts;

        public void ShowInfo()
        {

        }
    }

    class CarFactory
    {
        private readonly PartFactory _partFactory;

        public CarFactory (PartFactory partFactory)
        {
            _partFactory = partFactory;
        }
    }

    class Car : IRepairable
    {
        private List<Part> _parts;

        public Car(List<Part> parts, string name)
        {
            _parts = parts;
            Name = name;
        }

        public string Name { get; }
        public string BrokenPartName => GetBrokenPart() != null ? GetBrokenPart().Name : "неисправных деталей нет";
        public bool IsNeedRepair => GetBrokenPart() != null ? true : false;
        public string HealthyStatus => IsNeedRepair == true ? "автомобиль неисправен" : "автомобиль в порядке";

        public bool ApplyRepair(Part part)
        {
            if (_parts.Contains(part))
            {
                ReplacePart(part);

                return true;
            }

            return false;
        }

        private void ReplacePart(Part part)
        {
            int indexPartToReplace = _parts.IndexOf(part);
            _parts[indexPartToReplace] = part;
        }

        private Part? GetBrokenPart()
        {
            return _parts.FirstOrDefault(part => part.IsBroken == true);
        }
    }

    interface IRepairable
    {

    }

    interface ICloneable
    {
        Part Clone(bool isBroken);
    }

    static class PartsDictionary
    {
        private static readonly List<string> s_PartsNames;

        static PartsDictionary()
        {
            s_PartsNames = new List<string>()
            {
                "Двигатель",
                "Трансмиссия",
                "Колесо",
                "Стекло",
                "Глушитель",
                "Тормоз",
                "Подвеска",
                "Генератор",
                "Кондиционер",
                "Стартер",
                "ГРМ",
                "Водяная помпа",
                "Бензобак",
                "Руль",
                "Рулевая рейка",
                "Усилитель руля",
                "Приборная панель",
                "Электропроводка",
                "Аккумулятор",
                "Свеча зажигания",
                "Топливный насос",
                "Масляный фильтр",
                "Коленвал",
                "Катализатор"
            };
        }

        internal static List<string>? GetPartsNames()
        {
            return s_PartsNames;
        }
    }

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
            Print($"\nДля продолжения нажмите любую клавишу...");
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
}

//Автосервис
//У вас есть автосервис,
//в который приезжают люди, чтобы починить свои автомобили.
//У вашего автосервиса есть баланс денег и склад деталей.
//Когда приезжает автомобиль,
//у него сразу ясна его поломка,
//и эта поломка отображается у вас в консоли вместе с ценой за починку
//(цена за починку складывается из цены детали + цена за работу).
//Поломка всегда чинится заменой детали,
//но количество деталей ограничено тем,
//что находится на вашем складе деталей.
//Если у вас нет нужной детали на складе,
//то вы можете отказать клиенту,
//и в этом случае вам придется выплатить штраф.
//Если вы замените не ту деталь,
//то вам придется возместить ущерб клиенту.
//За каждую удачную починку вы получаете выплату за ремонт,
//которая указана в чек-листе починки.
//Класс Деталь не может содержать значение “количество”.
//Деталь всего одна, за количество отвечает тот, кто хранит детали.
//При необходимости можно создать дополнительный класс для конкретной детали и работе с количеством.
