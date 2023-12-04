namespace Lesson_50
{
    using static Randomaizer;
    using static Display;
    using static UserInput;

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
                new Catalyst(false)
            };


            for (int i = 0; i < details.Count; i++)
            {
                Print($"{i + 1}. {details[i].ShowInfo()}\n");
            }

            Console.ReadKey();
        }
    }

    //Автосервис
    class CarService : IRepairProvider
    {
        private PartsWarhouse _partsWarhouse;
        private int _moneyBalance;

        private void TryRepair(List<Detail> brokenDetails)
        {

        }

        private void PerformDiagnosis(IRepairable vihicle)
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
    }

    class Car : IRepairable
    {

    }

    //склад
    class PartsWarhouse
    {

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

        public abstract Detail Clone(bool isBroken);

        public string ShowInfo()
        {
            return $"{Name}. Состояние: {IsBrokenToString}";
        }
    }

    class Engine : Detail
    {
        public Engine(bool isBroken) : base(isBroken)
        {
        }

        public override Detail Clone(bool isBroken)
        {
            return new Engine(isBroken);
        }
    }

    class Transmission : Detail
    {
        public Transmission(bool isBroken) : base(isBroken)
        {
        }

        public override Detail Clone(bool isBroken)
        {
            return new Transmission(isBroken);
        }
    }

    class Wheel : Detail
    {
        public Wheel(bool isBroken) : base(isBroken)
        {
        }

        public override Detail Clone(bool isBroken)
        {
            return new Wheel(isBroken);
        }
    }

    class Glass : Detail
    {
        public Glass(bool isBroken) : base(isBroken)
        {
        }

        public override Detail Clone(bool isBroken)
        {
            return new Glass(isBroken);
        }
    }

    class Muffler : Detail
    {
        public Muffler(bool isBroken) : base(isBroken)
        {
        }

        public override Detail Clone(bool isBroken)
        {
            return new Muffler(isBroken);
        }
    }

    class Brake : Detail
    {
        public Brake(bool isBroken) : base(isBroken)
        {
        }

        public override Detail Clone(bool isBroken)
        {
            return new Brake(isBroken);
        }
    }

    class Suspension : Detail
    {
        public Suspension(bool isBroken) : base(isBroken)
        {
        }

        public override Detail Clone(bool isBroken)
        {
            return new Suspension(isBroken);
        }
    }

    class Generator : Detail
    {
        public Generator(bool isBroken) : base(isBroken)
        {
        }

        public override Detail Clone(bool isBroken)
        {
            return new Generator(isBroken);
        }
    }

    class AirConditioner : Detail
    {
        public AirConditioner(bool isBroken) : base(isBroken)
        {
        }

        public override Detail Clone(bool isBroken)
        {
            return new AirConditioner(isBroken);
        }
    }

    class Starter : Detail
    {
        public Starter(bool isBroken) : base(isBroken)
        {
        }

        public override Detail Clone(bool isBroken)
        {
            return new Starter(isBroken);
        }
    }

    class TimingBelt : Detail
    {
        public TimingBelt(bool isBroken) : base(isBroken)
        {
        }

        public override Detail Clone(bool isBroken)
        {
            return new TimingBelt(isBroken);
        }
    }

    class WaterPump : Detail
    {
        public WaterPump(bool isBroken) : base(isBroken)
        {
        }

        public override Detail Clone(bool isBroken)
        {
            return new WaterPump(isBroken);
        }
    }

    class GasTank : Detail
    {
        public GasTank(bool isBroken) : base(isBroken)
        {
        }

        public override Detail Clone(bool isBroken)
        {
            return new GasTank(isBroken);
        }
    }

    class SteeringWheel : Detail
    {
        public SteeringWheel(bool isBroken) : base(isBroken)
        {
        }

        public override Detail Clone(bool isBroken)
        {
            return new SteeringWheel(isBroken);
        }
    }

    class SteeringRack : Detail
    {
        public SteeringRack(bool isBroken) : base(isBroken)
        {
        }

        public override Detail Clone(bool isBroken)
        {
            return new SteeringRack(isBroken);
        }
    }

    class PowerSteering : Detail
    {
        public PowerSteering(bool isBroken) : base(isBroken)
        {
        }

        public override Detail Clone(bool isBroken)
        {
            return new PowerSteering(isBroken);
        }
    }

    class Dashboard : Detail
    {
        public Dashboard(bool isBroken) : base(isBroken)
        {
        }

        public override Detail Clone(bool isBroken)
        {
            return new Dashboard(isBroken);
        }
    }

    class Wiring : Detail
    {
        public Wiring(bool isBroken) : base(isBroken)
        {
        }

        public override Detail Clone(bool isBroken)
        {
            return new Wiring(isBroken);
        }
    }

    class Battery : Detail
    {
        public Battery(bool isBroken) : base(isBroken)
        {
        }

        public override Detail Clone(bool isBroken)
        {
            return new Battery(isBroken);
        }
    }

    class SparkPlug : Detail
    {
        public SparkPlug(bool isBroken) : base(isBroken)
        {
        }

        public override Detail Clone(bool isBroken)
        {
            return new SparkPlug(isBroken);
        }
    }

    class FuelPump : Detail
    {
        public FuelPump(bool isBroken) : base(isBroken)
        {
        }

        public override Detail Clone(bool isBroken)
        {
            return new FuelPump(isBroken);
        }
    }

    class OilFilter : Detail
    {
        public OilFilter(bool isBroken) : base(isBroken)
        {
        }

        public override Detail Clone(bool isBroken)
        {
            return new OilFilter(isBroken);
        }
    }

    class Crankshaft : Detail
    {
        public Crankshaft(bool isBroken) : base(isBroken)
        {
        }

        public override Detail Clone(bool isBroken)
        {
            return new Crankshaft(isBroken);
        }
    }

    class Catalyst : Detail
    {
        public Catalyst(bool isBroken) : base(isBroken)
        {
        }

        public override Detail Clone(bool isBroken)
        {
            return new Catalyst(isBroken);
        }
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

    }

    interface IRepairProvider
    {

    }

    interface ICloneable
    {
        abstract Detail Clone(bool isBroken);
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