//Создать класс игрока, у которого есть поля с его положением в x, y.
//Создать класс отрисовщик, с методом, который отрисует игрока.

//Попрактиковаться в работе со свойствами.

//Работа со свойствами

namespace Lesson_39
{
    class Program
    {
        static void Main()
        {
            Console.CursorVisible = false;
            int playerPositionX = 5;
            int playerPositionY = 5;
            Player player = new Player(playerPositionX, playerPositionY);
            Renderer renderer = new Renderer();
            renderer.Draw(player);

            Console.ReadLine();
        }
    }

    class Player
    {
        public Player(int positionX, int positionY, char symbol = '@')
        {
            PositionX = positionX;
            PositionY = positionY;
            Symbol = symbol;
        }

        public int PositionX { get; private set; }
        public int PositionY { get; private set; }
        public char Symbol { get; private set; }
    }

    class Renderer
    {
        public void Draw(Player player)
        {
            Console.SetCursorPosition(player.PositionX, player.PositionY);
            Console.Write(player.Symbol);
        }
    }
}