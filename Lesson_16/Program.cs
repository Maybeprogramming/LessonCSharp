namespace Lesson_16
{
    internal class Program
    {
        static void Main()
        {
            int number;
            int startRandomNumber = 1;
            int endRandomNumber = 27;
            int startNaturalNumber = 100;
            int endNaturalNumber = 999;
            int amountNaturalNumbers = 0;
            Random random = new Random();

            number = random.Next(startRandomNumber, endRandomNumber + 1);
            Console.WriteLine($"Число N = {number}");

            for (int i = number; i <= endNaturalNumber; i += number)
            {
                if (i >= startNaturalNumber)
                {
                    amountNaturalNumbers++;
                }
            }

            Console.WriteLine($"Колличество трехзначных натуральных чисел кратных {number} равняется: {amountNaturalNumbers}");
            Console.ReadLine();
        }
    }
}