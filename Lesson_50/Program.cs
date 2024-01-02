namespace Lesson_50
{
    using System.Linq;
    using static Display;
    using static Randomaizer;

    class Program
    {
        static void Main()
        {
            Console.Title = "Автосервис";

            List<Part> parts = new List<Part>()
            {
                new Engine(false),
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
                new Catalyst(true)
            };

            //Проверка класса - машина!
            #region Машина
            Car car = new Car(parts);
            Console.WriteLine($"Применить деталь: {car.TryAcceptRepair(null)}");
            Console.WriteLine($"Нужна ли починка? - {car.IsNeedRepair(out string brokenPartName)}");
            Console.WriteLine($"{brokenPartName}");
            Console.WriteLine($"\n----------------------------------\n");

            for (int i = 0; i < parts.Count; i++)
            {
                Print($"{i + 1}. {parts[i].ShowInfo()}\n");
            }

            Console.WriteLine($"\n----------------------------------");
            #endregion

            //Проверка класса склад!
            #region Склад
            PartsStock partsStock = new PartsStock();

            Console.WriteLine($"Запчастей на складе:\n");
            partsStock.ShowInfo();
            Console.WriteLine($"\n----------------------------------\n");

            PartType engine = PartType.Engine;
            PartType transmission = PartType.Transmission;

            for (int i = 0; i < 10; i++)
            {
                if (partsStock.TryGetPart(engine, out Part part))
                {
                    partsStock.TryGetPrice(engine, out int price);
                    Console.WriteLine($"{part.Name}");
                    Console.WriteLine($"Цена: {price}");
                }
                else
                {
                    Console.WriteLine($"Нет в наличии запчасти: {PartsDictionary.TryGetPartName(engine)}");
                }

                Console.WriteLine($"\n----------------------------------\n");

                if (partsStock.TryGetPart(transmission, out Part partT))
                {
                    partsStock.TryGetPrice(transmission, out int price);
                    Console.WriteLine($"{partT.Name}");
                    Console.WriteLine($"Цена: {price}");
                }
                else
                {
                    Console.WriteLine($"Нет в наличии запчасти: {PartsDictionary.TryGetPartName(transmission)}");
                }

                Console.WriteLine($"\n----------------------------------\n");
            }

            Console.WriteLine($"Запчастей на складе:\n");
            partsStock.ShowInfo();
            Console.WriteLine($"\n----------------------------------\n");
            #endregion

            //Проверка классов фабрик

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

    //Машина
    class Car : IRepairable
    {
        private List<Part> _parts;

        public Car(List<Part> parts)
        {
            _parts = parts;
        }

        //Вернуть может быть тип неисправной детали?!
        //- Не получится с текущим алгоритмом, так как деталь может быть Null -> будет ошибка во время выполнения
        public bool IsNeedRepair(out string brokenPartName)
        {
            Part brokenPart = GetBrokenPart();

            if (brokenPart != null)
            {
                brokenPartName = brokenPart.Name;
                return true;
            }

            brokenPartName = String.Empty;
            return false;
        }

        public bool TryAcceptRepair(Part part)
        {
            if (part != null && _parts.Contains(part) == true)
            {
                int index = _parts.IndexOf(part);
                _parts[index] = part;

                return true;
            }

            return false;
        }

        private Part GetBrokenPart()
        {
            return _parts.FirstOrDefault(part => part.IsBroken == true);
        }
    }

    //склад
    class PartsStock
    {
        private Dictionary<PartType, int> _pricesOfParts;
        private Dictionary<PartType, int> _partsCountsAvailable;
        private List<PartType> _partsTypes;

        public PartsStock()
        {
            _pricesOfParts = new Dictionary<PartType, int>()
            {
                {PartType.Engine, 1000},
                {PartType.Transmission, 850},
                {PartType.Wheel, 200},
                {PartType.Glass, 150},
                {PartType.Muffler,  100},
                {PartType.Brake,  100},
                {PartType.Suspension,  100},
                {PartType.Generator,  150},
                {PartType.AirConditioner,  300},
                {PartType.Starter,  200},
                {PartType.TimingBelt,  250},
                {PartType.WaterPump,  230},
                {PartType.GasTank,  350},
                {PartType.SteeringWheel,  450},
                {PartType.SteeringRack,  650},
                {PartType.PowerSteering,  500},
                {PartType.Dashboard,  700},
                {PartType.Wiring,  550},
                {PartType.Battery,  250},
                {PartType.SparkPlug,  100},
                {PartType.FuelPump,  300},
                {PartType.OilFilter,  180},
                {PartType.Crankshaft,  400},
                {PartType.Catalyst,  900},
            };

            _partsTypes = PartsDictionary.GetPartsTypesList();

            _partsCountsAvailable = FillParts();
        }

        //Возвращать надо запчасть
        public bool TryGetPart(PartType partType, out Part part)
        {
            _partsCountsAvailable.TryGetValue(partType, out int partCountAvailale);
            string partName = PartsDictionary.TryGetPartName(partType);

            if (partCountAvailale > 0)
            {
                part = PartsDictionary.TryGetPart(partName);
                AcceptToChangePartValue(partType);

                return true;
            }
            else
            {
                part = null;
                return false;
            }
        }

        //Цена должна возвращаться в числе
        public bool TryGetPrice(PartType partType, out int priceOfPart)
        {
            if (_pricesOfParts.TryGetValue(partType, out int price) == true)
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

        public void ShowInfo()
        {
            int index = 0;

            foreach (var part in _partsCountsAvailable)
            {
                int price;
                string partName = PartsDictionary.TryGetPartName(part.Key);
                _pricesOfParts.TryGetValue(part.Key, out price);

                Console.WriteLine($"{++index}. {partName} - {part.Value} штук. Цена: {price} руб. за 1 деталь.");
            }
        }

        private Dictionary<PartType, int> FillParts()
        {
            Dictionary<PartType, int> partsCountsAvailable = new Dictionary<PartType, int>();
            int minPartsCount = 0;
            int maxPartsCount = 10;

            for (int i = 0; i < _partsTypes.Count; i++)
            {
                partsCountsAvailable.Add(_partsTypes[i], GenerateRandomNumber(minPartsCount, maxPartsCount + 1));
            }

            return partsCountsAvailable;
        }

        //Это точно должно так быть?
        private void AcceptToChangePartValue(PartType partType)
        {
            _partsCountsAvailable.TryGetValue(partType, out int PartCount);
            PartCount--;
            _partsCountsAvailable.Remove(partType);
            _partsCountsAvailable.Add(partType, PartCount);
        }
    }

    #region Классы деталей

    abstract class Part : ICloneable
    {
        public Part(bool isBroken)
        {
            IsBroken = isBroken;
        }

        public PartType PartType { get => PartsDictionary.TryGetPartType(GetType()); }
        public string Name { get => PartsDictionary.TryGetPartName(PartType); }
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
        private static Dictionary<string, Part> s_Part;
        private static Dictionary<Part, string> s_PartsNames;
        private static Dictionary<PartType, string> s_PartsTypesNames;
        private static Dictionary<Type, PartType> s_PartsTypes;
        private static List<PartType> s_PartsTypesList;

        //Сделать методы для заполнения словарей -> облегчит добавление новых деталей в словари.
        static PartsDictionary()
        {
            s_Part = new Dictionary<string, Part>()
            {
                { "Двигатель", new Engine(false)},
                {"Трансмиссия" , new Transmission(false) },
                {"Колесо" , new Wheel(false) },
                {"Стекло" , new Glass(false) },
                {"Глушитель" , new Muffler(false) },
                {"Тормоз" , new Brake(false) },
                {"Подвеска" , new Suspension(false) },
                {"Генератор" , new Generator(false) },
                {"Кондиционер" , new AirConditioner(false) },
                {"Стартер" , new Starter(false) },
                {"ГРМ" , new TimingBelt(false) },
                {"Водяная помпа" , new WaterPump(false) },
                {"Бензобак" , new GasTank(false) },
                {"Руль" , new SteeringWheel(false) },
                {"Рулевая рейка" , new SteeringRack(false) },
                {"Усилитель руля" , new PowerSteering(false) },
                {"Приборная панель" , new Dashboard(false) },
                {"Электропроводка" , new Wiring(false) },
                {"Аккумулятор" , new Battery(false) },
                {"Свеча зажигания" , new SparkPlug(false) },
                {"Топливный насос" , new FuelPump(false) },
                {"Масляный фильтр" , new OilFilter(false) },
                {"Коленвал" , new Crankshaft(false) },
                {"Катализатор" , new Catalyst(false) }
            };

            s_PartsNames = new Dictionary<Part, string>()
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

            s_PartsTypesNames = new Dictionary<PartType, string>()
            {
                {PartType.Engine, "Двигатель"},
                {PartType.Transmission, "Трансмиссия" },
                {PartType.Wheel, "Колесо" },
                {PartType.Glass, "Стекло" },
                {PartType.Muffler, "Глушитель" },
                {PartType.Brake, "Тормоз" },
                {PartType.Suspension, "Подвеска" },
                {PartType.Generator, "Генератор" },
                {PartType.AirConditioner, "Кондиционер" },
                {PartType.Starter, "Стартер" },
                {PartType.TimingBelt, "ГРМ" },
                {PartType.WaterPump, "Водяная помпа" },
                {PartType.GasTank, "Бензобак" },
                {PartType.SteeringWheel, "Руль" },
                {PartType.SteeringRack, "Рулевая рейка" },
                {PartType.PowerSteering, "Усилитель руля" },
                {PartType.Dashboard, "Приборная панель" },
                {PartType.Wiring, "Электропроводка" },
                {PartType.Battery, "Аккумулятор" },
                {PartType.SparkPlug, "Свеча зажигания" },
                {PartType.FuelPump, "Топливный насос" },
                {PartType.OilFilter, "Масляный фильтр" },
                {PartType.Crankshaft, "Коленвал" },
                {PartType.Catalyst, "Катализатор" }
            };

            s_PartsTypes = new Dictionary<Type, PartType>()
            {
                {typeof(Engine), PartType.Engine},
                {typeof(Transmission), PartType.Transmission },
                {typeof(Wheel), PartType.Wheel },
                {typeof(Glass), PartType.Glass },
                {typeof(Muffler), PartType.Muffler },
                {typeof(Brake), PartType.Brake },
                {typeof(Suspension), PartType.Suspension },
                {typeof(Generator), PartType.Generator },
                {typeof(AirConditioner), PartType.AirConditioner },
                {typeof(Starter), PartType.Starter },
                {typeof(TimingBelt), PartType.TimingBelt },
                {typeof(WaterPump), PartType.WaterPump },
                {typeof(GasTank), PartType.GasTank },
                {typeof(SteeringWheel), PartType.SteeringWheel },
                {typeof(SteeringRack), PartType.SteeringRack},
                {typeof(PowerSteering), PartType.PowerSteering },
                {typeof(Dashboard), PartType.Dashboard },
                {typeof(Wiring), PartType.Wiring },
                {typeof(Battery), PartType.Battery },
                {typeof(SparkPlug), PartType.SparkPlug },
                {typeof(FuelPump), PartType.FuelPump },
                {typeof(OilFilter), PartType.OilFilter },
                {typeof(Crankshaft), PartType.Crankshaft },
                {typeof(Catalyst), PartType.Catalyst }
            };

            s_PartsTypesList = new List<PartType>();
            s_PartsTypesList.AddRange(s_PartsTypesNames.Keys);
        }

        public static PartType TryGetPartType(Type part)
        {
            s_PartsTypes.TryGetValue(part, out PartType detailsTypes);

            return detailsTypes;
        }

        public static string TryGetPartName(PartType partType)
        {
            if (s_PartsTypesNames.TryGetValue(partType, out string name) == true)
            {
                return name;
            }
            else
            {
                return "Деталь";
            }
        }

        public static List<PartType> GetPartsTypesList() => s_PartsTypesList;

        public static Part TryGetPart(string partName)
        {
            s_Part.TryGetValue(partName, out Part part);
            return part;
        }
    }

    #endregion

    #region Interfaces

    interface IRepairable
    {
        bool IsNeedRepair(out string brokenPartName);
        bool TryAcceptRepair(Part detail);
    }

    interface ICloneable
    {
        abstract Part Clone();
    }

    #endregion

    #region Enums

    enum PartType
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