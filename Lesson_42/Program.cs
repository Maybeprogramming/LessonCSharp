namespace Lesson_42
{
    class Program
    {
        static void Main()
        {
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
            libraryBooks.FindBook(PropertyBookToFind.ByAuthor);
            libraryBooks.FindBook(PropertyBookToFind.ByYear);
            libraryBooks.FindBook(PropertyBookToFind.ByTitleName);
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
            return $"Название: {TiteleName}, Автор {Author}, Дата выхода: {FirstPublicationYear}";
        }
    }

    class LibraryBooks
    {
        private List<Book> _books = new()
        {
            new Book("Дубровский","Александр Пушкин", 1841, "Любовный роман"),
            new Book("Анна Коренина", "Лев Толстой",1875,"Любовный роман"),
            new Book("По ком звонит колокол", "Эрнест Хемингуэй", 1940, "Любовные романы"),
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

        public void AddBook(string titleName, string author, int firstPublicationYear, string genre)
        {
            _books.Add(new Book(titleName, author, firstPublicationYear, genre));
        }

        public void RemoveBook()
        {

        }

        public void FindBook(PropertyBookToFind findBookParametr)
        {
            switch (findBookParametr)
            {
                case PropertyBookToFind.ByTitleName:
                    FindBookByTitleName();
                    break;

                case PropertyBookToFind.ByAuthor:
                    FindBookByAuthor();
                    break;

                case PropertyBookToFind.ByYear:
                    FindBookByYear();
                    break;
            }
        }

        private void FindBookByTitleName()
        {
            Display.Print("Поиск по названию");
        }

        private void FindBookByAuthor()
        {
            Display.Print("Поиск по автору");
        }

        private void FindBookByYear()
        {
            Display.Print("Поиск по году издания");
        }

    }

    enum PropertyBookToFind
    {
        ByTitleName,
        ByAuthor,
        ByYear
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