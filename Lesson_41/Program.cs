namespace Lesson_41
{
    class Program
    {
        static void Main()
        {

        }
    }

    class Player
    {
        private List<Card> _cards;
    }

    class Card
    {
        private string _name;
        private string _suit;

        public Card(string name, string suit)
        {
            _name = name;
            _suit = suit;
        }

        public string ShowInfo()
        {
            string cardInfo = $"{_name}, масть: {_suit}";
            return cardInfo;
        }
    }

    class Deck
    {
        private Queue<Card> cards;
    }

    class CardGenerator
    {
        Random random = new Random();
        List<string> namesCards = new ();
        {
            
        }
        public Card CreateRandomCard()
        {
            return new Card();
        }
    }
}

//Есть колода с картами.
//Игрок достает карты, пока не решит, что ему хватит карт
//(может быть как выбор пользователя, так и количество сколько карт надо взять).
//После выводиться вся информация о вытянутых картах.
//Возможные классы: Карта, Колода, Игрок.

//Колода карт