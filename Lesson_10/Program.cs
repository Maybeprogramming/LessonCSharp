namespace Lesson_10
{
    internal class Program
    {
        static void Main()
        {
            int startNumber = 5;
            int endNumber = 96;
            int stepIteration = 7;

            Console.Write("Последовательность чисел:");

            for (int i = startNumber; i <= endNumber; i += stepIteration)
            {
                Console.Write(i + " ");
            }

            Console.ReadKey();
        }
    }
}