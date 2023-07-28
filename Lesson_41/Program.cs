using System.Runtime.CompilerServices;

namespace Lesson_41
{
    class Program
    {
        static void Main()
        {
            GameSpaces gameSpace = new();
            gameSpace.StartNewGame();

            Console.ReadLine();
        }
    }

    class GameSpaces
    {
        Player player = new("Василий");
        Deck deck = new();

        public void StartNewGame()
        {
            if (deck.TryGiveCard(out Card card) == true)
            {
                player.TakeCard(card);
            }
            else
            {
                Console.WriteLine("Колода пуста");
            }

            player.ShowCards();
        }
    }

    class Player
    {
        private List<Card> _cards = new();

        public Player(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public void TakeCard(Card card)
        {
            _cards.Add(card);
            Console.WriteLine($"Игрок {Name} взял из колоды карту: {card.ShowInfo()}");
        }

        public void ShowCards()
        {
            Console.WriteLine($"{Name} имеет на руках следующие карты:");

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
        public string Suit { get; private set; }

        public string ShowInfo()
        {
            return $"{Value} : {Suit}";
        }
    }

    class Deck
    {
        private Queue<Card> _cards;

        private List<string> _values = new()
            { "Два", "Три", "Четыре", "Пять", "Шесть", "Семь", "Восемь", "Девять", "Десять", "Валет", "Дама", "Король", "Туз" };
        private List<string> _suits = new()
            { "Червы", "Пики", "Бубны", "Трефы" };

        public Deck()
        {
            CreateCards();
            ShuffleCards();
        }

        public bool TryGiveCard(out Card card)
        {
            if (_cards.Count > 0)
            {
                card = _cards.Dequeue();
                return true;
            }

            card = null;
            return false;
        }

        public void ShowAllCardFromDeck()
        {
            foreach (Card card in _cards)
            {
                Console.WriteLine(card.ShowInfo());
            }
        }
        private void CreateCards()
        {
            _cards = new(_values.Count * _suits.Count);

            for (int suitIndex = 0; suitIndex < _suits.Count; suitIndex++)
            {
                for (int valueIndex = 0; valueIndex < _values.Count; valueIndex++)
                {
                    _cards.Enqueue(new Card(_values[valueIndex], _suits[suitIndex]));
                }
            }

            Console.WriteLine("-> Распаковка новой колоды с картами!");
        }

        private void ShuffleCards()
        {
            Random random = new();
            Queue<Card> tempCards = new();
            List<Card> currentCards = new(_cards.ToList());
            int elementIndex;

            for (int i = 0; i < _cards.Count; i++)
            {
                elementIndex = random.Next(currentCards.Count);
                tempCards.Enqueue(currentCards[elementIndex]);
                currentCards.RemoveAt(elementIndex);
            }

            Console.WriteLine($"-> Колода с картами перемешана!");
            _cards = tempCards;
        }
    }
}

//Есть колода с картами.
//Игрок достает карты, пока не решит, что ему хватит карт
//(может быть как выбор пользователя, так и количество сколько карт надо взять).
//После выводиться вся информация о вытянутых картах.
//Возможные классы: Карта, Колода, Игрок.

//Колода карт