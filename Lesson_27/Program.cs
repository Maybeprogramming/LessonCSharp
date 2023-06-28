namespace Lesson_27
{
    internal class Program
    {
        static void Main()
        {
            Random random = new Random();
            int[] numbers;
            int minLenthNumber = 4;
            int maxLenthNumber = 20;
            int maxRandNumber = 10;
            numbers = new int[random.Next(minLenthNumber, maxLenthNumber)];
            int userInput;
            int firstNumber;

            Console.Title = "Программа сдвига элементов массива влево на заданное значение";

            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = random.Next(maxRandNumber);
            }

            Console.WriteLine($"Исходный массив размером {numbers.Length}:");

            foreach (int number in numbers)
            {
                Console.Write($"{number} ");
            }

            Console.Write("\n\nВведите значение на которое желаете сдвинуть элементы массива на позицию влево: ");
            userInput = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < userInput; i++)
            {
                firstNumber = numbers[0];

                for (int j = 0; j < numbers.Length - 1; j++)
                {
                    numbers[j] = numbers[j + 1];
                }

                numbers[numbers.Length - 1] = firstNumber;
            }

            Console.WriteLine($"Полученный массив при сдвиге влево на: {userInput}\n");

            foreach (int number in numbers)
            {
                Console.Write($"{number} ");
            }

            Console.ReadKey();
        }
    }
}