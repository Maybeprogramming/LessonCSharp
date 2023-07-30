using Lesson_41;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Lesson_41
{
    class Program
    {
        static void Main()
        {
            Console.Title = "Колода карт";

            GameTable gameTable = new();
            gameTable.RunGame();

            Console.ReadLine();
        }
    }

    class GameTable
    {
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
            string partyEndMesage = "Партия завершена, до новых встреч!!!";
            string exitProgrammMessage = "\nРабота программы завершена.";
            string namePlayer = "Василий";
            string userInput;
            bool isRun = true;

            Player player = new(namePlayer);
            Console.Title += $", партию разыгрывает игрок: {namePlayer}";
            Deck deck = new();

            while (isRun == true)
            {
                Console.Clear();
                Console.Write(titleMenu);
                Console.Write(menu);
                Console.Write(requestMessge);
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case PlayerTakeOneCardCommand:
                        player.TakeOneCard(deck.GiveOneCard());
                        break;

                    case PlayerTakeSomeCardCommand:
                        PlayerTakeSomeCardsFromDeck(player, deck);
                        break;

                    case StopTakingСardsCommand:
                        isRun = IsPartyEnd(partyEndMesage);
                        break;

                    default:
                        Console.WriteLine("Такой команды нет!");
                        break;
                }

                Console.Write(continueMessage);
                Console.ReadLine();
            }

            player.ShowCards();
            Console.WriteLine(exitProgrammMessage);
        }

        private static void PlayerTakeSomeCardsFromDeck(Player player, Deck deck)
        {
            int amountCards = player.DesiredNumberCards();
            player.TakeSomeCards(deck.GiveSomeCards(amountCards));
        }

        private bool IsPartyEnd(string message)
        {
            Console.WriteLine(message);
            return false;
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
                Console.WriteLine($"Игрок {Name} взял из колоды карту: {card.ShowInfo()}");
            }
            else
            {
                Console.WriteLine($"Игрок {Name} не смог взять карту. Колода пуста");
            }
        }

        public void TakeSomeCards(List<Card> cards)
        {
            if (cards != null && cards.Count != 0)
            {
                _cards.AddRange(cards);
                Console.WriteLine($"Игрок {Name} взял {cards.Count} карты:");
                ShowTakingCards(cards);
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

            ShowTakingCards(_cards);
        }
        public int DesiredNumberCards()
        {
            Console.Write("Сколько хотите взять карт? Введите количество: ");
            int disireNumberCards = ReadInt();
            return disireNumberCards;
        }

        private void ShowTakingCards(List<Card> cards)
        {
            foreach (Card card in cards)
            {
                Console.WriteLine(card.ShowInfo());
            }
        }
        private int ReadInt()
        {
            bool isTryParse = false;
            string userInput;
            int number = 0;

            while (isTryParse == false)
            {
                userInput = Console.ReadLine();

                if (Int32.TryParse(userInput, out int result) == true)
                {
                    if (result > 0)
                    {
                        number = result;
                        isTryParse = true;
                    }
                    else
                    {
                        Console.Write($"Ошибка! Введеное число должно быть больше 0!\nПопробуйте снова: ");
                    }
                }
                else
                {
                    Console.Write($"Ошибка! Вы ввели не число: {userInput}!\nПопробуйте снова: ");
                }
            }

            return number;
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

        public List<Card> GiveSomeCards(int cardsAmount)
        {
            List<Card> givenCards = new();

            if (_cards.Count >= cardsAmount)
            {
                for (int i = 0; i < cardsAmount; i++)
                {
                    givenCards.Add(_cards.Dequeue());
                }

                return givenCards;
            }

            return null;
        }

        private void CreateCards()
        {
            List<string> values = new()
                { "Два", "Три", "Четыре", "Пять", "Шесть", "Семь", "Восемь", "Девять", "Десять", "Валет", "Дама", "Король", "Туз" };
            List<string> suits = new()
                { "Червы", "Пики", "Бубны", "Трефы" };

            _cards = new(values.Count * suits.Count);

            for (int suitIndex = 0; suitIndex < suits.Count; suitIndex++)
            {
                for (int valueIndex = 0; valueIndex < values.Count; valueIndex++)
                {
                    _cards.Enqueue(new Card(values[valueIndex], suits[suitIndex]));
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

//Доработать.
//1 - List<string> _values / List<string> _suits - переменные названы не по нотации.
//2 - public List<Card> GiveSomeCards() -не заметил в прошлый раз. Нарушена логика в методе.
//Колода спрашивает у пользователя, сколько карт ей передать.
//Не совсем верно.
//Лучше сделать следующее - передавать в метод число
//- public List<Card> GiveSomeCards(int cardsCount).
//А спрашивать у пользователя в другом месте.
//Так вы логически развязываете код