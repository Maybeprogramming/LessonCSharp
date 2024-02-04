namespace Lesson_08
{
    internal class Program
    {
        private static void Main()
        {
            string userMessage;
            int printCount;

            Console.WriteLine("Добропожаловать в типографию!");
            Console.WriteLine("Введите ваше сообщение для печати:");
            userMessage = Console.ReadLine();
            Console.Write("Сколько копий хотите распечатать? ");
            printCount = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < printCount; i++)
            {
                Console.WriteLine(userMessage);
            }

            Console.ReadLine();
        }
    }
}