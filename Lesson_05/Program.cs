namespace Lesson_05
{
    internal class Program
    {
        static void Main()
        {
            string name = "Иванов";
            string surname = "Сергей";
            string bufferName;

            Console.WriteLine($"Ваше имя: {name}, фамилия: {surname} ");

            bufferName = name;
            name = surname;
            surname = bufferName;

            Console.WriteLine($"\nВаше имя: {name}, фамилия: {surname} ");

            Console.ReadLine();
        }
    }
}