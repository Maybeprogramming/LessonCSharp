using System.Xml.Linq;

namespace Lesson_32
{
    //Реализуйте функцию Shuffle, которая перемешивает элементы массива в случайном порядке.
    //Kansas city shuffle

    internal class Program
    {
        static void Main()
        {
            ConsoleSetup();
            StartWork();
            Console.ReadKey();
        }

        private static void StartWork()
        {
            Random random = new();

            int[] arrayNumbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
            char[] arraySymbols = { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')' };
            string[] arrayText = { "Один", "Два", "Три", "Четыре", "Пять" };
            bool[] arrarBool = { true, true, true, false, false, false };
            float[] arrayFloat = { 1.1f, 2.2f, 3.3f, 4.4f, 5.5f, 6.6f, 7.7f, 8.8f, 9.9f, 0.01f };


            arrayNumbers = StartShuffle(random, arrayNumbers, "Применение функции Shuffle на массиве чисел типа int\n");
            arraySymbols = StartShuffle(random, arraySymbols, "\n\nПрименение функции Shuffle на массиве символов типа char\n");
            arrayText = StartShuffle(random, arrayText, "\n\nПрименение функции Shuffle на массиве строк типа string\n");
            arrarBool = StartShuffle(random, arrarBool, "\n\nПрименение функции Shuffle на массиве типа bool\n");
            arrayFloat = StartShuffle(random, arrayFloat, "\n\nПрименение функции Shuffle на массиве чисел типа float\n");
        }

        private static void ConsoleSetup()
        {
            int screenWidth = 100;
            int screenHeight = 30;
            int bufferWidth = screenWidth;
            int bufferHeight = 40;
            Console.SetWindowSize(screenWidth, screenHeight);
            Console.SetBufferSize(bufferWidth, bufferHeight);
            Console.Title = "Kansas city shuffle";
        }

        static T[] StartShuffle<T>(Random random, T[] sourceArray, string titleText = "")
        {
            if(titleText != string.Empty)
            {
                Print(titleText, ConsoleColor.Green);
            }

            Print("Исходный массив: \n");
            Print(ref sourceArray);

            sourceArray = Shuffle(ref sourceArray, random);

            Print("\n");
            Print("Перемешанный массив: \n");
            Print(ref sourceArray);

            return sourceArray;
        }

        static T[] Shuffle<T>(ref T[] array, Random random)
        {
            T[] tempArray = new T[array.Length];
            int elementIndex;

            for (int i = 0; i < tempArray.Length; i++)
            {
                elementIndex = random.Next(array.Length);
                tempArray[i] = array[elementIndex];
                array = RemoveElementInArray(ref array, elementIndex);
            }

            return tempArray;
        }

        static void Print<T>(ref T[] array)
        {
            foreach (var element in array)
            {
                Console.Write($" {element}");
            }
        }
        static void Print(string text, ConsoleColor color = ConsoleColor.White)
        {
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = defaultColor;
        }

        static T[] RemoveElementInArray<T>(ref T[] array, int indexToDelete)
        {
            T[] tempArray = new T[array.Length - 1];

            for (int i = 0; i < indexToDelete; i++)
            {
                tempArray[i] = array[i];
            }

            for (int i = indexToDelete + 1; i < array.Length; i++)
            {
                tempArray[i - 1] = array[i];
            }

            return tempArray;
        }
    }
}