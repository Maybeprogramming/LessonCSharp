namespace Lesson_25
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] numbers;
            Random random = new Random();
            int maxNumbers = 1000;
            int maxArrayLength = 30;
            numbers = new int[maxArrayLength];

            Console.Title = "Программа для сортировки массива от меньшего до большего \"методом выбора\"";

            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = random.Next(maxNumbers);
            }

            Console.WriteLine($"Успешно сгенерирован массив длинной: {numbers.Length} чисел");
            Console.WriteLine("\nИсходный массив:");

            foreach (int number in numbers)
            {
                Console.Write($"{number} ");
            }

            for (int i = 0; i < numbers.Length - 1; i++)
            {
                int minValue = i;

                for (int j = i + 1; j < numbers.Length; j++)
                {
                    if (numbers[j] < numbers[minValue])
                    {
                        minValue = j;
                    }
                }

                int tempNumbers = numbers[minValue];
                numbers[minValue] = numbers[i];
                numbers[i] = tempNumbers;
            }

            Console.WriteLine("\n\nПолученный массив:");

            foreach (int number in numbers)
            {
                Console.Write($"{number} ");
            }

            Console.ReadKey();
        }
    }
}