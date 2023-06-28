namespace Lesson_17
{
    internal class Program
    {
        static void Main()
        {
            int randomNumber;
            int totalNumber;
            int number = 2;
            int minDegreeCount = 1;
            int maxRandomNumber = 65;
            Random random = new Random();
            randomNumber = random.Next(maxRandomNumber);
            totalNumber = number;

            while (randomNumber >= totalNumber)
            {
                totalNumber *= number;
                minDegreeCount++;
            }

            Console.WriteLine($"Число {number} в степени {minDegreeCount} превосходит заданное число {randomNumber}");
            Console.WriteLine($"Результат: {totalNumber} > {randomNumber}");
            Console.WriteLine("\nРабота программы завершена.");
            Console.ReadKey();
        }
    }
}