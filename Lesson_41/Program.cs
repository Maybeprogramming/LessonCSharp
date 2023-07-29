namespace Lesson_41
{
    class Program
    {
        static void Main()
        {
            GameTable gameTable = new();
            gameTable.RunGame();

            Console.ReadLine();
        }
    }

    class GameTable
    {
        Player player = new("Василий");
        Deck deck = new();

        public void RunGame()
        {
            const string PlayerTakeOneCardCommand = "1";
            const string PlayerTakeSomeCardCommand = "2";
            const string StopTakingСardsCommand = "3";

            string titleMenu = "Доступные команды: ";
            string menu = $"\n{PlayerTakeOneCardCommand} - взять одну карту" +
                          $"\n{PlayerTakeSomeCardCommand} - взять несколько карт" +
                          $"\n{StopTakingСardsCommand} - завершить партию";
            string requestMessge = "\nВведите комадну: ";
            string continueMessage = "\nНажмите любую клавишу чтобы продолжить...";

            bool isRun = true;

            while (isRun == true)
            {
                Console.Clear();
                Console.Write(titleMenu);
                Console.Write(menu);
                Console.Write(requestMessge);

                switch (Console.ReadLine())
                {
                    case PlayerTakeOneCardCommand:
                        player.TakeOneCard(deck.GiveOneCard());
                        break;

                    case PlayerTakeSomeCardCommand:
                        player.TakeSomeCards(deck.GiveSomeCards());
                        break;

                    case StopTakingСardsCommand:
                        isRun = false;
                        break;

                    default:
                        break;
                }

                Console.Write(continueMessage);
                Console.ReadLine();
            }

            player.ShowCards();

            Console.WriteLine("Партия завершена, до новых встреч!!!");
            Console.ReadLine();
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

        public void TakeOneCard(Card card)
        {
            if (card != null)
            {
                _cards.Add(card);
                Console.WriteLine($"Игрок {Name} взял из колоды карту: {card?.ShowInfo()}");
            }
            else
            {
                Console.WriteLine($"Игрок {Name} не смог взять карту. Колода пуста");
            }
        }

        public void TakeSomeCards(List<Card> cards)
        {
            if (cards.Count != 0 && cards != null)
            {
                _cards.AddRange(cards);
                Console.WriteLine($"Игрок {Name} взял {cards.Count} карт");
            }
            else
            {
                Console.WriteLine($"Игрок {Name} не смог взять несколько карт.\nКолода пуста или там меньше желаемого количества карт");
            }
        }

        public void ShowCards()
        {
            Console.Clear();

            if (_cards.Count == 0)
            {
                Console.WriteLine($"У игрока {Name} нет карт на руках");
                return;
            }

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

        public Card GiveOneCard()
        {
            if (_cards.Count > 0)
            {
                return _cards.Dequeue();
            }

            return null;
        }

        public List<Card> GiveSomeCards()
        {
            Console.Write("Сколько хотите взять карт? Введите количество: ");

            if (Int32.TryParse(Console.ReadLine(), out int amount) == true)
            {
                List<Card> givenCards = new(amount);

                if (_cards.Count > 0 && _cards.Count >= amount)
                {
                    for (int i = 0; i < amount; i++)
                    {
                        givenCards.Add(_cards.Dequeue());
                    }

                    return givenCards;
                }
            }

            return null;
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