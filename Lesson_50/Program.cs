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

            CarService carService = new();
            //carService.Work();

            List<Cell> cells = new List<Cell>()
            {
                new Cell(new Engine(false).Name, 5, 200),
                new Cell(new Transmission(false).Name, 6, 150),
                new Cell(new Glass(false).Name, 20, 50),
                new Cell(new OilFilter(false).Name, 50, 15),
                new Cell(new Catalyst(false).Name, 0, 120)
            };

            Engine part = new Engine(true);
            string partName = PartsDictionary.TryGetName(part);

            Print($"{part.Name} | {part.GetType().Name} | {partName}");
            cells[0] = new Cell(part.Name, 10, 20000);

            int index = 0;

            foreach (var cell in cells)
            {
                Print($"\n{++index}. ");
                cell.ShowInfo();
            }


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

        private Dictionary<string, int> _pricesOfParts;
        private Dictionary<string, int> _pricesForJob;

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

            //_pricesOfParts = new Dictionary<PartType, int>()
            //{
            //    {PartType.Engine, 1000},
            //    {PartType.Transmission, 850},
            //    {PartType.Wheel, 200},
            //    {PartType.Glass, 150},
            //    {PartType.Muffler,  100},
            //    {PartType.Brake,  100},
            //    {PartType.Suspension,  100},
            //    {PartType.Generator,  150},
            //    {PartType.AirConditioner,  300},
            //    {PartType.Starter,  200},
            //    {PartType.TimingBelt,  250},
            //    {PartType.WaterPump,  230},
            //    {PartType.GasTank,  350},
            //    {PartType.SteeringWheel,  450},
            //    {PartType.SteeringRack,  650},
            //    {PartType.PowerSteering,  500},
            //    {PartType.Dashboard,  700},
            //    {PartType.Wiring,  550},
            //    {PartType.Battery,  250},
            //    {PartType.SparkPlug,  100},
            //    {PartType.FuelPump,  300},
            //    {PartType.OilFilter,  180},
            //    {PartType.Crankshaft,  400},
            //    {PartType.Catalyst,  900},
            //};

            //_pricesForJob = new Dictionary<PartType, int>()
            //{
            //    {PartType.Engine, 200},
            //    {PartType.Transmission, 85},
            //    {PartType.Wheel, 20},
            //    {PartType.Glass, 15},
            //    {PartType.Muffler,  10},
            //    {PartType.Brake,  10},
            //    {PartType.Suspension,  10},
            //    {PartType.Generator,  15},
            //    {PartType.AirConditioner,  30},
            //    {PartType.Starter,  20},
            //    {PartType.TimingBelt,  25},
            //    {PartType.WaterPump,  20},
            //    {PartType.GasTank,  35},
            //    {PartType.SteeringWheel,  45},
            //    {PartType.SteeringRack,  65},
            //    {PartType.PowerSteering,  50},
            //    {PartType.Dashboard,  70},
            //    {PartType.Wiring,  55},
            //    {PartType.Battery,  25},
            //    {PartType.SparkPlug,  10},
            //    {PartType.FuelPump,  30},
            //    {PartType.OilFilter,  15},
            //    {PartType.Crankshaft,  40},
            //    {PartType.Catalyst,  90},
            //};
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
                string brokenPartName = currentCar.BrokenPartName;
                goodPart = PartsDictionary.TryGetPart(brokenPartName);

                currentCar.TryAcceptRepair(goodPart);

                Print($"\nБыла заменена неисправная деталь: {goodPart.Name}");
                Print($"\nСтатус машины: {currentCar.HealthStatus}");
                Print($"\nНеисправность: {currentCar.BrokenPartName}");
            }
            else
            {
                List<string> partsNames = PartsDictionary.GetPartsNames();
                int indexNumber = GenerateRandomNumber(0, partsNames.Count);
                string somePartName = partsNames[indexNumber];
                Part wrongPart = PartsDictionary.TryGetPart(somePartName);

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

        private int TryGetPriceOfPart(string partName)
        {
            if (_pricesOfParts.TryGetValue(partName, out int price) == true)
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
        List<string> _somePartsNames;

        public List<Part> CreateSomeParts()
        {
            _somePartsNames = CreateSomePartsTypes();
            List<Part> parts = new();
            bool isBrokenPart = false;
            Part part;

            for (int i = 0; i < _somePartsNames.Count; i++)
            {
                part = PartsDictionary.TryGetPart(_somePartsNames[i]).Clone(isBrokenPart);
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

        private List<string> CreateSomePartsTypes()
        {
            int minPartsTypesCount = 5;
            int maxPartsTypesCount = 10;
            int somePartsTypesCount = GenerateRandomNumber(minPartsTypesCount, maxPartsTypesCount + 1);
            List<string> partsNames = new(PartsDictionary.GetPartsNames());
            List<string> somePartsNames = new();
            string tempPartName;

            for (int i = 0; i < somePartsTypesCount; i++)
            {
                int indexNumber = GenerateRandomNumber(0, partsNames.Count - i);
                somePartsNames.Add(partsNames[indexNumber]);

                tempPartName = partsNames[partsNames.Count - 1];
                partsNames[partsNames.Count - 1] = partsNames[indexNumber];
                partsNames[indexNumber] = tempPartName;
            }

            return somePartsNames;
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

        public string HealthStatus =>
            GetBrokenPart() != null ? "Требуется ремонт" : "В рабочем состоянии";

        public bool IsNeedRepair =>
            GetBrokenPart() != null;

        public string BrokenPartName =>
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
        //del and make a new
        private Dictionary<string, int> _partsCountsAvailable;
        private List<string> _partsNames;

        public PartsStock()
        {
            _partsNames = PartsDictionary.GetPartsNames();

            _partsCountsAvailable = FillParts();
        }

        public bool IsPartAvaible(string partName)
        {
            _partsCountsAvailable.TryGetValue(partName, out int partCountAvailale);

            if (partCountAvailale > 0)
            {
                return true;
            }

            return false;
        }

        public Part TryGetPart(string partName)
        {
            if (IsPartAvaible(partName) == true)
            {
                Part part = PartsDictionary.TryGetPart(partName);
                AcceptToChangePartValue(partName);

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
                Print($"\n{++index}. {part.Key} - [");
                Print($"{part.Value}", ConsoleColor.Green);
                Print($"] штук.");
            }

            Print("\n");
            PrintLine();
        }

        private Dictionary<string, int> FillParts()
        {
            Dictionary<string, int> partsCountsAvailable = new();
            int minPartsCount = 0;
            int maxPartsCount = 10;

            for (int i = 0; i < _partsNames.Count; i++)
            {
                partsCountsAvailable.Add(_partsNames[i], GenerateRandomNumber(minPartsCount, maxPartsCount + 1));
            }

            return partsCountsAvailable;
        }

        private void AcceptToChangePartValue(string partName)
        {
            _partsCountsAvailable.TryGetValue(partName, out int PartCount);
            PartCount--;
            _partsCountsAvailable.Remove(partName);
            _partsCountsAvailable.Add(partName, PartCount);
        }
    }

    class Cell
    {
        private readonly string _partName;
        private readonly int _price;
        private int _amount;

        public Cell(string partName, int amount, int price)
        {
            _partName = partName;
            _amount = amount;
            _price = price;
        }

        public int Amount => _amount;

        public int Price => _price;

        public string GetPartName() => _partName;

        public void SetPartCount(int value) =>
            _amount = value > 0 ? value : 0;

        public void ShowInfo()
        {
            Print($"Деталь: ");
            Print($"{_partName}", ConsoleColor.Green);
            Print($". Количество: ");
            Print($"{Amount}", Amount > 0 ? ConsoleColor.Green : ConsoleColor.Red);
            Print($".");

            if (Amount > 0)
            {
                Print($" Цена: ");
                Print($"{Price}", ConsoleColor.Green);
                Print($" рублей.");
            }
        }
    }

    #region Part Classes

    abstract class Part : ICloneable
    {
        public Part(bool isBroken)
        {
            IsBroken = isBroken;
        }

        public virtual string Name { get => PartsDictionary.TryGetName(this); }
        public bool IsBroken { get; }
        public virtual string IsBrokenToString { get => IsBroken == true ? "не исправен" : "исправен"; }

        public abstract Part Clone(bool isBroken);

        public string ShowInfo()
        {
            return $"{Name}: [{IsBrokenToString}]";
        }
    }

    class Engine : Part
    {
        public Engine(bool isBroken) : base(isBroken) { }

        public override string Name => PartsDictionary.TryGetName(this);

        public override Part Clone(bool isBroken) => new Engine(isBroken);
    }

    class Transmission : Part
    {
       // public override string Name => PartsDictionary.TryGetName(this);
        public Transmission(bool isBroken) : base(isBroken) { }

        public override Part Clone(bool isBroken) => new Transmission(isBroken);
    }

    class Wheel : Part
    {
        //public override string Name => PartsDictionary.TryGetName(this);
        public Wheel(bool isBroken) : base(isBroken) { }

        public override Part Clone(bool isBroken) => new Wheel(isBroken);
    }

    class Glass : Part
    {
       // public override string Name => PartsDictionary.TryGetName(this);
        public Glass(bool isBroken) : base(isBroken) { }

        public override Part Clone(bool isBroken) => new Glass(isBroken);
    }

    class Muffler : Part
    {
       // public override string Name => PartsDictionary.TryGetName(this);
        public Muffler(bool isBroken) : base(isBroken) { }

        public override Part Clone(bool isBroken) => new Muffler(isBroken);
    }

    class Brake : Part
    {
        //public override string Name => PartsDictionary.TryGetName(this);
        public Brake(bool isBroken) : base(isBroken) { }

        public override Part Clone(bool isBroken) => new Brake(isBroken);
    }

    class Suspension : Part
    {
        //public override string Name => PartsDictionary.TryGetName(this);
        public Suspension(bool isBroken) : base(isBroken) { }

        public override Part Clone(bool isBroken) => new Suspension(isBroken);
    }

    class Generator : Part
    {
        //public override string Name => PartsDictionary.TryGetName(this);
        public Generator(bool isBroken) : base(isBroken) { }

        public override Part Clone(bool isBroken) => new Generator(isBroken);
    }

    class AirConditioner : Part
    {
        //public override string Name => PartsDictionary.TryGetName(this);
        public AirConditioner(bool isBroken) : base(isBroken) { }

        public override Part Clone(bool isBroken) => new AirConditioner(isBroken);
    }

    class Starter : Part
    {
        //public override string Name => PartsDictionary.TryGetName(this);
        public Starter(bool isBroken) : base(isBroken) { }

        public override Part Clone(bool isBroken) => new Starter(isBroken);
    }

    class TimingBelt : Part
    {
        //public override string Name => PartsDictionary.TryGetName(this);
        public TimingBelt(bool isBroken) : base(isBroken) { }

        public override Part Clone(bool isBroken) => new TimingBelt(isBroken);
    }

    class WaterPump : Part
    {
        //public override string Name => PartsDictionary.TryGetName(this);
        public WaterPump(bool isBroken) : base(isBroken) { }

        public override Part Clone(bool isBroken) => new WaterPump(isBroken);
    }

    class GasTank : Part
    {
        //public override string Name => PartsDictionary.TryGetName(this);
        public GasTank(bool isBroken) : base(isBroken) { }

        public override Part Clone(bool isBroken) => new GasTank(isBroken);
    }

    class SteeringWheel : Part
    {
        //public override string Name => PartsDictionary.TryGetName(this);
        public SteeringWheel(bool isBroken) : base(isBroken) { }

        public override Part Clone(bool isBroken) => new SteeringWheel(isBroken);
    }

    class SteeringRack : Part
    {
        //public override string Name => PartsDictionary.TryGetName(this);
        public SteeringRack(bool isBroken) : base(isBroken) { }

        public override Part Clone(bool isBroken) => new SteeringRack(isBroken);
    }

    class PowerSteering : Part
    {
        //public override string Name => PartsDictionary.TryGetName(this);
        public PowerSteering(bool isBroken) : base(isBroken) { }

        public override Part Clone(bool isBroken) => new PowerSteering(isBroken);
    }

    class Dashboard : Part
    {
        //public override string Name => PartsDictionary.TryGetName(this);
        public Dashboard(bool isBroken) : base(isBroken) { }

        public override Part Clone(bool isBroken) => new Dashboard(isBroken);
    }

    class Wiring : Part
    {
        //public override string Name => PartsDictionary.TryGetName(this);
        public Wiring(bool isBroken) : base(isBroken) { }

        public override Part Clone(bool isBroken) => new Wiring(isBroken);
    }

    class Battery : Part
    {
        //public override string Name => PartsDictionary.TryGetName(this);
        public Battery(bool isBroken) : base(isBroken) { }

        public override Part Clone(bool isBroken) => new Battery(isBroken);
    }

    class SparkPlug : Part
    {
        //public override string Name => PartsDictionary.TryGetName(this);
        public SparkPlug(bool isBroken) : base(isBroken) { }

        public override Part Clone(bool isBroken) => new SparkPlug(isBroken);
    }

    class FuelPump : Part
    {
        //public override string Name => PartsDictionary.TryGetName(this);
        public FuelPump(bool isBroken) : base(isBroken) { }

        public override Part Clone(bool isBroken) => new FuelPump(isBroken);
    }

    class OilFilter : Part
    {
        //public override string Name => PartsDictionary.TryGetName(this);
        public OilFilter(bool isBroken) : base(isBroken) { }

        public override Part Clone(bool isBroken) => new OilFilter(isBroken);
    }

    class Crankshaft : Part
    {
        //public override string Name => PartsDictionary.TryGetName(this);
        public Crankshaft(bool isBroken) : base(isBroken) { }

        public override Part Clone(bool isBroken) => new Crankshaft(isBroken);
    }

    class Catalyst : Part
    {
        //public override string Name => PartsDictionary.TryGetName(this);
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

    #region UserUtils

    static class PartsDictionary
    {
        private static Dictionary<string, Part> s_Parts;
        private static Dictionary<Part, string> s_Names;
        private static List<string> s_PartsNames;

        static PartsDictionary()
        {
            s_Parts = new Dictionary<string, Part>()
            {
                {"Двигатель",  new Engine(false)},
                {"Трансмиссия" , new Transmission(false)},
                { "Колесо", new Wheel(false) },
                {"Стекло" , new Glass(false)},
                {"Глушитель" , new Muffler(false)},
                {"Тормоз" , new Brake(false) },
                {"Подвеска" , new Suspension(false)},
                {"Генератор" , new Generator(false)},
                {"Кондиционер" , new AirConditioner(false)},
                {"Стартер" , new Starter(false)},
                {"ГРМ" , new TimingBelt(false)},
                {"Водяная помпа" , new WaterPump(false)},
                {"Бензобак" , new GasTank(false)},
                {"Руль" , new SteeringWheel(false)},
                {"Рулевая рейка" , new SteeringRack(false)},
                {"Усилитель руля" , new PowerSteering(false)},
                {"Приборная панель" , new Dashboard(false)},
                {"Электропроводка" , new Wiring(false)},
                {"Аккумулятор" , new Battery(false)},
                {"Свеча зажигания" , new SparkPlug(false)},
                {"Топливный насос" , new FuelPump(false)},
                {"Масляный фильтр" , new OilFilter(false)},
                {"Коленвал" , new Crankshaft(false)},
                {"Катализатор" , new Catalyst(false)}
            };

            s_Names = FillNames(s_Parts);
            s_PartsNames = new List<string>(s_Parts.Keys);
        }

        public static List<string> GetPartsNames() => s_PartsNames;        
     
        public static Part TryGetPart(string partName)
        {
            s_Parts.TryGetValue(partName, out Part part);
            return part;
        }

        public static string TryGetName(Part part)
        {
            s_Names.TryGetValue(part, out string name);
            return name;
        }

        private static Dictionary<Part, string> FillNames(Dictionary<string, Part> parts)
        {
            Dictionary<Part, string> names = new Dictionary<Part, string>();

            foreach (var part in parts)
            {
                names.Add(part.Value, part.Key);
            }

            return names;
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