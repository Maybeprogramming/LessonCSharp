namespace Lesson_31
{

    //Сделать игровую карту с помощью двумерного массива.
    //Сделать функцию рисования карты.
    //Помимо этого, дать пользователю возможность перемещаться по карте
    //и взаимодействовать с элементами
    //(например пользователь не может пройти сквозь стену)

    //Все элементы являются обычными символами
    //Brave New World

    internal class Program
    {
        static void Main()
        {
            Console.CursorVisible = false;

            char[,] map = ReadMap("map.txt");
            int pacmanX = 0;
            int pacmanY = 0;
            int pacmanDX = 0;
            int pacmanDY = 1;

            bool isPlaying = true;
            bool isAlive = true;

            char player = '@';
            char ghost = '$';

            int ghostX = 0;
            int ghostY = 0;

            int allCherry = 0;
            int collectCherry = 0;

            FindSpawnPoint(map, ref pacmanX, ref pacmanY, player);
            FindSpawnPoint(map, ref ghostX, ref ghostY, ghost);
            AddCherryToMap(map);
            allCherry = CalculateAllCherryInMap(map);
            DrowMap(map);

            while (isPlaying == true)
            {
                Console.SetCursorPosition(0, 20);
                Console.WriteLine($"Собрано: {collectCherry}/{allCherry}");

                if (Console.KeyAvailable == true)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);

                    ChangeDirection(key, ref pacmanDX, ref pacmanDY);
                }

                if (map[pacmanX + pacmanDX, pacmanY + pacmanDY] != '#')
                {
                    Move(ref pacmanX, ref pacmanY, pacmanDX, pacmanDY);

                    CollectDots(map, pacmanX, pacmanY, ref collectCherry);
                }

                Task.Delay(100).Wait();

                if (collectCherry == allCherry)
                {
                    isPlaying = false;
                }
            }

            if (collectCherry == allCherry)
            {
                Console.SetCursorPosition(0, 25);
                Console.WriteLine("Вы победили");
            }

            Console.ReadLine();
        }

        static void CollectDots(char[,] map, int pacmanX, int pacmanY, ref int collectCherry)
        {
            if (map[pacmanX, pacmanY] == '.')
            {
                collectCherry++;
                map[pacmanX, pacmanY] = ' ';
            }
        }

        static void Move(ref int X, ref int Y, int DX, int DY)
        {
            Console.SetCursorPosition(Y, X);
            Console.Write(" ");

            X += DX;
            Y += DY;

            Console.SetCursorPosition(Y, X);
            Console.Write('@');
        }

        static void ChangeDirection(ConsoleKeyInfo key, ref int DX, ref int DY)
        {
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    DX = -1;
                    DY = 0;
                    break;

                case ConsoleKey.DownArrow:
                    DX = 1;
                    DY = 0;
                    break;

                case ConsoleKey.LeftArrow:
                    DX = 0;
                    DY = -1;
                    break;

                case ConsoleKey.RightArrow:
                    DX = 0;
                    DY = 1;
                    break;
            }
        }

        static char[,] ReadMap(string mapName)
        {
            string[] newFile = File.ReadAllLines($"Map\\{mapName}");
            char[,] map = new char[newFile.Length, newFile[0].Length];

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = newFile[i][j];
                }
            }

            return map;
        }

        static void AddCherryToMap(char[,] map)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == ' ')
                    {
                        map[i, j] = '.';
                    }
                }
            }
        }

        static int CalculateAllCherryInMap(char[,] map)
        {
            int cherryInMap = 0;
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == '.')
                    {
                        cherryInMap++;
                    }
                }
            }

            return cherryInMap;
        }

        static void FindSpawnPoint(char[,] map, ref int posX, ref int posY, char characterSymbol)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == characterSymbol)
                    {
                        posX = i;
                        posY = j;
                        break;
                    }
                }
            }
        }

        static void DrowMap(char[,] map)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}