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

    static class Display
    {
        static public void Print <T> (T message, ConsoleColor consoleColor = ConsoleColor.White)
        {
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = consoleColor;
            Console.Write(message.ToString());
            Console.ForegroundColor = defaultColor;
        }

        static public void PrintWithExpectation <T>(T message, ConsoleColor consoleColor = ConsoleColor.White)
        {
            Print(message, consoleColor);
            Print("\n\nНажмите любую клавишу чтобы продолжить...");
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
            string requestMessage = "\nВведите комадну: ";
            string partyEndMesage = "Партия завершена, до новых встреч!!!";
            string noCommandMessage = "Такой команды нет!";
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
                Display.Print(titleMenu);
                Display.Print(menu);
                Display.Print(requestMessage, ConsoleColor.DarkYellow);

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
                        Display.Print($"\n\"{userInput}\" - {noCommandMessage}", ConsoleColor.Red);
                        break;
                }

                Display.PrintWithExpectation("", ConsoleColor.DarkGreen);
            }

            player.ShowCards();
            Display.Print("\n" + exitProgrammMessage, ConsoleColor.DarkYellow);
        }

        private static void PlayerTakeSomeCardsFromDeck(Player player, Deck deck)
        {
            int amountCards = player.DesiredNumberCards();
            player.TakeSomeCards(deck.GiveSomeCards(amountCards));
        }

        private bool IsPartyEnd(string message)
        {
            Display.Print(message, ConsoleColor.DarkGreen);
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
                Display.Print($"\nИгрок {Name} взял из колоды карту: {card}");
            }
            else
            {
                Display.Print($"\nИгрок {Name} не смог взять карту. Колода пуста");
            }
        }

        public void TakeSomeCards(List<Card> cards)
        {
            if (cards != null && cards.Count != 0)
            {
                _cards.AddRange(cards);
                Display.Print($"\nИгрок {Name} взял {cards.Count} карты:");
                ShowTakingCards(cards);
            }
            else
            {
                Display.Print($"\nИгрок {Name} не смог взять несколько карт.\nКолода пуста или там меньше желаемого количества карт");
            }
        }

        public void ShowCards()
        {
            Console.Clear();

            if (_cards.Count == 0)
            {
                Display.Print("У игрока ");
                Display.Print($"{Name}", ConsoleColor.Green);
                Display.Print(" нет карт на руках");
                return;
            }

            Display.Print("Игрок ");
            Display.Print($"{Name}", ConsoleColor.Green);
            Display.Print(" имеет на руках следующие карты:");
            ShowTakingCards(_cards);
        }

        public int DesiredNumberCards()
        {
            Display.Print("\nСколько хотите взять карт? Введите количество: ");
            int disireNumberCards = ReadInt();
            return disireNumberCards;
        }

        private void ShowTakingCards(List<Card> cards)
        {
            foreach (Card card in cards)
            {
                Display.Print("\n" + card);
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
                        Display.Print($"\nОшибка! Введеное число должно быть больше 0!\nПопробуйте снова: ");
                    }
                }
                else
                {
                    Display.Print($"\nОшибка! Вы ввели не число: {userInput}!\nПопробуйте снова: ");
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

        public override string ToString()
        {
            return $"{Value} : {Suit}";
        }
    }

    class Deck
    {
        private Stack<Card> _cards;

        public Deck()
        {
            FillDeck();
        }

        public Card GiveOneCard()
        {
            if (_cards.Count > 0)
            {
                return _cards.Pop();
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
                    givenCards.Add(_cards.Pop());
                }

                return givenCards;
            }

            return null;
        }

        private void FillDeck()
        {
            Random random = new();
            List<Card> tempCards = CreateCards();
            tempCards = Shuffle(tempCards, random);
            _cards = new();

            for (int i = 0; i < tempCards.Count; i++)
            {
                _cards.Push(tempCards[i]);
            }

            Display.PrintWithExpectation("\n-> Колода готова к началу новой партии.", ConsoleColor.DarkGreen);
        }

        private List<Card> CreateCards()
        {
            List<string> values = new()
                { "Два", "Три", "Четыре", "Пять", "Шесть", "Семь", "Восемь", "Девять", "Десять", "Валет", "Дама", "Король", "Туз" };
            List<string> suits = new()
                { "Червы", "Пики", "Бубны", "Трефы" };
            List<Card> cards = new();

            for (int suitIndex = 0; suitIndex < suits.Count; suitIndex++)
            {
                for (int valueIndex = 0; valueIndex < values.Count; valueIndex++)
                {
                    cards.Add(new Card(values[valueIndex], suits[suitIndex]));
                }
            }

            Display.Print("-> Распаковка новой колоды с картами!");
            return cards;
        }

        private List<Card> Shuffle(List<Card> collection, Random random)        
        {
            List<Card> tempCollection = new (collection);
            int elementIndex;

            for (int i = 0; i < tempCollection.Count; i++)
            {
                elementIndex = random.Next(collection.Count);
                tempCollection[i] = collection[elementIndex];
                collection.RemoveAt(elementIndex);
            }

            Display.Print("\n-> Колода с картами перемешана!");
            return tempCollection;
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
//+1 - List<string> _values / List<string> _suits - переменные названы не по нотации.
//+2 - public List<Card> GiveSomeCards() -не заметил в прошлый раз. Нарушена логика в методе.
//Колода спрашивает у пользователя, сколько карт ей передать.
//Не совсем верно.
//Лучше сделать следующее - передавать в метод число
//- public List<Card> GiveSomeCards(int cardsCount).
//А спрашивать у пользователя в другом месте.
//Так вы логически развязываете код

//Доработать.
//+1 - _values / _suits - поля не нужны всему классу,
//только в конкретном методе. Перенесите в него.
//+2 - public void ShowAllCardFromDeck() -метод не используется.
//+3 - взял из колоды карту: { card?.ShowInfo()}
//-зачем дополнительная проверка? При получении уже проверяете.
//+4 - string ExitProgrammMessage - переменная названа не по нотации.

//1) для колоды карт больше подходит Stack, чем Queue .
//С очередью, например при игре в дурака,
//первой картой возьмется козырь,
//а потом будут браться карты снизу колоды .
//2) Сложный путь тасования, куда вовлечены 3 коллекции.
//В идеале один список, но в Вашей реализации - хотя бы 2 коллекции.
//Очередь превратите в список, в списке меняйте карты местами, перемешенный список превратите в очередь .