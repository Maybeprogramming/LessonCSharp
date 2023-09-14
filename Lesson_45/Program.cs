namespace Lesson_45
{
    using static Display;

    class Program
    {
        static void Main()
        {
            Fighter[] fighters =
            {
                new Warrior("Володя", 100, 20, 20),
                new Hunter ("Ева", 80, 25, 15)
            };

            foreach (var fighter in fighters)
            {
                PrintColorText(fighter.ShowInfo() + "\n", '<', '>', ConsoleColor.Green);
                
                if (fighter is Warrior warrior)
                    warrior.Healing();

                if (fighter is Hunter hunter)
                    hunter.SummonPet();

                Console.WriteLine(new string('-', 50));
            }

            Console.ReadLine();
        }
    }

    abstract class Fighter
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

        public abstract void TakeDamage(int damage);

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
    }

    static class Display
    {
        public static void PrintColorText(string text,char startChar, char endChar, ConsoleColor consoleColor = ConsoleColor.White)
        {
            bool isStartChar = false;
            bool isFinishChar = false;
            ConsoleColor defaulColor = Console.ForegroundColor;

            foreach (char symbol in text)
            {
                if(symbol.Equals(startChar)) 
                {
                    isStartChar = true;
                    Console.ForegroundColor = consoleColor;
                    Console.Write(symbol);
                }
                else if(symbol.Equals(endChar))
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
                else if(isStartChar == true & isFinishChar == true)
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