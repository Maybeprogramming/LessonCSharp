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
        public string TiteleName { get; private set; }
        public string Author { get; private set; }
        public int PublicationYear { get; private set; }

        public string ShowInfo()
        {
            return $"Название: {TiteleName}, Автор {Author}, Дата выхода: {PublicationYear}";
        }
    }

    class LibraryBooks
    {
        public void AddBook()
        {

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
            Console.WriteLine("Поиск по названию");
        }

        private void FindBookByAuthor()
        {
            Console.WriteLine("Поиск по автору");
        }

        private void FindBookByYear()
        {
            Console.WriteLine("Поиск по году издания");
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