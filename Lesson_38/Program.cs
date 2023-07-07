//Работа с классами

//Создать класс игрока,
//с полями, содержащими информацию об игроке и
//методом, который выводит информацию на экран.
//В классе обязательно должен быть конструктор

//Условия для зачёта
//Задача выполнена полностью
//Имя переменной отражает её суть.
//Поле/свойство названо в соответствии с нотацией
//Название метода отражает то, что он делает
//Очередность в классе
//Отсутствие повторения имени класса внутри класса
//Соблюдена необходимая доступность полей класс

//Доработать.
//1 - Не должно быть публичных полей/сеттеров,
//их значение вы можете изменить из любой точки программы
//- это нарушает инкапсуляцию, данные не защищены.
//Сделайте их приватными. { get; set; } - не отличается от публичного поля.
//2 - public bool IsAlive => _health > 0; - так запись более лаконичная.
//3 - немного странно, что игрок бьет сам себя. Сделайте тогда двух игроков.

using System.Runtime.CompilerServices;

namespace Lesson_38
{
    internal class Program
    {
        static void Main()
        {
            Console.SetBufferSize(70, 300);
            Console.SetWindowSize(70, 30);

            int catDamage = 20;
            int catHealth = 50;
            int catArmor = 5;
            int dogDamage = 15;
            int dogHealth = 65;
            int dogArmor = 10;
            int attackCount = 5;
            Player playerCat = new("Кот", "Ассасин", catHealth, catDamage, catArmor);
            Player playerDog = new Player("Пёс", "Воин", dogHealth, dogDamage, dogArmor);

            playerCat.ShowInfo();
            Console.WriteLine();
            playerDog.ShowInfo();

            for (int i = 0; i < attackCount; i++)
            {
                Console.WriteLine($"\n{i + 1} - ход:");

                playerCat.Attack(playerDog);
                playerDog.Attack(playerCat);

                Console.WriteLine();
                Task.Delay(100).Wait();
            }

            if (playerCat.IsAlive == false & playerDog.IsAlive == false)
            {
                Console.WriteLine($"В этой битве нет победителй...");
            }
            else if (playerCat.IsAlive == true & playerDog.IsAlive == false)
            {
                Console.WriteLine($"{playerCat.Name} победил в этой битве");
            }
            else if (playerCat.IsAlive == false & playerDog.IsAlive == true)
            {
                Console.WriteLine($"{playerDog.Name} победил в этой битве");              
            }

            Console.ReadLine();
        }
    }

    class Player
    {
        private int _health;
        private int _damage;
        private int _armor;

        public Player(string name, string rank, int health = 50, int damage = 10, int armor = 0)
        {
            Name = name;
            Rank = rank;
            _health = health;
            _damage = damage;
            _armor = armor;
        }

        public string Name { get; private set; }
        public string Rank { get; private set; }
        public int Health 
        { 
            get 
            { 
                return _health;
            } 
            private set 
            { 
                if (value < 0) 
                    _health = 0;
                else 
                    _health = value;
            } 
        }
        public bool IsAlive => _health > 0;

        public void ShowInfo()
        {
            string infoText = $"Никнейм: {Name}" +
                              $"\nЗвание: {Rank}" +
                              $"\nHP: {_health}" +
                              $"\nУрон: {_damage}" +
                              $"\nБроня: {_armor}\n";

            Console.Write(infoText);
        }

        public void Attack(Player target)
        {
            Console.WriteLine($"Игрок ({Name}) атакует игрока {target.Name}!");
            target.TryTakeDamage(_damage);
        }

        private void TryTakeDamage(int damage)
        {
            if (IsAlive == true)
            {
                Health -= damage - _armor;
                Console.WriteLine($"Игрок ({Name}) получает урон: {damage}, осталось здоровья: {_health}!");
            }
            else
            {
                Console.WriteLine($"Урон: {damage} по игроку {Name} не был нанесён!");
            }
        }
    }
}