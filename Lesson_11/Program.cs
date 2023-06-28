namespace Lesson_11
{
    internal class Program
    {
        static void Main()
        {

            int number;
            int minNumber = 0;
            int maxNumber = 100;
            int firstMultipleNumber = 3;
            int secondMultipleNumber = 5;
            int sumNumber = 0;
            Random random = new Random();

            number = random.Next(minNumber, maxNumber + 1);
            Console.Write($"Ряд чисел кратных \"{firstMultipleNumber}\" и \"{secondMultipleNumber}\":");

            for (int i = 0; i <= number; i++)
            {
                if (i % firstMultipleNumber == 0 || i % secondMultipleNumber == 0)
                {
                    sumNumber += i;
                    Console.Write($" {i} ");
                }
            }

            Console.WriteLine($"\nСумма положительных кратных чисел равен: {sumNumber}");
            Console.ReadKey();
        }
    }
}