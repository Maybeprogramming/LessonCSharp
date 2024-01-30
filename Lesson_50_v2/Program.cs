namespace Lesson_50_v2
{
    using static UserInput;
    using static Randomaizer;
    using static Display;

    class Program
    {
        static void Main()
        {
            Console.Title = "Автосервис";
            CarService carService = new CarService();
            carService.Work();
            Console.ReadKey();
        }
    }

    class CarService
    {
        private Stock _stock;
        private Queue<Car> _cars;
        private CarFactory _carFactory;
        private int _fineForRefusal;
        private int _moneyBalance;
        private Dictionary<string, int> _pricesOfParts;
        private Dictionary<string, int> _pricesOfJob;
        private List<string> _partsNames;

        public CarService()
        {
            _pricesOfParts = new Dictionary<string, int>()
            {
                { "Двигатель", 1000 },
                { "Трансмиссия", 850 },
                { "Колесо", 200 },
                { "Стекло", 500 },
                { "Глушитель", 450 },
                { "Тормоз", 100 },
                { "Подвеска", 150 },
                { "Генератор", 300 },
                { "Кондиционер", 350 },
                { "Стартер", 270 },
                { "ГРМ", 360 },
                { "Водяная помпа", 300 },
                { "Бензобак", 220 },
                { "Руль", 150 },
                { "Рулевая рейка", 260 },
                { "Усилитель руля", 340 },
                { "Приборная панель", 290 },
                { "Электропроводка", 300 },
                { "Аккумулятор", 250 },
                { "Свеча зажигания", 50 },
                { "Топливный насос", 180 },
                { "Масляный фильтр", 50 },
                { "Коленвал", 420 },
                { "Катализатор", 650 }
            };

            _pricesOfJob = new Dictionary<string, int>()
            {
                { "Двигатель", 200 },
                { "Трансмиссия", 150 },
                { "Колесо", 20 },
                { "Стекло", 50 },
                { "Глушитель", 50 },
                { "Тормоз", 25 },
                { "Подвеска", 25 },
                { "Генератор", 50 },
                { "Кондиционер", 50 },
                { "Стартер", 50 },
                { "ГРМ", 50 },
                { "Водяная помпа", 45 },
                { "Бензобак", 75 },
                { "Руль", 35 },
                { "Рулевая рейка", 45 },
                { "Усилитель руля", 60 },
                { "Приборная панель", 25 },
                { "Электропроводка", 30 },
                { "Аккумулятор", 15 },
                { "Свеча зажигания", 15 },
                { "Топливный насос", 20 },
                { "Масляный фильтр", 20 },
                { "Коленвал", 100 },
                { "Катализатор", 75 }
            };

            _partsNames = new List<string>()
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

            _stock = new Stock(new List<string>(_partsNames));
            _carFactory = new CarFactory(new PartFactory(new List<string>(_partsNames)));
            _cars = _carFactory.CreateSeveralCars();
            _fineForRefusal = 500;
        }

        public void Work()
        {
            const string RefuseCommand = "1";
            const string AutoRepairCommand = "2";
            const string ShowPartStockCommand = "3";
            const string ExitCommand = "4";

            int minMoneyBalance = 1000;
            int maxMoneyBalance = 3000;
            bool isRun = true;
            string userInput;
            ConsoleColor numberMenuColor = ConsoleColor.DarkYellow;

            _moneyBalance = GenerateRandomNumber(minMoneyBalance, maxMoneyBalance + 1);

            while (_cars.Count > 0 && isRun == true)
            {
                Console.Clear();
                Print($"Добро пожаловать в наш автосервис: \"Мастер на все руки\"!\n", ConsoleColor.Cyan);

                ShowBalance(_moneyBalance);
                ShowClientsNumbersInQueue();

                Print($"\nТекущий автомобиль на ремонт: ", ConsoleColor.DarkYellow);
                _cars?.First().ShowInfo();

                Print($"\n");
                PrintLine();
                Print($"\nДоступные функции:", ConsoleColor.Green);
                Print($"\n{RefuseCommand}", numberMenuColor);
                Print($" - Отказать в ремонте автомобиля");
                Print($"\n{AutoRepairCommand}", numberMenuColor);
                Print($" - Отдать машину для ремонт слесарю");
                Print($"\n{ShowPartStockCommand}", numberMenuColor);
                Print($" - Посмотреть остатки деталей на складе");
                Print($"\n{ExitCommand}", numberMenuColor);
                Print($" - Выйти из программы", ConsoleColor.Red);
                Print($"\nВведите номер команды: ", ConsoleColor.Green);

                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case RefuseCommand:
                        RefuseToRepairCar(_cars?.Dequeue());
                        break;

                    case AutoRepairCommand:
                        RepairCar(_cars?.Dequeue());
                        break;

                    case ShowPartStockCommand:
                        _stock.ShowInfo();
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

        private void RepairCar(Car currentCar)
        {
            int minChanceWrongJob = 0;
            int maxChanceWrongJob = 30;
            int maxScaleChanceToDoJob = 100;
            int currentChanceToDoJob = GenerateRandomNumber(minChanceWrongJob, maxScaleChanceToDoJob);
            int fullCost;

            Print($"\nСлесарь сервиса принялся за ремонт машины");

            if (currentChanceToDoJob > maxChanceWrongJob)
            {
                Part goodPart;
                string brokenPartName = currentCar.BrokenPartName;

                if (_stock.TryGetPart(brokenPartName, out goodPart))
                {
                    currentCar.ApplyRepair(goodPart);

                    Print($"\nБыла заменена неисправная деталь: ");
                    Print($"{goodPart.Name}", ConsoleColor.Green);
                    currentCar.ShowInfo();

                    fullCost = CalculatePayingInfo(brokenPartName);
                    _moneyBalance += fullCost;
                }
                else
                {
                    RefuseToRepairCar(currentCar, $"\nНа складе нет такой детали: {brokenPartName}");
                }
            }
            else
            {
                string somePartName = GenerateRandomName(_partsNames);

                if (_stock.TryGetPart(somePartName, out Part part))
                {
                    currentCar.ApplyRepair(part);
                    Print($"\nБыла заменена деталь: {somePartName}");

                    fullCost = CalculatePayingInfo(somePartName);
                    _moneyBalance += fullCost;

                    Print($"\nИзвините за ошибку при ремонте, ");
                    Print($"\nМы возвращаем вам уплаченные вами деньги в размере: <");
                    Print($"{fullCost}", ConsoleColor.Green);
                    Print($"> рублей");
                    Print($"\nТак же в качестве компенсации мы выплатим вам: <");
                    Print($"{_fineForRefusal}", ConsoleColor.Green);
                    Print($"> рублей\n");
                    PrintLine();

                    _moneyBalance -= fullCost;
                    _moneyBalance -= _fineForRefusal;
                }
                else
                {
                    RefuseToRepairCar(currentCar, $"\nНа складе нет такой детали: {somePartName}");
                }
            }
        }

        private int CalculatePayingInfo(string brokenPartName)
        {
            _pricesOfParts.TryGetValue(brokenPartName, out int priceofPart);
            _pricesOfJob.TryGetValue(brokenPartName, out int priceOfJob);

            int fullPrice = priceofPart + priceOfJob;

            Print($"\n\nСформирован чек:");
            Print($"\n{new string('-', 40)}");
            Print($"\n1. Цена детали <");
            Print($"{brokenPartName}", ConsoleColor.Green);
            Print($">: ");
            Print($"{priceofPart}", ConsoleColor.DarkYellow);
            Print($" рублей");
            Print($"\n2. Стоимость выполненных работ: ");
            Print($"{priceOfJob}", ConsoleColor.DarkYellow);
            Print($" рублей");
            Print($"\n{new string('-', 40)}");
            Print($"\nИтого к оплате: ");
            Print($"{fullPrice}", ConsoleColor.Green);
            Print($" рублей\n");
            PrintLine();
            Print("\n");

            return fullPrice;
        }

        private void RefuseToRepairCar(Car car, string refuseReason = "")
        {
            _moneyBalance -= _fineForRefusal;

            Print($"{refuseReason}");
            Print($"\nВы отказались ремонтировать автомобиль: ");
            Print($"{car.Name}", ConsoleColor.Green);
            Print($"\nВам пришлось оплатить штраф за отказ: ");
            Print($"{_fineForRefusal}", ConsoleColor.Red);
            Print($" рублей");
        }

        private void ShowClientsNumbersInQueue()
        {
            Print($"\nКлиентов в очереди на ремонт: ");
            Print($"{_cars.Count}\n", ConsoleColor.Green);
            PrintLine();
        }

        private void ShowBalance(int moneyBalance)
        {
            string name = "Автосервис";
            Console.Title = $"{name}. Баланс: {moneyBalance} рублей.";

            Print($"\nБаланс на счёте автосервиса: ");
            Print($"{moneyBalance}", ConsoleColor.Green);
            Print($" рублей\n");
            PrintLine();
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

        public PartFactory(List<string> partsNames)
        {
            _partsNames = partsNames;
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

            CreateBrokenPart(parts);

            return parts;
        }

        private void CreateBrokenPart(List<Part> parts)
        {
            int indexBrokenPart = GenerateRandomNumber(0, parts.Count);
            Part brokenPart = parts[indexBrokenPart].Clone(true);
            parts[indexBrokenPart] = brokenPart;
        }

        private void MovePartNameToEnd(string somePartName, int currentIndex)
        {
            int lastIndex = _partsNames.Count - 1;
            string tempName = _partsNames[lastIndex];

            _partsNames[lastIndex] = somePartName;
            _partsNames[currentIndex] = tempName;
        }
    }

    class Part
    {
        public Part(string name, bool isBroken)
        {
            Name = name;
            IsBroken = isBroken;
        }

        public string Name { get; }
        public bool IsBroken { get; }

        public Part Clone(bool isBroken)
        {
            return new Part(Name, isBroken);
        }
    }

    class PartsInfo()
    {
        private string _name;
        private int _pricePart;
        private int _priceToJob;
    }

    class Stock
    {
        private List<Cell> _cellsParts;
        private List<string> _partsNames;

        public Stock(List<string> partsNames)
        {
            _partsNames = partsNames;
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
            PartFactory partFactory = new PartFactory(_partsNames);
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

        public Queue<Car> CreateSeveralCars(int minCarCount = 5, int maxCarCount = 10)
        {
            Queue<Car> cars = new();
            int someCarsCount = GenerateRandomNumber(minCarCount, maxCarCount + 1);

            for (int i = 0; i < someCarsCount; i++)
            {
                Car car = CreateSingleCar();
                cars.Enqueue(car);
            }

            return cars;
        }

        public Car CreateSingleCar()
        {
            return new Car(_partFactory.CreateSeveral(), GenerateRandomName(_names));
        }
    }

    class Car
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

//Влад Сахно от 27.01.2024
//Доработать.

//1. - Перенес в метод Work()
//private int _minMoneyBalance;
//private int _maxMoneyBalance;
//private int _moneyBalance;
//-минимум и максимум не должны быть в поле класса.
//Переменные в конструкторе или нужном методе

//2. - доработал
//private Dictionary<string, int> pricesOfParts; -
//поле названо не по нотации. Есть правило.
//Переменные именуются с маленькой буквы,
//приватные поля с символа _ и маленькой буквы (исключение, константы),
//а всё остальное с большой буквы.

//3. - удалил дубляж
//private Dictionary<string, int> pricesOfParts; и
//private Dictionary<string, int> _pricesOfParts;
//-зачем два одинаковых поля, и одно не используется

//4.
//_pricesOfParts и
//_pricesOfJob - вы можете создать сразу класс,
//который будет содержать название детали,
//цену детали и стоимость работы за эту деталь

//5. - статику убрал, логику перенёс в класс автосервиса
//static class PartsDictionary -статика, это плохо,
//её не должно быть в таком виде.
//Можно сделать класс без статики и
//из него получить детали с ценами и детали для машин
//(а можно оставить логику в автосервисе)

//6. GenerateRandomName(PartsDictionary.GetPartsNames());
//-при замене не той детали странно получить уведомление,
//что такой детали нет.
//Есть шанс ошибиться.
//Если ошибся, взять из склада любую деталь,
//что там есть (но отличную от нужной, которая в машине)

//7. - доработал
//List<Part> CreateBrokenPart(List<Part> parts)
//- можно ничего не возвращать, вы же меняете значение в списке,
//а список не изменяется

//8. - удалил не нужный интерфейс
//interface ICloneable -отдельно интерфейс вам не нужен,
//вы его не используете, просто дополнительный метод в детали появился

//9. - доработал
//private List<string> _partsNames;
//-вам эти данные не надо хранить в поле, они раз используются

//10. - это плохо.
//Пересмотрите "Проблема возвращения ссылки на массивы".
//Работа с коллекцией должна происходит внутри класса.
//А сама коллекция должна быть приватным полем.

