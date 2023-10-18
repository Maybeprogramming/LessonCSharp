namespace Lesson_47
{
    using static Randomaizer;
    using static UserInput;
    using static Display;

    class Program
    {
        static void Main()
        {
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

    }

    abstract class FighterUnit : ICombatEntity, IDamageable, IDamageProvider
    {
        protected FighterUnit()
        {
            ClassName = "Боец";
            Damage = 10;
            Health = 100;
            Armor = 5;
            EntityName = "Новичок";
        }

        public string ClassName { get; protected set; }
        public int Damage { get; protected set; }
        public int Health { get; protected set; }
        public int Armor { get; protected set; }
        public bool IsAlive { get => Health > 0; }
        public virtual string EntityName { get; set; }

        public virtual bool TryTakeDamage(int damage)
        {
            if (IsAlive == true)
            {
                Health -= damage;
                return true;
            }

            return false;
        }

        public virtual void AttackTo(FighterUnit target)
        {
            if (IsAlive == true && target.IsAlive == true)
            {
                target.TryTakeDamage(Damage);
            }
        }
    }

    abstract class Fighter : FighterUnit, IHeal
    {
        public Fighter()
        {
            ClassName = "Боец (По умолчанию)";
            EntityName = "Пехота";
            Damage = 10;
            Health = 100;
            Armor = 5;
        }

        public virtual void Heal(int healthPoint)
        {
            Health += healthPoint;
        }
    }

    class Stormtrooper : FighterUnit
    {
        public Stormtrooper()
        {
            Health = 20;
        }

    }

    class Sniper : FighterUnit
    {
    }

    class Paratrooper : FighterUnit
    {
    }

    class Scout : FighterUnit
    {
    }

    class Heavy : FighterUnit
    {
    }

    class GrenadeLauncher : FighterUnit
    {
    }

    class Medic : FighterUnit
    {
    }

    abstract class Vihicles : FighterUnit
    {
        protected Vihicles()
        {
            EntityName = "Боевая техника";
        }

        public override string EntityName { get; set; }
    }

    class Tank : Vihicles
    {
        
    }

    class Helicopter : Vihicles
    {

    }

    #region Intarfaces

    interface ICombatEntity
    {
        string EntityName { get; set; }
    }

    interface IDamageable
    {
        public abstract bool TryTakeDamage(int damage);
    }

    interface IDamageProvider
    {
        public abstract void AttackTo(FighterUnit target);
    }

    interface IHeal
    {
        public abstract void Heal(int healthPoint);
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

            Console.Write(message);

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