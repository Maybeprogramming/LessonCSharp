namespace Lesson_41
{
    class Program
    {
        static void Main()
        {

        }
    }

    class GameSpaces
    {

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
        public Card(string value = "NoName", string suit = "NoSuit")
        {
            Value = value;
            Suit = suit;
        }

        public string Value { get; private set; }
        public string Suit { get;  private set; }
        
        public string ShowInfo()
        {
            return $"Значение: {Value}, масть: {Suit}";
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