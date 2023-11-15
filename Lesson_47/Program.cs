namespace Lesson_47
{
    using static Display;
    using static Randomaizer;

    class Program
    {
        static void Main()
        {
            Console.Title = "Война";

            BattleField battleField = new BattleField();
            battleField.Work();

            PrintLine(ConsoleColor.Red);
            Print($"Бой завершён.\n");
            Console.ReadKey();
        }
    }

    class BattleField
    {
        private Squad? _squad1;
        private Squad? _squad2;
        private Unit _unit1;
        private Unit _unit2;

        public BattleField()
        {
            _squad1 = new Squad("Браво #1");
            PrintLine();

            _squad2 = new Squad("Дельта #2");
            PrintLine();
        }

        public void Work()
        {
            BeginFirstFighters();

            DecidingWhichSquadGoesFirst();

            Fight();

            AnnounceVictory();
        }

        private void BeginFirstFighters()
        {
            Print("Подготовка первых бойцов для начала сражения.\n", ConsoleColor.Green);

            _squad1.TryGetUnit(out _unit1);
            _squad2.TryGetUnit(out _unit2);

            Print($"Выбранные бойцы:\n" +
                $">в отряде: {_squad1.Name} [{_unit1.ClassName}]: {_unit1.Name}\n" +
                $">в отряде: {_squad2.Name} [{_unit2.ClassName}]: {_unit2.Name}\n");
            PrintLine();
        }

        private void Fight()
        {
            const int MillisecondsDelay = 1000;

            Print($"Война начинается...\n");

            while (_squad1.IsAlive == true && _squad2.IsAlive == true)
            {
                while (_unit1.IsAlive == true && _unit2.IsAlive == true)
                {
                    _unit1.AttackTo(_unit2);
                    _unit2.AttackTo(_unit1);

                    PrintLine(ConsoleColor.DarkYellow);
                }

                _unit1 = TryToChooseNewUnit(_squad1, _unit1);
                _unit2 = TryToChooseNewUnit(_squad2, _unit2);

                Task.Delay(MillisecondsDelay).Wait();
            }
        }

        private Unit TryToChooseNewUnit(Squad squad, Unit unit)
        {
            if (unit.IsAlive == true)
            {
                return unit;
            }
            else
            {
                Print($"В отряде {squad.Name} был побежден: [{unit.ClassName}]: {unit.Name}\n", ConsoleColor.Red);
                squad.TryGetUnit(out Unit newUnit);
                Print($"В отряде {squad.Name} выбран новый боец для продолжения битвы: [{newUnit.ClassName}]: {newUnit.Name}\n", ConsoleColor.Green);
                PrintLine(ConsoleColor.Cyan);

                return newUnit;
            }
        }

        private void DecidingWhichSquadGoesFirst()
        {
            int minNumber = 0;
            int maxNumber = 100;
            int middleNumber = (minNumber + maxNumber) / 2;
            int randomNumber;
            Squad tempSquad;

            Print($"Жеребьевка права на первый ход...\n", ConsoleColor.Green);
            Print($"Условия:\n" +
                  $"> если случайное число <50 первый ход делает отряд >> {_squad1.Name} <<\n" +
                  $"> если случайное число >=50 то право первого хода отдаётся отряду >> {_squad2.Name} <<\n\n");

            randomNumber = GenerateRandomNumber(minNumber, maxNumber);
            Print($"Случайное число: [> {randomNumber} <]\n\n", ConsoleColor.Green);

            if (randomNumber < middleNumber)
            {
                Print($"Первый ход делает отряд: {_squad1.Name}\n", ConsoleColor.Red);
            }
            else
            {
                tempSquad = _squad2;
                _squad2 = _squad1;
                _squad1 = tempSquad;

                Print($"Первый ход делает отряд: {_squad1.Name}\n", ConsoleColor.Red);
            }

            PrintLine();
        }

        private void AnnounceVictory()
        {
            Print($"Объявление победителя!\n", ConsoleColor.Red);

            if (_squad1.IsAlive == false && _squad2.IsAlive == false)
            {
                Print($"Победителей нет. Ничья!");
            }
            else if (_squad1.IsAlive == true && _squad2.IsAlive == false)
            {
                Print($"Победил отряд: > {_squad1.Name} <\n" +
                      $"В отряде осталось: [{_squad1.UnitsCount}] боевых единиц:\n");

                _squad1.ShowInfo();
            }
            else
            {
                Print($"Победил отряд: > {_squad2.Name} <\n" +
                      $"В отряде осталось: [{_squad2.UnitsCount}] боевых единиц\n");

                _squad2.ShowInfo();
            }
        }
    }

    class Squad
    {
        private string _name;
        private int _fightersCount;
        private int _vihiclesCount;
        private List<Unit>? _squad;
        private UnitFactory _fighterFactory;
        private UnitFactory _vihicleFactory;

        public Squad(string name)
        {
            _name = name;
            _fightersCount = GenerateRandomNumber(7, 10);
            _vihiclesCount = GenerateRandomNumber(2, 5);
            _squad = new();
            _fighterFactory = new FighterFactory();
            _vihicleFactory = new VihicleFactory();

            Create(_fightersCount, _vihiclesCount);
            _squad = Shuffle(_squad);
        }

        public string Name { get => _name; }
        public int UnitsCount { get => _squad.Count; }

        public bool TryGetUnit(out Unit unit)
        {
            if (IsAlive == true)
            {
                unit = _squad.First();
                _squad.Remove(_squad.First());

                return true;
            }
            else
            {
                unit = null;
                return false;
            }
        }

        public void ShowInfo()
        {
            int index = 0;

            foreach (Unit unit in _squad)
            {
                Print($"{++index}. [{unit.ClassName}]: {unit.Name}\n");
            }
        }

        public bool IsAlive => _squad.Count > 0;

        private void Create(int fighterCount, int vihiclesCount)
        {
            Print($">>> Начинается процедура формирования отряда #{_name}\n", ConsoleColor.Green);

            int fullCount = fighterCount + vihiclesCount;

            FillUnits(fighterCount, _fighterFactory);
            Print($"> Рекруты наняты.\n");

            FillUnits(vihiclesCount, _vihicleFactory);
            Print($"> Боевая техника изготовлена.\n");

            Print($"> В отряде {fullCount} боевых единиц:\n");
            ShowInfo();
            Print($"> Отряд сформирован!\n");
        }

        private void FillUnits(int unitCount, UnitFactory factory)
        {
            for (int i = 0; i < unitCount; i++)
            {
                object? gameUnit = factory.CreateRandomUnit();

                if (gameUnit is Unit unit)
                {
                    _squad?.Add(unit);
                }
                else
                {
                    throw new Exception($"\nОшибка! Объект с типом: {gameUnit?.GetType().FullName}\n" +
                                        $"не соответствует ожидаемому объекту: {typeof(Unit).FullName}\n");
                }
            }
        }
    }

    #region Abstract Factory

    abstract class UnitFactory
    {
        public abstract object? CreateRandomUnit();
    }

    #region Concrete Factory

    class FighterFactory : UnitFactory
    {
        private List<Type> _fightersType;

        public FighterFactory()
        {
            _fightersType = new()
            {
                typeof(Heavy),
                typeof(GrenadeLauncher),
                typeof(Paratrooper),
                typeof(Sniper),
                typeof(Scout),
                typeof(Stormtrooper)
            };
        }

        public override object? CreateRandomUnit()
        {
            int randomNumber = GenerateRandomNumber(0, _fightersType.Count);

            return Activator.CreateInstance(_fightersType[randomNumber]);
        }
    }

    class VihicleFactory : UnitFactory
    {
        private List<Type> _vihiclesType;

        public VihicleFactory()
        {
            _vihiclesType = new()
            {
                typeof(Tank),
                typeof(Helicopter)
            };
        }

        public override object? CreateRandomUnit()
        {
            int randomTypeFighterNumber = GenerateRandomNumber(0, _vihiclesType.Count);

            return Activator.CreateInstance(_vihiclesType[randomTypeFighterNumber]);
        }
    }

    #endregion

    #endregion

    #region Abstract class Unit

    abstract class Unit : ICombatEntity, IDamageable, IDamageProvider
    {
        protected int _health;

        protected Unit()
        {
            ClassName = "Юнит";
            Damage = 10;
            Health = 100;
            Armor = 5;
            Name = "Юнит";
        }

        public string ClassName { get; protected set; }
        public int Damage { get; protected set; }
        public int Health
        {
            get => _health;
            protected set => SetHealth(value);
        }
        public int Armor { get; protected set; }
        public bool IsAlive { get => Health > 0; }
        public virtual string Name { get; set; }

        public virtual bool TryTakeDamage(int damage)
        {
            if (IsAlive == true)
            {
                int damageTaken = damage - Armor;
                Health -= damageTaken;

                Print($"{ClassName}: {Name} получает >{damageTaken}< ед. урона. Осталось здоровья: >{Health}<\n");

                return true;
            }

            return false;
        }

        public virtual void AttackTo(IDamageable target)
        {
            if (IsAlive == true && target.IsAlive == true)
            {
                Print($"{ClassName}: {Name} атакует >>> {target.Name}\n");

                target.TryTakeDamage(Damage);
            }
        }

        protected void SetHealth(int value)
        {
            if (value > 0)
            {
                _health = value;
            }
            else
            {
                _health = 0;
            }
        }
    }

    #endregion

    #region Fighters classes

    abstract class Fighter : Unit
    {
        public Fighter()
        {
            ClassName = "Пехотинец";
            Name = GenerateRandomName();
            Damage = 10;
            Health = 100;
            Armor = 0;
        }
    }

    class Stormtrooper : Fighter
    {
        private int _chanceAbsorbingDamagePercent = 30;
        private int _reductionDamageValue = 2;

        public Stormtrooper()
        {
            ClassName = "Штурмовик";
            Damage = GenerateRandomNumber(10, 20);
            Health = GenerateRandomNumber(100, 150);
            Armor = GenerateRandomNumber(0, 6);
        }

        public override bool TryTakeDamage(int damage)
        {
            if (IsСhanceNow(_chanceAbsorbingDamagePercent) == true & damage > _reductionDamageValue)
            {
                damage -= _reductionDamageValue;
                Print($"{ClassName}: {Name} использовал способность снижения получаемого урона.\n");
                Print($"Урон уменьшился на {_reductionDamageValue} единиц.\n", ConsoleColor.Green);
            }
            
            return base.TryTakeDamage(damage);
        }
    }

    class Sniper : Fighter
    {
        public Sniper()
        {
            ClassName = "Снайпер";
            Damage = GenerateRandomNumber(15, 20);
            Health = GenerateRandomNumber(80, 100);
            Armor = GenerateRandomNumber(0, 1);
        }
    }

    class Paratrooper : Fighter
    {
        public Paratrooper()
        {
            ClassName = "Десантник";
            Damage = GenerateRandomNumber(10, 15);
            Health = GenerateRandomNumber(120, 180);
            Armor = GenerateRandomNumber(0, 6);
        }
    }

    class Scout : Fighter
    {
        public Scout()
        {
            ClassName = "Разведчик";
            Damage = GenerateRandomNumber(8, 10);
            Health = GenerateRandomNumber(60, 80);
            Armor = GenerateRandomNumber(0, 2);
        }
    }

    class Heavy : Fighter
    {
        public Heavy()
        {
            ClassName = "Пулеметчик";
            Damage = GenerateRandomNumber(10, 15);
            Health = GenerateRandomNumber(150, 200);
            Armor = GenerateRandomNumber(0, 2);
        }
    }

    class GrenadeLauncher : Fighter
    {
        private int _criticalModifier;
        private int _criticalDamage;
        private int _baseDamage;

        public GrenadeLauncher()
        {
            ClassName = "Гранатометчик";
            Damage = GenerateRandomNumber(20, 30);
            Health = GenerateRandomNumber(150, 200);
            Armor = GenerateRandomNumber(0, 2);
            _baseDamage = Damage;
            _criticalModifier = 2;
            _criticalDamage = _criticalModifier * Damage;
        }

        public override void AttackTo(IDamageable target)
        {
            if (target is Vihicle == true)
            {
                Damage = _criticalDamage;
            }
            else
            {
                Damage = _baseDamage;
            }

            base.AttackTo(target);
        }
    }

    #endregion

    #region Vihicles classes

    class Vihicle : Unit
    {
        public Vihicle()
        {
            ClassName = "Техника";
            
        }
    }

    class Tank : Vihicle
    {
        public Tank()
        {
            ClassName = "Танк";
            Name = GenerateRandomVihiclesName();
            Damage = GenerateRandomNumber(30, 50);
            Health = GenerateRandomNumber(180, 200);
        }
    }

    class Helicopter : Vihicle
    {
        public Helicopter()
        {
            ClassName = "Вертолёт";
            Name = GenerateRandomHelicopterName();
            Damage = GenerateRandomNumber(25, 40);
            Health = GenerateRandomNumber(150, 180);
        }
    }

    #endregion

    #region Interfaces

    interface ICombatEntity
    {
        string Name { get; }
        string ClassName { get; }
        int Damage { get; }
        int Health { get; }
        int Armor { get; }
    }

    interface IDamageable : ICombatEntity
    {
        bool IsAlive { get; }
        bool TryTakeDamage(int damage);
    }

    interface IDamageProvider
    {
        public void AttackTo(IDamageable target);
    }

    #endregion

    #region UserUtils

    static class Randomaizer
    {
        private static readonly Random s_random;
        private static readonly string[] s_names;
        private static readonly string[] s_vihicles_names;
        private static readonly string[] s_helicopter_names;

        static Randomaizer()
        {
            s_random = new();
            s_names = new string[]
            {
                "Варвар",
                "Космонафт",
                "Миледи",
                "Вульфич",
                "Страйк",
                "Герандич",
                "Фрея",
                "Крыса",
                "Нинка",
                "Царь",
                "Забота",
                "Прожариватель",
                "Овощ",
                "Имба",
                "Нагибатель",
                "Топчик",
                "Холивар",
                "Бывалый",
                "Пирожок",
                "Котейка",
                "Оливер",
                "Викрам",
                "Архидея",
                "Метрономщик",
                "Зимник",
                "Волкодав",
                "Богатырь",
                "Вафлич",
                "Вурдолакыч",
                "Зяблик",
                "Кудахта",
                "Чувиха",
                "Мордорка",
                "Куряха",
                "Смоляха",
                "Крендель",
                "Остряк",
                "Крушила",
                "Очкович",
                "Щавель",
                "Днище",
                "Нубичка",
                "Жираф",
                "Подлиза",
                "Лимурчик",
                "Попрыгун",
                "Тряпкович"
            };
            s_vihicles_names = new string[]
            {
                "Тигр",
                "Пантера",
                "Насорог",
                "Т-34",
                "ИС-2",
                "М48 Паттон",
                "Крусайдер",
                "Маус",
                "ИСУ-152",
                "Фердинанд",
                "Хелкат",
                "Вульфирина",
                "Шеридан",
                "Кромвель",
                "Челенджер",
                "Центурион",
            };
            s_helicopter_names = new string[]
            {
                "AH-64 Апач",
                "Вестленд Люкс",
                "Ка-60 Касатка",
                "Ми-24",
                "A129 Мангуст",
                "AH-1Z Вайпер",
                "AH-1Z Вайпер",
                "CH-47 Чинук",
                "UH-1 Ирокез",
                "Ка-52 Аллигатор",
                "WZ-10",
                "UH-60 Черная Акула"
            };
        }

        public static string GenerateRandomName()
        {
            return s_names[s_random.Next(0, s_names.Length)];
        }

        public static string GenerateRandomVihiclesName()
        {
            return s_vihicles_names[s_random.Next(0, s_vihicles_names.Length)];
        }

        public static string GenerateRandomHelicopterName()
        {
            return s_helicopter_names[s_random.Next(0, s_helicopter_names.Length)];
        }

        public static int GenerateRandomNumber(int minValue, int maxValue)
        {
            return s_random.Next(minValue, maxValue);
        }

        public static List<T> Shuffle<T>(List<T> array)
        {
            List<T> tempArray = new();
            int elementIndex;
            int arrayElementCount = array.Count;

            for (int i = 0; i < arrayElementCount; i++)
            {
                elementIndex = GenerateRandomNumber(0, array.Count);
                tempArray.Add(array[elementIndex]);
                array.RemoveAt(elementIndex);
            }

            return tempArray;
        }

        public static bool IsСhanceNow(int chancePercent)
        {
            int minPercent = 0;
            int maxPercent = 100;
            int randomValue = GenerateRandomNumber(minPercent, maxPercent);

            if (randomValue < chancePercent)
            {
                return true;
            }

            return false;
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

//Война
//Есть 2 взвода.
//1 взвод страны один,
//2 взвод страны два.
//Каждый взвод внутри имеет солдат.
//Нужно написать программу, которая будет моделировать бой этих взводов.
//Каждый боец - это уникальная единица,
//он может иметь уникальные способности или же уникальные характеристики,
//такие как повышенная сила.
//Побеждает та страна, во взводе которой остались выжившие бойцы.
//Не важно, какой будет бой, рукопашный, стрелковый.

//Штурмовик - Stormtrooper
//Снайпер - Sniper
//Десантник - Paratrooper
//Разведчик - Scout
//Пулеметчик - Heavy
//Гранатометчик - Grenade launcher
//Медик - Medic