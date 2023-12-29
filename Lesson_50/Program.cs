namespace Lesson_50
{
    using System.Linq;
    using static Display;
    using static Randomaizer;
    using static UserInput;

    class Program
    {
        static void Main()
        {
            List<Part> parts = new List<Part>()
            {
                new Engine(true),
                new Transmission(false),
                new Wheel(false),
                new Glass(false),
                new Muffler(false),
                new Brake(false),
                new Suspension(false),
                new Generator(false),
                new AirConditioner(false),
                new Starter(false),
                new TimingBelt(false),
                new WaterPump(false),
                new GasTank(false),
                new SteeringWheel(false),
                new SteeringRack(false),
                new PowerSteering(false),
                new Dashboard(false),
                new Wiring(false),
                new Battery(false),
                new SparkPlug(false),
                new FuelPump(false),
                new OilFilter(false),
                new Crankshaft(false),
                new Catalyst(false),
            };

            Car car = new Car(parts);
            Console.WriteLine($"{car.IsNeedRepair}");
            Console.WriteLine($"{car.TryGetNameBrokenPart()}");
            Console.WriteLine($"\n----------------------------------\n");

            for (int i = 0; i < parts.Count; i++)
            {
                Print($"{i + 1}. {parts[i].ShowInfo()}\n");
            }

            Console.WriteLine($"\n----------------------------------");

            PartsStock partsStock = new PartsStock();

            Console.WriteLine($"Запчастей на складе:\n");
            partsStock.ShowInfo();

            Console.ReadKey();
        }
    }

    //Автосервис
    class CarService
    {
        private PartsStock _partsStock;
        private int _moneyBalance;

        private void TryRepair(IRepairable car)
        {

        }

        public void Work()
        {

        }
    }

    class Client
    {
        private int _money;
        private Car _car;

        public IRepairable GiveCar()
        {
            return _car;
        }
    }

    class CarFactory
    {
        private PartsFactory _partsFactory;

        public CarFactory(PartsFactory partsFactory)
        {
            _partsFactory = partsFactory;
        }

        public Car Create()
        {
            return new Car(_partsFactory.CreateSomeDetails());
        }
    }

    class PartsFactory
    {
        private int _wheelsCount = 4;
        private int _glassesCount = 4;
        private int _minSparkesPlug = 4;
        private int _maxSparkesPlug = 12;
        private int _stepSparkesPlug = 2;

        public List<Part> CreateSomeDetails()
        {
            List<Part> parts = new List<Part>();

            return parts;
        }
    }

    class Car : IRepairable
    {
        private List<Part> _parts;

        public Car(List<Part> parts)
        {
            _parts = parts;
        }

        public bool IsNeedRepair { get => _parts.Contains(TryGetBrokenPart()); }

        public string TryGetNameBrokenPart()
        {
            Part part = TryGetBrokenPart();

            if (part != null)
            {
                return part.Name;
            }

            return "Нет неисправных деталей в машине";
        }

        public bool TryAcceptRepair(Part part)
        {
            if (_parts.Contains(part) == true)
            {
                int index = _parts.IndexOf(part);
                _parts[index] = part;

                return true;
            }

            return false;
        }

        private Part TryGetBrokenPart()
        {
            return _parts.FirstOrDefault(part => part.IsBroken == true);
        }
    }

    //склад
    class PartsStock
    {
        private Dictionary<PartsTypes, int> _pricesOfParts;
        private Dictionary<PartsTypes, int> _partsAvailable;
        private List<PartsTypes> _partsTypes;

        public PartsStock()
        {
            _pricesOfParts = new Dictionary<PartsTypes, int>()
            {
                {PartsTypes.Engine, 1000},
                {PartsTypes.Transmission, 850},
                {PartsTypes.Wheel, 200},
                {PartsTypes.Glass, 150},
                {PartsTypes.Muffler,  100},
                {PartsTypes.Brake,  100},
                {PartsTypes.Suspension,  100},
                {PartsTypes.Generator,  150},
                {PartsTypes.AirConditioner,  300},
                {PartsTypes.Starter,  200},
                {PartsTypes.TimingBelt,  250},
                {PartsTypes.WaterPump,  230},
                {PartsTypes.GasTank,  350},
                {PartsTypes.SteeringWheel,  450},
                {PartsTypes.SteeringRack,  650},
                {PartsTypes.PowerSteering,  500},
                {PartsTypes.Dashboard,  700},
                {PartsTypes.Wiring,  550},
                {PartsTypes.Battery,  250},
                {PartsTypes.SparkPlug,  100},
                {PartsTypes.FuelPump,  300},
                {PartsTypes.OilFilter,  180},
                {PartsTypes.Crankshaft,  400},
                {PartsTypes.Catalyst,  900},
            };

            //Получать список типов из Словаря
            _partsTypes = new List<PartsTypes>()
            {
                PartsTypes.Engine,
                PartsTypes.Transmission,
                PartsTypes.Wheel,
                PartsTypes.Glass,
                PartsTypes.Muffler,
                PartsTypes.Brake,
                PartsTypes.Suspension,
                PartsTypes.Generator,
                PartsTypes.AirConditioner,
                PartsTypes.Starter,
                PartsTypes.TimingBelt,
                PartsTypes.WaterPump,
                PartsTypes.GasTank,
                PartsTypes.SteeringWheel,
                PartsTypes.SteeringRack,
                PartsTypes.PowerSteering,
                PartsTypes.Dashboard,
                PartsTypes.Wiring,
                PartsTypes.Battery,
                PartsTypes.SparkPlug,
                PartsTypes.FuelPump,
                PartsTypes.OilFilter,
                PartsTypes.Crankshaft,
                PartsTypes.Catalyst,
            };

            _partsAvailable = FillDetails();
        }

        public bool TryGetPrice(PartsTypes part, out int priceOfPart)
        {
            if (_pricesOfParts.TryGetValue(part, out int price) == true)
            {
                priceOfPart = price;

                return true;
            }
            else
            {
                priceOfPart = 0;

                return false;
            }
        }

        //Подумать как сделать по другому, пока не нравится
        public void ShowInfo()
        {
            int index = 0;

            foreach (var part in _partsAvailable)
            {
                int price;
                string partName = PartsDictionary.TryGetName(part.Key);
                _pricesOfParts.TryGetValue(part.Key, out price);

                Console.WriteLine($"{++index}. {partName} - {part.Value} штук. Цена: {price} руб. за 1 деталь.");
            }
        }

        private Dictionary<PartsTypes, int> FillDetails()
        {
            Dictionary<PartsTypes, int> partsCounts = new Dictionary<PartsTypes, int>();
            int minPartsCount = 0;
            int maxPartsCount = 10;

            for (int i = 0; i < _partsTypes.Count; i++)
            {
                partsCounts.Add(_partsTypes[i], GenerateRandomNumber(minPartsCount, maxPartsCount + 1));
            }

            return partsCounts;
        }
    }

    #region Классы деталей

    abstract class Part : ICloneable
    {
        public Part(bool isBroken)
        {
            IsBroken = isBroken;
        }

        public PartsTypes partType { get => PartsDictionary.TryGetDetailType(GetType()); }
        public string Name { get => PartsDictionary.TryGetName(partType); }
        public bool IsBroken { get; }
        public virtual string IsBrokenToString { get => IsBroken == true ? "не исправен" : "исправен"; }

        public abstract Part Clone();

        public string ShowInfo()
        {
            return $"{Name}: [{IsBrokenToString}]";
        }
    }

    class Engine : Part
    {
        public Engine(bool isBroken) : base(isBroken) { }

        public override Part Clone() => new Engine(IsBroken);
    }

    class Transmission : Part
    {
        public Transmission(bool isBroken) : base(isBroken) { }

        public override Part Clone() => new Transmission(IsBroken);
    }

    class Wheel : Part
    {
        public Wheel(bool isBroken) : base(isBroken) { }

        public override Part Clone() => new Wheel(IsBroken);
    }

    class Glass : Part
    {
        public Glass(bool isBroken) : base(isBroken) { }

        public override Part Clone() => new Glass(IsBroken);
    }

    class Muffler : Part
    {
        public Muffler(bool isBroken) : base(isBroken) { }

        public override Part Clone() => new Muffler(IsBroken);
    }

    class Brake : Part
    {
        public Brake(bool isBroken) : base(isBroken) { }

        public override Part Clone() => new Brake(IsBroken);
    }

    class Suspension : Part
    {
        public Suspension(bool isBroken) : base(isBroken) { }

        public override Part Clone() => new Suspension(IsBroken);
    }

    class Generator : Part
    {
        public Generator(bool isBroken) : base(isBroken) { }

        public override Part Clone() => new Generator(IsBroken);
    }

    class AirConditioner : Part
    {
        public AirConditioner(bool isBroken) : base(isBroken) { }

        public override Part Clone() => new AirConditioner(IsBroken);
    }

    class Starter : Part
    {
        public Starter(bool isBroken) : base(isBroken) { }

        public override Part Clone() => new Starter(IsBroken);
    }

    class TimingBelt : Part
    {
        public TimingBelt(bool isBroken) : base(isBroken) { }

        public override Part Clone() => new TimingBelt(IsBroken);
    }

    class WaterPump : Part
    {
        public WaterPump(bool isBroken) : base(isBroken) { }

        public override Part Clone() => new WaterPump(IsBroken);
    }

    class GasTank : Part
    {
        public GasTank(bool isBroken) : base(isBroken) { }

        public override Part Clone() => new GasTank(IsBroken);
    }

    class SteeringWheel : Part
    {
        public SteeringWheel(bool isBroken) : base(isBroken) { }

        public override Part Clone() => new SteeringWheel(IsBroken);
    }

    class SteeringRack : Part
    {
        public SteeringRack(bool isBroken) : base(isBroken) { }

        public override Part Clone() => new SteeringRack(IsBroken);
    }

    class PowerSteering : Part
    {
        public PowerSteering(bool isBroken) : base(isBroken) { }

        public override Part Clone() => new PowerSteering(IsBroken);
    }

    class Dashboard : Part
    {
        public Dashboard(bool isBroken) : base(isBroken) { }

        public override Part Clone() => new Dashboard(IsBroken);
    }

    class Wiring : Part
    {
        public Wiring(bool isBroken) : base(isBroken) { }

        public override Part Clone() => new Wiring(IsBroken);
    }

    class Battery : Part
    {
        public Battery(bool isBroken) : base(isBroken) { }

        public override Part Clone() => new Battery(IsBroken);
    }

    class SparkPlug : Part
    {
        public SparkPlug(bool isBroken) : base(isBroken) { }

        public override Part Clone() => new SparkPlug(IsBroken);
    }

    class FuelPump : Part
    {
        public FuelPump(bool isBroken) : base(isBroken) { }

        public override Part Clone() => new FuelPump(IsBroken);
    }

    class OilFilter : Part
    {
        public OilFilter(bool isBroken) : base(isBroken) { }

        public override Part Clone() => new OilFilter(IsBroken);
    }

    class Crankshaft : Part
    {
        public Crankshaft(bool isBroken) : base(isBroken) { }

        public override Part Clone() => new Crankshaft(IsBroken);
    }

    class Catalyst : Part
    {
        public Catalyst(bool isBroken) : base(isBroken) { }

        public override Part Clone() => new Catalyst(IsBroken);
    }

    #endregion

    #region Словарь деталей 

    static class PartsDictionary
    {
        private static Dictionary<Part, string> s_Parts;
        private static Dictionary<PartsTypes, string> s_PartsNames;
        private static Dictionary<Type, PartsTypes> s_PartsTypes;

        //Сделать методы для заполнения словарей -> облегчит добавление новых деталей в словари.
        static PartsDictionary()
        {
            s_Parts = new Dictionary<Part, string>()
            {
                {new Engine(false), "Двигатель"},
                {new Transmission(false), "Трансмиссия" },
                {new Wheel(false), "Колесо" },
                {new Glass(false), "Стекло" },
                {new Muffler(false), "Глушитель" },
                {new Brake(false), "Тормоз" },
                {new Suspension(false), "Подвеска" },
                {new Generator(false), "Генератор" },
                {new AirConditioner(false), "Кондиционер" },
                {new Starter(false), "Стартер" },
                {new TimingBelt(false), "ГРМ" },
                {new WaterPump(false), "Водяная помпа" },
                {new GasTank(false), "Бензобак" },
                {new SteeringWheel(false), "Руль" },
                {new SteeringRack(false), "Рулевая рейка" },
                {new PowerSteering(false), "Усилитель руля" },
                {new Dashboard(false), "Приборная панель" },
                {new Wiring(false), "Электропроводка" },
                {new Battery(false), "Аккумулятор" },
                {new SparkPlug(false), "Свеча зажигания" },
                {new FuelPump(false), "Топливный насос" },
                {new OilFilter(false), "Масляный фильтр" },
                {new Crankshaft(false), "Коленвал" },
                {new Catalyst(false), "Катализатор" }
            };

            s_PartsNames = new Dictionary<PartsTypes, string>()
            {
                {PartsTypes.Engine, "Двигатель"},
                {PartsTypes.Transmission, "Трансмиссия" },
                {PartsTypes.Wheel, "Колесо" },
                {PartsTypes.Glass, "Стекло" },
                {PartsTypes.Muffler, "Глушитель" },
                {PartsTypes.Brake, "Тормоз" },
                {PartsTypes.Suspension, "Подвеска" },
                {PartsTypes.Generator, "Генератор" },
                {PartsTypes.AirConditioner, "Кондиционер" },
                {PartsTypes.Starter, "Стартер" },
                {PartsTypes.TimingBelt, "ГРМ" },
                {PartsTypes.WaterPump, "Водяная помпа" },
                {PartsTypes.GasTank, "Бензобак" },
                {PartsTypes.SteeringWheel, "Руль" },
                {PartsTypes.SteeringRack, "Рулевая рейка" },
                {PartsTypes.PowerSteering, "Усилитель руля" },
                {PartsTypes.Dashboard, "Приборная панель" },
                {PartsTypes.Wiring, "Электропроводка" },
                {PartsTypes.Battery, "Аккумулятор" },
                {PartsTypes.SparkPlug, "Свеча зажигания" },
                {PartsTypes.FuelPump, "Топливный насос" },
                {PartsTypes.OilFilter, "Масляный фильтр" },
                {PartsTypes.Crankshaft, "Коленвал" },
                {PartsTypes.Catalyst, "Катализатор" }
            };

            s_PartsTypes = new Dictionary<Type, PartsTypes>()
            {
                {typeof(Engine), PartsTypes.Engine},
                {typeof(Transmission), PartsTypes.Transmission },
                {typeof(Wheel), PartsTypes.Wheel },
                {typeof(Glass), PartsTypes.Glass },
                {typeof(Muffler), PartsTypes.Muffler },
                {typeof(Brake), PartsTypes.Brake },
                {typeof(Suspension), PartsTypes.Suspension },
                {typeof(Generator), PartsTypes.Generator },
                {typeof(AirConditioner), PartsTypes.AirConditioner },
                {typeof(Starter), PartsTypes.Starter },
                {typeof(TimingBelt), PartsTypes.TimingBelt },
                {typeof(WaterPump), PartsTypes.WaterPump },
                {typeof(GasTank), PartsTypes.GasTank },
                {typeof(SteeringWheel), PartsTypes.SteeringWheel },
                {typeof(SteeringRack), PartsTypes.SteeringRack},
                {typeof(PowerSteering), PartsTypes.PowerSteering },
                {typeof(Dashboard), PartsTypes.Dashboard },
                {typeof(Wiring), PartsTypes.Wiring },
                {typeof(Battery), PartsTypes.Battery },
                {typeof(SparkPlug), PartsTypes.SparkPlug },
                {typeof(FuelPump), PartsTypes.FuelPump },
                {typeof(OilFilter), PartsTypes.OilFilter },
                {typeof(Crankshaft), PartsTypes.Crankshaft },
                {typeof(Catalyst), PartsTypes.Catalyst }
            };
        }

        public static int PartsCount => s_Parts.Count;

        internal static PartsTypes TryGetDetailType(Type part)
        {
            s_PartsTypes.TryGetValue(part, out PartsTypes detailsTypes);

            return detailsTypes;
        }

        internal static string TryGetName(PartsTypes partType)
        {
            if (s_PartsNames.TryGetValue(partType, out string name) == true)
            {
                return name;
            }
            else
            {
                return "Деталь";
            }
        }
    }

    #endregion

    #region Interfaces

    interface IRepairable
    {
        bool IsNeedRepair { get; }

        string TryGetNameBrokenPart();

        bool TryAcceptRepair(Part detail);
    }

    interface ICloneable
    {
        abstract Part Clone();
    }

    #endregion

    #region Enums

    enum PartsTypes
    {
        Engine,
        Transmission,
        Wheel,
        Glass,
        Muffler,
        Brake,
        Suspension,
        Generator,
        AirConditioner,
        Starter,
        TimingBelt,
        WaterPump,
        GasTank,
        SteeringWheel,
        SteeringRack,
        PowerSteering,
        Dashboard,
        Wiring,
        Battery,
        SparkPlug,
        FuelPump,
        OilFilter,
        Crankshaft,
        Catalyst
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

    #endregion
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