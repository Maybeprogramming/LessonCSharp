namespace Lesson_22
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] numbers;
            int arraySize;
            int firstElementIndex = 0;
            int lastElementIndex;
            int maxRandNumber = 1000;
            Random random = new Random();

            Console.Title = "Программа для поиска локальных максимумов в одномерном массиве";
            Console.Write("Введите размер массива: ");
            arraySize = Convert.ToInt32(Console.ReadLine());
            numbers = new int[arraySize];
            lastElementIndex = arraySize - 1;

            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = random.Next(maxRandNumber);
            }

            Console.WriteLine("Массив успешно заполнен случайными целыми числами.");
            Console.WriteLine($"\nПолученный одномерный массив состоящий из {arraySize} элементов:");

            foreach (int number in numbers)
            {
                Console.Write($"{number} ");
            }

            Console.WriteLine("\n\nЛокальные максимумы: ");

            if (numbers[firstElementIndex] > numbers[firstElementIndex + 1])
            {
                Console.Write($"{numbers[firstElementIndex]} ");
            }

            for (int i = firstElementIndex + 1; i < numbers.Length - 1; i++)
            {
                if (numbers[i] > numbers[i - 1] && numbers[i] > numbers[i + 1])
                {
                    Console.Write($"{numbers[i]} ");
                }
            }

            if (numbers[lastElementIndex] > numbers[lastElementIndex - 1])
            {
                Console.Write($"{numbers[lastElementIndex]} ");
            }

            Console.ReadKey();
        }
    }
}