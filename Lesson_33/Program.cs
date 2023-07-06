namespace Lesson_33
{
    //Создать программу, которая принимает от пользователя слово и
    //выводит его значение.
    //Если такого слова нет,
    //то следует вывести соответствующее сообщение.

    // Толковый словарь
    // Explanatory dictionary

    internal class Program
    {
        static void Main()
        {
            Dictionary<string, string> dictionaryWords;

            dictionaryWords = InitialDictionary();

            PrintDictionary(dictionaryWords);

            Console.ReadLine();

        }

        private static void PrintDictionary(Dictionary<string, string> dictionaryWords)
        {
            int indexElement = 1;
            Console.WriteLine("Перечень слов в словаре: ");

            foreach (var word in dictionaryWords)
            {
                Console.WriteLine($"{indexElement}. \"{word.Key}\" - {word.Value}");
                indexElement++;
            }
        }

        private static Dictionary<string, string> InitialDictionary()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>() { 
                {"Программист", "Специалист по программированию" },
                {"Комрьютер", "Электронная вычислительная машина (ЭВМ). Персональный компьютер" },
                {"Слово", "Единица языка, служащая для наименования понятий, предметов, лиц, действий, состояний, признаков, связей, отношений, оценок" },
                {"Бит", "Единица измерения количества информации" },
                {"Кремний", "Химический элемент, тёмно-серые кристаллы с металлическим блеском, одна из главных составных частей горных пород" },
                {"Лазер", "Оптический квантовый генератор, устройство для получения мощных узконаправленных пучков света" },
                {"Логика", "Ход рассуждений, умозаключений. Разумность, внутренняя закономерность вещей, событий" },
                {"Алгоритм","Совокупность действий, правил для решения данной задачи" },
                {"Атом","Мельчайшая частица химического элемента, состоящая из ядра и электронов" },
                {"Сила","Величина, являющаяся мерой механического взаимодействия тел, вызывающего их ускорение или деформацию; характеристика интенсивности физических процессов" },
            };

            return dictionary;
        }

        static void TryFindWord(string word, Dictionary<string, string> dictionaryWords)
        {

        }
    }
}