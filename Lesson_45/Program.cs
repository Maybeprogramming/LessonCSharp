namespace Lesson_45
{
    class Program
    {
        static void Main()
        {
            Fighter[] fighters =
            {
                new Warrior("Володя", 100, 20, 20),
                new Hunter ("Ева", 80, 25, 15),
                new Assasin ("Колян", 80, 25, 15),
                new Wizzard ("Мишаня", 80, 25, 15, 50),
                new Shaman ("Ангелинка", 80, 25, 15, 100)
            };

            foreach (var fighter in fighters)
            {
                Display.Print(fighter.ShowInfo() + $"({fighter.GetType()})" + "\n", '<', '>', ConsoleColor.Green);

                if (fighter is Warrior warrior)
                    warrior.Healing();

                if (fighter is Hunter hunter)
                    hunter.SummonPet();

                Console.WriteLine(new string('-', 50));
            }

            Input.TryEnterNumber("Введите число: ", out int number);

            Display.Print(Input.EnterString("Введите имя: "));

            Console.ReadLine();
        }
    }


    class BattleField
    {
        private List<Fighter> _fighters;
        private List<string> _namesFigthers;

        public BattleField()
        {
            _namesFigthers = new()
            {
                "Василий", "Аркадий", "Геннадий", "Евгения", "Мартин", "Джон",
                "Калин", "Питер", "Снежок", "Аннет", "Валькирия", "Виверна",
                "Полинка", "Волкодав", "Кинетик", "Антибиотик"
            };
        }

        public void Fight(Fighter fighterFirst, Fighter fighterSecond)
        {

        }

        private List<Fighter> FillFighters()
        {
            return _fighters;
        }
    }

    abstract class Fighter : IAttackProvider, IDamageable
    {
        public Fighter(string name, int health, int damage, int armor)
        {
            Name = name;
            Health = health;
            Damage = damage;
            Armor = armor;
        }

        public string Name { get; private set; }
        public int Health { get; private set; }
        public int Damage { get; private set; }
        public int Armor { get; private set; }
        public bool IsAlive { get => Health > 0; }

        public virtual void TakeDamage(int damage)
        {
            if (IsAlive == false) return;

            Health -= damage - Armor;
        }

        public virtual void Attack(Fighter fighter)
        {
            fighter.TakeDamage(Damage);
        }

        public virtual string ShowInfo()
        {
            return $"Имя бойца: {Name}. Характеристики: здоровье <{Health}>, наносимый урон <{Damage}>, показатель брони <{Armor}>";
        }
    }

    class Warrior : Fighter
    {
        public Warrior(string name, int health, int damage, int armor) : base(name, health, damage, armor)
        {

        }

        public override void TakeDamage(int damage)
        {
            Console.WriteLine($"Я {Name} - получаю урон {damage}");
        }

        public void Healing()
        {
            Console.WriteLine("Прилив священного огня! Исцели моё тело и душу!");
        }

        public override void Attack(Fighter target)
        {
            target.TakeDamage(Damage);
        }
    }

    class Hunter : Fighter
    {
        public Hunter(string name, int health, int damage, int armor) : base(name, health, damage, armor)
        {
        }

        public override void TakeDamage(int damage)
        {
            Console.WriteLine($"Я {Name} - получаю урон {damage}");
        }

        public void SummonPet()
        {
            Console.WriteLine("Призываю грозную морскую свинку!");
        }

        public override void Attack(Fighter target)
        {
            target.TakeDamage(Damage);
        }
    }

    class Assasin : Fighter
    {
        public Assasin(string name, int health, int damage, int armor) : base(name, health, damage, armor)
        {
        }

        public override void Attack(Fighter target)
        {
            target.TakeDamage(Damage);
        }

        public override void TakeDamage(int damage)
        {

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

        public override void TakeDamage(int damage)
        {

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

        public override void TakeDamage(int damage)
        {

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

    interface IAttackProvider
    {
        public void Attack(Fighter target);
    }

    interface IDamageable
    {
        public void TakeDamage(int damage);
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