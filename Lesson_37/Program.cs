// Объединение в одну коллекцию
// Merging into one collection

//Есть два массива строк.
//Надо их объединить в одну коллекцию,
//исключив повторения,
//не используя Linq.
//Пример: { "1", "2", "1"} + {"3", "2"} => { "1", "2", "3"}

#region Объединение коллекций с помощью Linq
//namespace Lesson_37
//{
//    class Program
//    {
//        static void Main()
//        {
//            Console.SetWindowSize(80, 30);
//            Console.SetBufferSize(80, 30);
//            Console.Title = "Объединение в одну коллекцию";

//            List<string> collectionA = new List<string>() { "1", "2", "1" };
//            List<string> collectionB = new List<string>() { "3", "2" };

//            var unionCollections = collectionA.Concat(collectionB).Distinct();

//            Console.Write("Исходная коллекция 1: ");
//            PrintCollection(collectionA);
//            Console.Write("\nИсходная коллекция 2: ");
//            PrintCollection(collectionB);
//            Console.Write("\nРезультат объединения, ислючая повторения: ");
//            PrintCollection(unionCollections);

//            Console.ReadLine();
//        }

//        private static void PrintCollection(IEnumerable<string> collection)
//        {
//            foreach (string element in collection)
//            {
//                Console.Write($"\"{element}\" ");
//            }
//        }
//    }
//}
#endregion

namespace Lesson_37
{
    class Program
    {
        static void Main()
        {
            Console.SetWindowSize(80, 30);
            Console.SetBufferSize(80, 30);
            Console.Title = "Объединение в одну коллекцию";

            List<string> collectionNumbers1 = new List<string>() { "1", "2", "1" };
            List<string> collectionNumbers2 = new List<string>() { "3", "2" };

            var mergedCollection = MergeCollections(collectionNumbers1, collectionNumbers2);

            Console.Write("Исходная коллекция №1: ");
            PrintCollection(collectionNumbers1);
            Console.Write("\nИсходная коллекция №2: ");
            PrintCollection(collectionNumbers2);
            Console.Write("\nРезультат объединения двух коллекций, ислючая повторения: ");
            PrintCollection(mergedCollection);

            Console.ReadLine();
        }

        private static List<string> MergeCollections(List<string> collection1, List<string> collection2)
        {
            List<string> result = new List<string>();

            ExcludeIdentialNumbers(collection1, result);
            ExcludeIdentialNumbers(collection2, result);

            return result;
        }

        private static void ExcludeIdentialNumbers(List<string> collection, List<string> result)
        {
            foreach (var item in collection)
            {
                if (result.Contains(item) == false)
                {
                    result.Add(item);
                }
            }
        }

        private static void PrintCollection(IEnumerable<string> collection)
        {
            foreach (string element in collection)
            {
                Console.Write($"\"{element}\" ");
            }
        }
    }
}