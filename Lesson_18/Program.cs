namespace Lesson_18
{
    internal class Program
    {
        static void Main()
        {
            string userInputText;
            int bracketsCount = 0;
            int depthCount = 0;
            int balanceValue = 0;
            char bracketOpen = '(';
            char bracketClose = ')';

            Console.WriteLine("Введите строку из символов: \"(\" и \")\".");
            userInputText = Console.ReadLine();

            for (int i = 0; i < userInputText.Length; i++)
            {
                if (userInputText[i] == bracketOpen)
                {
                    bracketsCount++;
                }
                else if (userInputText[i] == bracketClose)
                {
                    if (i != userInputText.Length - 1 && userInputText[i + 1] != bracketOpen)
                    {
                        depthCount++;
                    }

                    bracketsCount--;
                }

                if (bracketsCount < balanceValue)
                {
                    break;
                }
            }

            if (bracketsCount == balanceValue)
            {
                Console.WriteLine($"\"{userInputText}\" - cтрока корректная, максимальная глубина составляет: {depthCount + 1}");
            }
            else
            {
                Console.WriteLine($"\"{userInputText}\" - некорректная строка!");
            }

            Console.ReadKey();
        }
    }
}