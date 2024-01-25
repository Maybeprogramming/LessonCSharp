namespace Lesson_50_v2
{
    using static UserInput;
    using static Randomaizer;
    using static Display;

    class Program
    {
        static void Main()
        {
            Stock stock = new Stock();

            stock.ShowInfo();

            bool isGetPart = stock.TryGetPart("Двигатель", out Part part);
            Print($"\n\n{isGetPart}\n\n");

            stock.ShowInfo();

            Console.ReadKey();
        }
    }

    class Cell
    {
        private Queue<Part> _parts;

        public Cell(Queue<Part> parts, string name)
        {
            _parts = parts;
            Name = name;
        }

        public int Count => _parts.Count;
        public string Name { get; }

        public Part TryGetPart() => Count > 0 ? _parts.Dequeue() : null;

        public void ShowInfo()
        {
            Print($"Деталь: <");
            Print($"{Name}", ConsoleColor.Green);
            Print($">. Количество: <");
            Print($"{Count}", Count > 0 ? ConsoleColor.Green : ConsoleColor.Red);
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

    class Part : ICloneable, IEquatable<Part>
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

        public bool Equals(Part? other)
        {
            if (other == null)
            {
                return false;
            }

            return Name == other.Name;
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
        private List<Cell> _cellsParts;
        private List<string> _partsNames;

        public Stock()
        {
            _partsNames = PartsDictionary.GetPartsNames();
            _cellsParts = FillCellsParts();
        }

        public bool TryGetPart(string partName, out Part part)
        {
            part = _cellsParts.First(cell => cell.Name == partName).TryGetPart();

            if (part != null)
            {
                return true;
            }

            return false;
        }

        public void ShowInfo()
        {
            int index = 0;
            Print($"Доступные детали на складе:\n", ConsoleColor.Green);
            PrintLine();

            foreach (var cell in _cellsParts)
            {
                Print($"\n{++index}. ");

                cell.ShowInfo();
            }

            Print($"\n");
            PrintLine();
        }

        private List<Cell> FillCellsParts()
        {
            List<Cell> cellsParts = new List<Cell>();
            PartFactory partFactory = new PartFactory();
            int minPartsCount = 0;
            int maxPartCount = 10;
            int partsCount;
            int positionsPartsCount = _partsNames.Count;
            Cell cellPart;
            Queue<Part> parts;
            string partName;

            for (int i = 0; i < positionsPartsCount; i++)
            {
                partsCount = GenerateRandomNumber(minPartsCount, maxPartCount + 1);
                partName = _partsNames[i];
                parts = new Queue<Part>();

                for (int j = 0; j < partsCount; j++)
                {
                    parts.Enqueue(partFactory.CreateSingle(partName));
                }

                cellPart = new Cell(parts, partName);
                cellsParts.Add(cellPart);
            }

            return cellsParts;
        }
    }

    class CarFactory
    {
        private readonly PartFactory _partFactory;
        private readonly List<string> _names;

        public CarFactory(PartFactory partFactory)
        {
            _partFactory = partFactory;

            _names = new List<string>()
            {
                "Audi A4",
                "Лада Калина",
                "BMW X5",
                "Chevrolet Cruze",
                "Ford Focus",
                "Hyndai Solaris",
                "Lexus LX",
                "Honda Accord",
                "Mazda CX-5",
                "Nissan X-Trail",
                "Porshe 911",
                "Renault Logan",
                "Skoda Octavia",
                "Subaru Impreza",
                "Suzuki Vitara",
                "Toyota Corolla",
                "Volkswagen Golf",
                "Уаз Патриот",
                "Газ Волга",
                "Kia Ceed",
            };
        }

        public List<Car> CreateSeveralCars(int minCarCount = 5, int maxCarCount = 10)
        {
            List<Car> cars = new List<Car>();
            int someCarsCount = GenerateRandomNumber(minCarCount, maxCarCount + 1);

            for (int i = 0; i < someCarsCount; i++)
            {
                Car car = CreateSingleCar();
                cars.Add(car);
            }

            return cars;
        }

        public Car CreateSingleCar()
        {
            return new Car(_partFactory.CreateSeveral(), GenerateRandomName(_names));
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
        public string HealthStatus => IsNeedRepair == true ? "автомобиль неисправен" : "автомобиль в порядке";

        public bool ApplyRepair(Part part)
        {
            if (_parts.Contains(part))
            {
                ReplacePart(part);

                return true;
            }

            return false;
        }

        public void ShowInfo()
        {
            Print($"\n{Name}");
            Print($". Cостояние: ");
            Print($"{HealthStatus}", IsNeedRepair == true ? ConsoleColor.Red : ConsoleColor.Green);

            if (IsNeedRepair == true)
            {
                Print($". ");
                Print($"Неисправная деталь: ");
                Print($"{BrokenPartName}", ConsoleColor.Green);
            }

            Print($".");
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

        public static string GenerateRandomName(List<string> names)
        {
            return names[s_random.Next(0, names.Count)];
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
            Print($"{new string('-', symbolCount)}", color);
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
