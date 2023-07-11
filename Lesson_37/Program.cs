namespace Lesson_37
{
    // Объединение в одну коллекцию
    // Merging into one collection

    //Есть два массива строк.
    //Надо их объединить в одну коллекцию,
    //исключив повторения,
    //не используя Linq.
    //Пример: { "1", "2", "1"} + {"3", "2"} => { "1", "2", "3"}

    class Program
    {
        static void Main()
        {
            Console.SetWindowSize(80, 30);
            Console.SetBufferSize(80, 30);

            List<int> collectionA = new List<int>() { 1, 2, 1 };
            List<int> collectionB = new List<int>() { 3, 2 };        

            duplicateElementsInArray(collectionA);
            duplicateElementsInArray(collectionB);
            duplicateElementsInArray(collectionA, collectionB);

            collectionA.Union(collectionB);

            foreach (var element in collectionA)
            {
                Console.Write($"{element} ");
            }

            Console.WriteLine();
            foreach (var element in collectionB)
            {
                Console.Write($"{element} ");
            }

            Console.ReadLine();
        }

        private static void duplicateElementsInArray(List<int> collectionA, List<int> collectionB)
        {
            int duplicateElement;

            for (int i = 0; i < collectionA.Count; i++)
            {
                duplicateElement = collectionA[i];

                for (int j = i + 1; j < collectionB.Count; j++)
                {
                    if (collectionA[i] == collectionB[j])
                    {
                        collectionB.RemoveAt(j);
                    }
                }
            }
        }

        private static void duplicateElementsInArray(List<int> collection)
        {
            int duplicateElement;

            for (int i = 0; i < collection.Count; i++)
            {
                duplicateElement = collection[i];

                for (int j = i + 1; j < collection.Count; j++)
                {
                    if (collection[i].Equals(collection[j]))
                    {
                        collection.RemoveAt(j);
                    }
                }
            }
        }
    }
}