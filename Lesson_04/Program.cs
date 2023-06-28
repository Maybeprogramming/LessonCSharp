namespace Lesson_04
{
    internal class Program
    {
        static void Main()
        {
            const int picturesInRow = 3;
            const int userPictures = 52;
            int filledRows;
            int outsidePictures;

            filledRows = userPictures / picturesInRow;
            outsidePictures = userPictures % picturesInRow;

            Console.WriteLine($"Заполненных рядов: {filledRows}");
            Console.WriteLine($"Картинок вне рядов: {outsidePictures}");

            Console.ReadLine();
        }
    }
}