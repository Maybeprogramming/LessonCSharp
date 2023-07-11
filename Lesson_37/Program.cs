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

            List<string> collectionA = new List<string>() { "1", "2", "1" };
            List<string> collectionB = new List<string>() { "3", "2" };

            var unionCollection = Union(collectionA, collectionB);

            Console.Write("Исходная коллекция A: ");
            PrintCollection(collectionA);
            Console.Write("\nИсходная коллекция B: ");
            PrintCollection(collectionB);
            Console.Write("\nРезультат объединения, ислючая повторения: ");
            PrintCollection(unionCollection);

            Console.ReadLine();
        }

        private static List<string> Union(List<string> collection1, List<string> collection2)
        {
            List<string> result = new List<string>();

            Comparer(collection1, result);
            Comparer(collection2, result);

            return result;
        }

        private static void Comparer(List<string> collection, List<string> result)
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