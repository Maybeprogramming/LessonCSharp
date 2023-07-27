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
        private List<Card> _cards = new();

        public void TakeCard(Card card)
        {
            _cards.Add(card);
        }

        public void ShowCards()
        {
            foreach (Card card in _cards)
            {
                Console.WriteLine(card.ShowInfo());
            }
        }
    }

    class Card
    {
        private string _name;
        private string _suit;

        public Card(string name = "defaultName", string suit = "defaultSuit")
        {
            _name = name;
            _suit = suit;
        }

        public string ShowInfo()
        {
            return $"{_name}, масть: {_suit}";
        }
    }

    class Deck
    {
        private Queue<Card> _cards = new();
    }

    class CardGenerator
    {
        Random random = new Random();
        List<string> namesCards = new();

        public Card CreateRandomCard()
        {
            return new Card("");
        }
    }
}

//Есть колода с картами.
//Игрок достает карты, пока не решит, что ему хватит карт
//(может быть как выбор пользователя, так и количество сколько карт надо взять).
//После выводиться вся информация о вытянутых картах.
//Возможные классы: Карта, Колода, Игрок.

//Колода карт