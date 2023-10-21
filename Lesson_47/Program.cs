namespace Lesson_47
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
            Engineer engineer = new ();

            engineer.Repair(tank);
            medic.Heal(stormtrooper);

            List<Unit> units = new()
            {
                medic,
                stormtrooper,
                tank,
                engineer
            };


            Console.ReadKey();
        }
    }

    class BattleField
    {
        Squad squad1 = new Squad();
        Squad squad2 = new Squad();

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
    }

    abstract class Unit : ICombatEntity, IDamageable, IDamageProvider
    {
        protected Unit()
        {
            ClassName = "Юнит";
            Damage = 10;
            Health = 100;
            Armor = 5;
            UnitName = "Юнит";
        }

        public string ClassName { get; protected set; }
        public int Damage { get; protected set; }
        public int Health { get; protected set; }
        public int Armor { get; protected set; }
        public bool IsAlive { get => Health > 0; }
        public virtual string UnitName { get; set; }

        public virtual bool TryTakeDamage(int damage)
        {
            if (IsAlive == true)
            {
                Health -= damage;
                return true;
            }

            return false;
        }

        public virtual void AttackTo(Unit target)
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
            ClassName = "Пехота";
            UnitName = "Василий";
            Damage = 10;
            Health = 100;
            Armor = 5;
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
            Health = 20;
        }

    }

    class Sniper : Fighter
    {
    }

    class Paratrooper : Fighter
    {
    }

    class Scout : Fighter
    {
    }

    class Heavy : Fighter
    {
    }

    class GrenadeLauncher : Fighter
    {
    }

    class Engineer : Unit, IRepairProvider
    {
        public void Repair(Unit target)
        {
            IRepairable healableTarget = target as IRepairable;

            if (healableTarget != null)
            {
                if (healableTarget.TryAcceptRepair(50) == true)
                {
                    Print($"Получилось отремонтровать {target.UnitName} на {50} поинтов\n");
                }
                else
                {
                    Print($"Ааааа, не получилось отремонтировать {target.UnitName}!!!\n");
                }
            }
            else
            {
                Print($"Всё сломалось! цель без цели! =(\n");
            }
        }
    }

    class Medic : Fighter, IHeal
    {
        public void Heal(Unit target)
        {
            IHealable healableTarget = target as IHealable;

            if (healableTarget != null)
            {
                if (healableTarget.TryTakeHealing(50) == true)
                {
                    Print($"Получилось вылечить {target.ClassName} на {50} поинтов\n");
                }
                else
                {
                    Print($"Ааааа, не получилось вылечить {target.ClassName}!!!\n");
                }
            }
            else
            {
                Print($"Всё сломалось! цель без цели! =(\n");
            }
        }
    }

    abstract class Vihicles : Unit, IRepairable
    {
        protected Vihicles()
        {
            UnitName = "Боевая техника";
        }

        public override string UnitName { get; set; }

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

    class Tank : Vihicles
    {

    }

    class Helicopter : Vihicles
    {

    }

    #region Interfaces

    interface ICombatEntity
    {
        string UnitName { get; set; }
    }

    interface IDamageable
    {
        public abstract bool TryTakeDamage(int damage);
    }

    interface IDamageProvider
    {
        public abstract void AttackTo(Unit target);
    }

    interface IHeal
    {
        public abstract void Heal(Unit target);
    }

    interface IHealable
    {
        public abstract bool TryTakeHealing(int healthPoint);
    }

    interface IRepairProvider
    {
        public abstract void Repair(Unit target);
    }

    interface IRepairable
    {
        public abstract bool TryAcceptRepair(int healthPoint);
    }

    #endregion

    static class Randomaizer
    {
        private static readonly Random s_random;
        private static readonly string[] s_names;

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
        }

        public static string GenerateRandomName()
        {
            return s_names[s_random.Next(0, s_names.Length)];
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

//Combat Unit - реализовать