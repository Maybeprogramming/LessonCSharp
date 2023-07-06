using System.Diagnostics;

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

            long intArray;
            long intArray2;
            long charArray;
            long charArray2;
            long stringArray;
            long stringArray2;
            long boolArray;
            long boolArray2;
            long floatArray;
            long floatArray2;
            Stopwatch timer = new();

            int[] arrayNumbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };
            char[] arraySymbols = { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')' };
            string[] arrayText = { "Один", "Два", "Три", "Четыре", "Пять" };
            bool[] arrarBool = { true, true, true, false, false, false };
            float[] arrayFloat = { 1.1f, 2.2f, 3.3f, 4.4f, 5.5f, 6.6f, 7.7f, 8.8f, 9.9f, 0.01f };

            Task.Delay(200).Wait();

            timer.Start();
            arrayNumbers = StartShuffle(random, arrayNumbers, "Применение функции Shuffle на массиве чисел типа int\n");
            intArray = timer.ElapsedMilliseconds;
            timer.Reset();

            Task.Delay(200).Wait();

            timer.Start();
            arrayNumbers = StartShuffle2(random, arrayNumbers, "\n\nПрименение функции Shuffle на массиве чисел типа int\n");
            intArray2 = timer.ElapsedMilliseconds;
            timer.Reset();

            Task.Delay(200).Wait();

            timer.Start();
            arraySymbols = StartShuffle(random, arraySymbols, "\n\nПрименение функции Shuffle на массиве символов типа char\n");
            charArray = timer.ElapsedMilliseconds;
            timer.Reset();

            Task.Delay(200).Wait();

            timer.Start();
            arraySymbols = StartShuffle2(random, arraySymbols, "\n\nПрименение функции Shuffle на массиве символов типа char\n");
            charArray2 = timer.ElapsedMilliseconds;
            timer.Reset();

            Task.Delay(200).Wait();

            timer.Start();
            arrayText = StartShuffle(random, arrayText, "\n\nПрименение функции Shuffle на массиве строк типа string\n");
            stringArray = timer.ElapsedMilliseconds;
            timer.Reset();

            Task.Delay(200).Wait();

            timer.Start();
            arrayText = StartShuffle2(random, arrayText, "\n\nПрименение функции Shuffle на массиве строк типа string\n");
            stringArray2 = timer.ElapsedMilliseconds;
            timer.Reset();

            Task.Delay(200).Wait();

            timer.Start();
            arrarBool = StartShuffle(random, arrarBool, "\n\nПрименение функции Shuffle на массиве типа bool\n");
            boolArray = timer.ElapsedMilliseconds;
            timer.Reset();

            Task.Delay(200).Wait();

            timer.Start();
            arrarBool = StartShuffle2(random, arrarBool, "\n\nПрименение функции Shuffle на массиве типа bool\n");
            boolArray2 = timer.ElapsedMilliseconds;
            timer.Reset();

            Task.Delay(200).Wait();

            timer.Start();
            arrayFloat = StartShuffle(random, arrayFloat, "\n\nПрименение функции Shuffle на массиве чисел типа float\n");
            floatArray = timer.ElapsedMilliseconds;
            timer.Stop();

            Task.Delay(200).Wait();

            timer.Start();
            arrayFloat = StartShuffle2(random, arrayFloat, "\n\nПрименение функции Shuffle на массиве чисел типа float\n");
            floatArray2 = timer.ElapsedMilliseconds;
            timer.Stop();

            Task.Delay(200).Wait();

            Console.WriteLine("\n\nБенчмарк: ");
            Console.WriteLine("Int32 (Алгоритм Фишера — Йетса): " + intArray + " мс");
            Console.WriteLine("Int32 (Алгоритм Дурштенфельда): " + intArray2 + " мс");
            Console.WriteLine("Char (Алгоритм Фишера — Йетса): " + charArray + " мс");
            Console.WriteLine("Char (Алгоритм Дурштенфельда): " + charArray2 + " мс");
            Console.WriteLine("String (Алгоритм Фишера — Йетса): " + stringArray + " мс");
            Console.WriteLine("String (Алгоритм Дурштенфельда): " + stringArray2 + " мс");
            Console.WriteLine("Bool (Алгоритм Фишера — Йетса): " + boolArray + " мс");
            Console.WriteLine("Bool (Алгоритм Дурштенфельда): " + boolArray2 + " мс");
            Console.WriteLine("Float (Алгоритм Фишера — Йетса): " + floatArray + " мс");
            Console.WriteLine("Float (Алгоритм Дурштенфельда): " + floatArray2 + " мс");
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
            if (titleText != string.Empty)
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

        static T[] StartShuffle2<T>(Random random, T[] sourceArray, string titleText = "")
        {
            if (titleText != string.Empty)
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

        static T[] Shuffle2<T>(ref T[] array, Random random)
        {
            int index;

            for (int i = 0; i < array.Length; i++)
            {
                index = random.Next(array.Length);
                T element = array[array.Length - i - 1];
                array[array.Length - i - 1] = array[index];
                array[index] = element;
            }

            return array;
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