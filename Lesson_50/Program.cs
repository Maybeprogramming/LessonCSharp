namespace Lesson_50
{
    using System;
    using static Display;
    using static Randomaizer;
    using static UserInput;

    class Program
    {
        static void Main()
        {
            Console.Title = "Автосервис";

            CarService carService = new CarService();
            carService.Work();

            WaitToPressKey();
        }
    }

    class CarService
    {
        private PartsStock _partsStock;
        private int _minMoneyBalance;
        private int _maxMoneyBalance;
        private int _moneyBalance;
        private int _minClientCount;
        private int _maxClientsCount;
        private Queue<Client> _clients;
        private ClientFactory _clientFactory;
        private int _fineForRefusal;

        private Dictionary<PartType, int> _pricesOfParts;
        private Dictionary<PartType, int> _pricesForJob;

        public CarService()
        {
            _fineForRefusal = 500;
            _minMoneyBalance = 1000;
            _maxMoneyBalance = 3000;
            _moneyBalance = GenerateRandomNumber(_minMoneyBalance, _maxMoneyBalance);

            _minClientCount = 5;
            _maxClientsCount = 10;
            _clientFactory = new(_minClientCount, _maxClientsCount);

            _clients = _clientFactory.CreateQueue();
            _partsStock = new();

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

            _pricesForJob = new Dictionary<PartType, int>()
            {
                {PartType.Engine, 200},
                {PartType.Transmission, 85},
                {PartType.Wheel, 20},
                {PartType.Glass, 15},
                {PartType.Muffler,  10},
                {PartType.Brake,  10},
                {PartType.Suspension,  10},
                {PartType.Generator,  15},
                {PartType.AirConditioner,  30},
                {PartType.Starter,  20},
                {PartType.TimingBelt,  25},
                {PartType.WaterPump,  20},
                {PartType.GasTank,  35},
                {PartType.SteeringWheel,  45},
                {PartType.SteeringRack,  65},
                {PartType.PowerSteering,  50},
                {PartType.Dashboard,  70},
                {PartType.Wiring,  55},
                {PartType.Battery,  25},
                {PartType.SparkPlug,  10},
                {PartType.FuelPump,  30},
                {PartType.OilFilter,  15},
                {PartType.Crankshaft,  40},
                {PartType.Catalyst,  90},
            };
        }

        public void Work()
        {
            const string RefuseCommand = "1";
            const string AutoRepairCommand = "2";
            const string ManualRepairCommand = "3";
            const string ShowPartStockCommand = "4";
            const string ExitCommand = "5";

            bool isRun = true;
            string userInput;

            ConsoleColor numberMenuColor = ConsoleColor.DarkYellow;

            while (_clients.Count > 0 && isRun == true)
            {
                Console.Clear();
                Print($"Добро пожаловать в наш автосервис: \"Мастер на все руки\"!\n", ConsoleColor.Cyan);

                ShowBalance(_moneyBalance);
                ShowClientsNumbersInQueue();

                Client currentClient = _clients?.Dequeue();
                IRepairable currentCar = currentClient.GiveCar();

                ShowBrokenPartInCar(currentCar);

                Print($"\nДоступные функции:", ConsoleColor.Green);
                Print($"\n{RefuseCommand}", numberMenuColor);
                Print($" - Отказать в ремонте автомобиля");
                Print($"\n{AutoRepairCommand}", numberMenuColor);
                Print($" - Отдать машину для ремонт слесарю");
                Print($"\n{ManualRepairCommand}", numberMenuColor);
                Print($" - Выбрать деталь и отремонтировать самостоятельно");
                Print($"\n{ShowPartStockCommand}", numberMenuColor);
                Print($" - Посмотреть остатки деталей на складе");
                Print($"\n{ExitCommand}", numberMenuColor);
                Print($" - Выйти из программы", ConsoleColor.Red);
                Print($"\nВведите номер команды: ", ConsoleColor.Green);

                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case RefuseCommand:
                        RefuseToRepairCar();
                        break;

                    case AutoRepairCommand:
                        RepairCarAuto(currentCar);
                        break;

                    case ManualRepairCommand:
                        RepairCarManual(currentCar);
                        break;

                    case ShowPartStockCommand:
                        _partsStock.ShowInfo();
                        break;

                    case ExitCommand:
                        Print($"\nРабота автосервиса завершена, программа выключается!", ConsoleColor.Green);
                        isRun = false;
                        break;

                    default:
                        Print($"\nТакой команды нет, попробуйте снова!", ConsoleColor.DarkRed);
                        break;
                }
                                
                WaitToPressKey();
            }

            PrintLine();
            ShowClientsNumbersInQueue();
        }

        private void ShowBrokenPartInCar(IRepairable currentCar)
        {
            Print($"Статус текущей машины: ");
            Print($"{currentCar.HealthStatus}", currentCar.IsNeedRepair == true ? ConsoleColor.Red : ConsoleColor.Green);
            Print($"\nНеисправная деталь: - ");
            Print($"{currentCar.BrokenPartName}\n", ConsoleColor.Green);
            PrintLine();
        }

        private void RepairCarManual(IRepairable currentCar)
        {
            Print($"Отремонтировать машину в ручную\n");
        }

        private void RepairCarAuto(IRepairable currentCar)
        {
            int minChanceWrongJob = 0;
            int maxChanceWrongJob = 30;
            int maxScaleChanceToDoJob = 100;
            int currentChanceToDoJob = GenerateRandomNumber(minChanceWrongJob, maxScaleChanceToDoJob);

            Print($"\nСлесарь сервиса принялся за ремонт машины");

            if (currentChanceToDoJob > maxChanceWrongJob)
            {
                Part goodPart;
                string brokenPartClassName = currentCar.BrokenPartClassName;
                PartType brokenPartType = PartsDictionary.TryGetTypePartByName(brokenPartClassName);
                goodPart = PartsDictionary.TryGetPartByType(brokenPartType);

                currentCar.TryAcceptRepair(goodPart);

                Print($"\nБыла заменена неисправная деталь: {goodPart.Name}");
                Print($"\nСтатус машины: {currentCar.HealthStatus}");
                Print($"\nНеисправность: {currentCar.BrokenPartName}");
            }
            else
            {
                List<PartType> partsTypes = PartsDictionary.GetPartsTypesToList();
                int indexNumber = GenerateRandomNumber(0, partsTypes.Count);
                PartType somePartType = partsTypes[indexNumber];
                Part wrongPart = PartsDictionary.TryGetPartByType(somePartType);

                currentCar.TryAcceptRepair(wrongPart);

                Print($"\nБыла заменена деталь: {wrongPart.Name}");
                Print($"\nСтатус машины: {currentCar.HealthStatus}");
                Print($"\nНеисправность: {currentCar.BrokenPartName}");

                Print("\nХе-хой, ой, лaять на баян...");
            }
        }

        private void RefuseToRepairCar()
        {
            _moneyBalance -= _fineForRefusal;

            Print($"\nВы отказались ремонтировать автомобиль");
            Print($"\nВам пришлось оплатить штраф за отказ: ");
            Print($"{_fineForRefusal}", ConsoleColor.Red);
            Print($" рублей");            
        }

        private void ShowClientsNumbersInQueue()
        {
            Print($"Клиентов в очереди на ремонт: ");
            Print($"{_clients.Count}\n", ConsoleColor.Green);
            PrintLine();
        }

        private void ShowBalance(int moneyBalance)
        {
            Print($"\nБаланс на счёте автосервиса: ");
            Print($"{moneyBalance}", ConsoleColor.Green);
            Print($" рублей\n");
            PrintLine();
        }

        private void TryRepair(IRepairable car)
        {

        }

        private void ShowInfo()
        {

        }

        private int TryGetPriceOfPart(PartType partType)
        {
            if (_pricesOfParts.TryGetValue(partType, out int price) == true)
            {
                return price;
            }

            return 0;
        }
    }

    #region Factories Classes
    class ClientFactory
    {
        private int _minClientsCount;
        private int _maxClientsCount;

        public ClientFactory(int minClientsCount, int maxClientsCount)
        {
            _minClientsCount = minClientsCount;
            _maxClientsCount = maxClientsCount;
        }

        public Queue<Client> CreateQueue()
        {
            int someClientsCount = GenerateRandomNumber(_minClientsCount, _maxClientsCount + 1);
            Queue<Client> clients = new();
            PartsFactory partsFactory = new();
            CarFactory carFactory = new(partsFactory);

            for (int i = 0; i < someClientsCount; i++)
            {
                Car car = carFactory.Create();
                Client client = new(car);
                clients.Enqueue(client);
            }

            return clients;
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
            return new Car(_partsFactory.CreateSomeParts());
        }
    }

    class PartsFactory
    {
        List<PartType> _somePartsTypes;

        public List<Part> CreateSomeParts()
        {
            _somePartsTypes = CreateSomePartsTypes();
            List<Part> parts = new();
            bool isBrokenPart = false;
            Part part;

            for (int i = 0; i < _somePartsTypes.Count; i++)
            {
                part = PartsDictionary.TryGetPartByType(_somePartsTypes[i]).Clone(isBrokenPart);
                parts.Add(part);
            }

            CreateOneRandomBrokenPart(parts);

            return parts;
        }

        private void CreateOneRandomBrokenPart(List<Part> parts)
        {
            int brokenPartIndex = GenerateRandomNumber(0, parts.Count);
            bool isBrokenPart = true;
            parts[brokenPartIndex] = parts[brokenPartIndex].Clone(isBrokenPart);
        }

        private List<PartType> CreateSomePartsTypes()
        {
            int minPartsTypesCount = 5;
            int maxPartsTypesCount = 10;
            int somePartsTypesCount = GenerateRandomNumber(minPartsTypesCount, maxPartsTypesCount + 1);
            List<PartType> allPartsTypes = new(PartsDictionary.GetPartsTypesToList());
            List<PartType> somePartsTypes = new();
            PartType tempPartType;

            for (int i = 0; i < somePartsTypesCount; i++)
            {
                int indexNumber = GenerateRandomNumber(0, allPartsTypes.Count - i);
                somePartsTypes.Add(allPartsTypes[indexNumber]);

                tempPartType = allPartsTypes[allPartsTypes.Count - 1];
                allPartsTypes[allPartsTypes.Count - 1] = allPartsTypes[indexNumber];
                allPartsTypes[indexNumber] = tempPartType;
            }

            return somePartsTypes;
        }
    }
    #endregion

    class Client
    {
        private Car _car;

        public Client(Car car)
        {
            _car = car;
        }

        public IRepairable GiveCar()
        {
            return _car;
        }
    }

    class Car : IRepairable
    {
        private List<Part> _parts;

        public Car(List<Part> parts)
        {
            _parts = parts;
        }

        public string HealthStatus  => 
            GetBrokenPart() != null ? "Требуется ремонт" : "В рабочем состоянии";

        public bool IsNeedRepair => 
            GetBrokenPart() != null;

        public string BrokenPartName  => 
            GetBrokenPart() != null ? GetBrokenPart().Name : "Неисправных деталей нет";

        public string BrokenPartClassName => 
            GetBrokenPart() != null ? GetBrokenPart().GetType().Name : "Empty";

        public bool TryAcceptRepair(Part part)
        {
            if (part == null)
            {
                Print($"Детали не существует!\n", ConsoleColor.Red);
                return false;
            }

            if (_parts.Contains(part) == true)
            {
                int index = _parts.IndexOf(part);
                _parts[index] = part;

                return true;
            }

            return false;
        }

        public void ShowInfo()
        {
            ConsoleColor statusColor = IsNeedRepair == true ? ConsoleColor.Red : ConsoleColor.Green;

            Print($"\nСостояние машины: ");
            Print($"{HealthStatus}.", statusColor);
            Print($"Неисправная деталь: ");
            Print($"{GetBrokenPart()?.Name}.");
        }

        private Part GetBrokenPart()
        {
            return _parts.FirstOrDefault(part => part.IsBroken == true);
        }
    }

    class PartsStock
    {
        private Dictionary<PartType, int> _partsCountsAvailable;
        private List<PartType> _partsTypes;

        public PartsStock()
        {
            _partsTypes = PartsDictionary.GetPartsTypesToList();

            _partsCountsAvailable = FillParts();
        }

        public bool IsPartAvaible(PartType partType)
        {
            _partsCountsAvailable.TryGetValue(partType, out int partCountAvailale);

            if (partCountAvailale > 0)
            {
                return true;
            }

            return false;
        }

        public Part TryGetPart(PartType partType)
        {
            if (IsPartAvaible(partType) == true)
            {
                Part part = PartsDictionary.TryGetPartByType(partType);
                AcceptToChangePartValue(partType);

                return part;
            }

            return null;
        }

        public void ShowInfo()
        {
            int index = 0;

            Print($"\nОстатки запчастей на складе:", ConsoleColor.Green);

            foreach (var part in _partsCountsAvailable)
            {
                string partName = PartsDictionary.TryGetPartNameByType(part.Key);

                Print($"\n{++index}. {partName} - [");
                Print($"{part.Value}", ConsoleColor.Green);
                Print($"] штук.");
            }

            Print("\n");
            PrintLine();
        }

        private Dictionary<PartType, int> FillParts()
        {
            Dictionary<PartType, int> partsCountsAvailable = new();
            int minPartsCount = 0;
            int maxPartsCount = 10;

            for (int i = 0; i < _partsTypes.Count; i++)
            {
                partsCountsAvailable.Add(_partsTypes[i], GenerateRandomNumber(minPartsCount, maxPartsCount + 1));
            }

            return partsCountsAvailable;
        }

        private void AcceptToChangePartValue(PartType partType)
        {
            _partsCountsAvailable.TryGetValue(partType, out int PartCount);
            PartCount--;
            _partsCountsAvailable.Remove(partType);
            _partsCountsAvailable.Add(partType, PartCount);
        }
    }

    #region Part Classes

    abstract class Part : ICloneable, IEquatable<Part>
    {
        public Part(bool isBroken)
        {
            IsBroken = isBroken;
        }

        public PartType PartType { get => PartsDictionary.TryGetTypePartByName(GetType().Name); }
        public string Name { get => PartsDictionary.TryGetPartNameByType(PartType); }
        public bool IsBroken { get; }
        public virtual string IsBrokenToString { get => IsBroken == true ? "не исправен" : "исправен"; }

        public abstract Part Clone(bool isBroken);

        public bool Equals(Part? otherPart)
        {
            if (otherPart == null)
            {
                return false;
            }
            else if (Name.Equals(otherPart.Name) == true && PartType == otherPart.PartType)
            {
                return true;
            }

            return false;
        }

        public override bool Equals(object? objectOther) => objectOther is Part otherPart && Equals(otherPart);

        public override int GetHashCode() => Name.GetHashCode();

        public string ShowInfo()
        {
            return $"{Name}: [{IsBrokenToString}]";
        }
    }

    class Engine : Part
    {
        public Engine(bool isBroken) : base(isBroken) { }

        public override Part Clone(bool isBroken) => new Engine(isBroken);
    }

    class Transmission : Part
    {
        public Transmission(bool isBroken) : base(isBroken) { }

        public override Part Clone(bool isBroken) => new Transmission(isBroken);
    }

    class Wheel : Part
    {
        public Wheel(bool isBroken) : base(isBroken) { }

        public override Part Clone(bool isBroken) => new Wheel(isBroken);
    }

    class Glass : Part
    {
        public Glass(bool isBroken) : base(isBroken) { }

        public override Part Clone(bool isBroken) => new Glass(isBroken);
    }

    class Muffler : Part
    {
        public Muffler(bool isBroken) : base(isBroken) { }

        public override Part Clone(bool isBroken) => new Muffler(isBroken);
    }

    class Brake : Part
    {
        public Brake(bool isBroken) : base(isBroken) { }

        public override Part Clone(bool isBroken) => new Brake(isBroken);
    }

    class Suspension : Part
    {
        public Suspension(bool isBroken) : base(isBroken) { }

        public override Part Clone(bool isBroken) => new Suspension(isBroken);
    }

    class Generator : Part
    {
        public Generator(bool isBroken) : base(isBroken) { }

        public override Part Clone(bool isBroken) => new Generator(isBroken);
    }

    class AirConditioner : Part
    {
        public AirConditioner(bool isBroken) : base(isBroken) { }

        public override Part Clone(bool isBroken) => new AirConditioner(isBroken);
    }

    class Starter : Part
    {
        public Starter(bool isBroken) : base(isBroken) { }

        public override Part Clone(bool isBroken) => new Starter(isBroken);
    }

    class TimingBelt : Part
    {
        public TimingBelt(bool isBroken) : base(isBroken) { }

        public override Part Clone(bool isBroken) => new TimingBelt(isBroken);
    }

    class WaterPump : Part
    {
        public WaterPump(bool isBroken) : base(isBroken) { }

        public override Part Clone(bool isBroken) => new WaterPump(isBroken);
    }

    class GasTank : Part
    {
        public GasTank(bool isBroken) : base(isBroken) { }

        public override Part Clone(bool isBroken) => new GasTank(isBroken);
    }

    class SteeringWheel : Part
    {
        public SteeringWheel(bool isBroken) : base(isBroken) { }

        public override Part Clone(bool isBroken) => new SteeringWheel(isBroken);
    }

    class SteeringRack : Part
    {
        public SteeringRack(bool isBroken) : base(isBroken) { }

        public override Part Clone(bool isBroken) => new SteeringRack(isBroken);
    }

    class PowerSteering : Part
    {
        public PowerSteering(bool isBroken) : base(isBroken) { }

        public override Part Clone(bool isBroken) => new PowerSteering(isBroken);
    }

    class Dashboard : Part
    {
        public Dashboard(bool isBroken) : base(isBroken) { }

        public override Part Clone(bool isBroken) => new Dashboard(isBroken);
    }

    class Wiring : Part
    {
        public Wiring(bool isBroken) : base(isBroken) { }

        public override Part Clone(bool isBroken) => new Wiring(isBroken);
    }

    class Battery : Part
    {
        public Battery(bool isBroken) : base(isBroken) { }

        public override Part Clone(bool isBroken) => new Battery(isBroken);
    }

    class SparkPlug : Part
    {
        public SparkPlug(bool isBroken) : base(isBroken) { }

        public override Part Clone(bool isBroken) => new SparkPlug(isBroken);
    }

    class FuelPump : Part
    {
        public FuelPump(bool isBroken) : base(isBroken) { }

        public override Part Clone(bool isBroken) => new FuelPump(isBroken);
    }

    class OilFilter : Part
    {
        public OilFilter(bool isBroken) : base(isBroken) { }

        public override Part Clone(bool isBroken) => new OilFilter(isBroken);
    }

    class Crankshaft : Part
    {
        public Crankshaft(bool isBroken) : base(isBroken) { }

        public override Part Clone(bool isBroken) => new Crankshaft(isBroken);
    }

    class Catalyst : Part
    {
        public Catalyst(bool isBroken) : base(isBroken) { }

        public override Part Clone(bool isBroken) => new Catalyst(isBroken);
    }

    #endregion

    #region Interfaces

    interface IRepairable
    {
        string HealthStatus { get; }
        bool IsNeedRepair { get; }
        string BrokenPartName { get; }
        string BrokenPartClassName { get; }

        bool TryAcceptRepair(Part part);
    }

    interface ICloneable
    {
        abstract Part Clone(bool isBroken);
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

    static class PartsDictionary
    {
        private static Dictionary<PartType, Part> s_PartByType;
        private static Dictionary<PartType, string> s_PartsTypesNames;
        private static Dictionary<string, PartType> s_PartsTypes;
        private static List<PartType> s_AllTypesParts;

        static PartsDictionary()
        {
            s_PartByType = new Dictionary<PartType, Part>()
            {
                { PartType.Engine, new Engine(false)},
                { PartType.Transmission , new Transmission(false) },
                { PartType.Wheel , new Wheel(false) },
                { PartType.Glass , new Glass(false) },
                { PartType.Muffler , new Muffler(false) },
                { PartType.Brake , new Brake(false) },
                { PartType.Suspension , new Suspension(false) },
                { PartType.Generator , new Generator(false) },
                { PartType.AirConditioner , new AirConditioner(false) },
                { PartType.Starter , new Starter(false) },
                { PartType.TimingBelt , new TimingBelt(false) },
                { PartType.WaterPump , new WaterPump(false) },
                { PartType.GasTank , new GasTank(false) },
                { PartType.SteeringWheel , new SteeringWheel(false) },
                { PartType.SteeringRack , new SteeringRack(false) },
                { PartType.PowerSteering , new PowerSteering(false) },
                { PartType.Dashboard , new Dashboard(false) },
                { PartType.Wiring , new Wiring(false) },
                { PartType.Battery , new Battery(false) },
                { PartType.SparkPlug , new SparkPlug(false) },
                { PartType.FuelPump , new FuelPump(false) },
                { PartType.OilFilter , new OilFilter(false) },
                { PartType.Crankshaft , new Crankshaft(false) },
                { PartType.Catalyst , new Catalyst(false) }
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

            s_PartsTypes = new Dictionary<string, PartType>()
            {
                {nameof(Engine), PartType.Engine},
                {nameof(Transmission), PartType.Transmission },
                {nameof(Wheel), PartType.Wheel },
                {nameof(Glass), PartType.Glass },
                {nameof(Muffler), PartType.Muffler },
                {nameof(Brake), PartType.Brake },
                {nameof(Suspension), PartType.Suspension },
                {nameof(Generator), PartType.Generator },
                {nameof(AirConditioner), PartType.AirConditioner },
                {nameof(Starter), PartType.Starter },
                {nameof(TimingBelt), PartType.TimingBelt },
                {nameof(WaterPump), PartType.WaterPump },
                {nameof(GasTank), PartType.GasTank },
                {nameof(SteeringWheel), PartType.SteeringWheel },
                {nameof(SteeringRack), PartType.SteeringRack},
                {nameof(PowerSteering), PartType.PowerSteering },
                {nameof(Dashboard), PartType.Dashboard },
                {nameof(Wiring), PartType.Wiring },
                {nameof(Battery), PartType.Battery },
                {nameof(SparkPlug), PartType.SparkPlug },
                {nameof(FuelPump), PartType.FuelPump },
                {nameof(OilFilter), PartType.OilFilter },
                {nameof(Crankshaft), PartType.Crankshaft },
                {nameof(Catalyst), PartType.Catalyst }
            };

            s_AllTypesParts = new List<PartType>();
            s_AllTypesParts.AddRange(s_PartsTypesNames.Keys);
        }

        public static PartType TryGetTypePartByName(string nameOfPart)
        {
            s_PartsTypes.TryGetValue(nameOfPart, out PartType partType);
            return partType;
        }

        public static string TryGetPartNameByType(PartType partType)
        {
            if (s_PartsTypesNames.TryGetValue(partType, out string name) == true)
            {
                return name;
            }
            else
            {
                return "Ошибка!";
            }
        }

        public static List<PartType> GetPartsTypesToList() => s_AllTypesParts;

        public static Part TryGetPartByType(PartType partType)
        {
            s_PartByType.TryGetValue(partType, out Part part);
            return part;
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