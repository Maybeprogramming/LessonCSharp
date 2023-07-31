namespace Lesson_42
{
    class Program
    {
        static void Main()
        {
            Console.Title = "Хранилище книг";
            ViewLibrary viewLibrary = new();
            viewLibrary.View();

            Console.ReadLine();
        }
    }

    static class Display
    {
        public static void Print<T>(T message)
        {
            Console.Write(message.ToString());
        }

        public static void Print<T>(T message, ConsoleColor consoleColor = ConsoleColor.White)
        {
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = consoleColor;
            Print(message);
            Console.ForegroundColor = defaultColor;
        }
    }

    class ViewLibrary
    {
        LibraryBooks libraryBooks = new();

        public void View()
        {
            libraryBooks.ShowAllBook();

            libraryBooks.FindBook(PropertyBook.Author);
            libraryBooks.FindBook(PropertyBook.Year);
            libraryBooks.FindBook(PropertyBook.TitleName);
            libraryBooks.FindBook(PropertyBook.Genre);
        }
    }

    class Book
    {
        public Book(string titeleName, string author, int firstPublicationYear, string genre)
        {
            TiteleName = titeleName;
            Author = author;
            FirstPublicationYear = firstPublicationYear;
            Genre = genre;
        }

        public string TiteleName { get; private set; }
        public string Author { get; private set; }
        public int FirstPublicationYear { get; private set; }
        public string Genre { get; private set; }

        public string ShowInfo()
        {
            return $"Название: \"{TiteleName}\", Автор: \"{Author}\", Дата выхода: {FirstPublicationYear}, Жанр: \"{Genre}\"";
        }
    }

    class LibraryBooks
    {
        private List<Book> _books = new()
        {
            new Book("Дубровский","Александр Пушкин", 1841, "Любовный роман"),
            new Book("Анна Коренина", "Лев Толстой",1875,"Любовный роман"),
            new Book("По ком звонит колокол", "Эрнест Хемингуэй", 1940, "Любовный роман"),
            new Book("Алмазная колесница","Борис Акунин",2002,"Детектив"),
            new Book("Убийство на улице Морг","Эдгар По",1841,"Детектив"),
            new Book("Девушка с татуировкой дракона","Стиг Ларссон",2004,"Детектив"),
            new Book("Конец вечности","Айзек Азимов",1955,"Фантастика"),
            new Book("Дюна","Фрэнк Герберт",1965,"Фантастика"),
            new Book("Звёздный десант","Роберт Хайнлайн",1959,"Фантастика"),
            new Book("Гадкий утёнок","Ганс Христиан Андерсен",1843,"Сказки"),
            new Book("Удивительный волшебник Страны Оз", "Ганс Христиан Андерсен",1900,"Сказки"),
            new Book("Снежная королева", "Ганс Христиан Андерсен",1844,"Сказки"),
            new Book("Руслан и Людмила", "Александр Пушкин",1820,"Сказки"),
            new Book("Кот в сапогах" , "Шарль Перро",1697,"Сказки"),
            new Book("Красная Шапочка" , "Шарль Перро",1697,"Сказки"),
        };

        //Для теста, потом удалить!!!!
        public void TestDispay()
        {
            Display.Print("\n");
            Display.Print("\n");
            ShowAllBook();
        }

        public void ShowAllBook ()
        {

            Display.Print("\nСписок всех книг:", ConsoleColor.Blue);

            foreach (Book item in _books)
            {
                Display.Print("\n" + item.ShowInfo());
            }
        }

        public void AddBook(string titleName, string author, int firstPublicationYear, string genre)
        {
            _books.Add(new Book(titleName, author, firstPublicationYear, genre));
        }

        public void RemoveBook()
        {
            string option = string.Empty;
            Book book = TryGetBook(option);
            _books.Remove(book);
        }

        private Book TryGetBook(string findOption)
        {
            return new Book("Красная Шапочка", "Шарль Перро", 1697, "Сказки");
        }

        public void FindBook(PropertyBook findBookParametr)
        {
            switch (findBookParametr)
            {
                case PropertyBook.TitleName:
                    FindBookByTitleName();
                    break;

                case PropertyBook.Author:
                    FindBookByAuthor();
                    break;

                case PropertyBook.Year:
                    FindBookByYear();
                    break;

                case PropertyBook.Genre:
                    FindBookByGenre();
                    break;
            }
        }

        private void FindBookByGenre()
        {
            Display.Print("\nПоиск по жанру: ", ConsoleColor.Green);
            string userInput = "Любовный роман";
            Display.Print(userInput);

            foreach (Book item in _books)
            {
                if(item.Genre.Contains(userInput) == true)
                {
                    Display.Print("\n" + item.ShowInfo());
                }
            }
        }

        private void FindBookByTitleName()
        {
            Display.Print("\nПоиск по названию: ", ConsoleColor.Red);
            string userInput = "Кот в сапогах";
            Display.Print(userInput);

            foreach (Book item in _books)
            {
                if (item.TiteleName.Contains(userInput) == true)
                {
                    Display.Print("\n" + item.ShowInfo());
                }
            }
        }

        private void FindBookByAuthor()
        {
            Display.Print("\nПоиск по автору: ", ConsoleColor.Yellow);
            string userInput = "Александр Пушкин";
            Display.Print(userInput);


            foreach (Book item in _books)
            {
                if (item.Author.Contains(userInput) == true)
                {
                    Display.Print("\n" + item.ShowInfo());
                }
            }
        }

        private void FindBookByYear()
        {
            Display.Print("\nПоиск по году издания: ", ConsoleColor.DarkMagenta);
            int userInput = 1841;
            Display.Print(userInput);

            foreach (Book item in _books)
            {
                if (item.FirstPublicationYear == userInput )
                {
                    Display.Print("\n" + item.ShowInfo());
                }
            }
        }

    }

    enum PropertyBook
    {
        TitleName,
        Author,
        Year,
        Genre
    }
}

//Создать хранилище книг.
//Каждая книга имеет название, автора и год выпуска (можно добавить еще параметры).
//В хранилище можно добавить книгу, убрать книгу,
//показать все книги и показать книги по указанному параметру
//(по названию, по автору, по году выпуска).
//Про указанный параметр, к примеру нужен конкретный автор,
//выбирается показ по авторам,
//запрашивается у пользователя автор и показываются все книги с этим автором.