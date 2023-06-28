namespace Lesson_20
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[,] numbers =
            {
            { 4, 6, 3, 1, 8, 6},
            { 3, 1, 8, 5, 2, 9},
            { 8, 3, 7, 2, 1, 4},
            { 1, 7, 3, 4, 6, 2},
            { 3, 1, 8, 5, 2, 9},
            { 8, 3, 7, 2, 1, 4}
        };
            int numbersSum = 0;
            int numbersMultiply = 1;
            int rowIndex = 2;
            int collumIndex = 1;

            Console.WriteLine("Дана двухмерная матрица чисел:\n");

            for (int i = 0; i < numbers.GetLength(0); i++)
            {
                for (int j = 0; j < numbers.GetLength(1); j++)
                {
                    Console.Write($" {numbers[i, j]} ");
                }

                Console.WriteLine();
            }

            for (int i = 0; i < numbers.GetLength(1); i++)
            {
                numbersSum += numbers[rowIndex - 1, i];
            }

            for (int i = 0; i < numbers.GetLength(0); i++)
            {
                numbersMultiply *= numbers[i, collumIndex - 1];
            }

            Console.WriteLine($"\nСумма всех чисел {rowIndex} строки равна: {numbersSum}");
            Console.WriteLine($"Произведение всех чисел {collumIndex} столбца равна: {numbersMultiply}");
            Console.ReadKey();
        }
    }
}