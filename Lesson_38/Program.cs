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

namespace Lesson_38
{
    internal class Program
    {
        static void Main()
        {
            int damage = 35;
            int attackCount = 4;
            Player player = new Player("Котопёс", "Солдат");

            player.ShowInfo();
            player.Attack();

            for (int i = 0; i < attackCount; i++)
            {
                player.TryTakeDamage(damage);
            }

            Console.ReadLine();
        }
    }

    class Player
    {        
        private int _health = 100;
        private int _damage = 20;
        private int _armor = 5;

        public Player(string name, string rank)
        {
            Name = name;
            Rank = rank;
        }

        public string Name { get; set; }
        public string Rank { get; set; }
        public bool IsAlive
        {  
            get { return _health > 0; }
        }

        public void ShowInfo()
        {
            string infoText = $"Никнейм: {Name}" +
                              $"\nЗвание: {Rank}" +
                              $"\nHP: {_health}" +
                              $"\nУрон: {_damage}" +
                              $"\nБроня: {_armor}";
            
            Console.Write(infoText);
        }

        public void Attack()
        {
            Console.WriteLine($"\n\nИгрок ({Name}) производит атаку!");
        }

        public void TryTakeDamage (int damage)
        {
            if(IsAlive == true && _health >= damage)
            {
                _health -= damage - _armor;
                Console.WriteLine($"\nИгрок ({Name}) получает урон: {damage}, осталось здоровья: {_health}!");
            }
            else
            {
                Console.WriteLine($"\nУрон: {damage} по игроку {Name} не был нанесён!");
            }
        }
    }
}