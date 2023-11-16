namespace Lesson_47
{
    using System.Reflection;
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
            const int MillisecondsDelay = 1500;

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
            int squadCount = 2;
            int minNumber = 0;
            int maxNumber = 100;
            int middleNumber = (minNumber + maxNumber) / squadCount;
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
            else if (_squad1.IsAlive == true)
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
        private FighterFactory _fighterFactory;
        private VihicleFactory _vihicleFactory;

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

            FillUnits(fighterCount, vihiclesCount);
            Print($"> Рекруты наняты.\n");
            Print($"> Боевая техника изготовлена.\n");

            Print($"> В отряде {fullCount} боевых единиц:\n");
            ShowInfo();
            Print($"> Отряд сформирован!\n");
        }

        private void FillUnits(int fightersCount, int vihiclesCount)
        {
            for (int i = 0; i < fightersCount; i++)
            {
                Fighter fighter = _fighterFactory.CreateRandomFigther();
                _squad.Add(fighter);
            }

            for (int i = 0; i < vihiclesCount; i++)
            {
                Vihicle vihicle = _vihicleFactory.CreateRandomVihicle();
                _squad.Add(vihicle);
            }
        }
    }

    #region Factory Metohds

    class FighterFactory
    {
        public Fighter CreateRandomFigther()
        {
            int damage = GenerateRandomNumber(20, 50);
            int health = GenerateRandomNumber(100, 200);
            int armor = GenerateRandomNumber(10, 30);

            List<Fighter> fighters = CreateFigters(GenerateRandomName(ClassName.Fighters), damage, health, armor);
            int figtherIndex = GenerateRandomNumber(0, fighters.Count);

            return fighters[figtherIndex];
        }

        private List<Fighter> CreateFigters(string name, int damage, int health, int armor)
        {
            return new List<Fighter>()
            {
                new Stormtrooper(name, damage, health , armor),
                new Heavy(name, damage, health , armor),
                new GrenadeLauncher(name, damage, health , armor),
                new Paratrooper(name, damage, health , armor),
                new Sniper(name, damage, health , armor),
                new Scout(name, damage, health , armor)
            };
        }
    }

    class VihicleFactory
    {
        public Vihicle CreateRandomVihicle()
        {
            int damage = GenerateRandomNumber(20, 50);
            int health = GenerateRandomNumber(100, 200);
            int armor = GenerateRandomNumber(10, 30);

            List<Vihicle> vihicles = CreateVihicles(damage, health, armor);
            int vihicleIndex = GenerateRandomNumber(0, vihicles.Count);

            return vihicles[vihicleIndex];
        }

        private List<Vihicle> CreateVihicles(int damage, int health, int armor)
        {
            return new List<Vihicle>()
            {
                new Tank(GenerateRandomName(ClassName.Tanks), damage, health, armor),
                new Helicopter(GenerateRandomName(ClassName.Helicopters), damage, health, armor)
            };
        }
    }

    #endregion

    #region Abstract class Unit

    abstract class Unit : IEntity, IDamageable, IDamageProvider
    {
        private int _health;

        protected Unit(string name, string className, int damage, int health, int armor)
        {
            ClassName = className;
            Damage = damage;
            Health = health;
            Armor = armor;
            Name = name;
        }

        public string ClassName { get; }
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
        public Fighter(string name, string className, int damage, int health, int armor) : base(name, className, damage, health, armor)
        {
        }
    }

    class Stormtrooper : Fighter
    {
        public Stormtrooper(string name, int damage, int health, int armor) : base(name, "Штурмовик", damage, health, armor) { }
    }

    class Sniper : Fighter
    {
        private readonly int _multiplyDamage;
        private readonly int _critChanceDamage;

        public Sniper(string name, int damage, int health, int armor) : base(name, "Снайпер", damage, health, armor)
        {
            _multiplyDamage = 2;
            _critChanceDamage = 50;
        }

        public override void AttackTo(IDamageable target)
        {
            TryToUsePreciseShot(target);

            base.AttackTo(target);
        }

        private void TryToUsePreciseShot(IDamageable target)
        {
            if (target is Fighter && IsСhanceNow(_critChanceDamage) == true)
            {
                int damage = Damage * _multiplyDamage;
                Print($"{ClassName}: {Name} произвёл точный выстрел!\n", ConsoleColor.Green);
                target.TryTakeDamage(damage);
            }
        }
    }

    class Paratrooper : Fighter
    {
        private int _chanceAbsorbingDamagePercent;
        private int _reductionDamageValue;

        public Paratrooper(string name, int damage, int health, int armor) : base(name, "Десантник", damage, health, armor)
        {
            _chanceAbsorbingDamagePercent = 30;
            _reductionDamageValue = 2;
        }

        public override bool TryTakeDamage(int damage)
        {
            damage = TryToUseAbsorbingDamage(damage);

            return base.TryTakeDamage(damage);
        }

        private int TryToUseAbsorbingDamage(int damage)
        {
            if (IsСhanceNow(_chanceAbsorbingDamagePercent) == true & damage > _reductionDamageValue)
            {
                damage -= _reductionDamageValue;
                Print($"{ClassName}: {Name} использовал способность снижения получаемого урона.\n", ConsoleColor.Green);
                Print($"Урон уменьшился на {_reductionDamageValue} единиц.\n", ConsoleColor.Green);
            }

            return damage;
        }
    }

    class Scout : Fighter
    {
        private int _chanceDodgePercent;

        public Scout(string name, int damage, int health, int armor) : base(name, "Разведчик", damage, health, armor)
        {
            _chanceDodgePercent = 30;
        }

        public override bool TryTakeDamage(int damage)
        {
            if (IsСhanceNow(_chanceDodgePercent) == true)
            {
                Print($"{ClassName}: {Name} уклонился от урона ({damage} единиц урона).\n", ConsoleColor.Green);
                return false;
            }

            return base.TryTakeDamage(damage);
        }
    }

    class Heavy : Fighter
    {
        private readonly int _numberShots;

        public Heavy(string name, int damage, int health, int armor) : base(name, "Пулеметчик", damage, health, armor)
        {
            _numberShots = 3;
        }

        public override void AttackTo(IDamageable target)
        {
            if (target is Fighter)
            {
                Print($"{ClassName}: {Name} стреляет очередью в сторону цели >{target.ClassName}: {target.Name}<\n", ConsoleColor.Green);

                for (int i = 0; i < _numberShots; i++)
                {
                    base.AttackTo(target);
                }
            }
            else if (target is Tank)
            {
                target.TryTakeDamage(ApplyGrenageDamage());
            }

            base.AttackTo(target);
        }

        private int ApplyGrenageDamage()
        {
            int grenageDamage = GenerateRandomNumber(30, 50);
            Print($"{ClassName}: {Name} кидает гранату в сторону цели\n", ConsoleColor.Green);
            return grenageDamage;
        }
    }

    class GrenadeLauncher : Fighter
    {
        private int _criticalModifier;
        private int _criticalDamage;
        private int _baseDamage;

        public GrenadeLauncher(string name, int damage, int health, int armor) : base(name, "Гранатометчик", damage, health, armor)
        {
            _baseDamage = Damage;
            _criticalModifier = 2;
            _criticalDamage = _criticalModifier * Damage;
        }

        public override void AttackTo(IDamageable target)
        {
            MultiplyDamageToAttackVihicle(target);

            base.AttackTo(target);
        }

        private void MultiplyDamageToAttackVihicle(IDamageable target)
        {
            if (target is Vihicle == true)
            {
                Damage = _criticalDamage;
            }
            else
            {
                Damage = _baseDamage;
            }
        }
    }

    #endregion

    #region Vihicles classes

    class Vihicle : Unit
    {
        public Vihicle(string name, string className, int damage, int health, int armor) : base(name, className, damage, health, armor)
        {
        }
    }

    class Tank : Vihicle
    {
        public Tank(string name, int damage, int health, int armor) : base(name, "Танк", damage, health, armor)
        {
        }
    }

    class Helicopter : Vihicle
    {
        private readonly int _barrageCount;

        public Helicopter(string name, int damage, int health, int armor) : base(name, "Вертолёт", damage, health, armor)
        {
            _barrageCount = 2;
        }

        public override void AttackTo(IDamageable target)
        {
            if (target is Tank)
            {
                UseBarrageFire(target);
            }
            else
            {
                base.AttackTo(target);
            }
        }

        private void UseBarrageFire(IDamageable target)
        {
            Print($"{ClassName}: {Name} применяет шквал огня против >{target.ClassName}: {target.Name}<\n", ConsoleColor.Green);

            for (int i = 0; i < _barrageCount; i++)
            {
                target.TryTakeDamage(Damage);
            }
        }
    }

    #endregion

    #region Enums

    enum ClassName
    {
        Fighters,
        Tanks,
        Helicopters
    }

    #endregion

    #region Interfaces

    interface IEntity
    {
        string Name { get; }
        string ClassName { get; }
        int Damage { get; }
        int Health { get; }
        int Armor { get; }
    }

    interface IDamageable : IEntity
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
                "CH-47 Чинук",
                "UH-1 Ирокез",
                "Ка-52 Аллигатор",
                "WZ-10",
                "UH-60 Черная Акула"
            };
        }

        public static string GenerateRandomName(ClassName className)
        {
            string[] name = null;

            switch (className)
            {
                case ClassName.Fighters:
                    name = s_names;
                    break;

                case ClassName.Tanks:
                    name = s_vihicles_names;
                    break;

                case ClassName.Helicopters:
                    name = s_helicopter_names;
                    break;
            }

            return GetRandomName(name);
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

            return randomValue < chancePercent;
        }

        private static string GetRandomName(string[] name)
        {
            return name[s_random.Next(0, name.Length)];
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

//Александр Михновец
//1) конструктор Randomaizer()
//-если инициализация поля занимает больше одной строки
//- отделяйте такую инициализацию пустыми строками .
//2) Слишком одинаковые методы генерации имени,
//названия транспортного средства, вертолета
//- есть смысл передавать в метод массив, и получать название.
//3) метод Shuffle()
//-Не удаляйте объекты при тасовании - меняйте их местами .
//Т.е. вполне можно справиться без дополнительного списка.
//4) IsСhanceNow() условие и 2 return
//можно сократить до одной строчки :
//return randomValue < chancePercent.
//5) class Unit - protected поле именуется с большой буквы .
//6) Если свойство занимает больше одной строки
//- отделяйте его от остальных методов пустой строкой.
//7) class Stormtrooper _className = "Штурмовик" и
//ClassName = "Штурмовик" - зачем поле?
//8) class FighterFactory -List < Type >
//-в такой список можно добавить не только Fighter
//-у меня получилось добавить транспорт
//9) class VihicleFactory -List < Type > -то же самое .
//10) к пункту 8 и 9
//- Использование Activator.CreateInstance
//для создания экземпляров объектов может быть не самой эффективной и безопасной практикой.
//Мы не можем контролировать конструктор, а создание объекта самое важное в ооп.
//11) (minNumber + maxNumber) / 2 - магическое число
//12) AnnounceVictory()
//- вторая ветка условия должна содержать только одно условие,
//т.к. то что после && дубляж проверки первой ветки
//13) Рекомендую обратится за более подробным разбором кода
//к ментору Напильника - Алексею Кононову.
//Он предупрежден и ждет Вас с 20.00 по мск в будние дни на голосовом канале дискорда

