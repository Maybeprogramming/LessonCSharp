namespace Lesson_03
{
    internal class Program
    {
        static void Main()
        {
            string name;
            int age;
            string zodiacSing;
            string job;

            Console.Write("Напишите ваше имя: ");
            name = Console.ReadLine();
            Console.Write($"{name} сколько вам лет? ");
            age = Convert.ToInt32(Console.ReadLine());
            Console.Write("К какому знаку задиака вы относитесь? ");
            zodiacSing = Console.ReadLine();
            Console.Write("Где вы работаете? ");
            job = Console.ReadLine();

            Console.WriteLine($"\nВас зовут {name}, вам {age} год, вы {zodiacSing} и работаете на {job}");
            Console.ReadLine();
        }
    }
}