namespace Lesson_42
{
    class Program
    {
        static void Main()
        {
            Console.Title = "Хранилище книг";
            ViewLibrary viewLibrary = new();
            viewLibrary.Work();

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
        public void Work()
        {
            const string ShowAllBooksCommand = "1";
            const string AddBookCommand = "2";
            const string RemoveBookCommand = "3";
            const string ShowByParameter = "4";
            const string ExitProgramm = "5";

            string menuTitle = "Меню книжного хранилища:";
            string menu = $"\n{ShowAllBooksCommand} - показать все книги в хранилище" +
                          $"\n{AddBookCommand} - добавить книгу в хранилище" +
                          $"\n{RemoveBookCommand} - убрать книгу из хранилища" +
                          $"\n{ShowByParameter} - показать книги по заданному параметру" +
                          $"\n{ExitProgramm} - закрыть хранилище книг";
            bool isRun = true;
            string inputUser;
            string continueMessage = "\nНажмите любую клавишу чтобы продолжить...";
            string requestMessage = "\nВведите команду: ";
            LibraryBooks libraryBooks = new();

            while (isRun)
            {
                Console.Clear();
                Display.Print(menuTitle, ConsoleColor.DarkYellow);
                Display.Print(menu);
                Display.Print(requestMessage, ConsoleColor.DarkGreen);

                inputUser = Console.ReadLine();

                switch (inputUser)
                {
                    case ShowAllBooksCommand:
                        libraryBooks.ShowAllBook();
                        break;

                    case AddBookCommand:
                        libraryBooks.AddBook();
                        break;

                    case RemoveBookCommand:
                        libraryBooks.RemoveBook();
                        break;

                    case ShowByParameter:
                        libraryBooks.FindBooksByParametr();
                        break;

                    case ExitProgramm:
                        isRun = false;
                        break;
                }

                Display.Print($"\n{continueMessage}", ConsoleColor.Green);
                Console.ReadLine();
            }
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

        public override string ToString()
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

        public void ShowAllBook()
        {
            int indexNumber = 0;
            Display.Print("Список всех книг:", ConsoleColor.Blue);

            foreach (Book book in _books)
            {
                Display.Print($"\n{++indexNumber}. {book}");
            }
        }

        public void AddBook()
        {
            Console.Clear();
            Display.Print("Введите название книги:");
            string inputTitleName = Console.ReadLine();
            Display.Print("Введите автора книги:");
            string inputAuthor = Console.ReadLine();
            Display.Print("Введите год первой публикации книги:");
            int inputPublicationYear = ReadInt();
            Display.Print("Введите жанр книги:");
            string inputGenre = Console.ReadLine();

            _books.Add(new Book(inputTitleName, inputAuthor, inputPublicationYear, inputGenre));
        }

        public void RemoveBook()
        {
            int startIndex = 0;
            int endIndex = _books.Count;
            Console.Clear();
            ShowAllBook();

            Display.Print("\nВведите номер книги для удаления из хранилища: ");
            int inputIndexNumber = ReadInt(startIndex, endIndex) - 1;

            Display.Print($"\nКнига убрана с полки: {_books[inputIndexNumber]}");
            Console.ReadLine();

            _books.Remove(_books[inputIndexNumber]);
        }

        public void FindBooksByParametr()
        {
            int startIndexMenu = 0;
            int endIndexMenu = 4;
            string inputUserParametr;
            PropertyBook propertyBook;

            ShowLibraryMenu();

            Display.Print("\nВведите команду: ");
            propertyBook = (PropertyBook)ReadInt(startIndexMenu, endIndexMenu) - 1;

            Display.Print($"Конкретизируйте параметр для показа: ");
            inputUserParametr = Console.ReadLine();

            ShowBooksByParametr(propertyBook, inputUserParametr);
        }

        private static void ShowLibraryMenu()
        {
            const string TitleMenu = "1";
            const string AuthorMenu = "2";
            const string YearMenu = "3";
            const string GenreMenu = "4";

            string menu = $"\n{TitleMenu} - по названию" +
                          $"\n{AuthorMenu} - по автору" +
                          $"\n{YearMenu} - по году публикации" +
                          $"\n{GenreMenu} - по жанру";

            Console.Clear();
            Display.Print("По какому параметру хотите показать книги в хранилище?");
            Display.Print(menu);
        }

        private void ShowBooksByParametr(PropertyBook propertyBook, string inputUserParametr)
        {
            int indexNumber = 0;

            if (propertyBook == PropertyBook.TitleName)
            {
                foreach (var book in _books)
                {
                    if (book.TiteleName.Equals(inputUserParametr))
                    {
                        Display.Print($"\n{++indexNumber}. " + book);
                    }
                }
            }
            else if (propertyBook == PropertyBook.Author)
            {
                foreach (var book in _books)
                {
                    if (book.Author.Equals(inputUserParametr))
                    {
                        Display.Print($"\n{++indexNumber}. " + book);
                    }
                }
            }
            else if (propertyBook == PropertyBook.Year)
            {
                foreach (var book in _books)
                {
                    if (book.FirstPublicationYear.ToString().Equals(inputUserParametr))
                    {
                        Display.Print($"\n{++indexNumber}. " + book);
                    }
                }
            }
            else if (propertyBook == PropertyBook.Genre)
            {
                foreach (var book in _books)
                {
                    if (book.Genre.Equals(inputUserParametr))
                    {
                        Display.Print($"\n{++indexNumber}. " + book);
                    }
                }
            }
        }

        private int ReadInt(int firstNumber = 0, int lastNumber = int.MaxValue)
        {
            bool isTryParse = false;
            string userInput;
            int number = 0;

            while (isTryParse == false)
            {
                userInput = Console.ReadLine();

                if (Int32.TryParse(userInput, out int result) == true)
                {
                    if (result > firstNumber && result <= lastNumber)
                    {
                        number = result;
                        isTryParse = true;
                    }
                    else
                    {
                        Display.Print($"\nОшибка! Введеное число [{userInput}] должно быть больше: [{firstNumber}] и меньше, либо равным: [{lastNumber}]!\nПопробуйте снова: ");
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