namespace Lesson_24
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int numberRepsCount = 1;
            int tempRepsCount = 1;
            int[] numbers = { 4, 4, 1, 4, 4, 4, 0, 3, 3, 3, 3, 4, 3, 3, 3, 1, 3, 3, 5, 5, 9, 9, 3, 2, 1, 0, 0, 3, 2, 1, 1, 1, 2, 0, 0, 0, 0, 0, 0 };
            int number = numbers[0];

            Console.Title = "Поиск числа, которое повторяется наибольшее количество раз подряд в массиве";
            Console.WriteLine("Массив чисел: ");

            foreach (var num in numbers)
            {
                Console.Write($"{num} ");
            }

            Console.WriteLine();

            for (int i = 1; i < numbers.Length; i++)
            {
                if (numbers[i] == numbers[i - 1])
                {
                    tempRepsCount++;
                }
                else
                {
                    tempRepsCount = 1;
                }

                if (tempRepsCount > numberRepsCount)
                {
                    numberRepsCount = tempRepsCount;
                    number = numbers[i];
                }
            }

            Console.WriteLine($"Число: {number} повторяется {numberRepsCount} раз подряд");
            Console.ReadKey();
        }
    }
}