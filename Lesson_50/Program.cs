namespace Lesson_50
{
    using System.Linq;
    using static Display;
    using static Randomaizer;

    class Program
    {
        static void Main()
        {
            List<Detail> details = new List<Detail>()
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
                new Catalyst(false),
            };

            Car car = new Car(details);
            Console.WriteLine($"{car.IsNeedRepair}");
            Console.WriteLine($"{car.TryGetNameBrokenDetail()}");
            Console.WriteLine($"----------------------------------");

            //for (int i = 0; i < details.Count; i++)
            //{
            //    Print($"{i + 1}. {details[i].ShowInfo()}\n");
            //}

            PartsStock partsStock = new PartsStock();
            partsStock.ShowInfo();

            Console.ReadKey();
        }
    }

    //Автосервис
    class CarService
    {
        private PartsStock _partsWarhouse;
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
        private DetailsFactory _detailsFactory;

        public CarFactory(DetailsFactory detailsFactory)
        {
            _detailsFactory = detailsFactory;
        }

        public Car Create()
        {
            return new Car(_detailsFactory.CreateSomeDetails());
        }
    }

    class DetailsFactory
    {
        private int _wheelsCount = 4;
        private int _glassesCount = 4;
        private int _minSparkesPlug = 4;
        private int _maxSparkesPlug = 12;
        private int _stepSparkesPlug = 2;

        public List<Detail> CreateSomeDetails()
        {
            List<Detail> details = new List<Detail>();

            return details;
        }
    }

    class Car : IRepairable
    {
        private List<Detail> _details;
        private Detail _brokenDetail;

        public Car(List<Detail> details)
        {
            _details = details;
        }

        public bool IsNeedRepair { get => _details.Contains(GetBrokenDetail()); }

        public string TryGetNameBrokenDetail()
        {
            if (_brokenDetail != null)
            {
                return _brokenDetail.Name;
            }

            return "Нет неисправных элементов в машине";
        }

        public bool TryAcceptRepair(Detail detail)
        {
            if (_details.Contains(detail) == true)
            {
                int index = _details.IndexOf(detail);
                _details[index] = detail;

                return true;
            }

            return false;
        }

        private Detail GetBrokenDetail()
        {
            _brokenDetail = _details.FirstOrDefault(detail => detail.IsBroken == true);

            return _brokenDetail;
        }
    }

    //склад
    class PartsStock
    {
        private Dictionary<DetailsTypes, int> _pricesOfDetails;
        private Dictionary<DetailsTypes, int> _detailsCounts;
        private List<DetailsTypes> _detailsTypes;

        public PartsStock()
        {
            _pricesOfDetails = new Dictionary<DetailsTypes, int>()
            {
                {DetailsTypes.Engine, 1000},
                {DetailsTypes.Transmission, 850},
                {DetailsTypes.Wheel, 200},
                {DetailsTypes.Glass, 150},
                {DetailsTypes.Muffler,  100},
                {DetailsTypes.Brake,  100},
                {DetailsTypes.Suspension,  100},
                {DetailsTypes.Generator,  150},
                {DetailsTypes.AirConditioner,  300},
                {DetailsTypes.Starter,  200},
                {DetailsTypes.TimingBelt,  250},
                {DetailsTypes.WaterPump,  230},
                {DetailsTypes.GasTank,  350},
                {DetailsTypes.SteeringWheel,  450},
                {DetailsTypes.SteeringRack,  650},
                {DetailsTypes.PowerSteering,  500},
                {DetailsTypes.Dashboard,  700},
                {DetailsTypes.Wiring,  550},
                {DetailsTypes.Battery,  250},
                {DetailsTypes.SparkPlug,  100},
                {DetailsTypes.FuelPump,  300},
                {DetailsTypes.OilFilter,  180},
                {DetailsTypes.Crankshaft,  400},
                {DetailsTypes.Catalyst,  900},
            };

            _detailsTypes = new List<DetailsTypes>()
            {
                DetailsTypes.Engine,
                DetailsTypes.Transmission,
                DetailsTypes.Wheel,
                DetailsTypes.Glass,
                DetailsTypes.Muffler,
                DetailsTypes.Brake,
                DetailsTypes.Suspension,
                DetailsTypes.Generator,
                DetailsTypes.AirConditioner,
                DetailsTypes.Starter,
                DetailsTypes.TimingBelt,
                DetailsTypes.WaterPump,
                DetailsTypes.GasTank,
                DetailsTypes.SteeringWheel,
                DetailsTypes.SteeringRack,
                DetailsTypes.PowerSteering,
                DetailsTypes.Dashboard,
                DetailsTypes.Wiring,
                DetailsTypes.Battery,
                DetailsTypes.SparkPlug,
                DetailsTypes.FuelPump,
                DetailsTypes.OilFilter,
                DetailsTypes.Crankshaft,
                DetailsTypes.Catalyst,
            };

            _detailsCounts = FillDetails();
        }

        public bool TryGetPrice(DetailsTypes detail, out int priceOfDetail)
        {
            if (_pricesOfDetails.TryGetValue(detail, out int price) == true)
            {
                priceOfDetail = price;

                return true;
            }
            else
            {
                priceOfDetail = 0;

                return false;
            }
        }

        public void ShowInfo()
        {
            foreach (var detail in _detailsCounts)
            {
                Console.WriteLine($"{detail.Key.ToString()} - {detail.Value}");
            }
        }

        private Dictionary<DetailsTypes, int> FillDetails()
        {
            Dictionary<DetailsTypes, int> detailsCounts = new Dictionary<DetailsTypes, int>();
            int minDetailCount = 0;
            int maxDetailCount = 10;

            for (int i = 0; i < _detailsTypes.Count; i++)
            {
                detailsCounts.Add(_detailsTypes[i], GenerateRandomNumber(minDetailCount, maxDetailCount + 1));
            }

            return detailsCounts;
        }
    }

    #region Классы деталей

    abstract class Detail : ICloneable
    {
        public Detail(bool isBroken)
        {
            IsBroken = isBroken;
        }

        public string Name { get => DetailsDictionary.TryGetName(GetType()); }
        public bool IsBroken { get; }
        public virtual string IsBrokenToString { get => IsBroken == true ? "не исправен" : "исправен"; }

        public abstract Detail Clone();

        public string ShowInfo()
        {
            return $"{Name}: [{IsBrokenToString}]";
        }
    }

    class Engine : Detail
    {
        public Engine(bool isBroken) : base(isBroken) { }

        public override Detail Clone() => new Engine(IsBroken);
    }

    class Transmission : Detail
    {
        public Transmission(bool isBroken) : base(isBroken) { }

        public override Detail Clone() => new Transmission(IsBroken);
    }

    class Wheel : Detail
    {
        public Wheel(bool isBroken) : base(isBroken) { }

        public override Detail Clone() => new Wheel(IsBroken);
    }

    class Glass : Detail
    {
        public Glass(bool isBroken) : base(isBroken) { }

        public override Detail Clone() => new Glass(IsBroken);
    }

    class Muffler : Detail
    {
        public Muffler(bool isBroken) : base(isBroken) { }

        public override Detail Clone() => new Muffler(IsBroken);
    }

    class Brake : Detail
    {
        public Brake(bool isBroken) : base(isBroken) { }

        public override Detail Clone() => new Brake(IsBroken);
    }

    class Suspension : Detail
    {
        public Suspension(bool isBroken) : base(isBroken) { }

        public override Detail Clone() => new Suspension(IsBroken);
    }

    class Generator : Detail
    {
        public Generator(bool isBroken) : base(isBroken) { }

        public override Detail Clone() => new Generator(IsBroken);
    }

    class AirConditioner : Detail
    {
        public AirConditioner(bool isBroken) : base(isBroken) { }

        public override Detail Clone() => new AirConditioner(IsBroken);
    }

    class Starter : Detail
    {
        public Starter(bool isBroken) : base(isBroken) { }

        public override Detail Clone() => new Starter(IsBroken);
    }

    class TimingBelt : Detail
    {
        public TimingBelt(bool isBroken) : base(isBroken) { }

        public override Detail Clone() => new TimingBelt(IsBroken);
    }

    class WaterPump : Detail
    {
        public WaterPump(bool isBroken) : base(isBroken) { }

        public override Detail Clone() => new WaterPump(IsBroken);
    }

    class GasTank : Detail
    {
        public GasTank(bool isBroken) : base(isBroken) { }

        public override Detail Clone() => new GasTank(IsBroken);
    }

    class SteeringWheel : Detail
    {
        public SteeringWheel(bool isBroken) : base(isBroken) { }

        public override Detail Clone() => new SteeringWheel(IsBroken);
    }

    class SteeringRack : Detail
    {
        public SteeringRack(bool isBroken) : base(isBroken) { }

        public override Detail Clone() => new SteeringRack(IsBroken);
    }

    class PowerSteering : Detail
    {
        public PowerSteering(bool isBroken) : base(isBroken) { }

        public override Detail Clone() => new PowerSteering(IsBroken);
    }

    class Dashboard : Detail
    {
        public Dashboard(bool isBroken) : base(isBroken) { }

        public override Detail Clone() => new Dashboard(IsBroken);
    }

    class Wiring : Detail
    {
        public Wiring(bool isBroken) : base(isBroken) { }

        public override Detail Clone() => new Wiring(IsBroken);
    }

    class Battery : Detail
    {
        public Battery(bool isBroken) : base(isBroken) { }

        public override Detail Clone() => new Battery(IsBroken);
    }

    class SparkPlug : Detail
    {
        public SparkPlug(bool isBroken) : base(isBroken) { }

        public override Detail Clone() => new SparkPlug(IsBroken);
    }

    class FuelPump : Detail
    {
        public FuelPump(bool isBroken) : base(isBroken) { }

        public override Detail Clone() => new FuelPump(IsBroken);
    }

    class OilFilter : Detail
    {
        public OilFilter(bool isBroken) : base(isBroken) { }

        public override Detail Clone() => new OilFilter(IsBroken);
    }

    class Crankshaft : Detail
    {
        public Crankshaft(bool isBroken) : base(isBroken) { }

        public override Detail Clone() => new Crankshaft(IsBroken);
    }

    class Catalyst : Detail
    {
        public Catalyst(bool isBroken) : base(isBroken) { }

        public override Detail Clone() => new Catalyst(IsBroken);
    }

    #endregion

    #region Словарь деталей

    #region Названия деталей

    // Двигатель            Engine              +
    // Трансмиссия          Transmission        +
    // Колесо               Wheel               +
    // Стекло               Glass               +
    // Глушитель            Muffler             +
    // Тормоз               Brake               +
    // Подвеска             Suspension          +
    // Генератор            Generator           +
    // Кондиционер          AirConditioner      +
    // Стартер              Starter             +
    // ГРМ                  TimingBelt          +
    // Водяная помпа        WaterPump           +
    // Бензобак             GasTank             +
    // Руль                 SteeringWheel       +
    // Рулевая рейка        SteeringRack        +
    // Усилитель руля       PowerSteering       +
    // Приборная панель     Dashboard           +
    // Электропроводка      Wiring              +
    // Аккумулятор          Battery             +
    // Свеча зажигания      SparkPlug           +
    // Топливный насос      FuelPump            +
    // Масляный фильтр      OilFilter           +
    // Коленвал             Crankshaft          +
    // Катализатор          Catalyst            +

    #endregion

    static class DetailsDictionary
    {
        private static Dictionary<Type, string> s_Details;

        static DetailsDictionary()
        {
            s_Details = new Dictionary<Type, string>()
            {
                {typeof(Engine), "Двигатель"},
                {typeof(Transmission), "Трансмиссия" },
                {typeof(Wheel), "Колесо" },
                {typeof(Glass), "Стекло" },
                {typeof(Muffler), "Глушитель" },
                {typeof(Brake), "Тормоз" },
                {typeof(Suspension), "Подвеска" },
                {typeof(Generator), "Генератор" },
                {typeof(AirConditioner), "Кондиционер" },
                {typeof(Starter), "Стартер" },
                {typeof(TimingBelt), "ГРМ" },
                {typeof(WaterPump), "Водяная помпа" },
                {typeof(GasTank), "Бензобак" },
                {typeof(SteeringWheel), "Руль" },
                {typeof(SteeringRack), "Рулевая рейка" },
                {typeof(PowerSteering), "Усилитель руля" },
                {typeof(Dashboard), "Приборная панель" },
                {typeof(Wiring), "Электропроводка" },
                {typeof(Battery), "Аккумулятор" },
                {typeof(SparkPlug), "Свеча зажигания" },
                {typeof(FuelPump), "Топливный насос" },
                {typeof(OilFilter), "Масляный фильтр" },
                {typeof(Crankshaft), "Коленвал" },
                {typeof(Catalyst), "Катализатор" },
            };
        }

        public static int DetailsCount => s_Details.Count;

        public static string TryGetName(Type detail)
        {
            if (s_Details.TryGetValue(detail, out string name) == true)
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

        string TryGetNameBrokenDetail();

        bool TryAcceptRepair(Detail detail);
    }

    interface ICloneable
    {
        abstract Detail Clone();
    }

    #endregion

    #region Enums

    enum DetailsTypes
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