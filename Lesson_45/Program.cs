namespace Lesson_45
{
    using static Generator;
    using static Display;
    using static Input;

    class Program
    {
        static void Main()
        {
            Fighter[] fighters =
            {
                new Warrior(AssignRandomName(), AssignRandomNumber(100,150), AssignRandomNumber(10,30), AssignRandomNumber(10,30)),
                new Warrior(AssignRandomName(), AssignRandomNumber(100,150), AssignRandomNumber(10,30), AssignRandomNumber(10,30)),
                new Warrior(AssignRandomName(), AssignRandomNumber(100,150), AssignRandomNumber(10,30), AssignRandomNumber(10,30)),
                new Warrior(AssignRandomName(), AssignRandomNumber(100,150), AssignRandomNumber(10,30), AssignRandomNumber(10,30)),
                new Warrior(AssignRandomName(), AssignRandomNumber(100,150), AssignRandomNumber(10,30), AssignRandomNumber(10,30))
            };

            foreach (var fighter in fighters)
            {
                Display.Print(fighter.ShowPresentationInfo(), '<', '>', ConsoleColor.Green);
                Console.Write("\n" + new string('-', 100) + "\n");
            }

            Input.TryEnterNumber("Введите число: ", out int number);

            Display.Print(Input.EnterString("Введите имя: "));

            Console.ReadLine();
        }
    }

    class BattleField
    {
        private List<Fighter> _fighters;

        public BattleField()
        {

        }

        public void StartAutoFight(Fighter fighterFirst, Fighter fighterSecond)
        {

        }

        private List<Fighter> FillFighters(int maxFighters)
        {
            return _fighters;
        }
    }

    abstract class Fighter : IAttackProvider, IDamageable
    {
        private int _health;
        public Fighter(string name, int health, int damage, int armor)
        {
            Name = name;
            Health = health;
            Damage = damage;
            Armor = armor;
        }

        public string Name { get; protected set; }
        public int Health
        {
            get => _health;
            protected set
            {
                if (value < 0)
                    _health = 0;
                else
                    _health = value;
            }
        }
        public int Damage { get; protected set; }
        public int Armor { get; protected set; }
        public bool IsAlive { get => Health > 0; }

        public virtual bool TakeDamage(int damage)
        {
            if (IsAlive == false)
                return false;

            Health -= damage - Armor;

            Console.WriteLine($"Я {Name} - получаю урон {damage}");

            return true;
        }

        public virtual void Attack(Fighter fighter)
        {
            fighter.TakeDamage(Damage);
        }

        public virtual string ShowPresentationInfo()
        {
            return $"Имя бойца: {Name}. " +
                   $"Характеристики: здоровье <{Health}>, " +
                   $"наносимый урон <{Damage}>, " +
                   $"показатель брони <{Armor}>";
        }
    }

    class Warrior : Fighter
    {
        private int _attackCounter = 0;
        private int _seriesAttackForCrit = 3;
        private int _critMultiplierDamage = 2;

        public Warrior(string name, int health, int damage, int armor) : base(name, health, damage, armor)
        {

        }

        public override void Attack(Fighter target)
        {
            if (_attackCounter == _seriesAttackForCrit)
            {
                target.TakeDamage(Damage * _critMultiplierDamage);
                _attackCounter = 0;
            }
            else
            {
                target.TakeDamage(Damage);
            }

            _attackCounter++;
        }
    }

    class Hunter : Fighter
    {
        private int _chanceDodgePercent;

        public Hunter(string name, int health, int damage, int armor) : base(name, health, damage, armor)
        {
            _chanceDodgePercent = 30;
        }

        public override bool TakeDamage(int damage)
        {
            if (IsDodged(_chanceDodgePercent) == true)
            {
                Console.WriteLine($"{Name} - уклонился от атаки");
                return false;
            }
            {
                return base.TakeDamage(damage);
            }
        }

        private bool IsDodged(int chanceDodgePercent)
        {
            int minPercentNumber = 1;
            int maxPercentNumber = 101;
            int resultPercent = AssignRandomNumber(minPercentNumber, maxPercentNumber);

            if (resultPercent <= chanceDodgePercent)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    class Assasin : Fighter
    {
        private int _healingPerAttackPercent;

        public Assasin(string name, int health, int damage, int armor) : base(name, health, damage, armor)
        {
            _healingPerAttackPercent = 10;
        }

        public override bool TakeDamage(int damage)
        {
            bool isTakeDamage = base.TakeDamage(damage);

            if (isTakeDamage == true)
                HealingPerTakeDamage(damage);

            return isTakeDamage;
        }

        private void HealingPerTakeDamage(int damage)
        {
            int healingPoint;
            int maxPercent = 100;

            healingPoint = damage * _healingPerAttackPercent / maxPercent;
            Health += healingPoint;
        }
    }

    class Wizzard : Fighter
    {
        public Wizzard(string name, int health, int damage, int armor, int mana) : base(name, health, damage, armor)
        {
            Mana = mana;
        }

        public int Mana { get; private set; }

        public override void Attack(Fighter target)
        {
            target.TakeDamage(Damage);
        }

        public override bool TakeDamage(int damage)
        {
            return false;
        }
    }

    class Shaman : Fighter
    {
        public Shaman(string name, int health, int damage, int armor, int mana) : base(name, health, damage, armor)
        {
            Mana = mana;
        }

        public int Mana { get; private set; }

        public override void Attack(Fighter target)
        {
            target.TakeDamage(Damage);
        }

        public override bool TakeDamage(int damage)
        {
            return false;
        }
    }

    static class Display
    {
        public static void Print(string sourceText, char startChar, char endChar, ConsoleColor consoleColor = ConsoleColor.White)
        {
            bool isStartChar = false;
            bool isFinishChar = false;
            ConsoleColor defaulColor = Console.ForegroundColor;

            foreach (char symbol in sourceText)
            {
                if (symbol.Equals(startChar))
                {
                    isStartChar = true;
                    Console.ForegroundColor = consoleColor;
                    Console.Write(symbol);
                }
                else if (symbol.Equals(endChar))
                {
                    Console.Write(symbol);
                    isFinishChar = true;
                    Console.ForegroundColor = defaulColor;
                }
                else if (isStartChar == true & isFinishChar == false)
                {
                    Console.ForegroundColor = consoleColor;
                    Console.Write(symbol);
                }
                else if (isStartChar == true & isFinishChar == true)
                {
                    isFinishChar = false;
                    isStartChar = false;
                    Console.ForegroundColor = defaulColor;
                    Console.Write(symbol);
                }
                else
                {
                    Console.ForegroundColor = defaulColor;
                    Console.Write(symbol);
                }
            }
        }

        public static void Print(string sourceText, ConsoleColor consoleColor = ConsoleColor.White)
        {
            ConsoleColor defaulColor = Console.ForegroundColor;
            Console.ForegroundColor = consoleColor;
            Console.Write(sourceText);
            Console.ForegroundColor = defaulColor;
        }
    }

    static class Input
    {
        public static bool TryEnterNumber(string messageText, out int number)
        {
            int result;
            bool isParse = false;

            while (isParse == false)
            {
                Display.Print($"\n{messageText} ");

                if (Int32.TryParse(Console.ReadLine(), out result) == true)
                {
                    isParse = true;
                    number = result;
                    return true;
                }
                else
                {
                    Display.Print("Ошибка! Вы ввели не число! Попробуйте снова...");
                }
            }

            number = 0;
            return false;
        }

        public static string EnterString(string message)
        {
            string inputString;

            Display.Print($"{message}");
            inputString = Console.ReadLine();

            if (inputString != String.Empty)
            {
                return inputString;
            }
            else
            {
                Display.Print("Строка не должна быть пустой! Попробуйте снова...\n", ConsoleColor.Red);
                EnterString(message);
            }

            return String.Empty;
        }
    }

    static class Generator
    {
        private static Random _random = new Random();
        private static List<string> _name = new()
        {
            "Василий", "Аркадий", "Геннадий", "Евгения", "Мартин", "Джон",
                "Калин", "Питер", "Снежок", "Аннет", "Валькирия", "Виверна",
                "Полинка", "Волкодав", "Кинетик", "Антибиотик", "Флеш"
        };

        public static int AssignRandomNumber(int minValue, int maxValue)
        {
            return _random.Next(minValue, maxValue + 1);
        }

        public static string AssignRandomName()
        {
            return _name[_random.Next(_name.Count)];
        }
    }

    interface IAttackProvider
    {
        public void Attack(Fighter target);
    }

    interface IDamageable
    {
        public bool TakeDamage(int damage);
    }
}

//Гладиаторские бои
//Создать 5 бойцов,
//пользователь выбирает 2 бойцов
//и они сражаются друг с другом до смерти.
//У каждого бойца могут быть свои статы.
//Каждый игрок должен иметь особую способность для атаки,
//которая свойственна только его классу!
//Если можно выбрать одинаковых бойцов,
//то это не должна быть одна и та же ссылка на одного бойца,
//чтобы он не атаковал сам себя.
//Пример, что может быть уникальное у бойцов.
//Кто-то каждый 3 удар наносит удвоенный урон,
//другой имеет 30% увернуться от полученного урона,
//кто-то при получении урона немного себя лечит.
//Будут новые поля у наследников.
//У кого-то может быть мана и это только его особенность.