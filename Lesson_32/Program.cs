namespace Lesson_32
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int[,] numbers;
            int rowCount;
            int collumCount;
            int minRandNumber = -9;
            int maxRandNumber = 9;
            int zeroValue = 0;
            int maxNumber = int.MinValue;

            Console.WriteLine("Программа по работе с двухмерной матрицой.");
            Console.Write("Введите колличество строк: ");
            rowCount = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите количество столбцов: ");
            collumCount = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"\nИсходная матрица чисел, размерностью: {rowCount}х{collumCount}\n");
            numbers = new int[rowCount, collumCount];

            for (int i = 0; i < numbers.GetLength(0); i++)
            {
                for (int j = 0; j < numbers.GetLength(1); j++)
                {
                    numbers[i, j] = random.Next(minRandNumber, maxRandNumber);
                    Console.Write($" {numbers[i, j]} \t");

                    if (numbers[i, j] > maxNumber)
                    {
                        maxNumber = numbers[i, j];
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine("\nПолученная матрица:\n");

            for (int i = 0; i < numbers.GetLength(0); i++)
            {
                for (int j = 0; j < numbers.GetLength(1); j++)
                {
                    if (numbers[i, j] == maxNumber)
                    {
                        numbers[i, j] = zeroValue;
                    }

                    Console.Write($" {numbers[i, j]} \t");
                }

                Console.WriteLine();
            }

            Console.WriteLine($"\nМаксимальное число в матрице равно: {maxNumber}");
            Console.ReadKey();
        }
    }
}