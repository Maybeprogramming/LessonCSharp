﻿namespace Lesson_47
{
    using static Display;

    class Program
    {
        static void Main()
        {
            Console.Title = "Война";

            Medic medic = new();
            Stormtrooper stormtrooper = new();
            Tank tank = new();
            Engineer engineer = new();

            engineer.Repair(tank);
            medic.Heal(stormtrooper);

            while (engineer.IsAlive == true && medic.IsAlive == true)
            {
                engineer.AttackTo(medic);
                medic.AttackTo(engineer);

                Print($"Медик: {medic.Health}\n" +
                    $"Инженер: {engineer.Health}\n");
            }

            Console.ReadKey();
        }
    }

    class BattleField
    {
        Squad squad1 = new Squad(8, 2);
        Squad squad2 = new Squad(9, 1);

        public void BeginWar()
        {

        }

        public void Fight()
        {

        }
    }

    class Squad
    {
        private List<Unit>? squad;

        public Squad(int fighterCount, int vihiclesCount)
        {

        }


    }

    #region Пехота

    abstract class Unit : ICombatEntity, IDamageable, IDamageProvider
    {
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
        public int Health { get; protected set; }
        public int Armor { get; protected set; }
        public bool IsAlive { get => Health > 0; }
        public virtual string Name { get; set; }

        public virtual bool TryTakeDamage(int damage)
        {
            if (IsAlive == true)
            {
                Health -= damage - Armor;
                return true;
            }

            return false;
        }

        public virtual void AttackTo(IDamageable target)
        {
            if (IsAlive == true && target.IsAlive == true)
            {
                target.TryTakeDamage(Damage);
            }
        }
    }

    abstract class Fighter : Unit, IHealable
    {
        public Fighter()
        {
            ClassName = "Пехотинец";
            Name = Randomaizer.GenerateRandomName();
            Damage = 10;
            Health = 100;
            Armor = 0;
        }

        public virtual bool TryTakeHealing(int healthPoint)
        {
            if (IsAlive == true)
            {
                Health += healthPoint;
                return true;
            }

            return false;
        }
    }

    class Stormtrooper : Fighter
    {
        public Stormtrooper()
        {
            ClassName = "Штурмовик";
            Damage = Randomaizer.GenerateRandomNumber(10, 20);
            Health = Randomaizer.GenerateRandomNumber(100, 150);
            Armor = Randomaizer.GenerateRandomNumber(0, 6);
        }
    }

    class Sniper : Fighter
    {
        public Sniper()
        {
            ClassName = "Снайпер";
            Damage = Randomaizer.GenerateRandomNumber(15, 20);
            Health = Randomaizer.GenerateRandomNumber(80, 100);
            Armor = Randomaizer.GenerateRandomNumber(0, 1);
        }
    }

    class Paratrooper : Fighter
    {
        public Paratrooper()
        {
            ClassName = "Десантник";
            Damage = Randomaizer.GenerateRandomNumber(10, 15);
            Health = Randomaizer.GenerateRandomNumber(120, 180);
            Armor = Randomaizer.GenerateRandomNumber(0, 6);
        }
    }

    class Scout : Fighter
    {
        public Scout()
        {
            ClassName = "Разведчик";
            Damage = Randomaizer.GenerateRandomNumber(8, 10);
            Health = Randomaizer.GenerateRandomNumber(60, 80);
            Armor = Randomaizer.GenerateRandomNumber(0, 2);
        }
    }

    class Heavy : Fighter
    {
        public Heavy()
        {
            ClassName = "Пулеметчик";
            Damage = Randomaizer.GenerateRandomNumber(10, 15);
            Health = Randomaizer.GenerateRandomNumber(150, 200);
            Armor = Randomaizer.GenerateRandomNumber(0, 2);
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
            Damage = Randomaizer.GenerateRandomNumber(20, 30);
            Health = Randomaizer.GenerateRandomNumber(150, 200);
            Armor = Randomaizer.GenerateRandomNumber(0, 2);
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

    class Engineer : Fighter, IRepairProvider
    {
        private int _repairPoints;

        public Engineer()
        {
            ClassName = "Инженер";
            _repairPoints = Randomaizer.GenerateRandomNumber(20, 40);
        }

        public void Repair(Vihicle target)
        {
            if (target == null || target is IRepairable == false)
            {
                Print($"Ошибка! Цели нет, памагите! =(\n");
                return;
            }

            if (target.TryAcceptRepair(_repairPoints) == true)
            {
                Print($"{ClassName}: {Name} отремотировал цель: {target.Name} на {_repairPoints} очков здоровья\n");
            }
            else
            {
                Print($"Не получилось отремонтировать цель: {target.Name}!!!\n");
            }
        }
    }

    class Medic : Fighter, IHeal
    {
        private int _healingPoints;

        public Medic()
        {
            ClassName = "Медик";
            _healingPoints = Randomaizer.GenerateRandomNumber(20, 40);
        }

        public void Heal(Fighter target)
        {
            if (target == null && target is IHealable == false)
            {
                Print($"Ошибка! Цели нет, памагите! =(\n");
                return;
            }

            if (target.TryTakeHealing(_healingPoints) == true)
            {
                Print($"{ClassName}: {Name} вылечил цель: {target.ClassName} на {_healingPoints} очков здоровья\n");
            }
            else
            {
                Print($"Не получилось вылечить цель: {target.ClassName}!!!\n");
            }
        }
    }

    #endregion

    #region Боевая техника
    abstract class Vihicle : Unit, IRepairable, IDamageable
    {
        protected Vihicle()
        {
            ClassName = "Техника";
            Name = Randomaizer.GenerateRandomVihiclesName();
        }

        public override string Name { get; set; }

        public virtual bool TryAcceptRepair(int healthPoint)
        {
            if (IsAlive == true)
            {
                Health += healthPoint;
                return true;
            }

            return false;
        }
    }

    class Tank : Vihicle
    {
        public Tank()
        {
            Damage = Randomaizer.GenerateRandomNumber(30, 50);
            ClassName = "Танк";
        }
    }

    class Helicopter : Vihicle
    {
        public Helicopter()
        {
            Damage = Randomaizer.GenerateRandomNumber(30, 50);
            ClassName = "Вертолёт";
        }
    }

    #endregion

    abstract class Ability
    {
        private string? _name;
        private string? _description;

        public Ability(string name, string description)
        {
            _name = name;
            _description = description;
        }
    }

    #region Enums

    enum Fighters
    {
        Stormtrooper,
        Sniper,
        Paratrooper,
        Scout,
        Heavy,
        GrenadeLauncher,
        Engineer,
        Medic
    }

    enum Vihicles
    {
        Tank,
        Helicopter
    }

    #endregion

    #region Interfaces

    interface ICombatEntity
    {
        string Name { get; set; }
    }

    interface IDamageable
    {
        bool IsAlive { get; }
        bool TryTakeDamage(int damage);
    }

    interface IDamageProvider
    {
        public void AttackTo(IDamageable target);
    }

    interface IHeal
    {
        public void Heal(Fighter target);
    }

    interface IHealable
    {
        public bool TryTakeHealing(int healthPoint);
    }

    interface IRepairProvider
    {
        public void Repair(Vihicle target);
    }

    interface IRepairable
    {
        public bool TryAcceptRepair(int healthPoint);
    }

    #endregion

    #region UserUtils

    static class Randomaizer
    {
        private static readonly Random s_random;
        private static readonly string[] s_names;
        private static readonly string[] s_vihicles_names;

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
        }

        public static string GenerateRandomName()
        {
            return s_names[s_random.Next(0, s_names.Length)];
        }

        public static string GenerateRandomVihiclesName()
        {
            return s_vihicles_names[s_random.Next(0, s_vihicles_names.Length)];
        }

        public static int GenerateRandomNumber(int minValue, int maxValue)
        {
            return s_random.Next(minValue, maxValue);
        }
    }

    static class UserInput
    {
        public static int ReadInt(string message, int minValue = int.MinValue, int maxValue = int.MaxValue)
        {
            int result;

            Print(message);

            while (int.TryParse(Console.ReadLine(), out result) == false || result < minValue || result >= maxValue)
            {
                Print("Ошибка!. Попробуйте снова!\n");
            }

            return result;
        }

        public static void WaitToPressKey(string message)
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

        public static void PrintLine(int symbolCount = 100)
        {
            Print($"{new string('-', symbolCount)}\n");
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