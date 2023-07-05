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
            Console.Title = "Brave New World";
            StartGame();
            Console.ReadLine();
        }

        private static void StartGame()
        {
            string[] mapFile = {"#######################################" ,
                                    "#x    #x        #         x#         x#" ,
                                    "#     ######    #  #########   ########" ,
                                    "#x    #   xx    #  #      x#   #     x#" ,
                                    "####  #  ########  #  ######   ## #####" ,
                                    "#                          #   #     x#" ,
                                    "#     x    x       @  x    #   #      #" ,
                                    "#     ######### #  ######  #   #  #####" ,
                                    "#x    #   x x   #       #  #   #     x#" ,
                                    "#     # x x x x #  #    #  #   #   #  #" ,
                                    "#     #   x x   #  # x  #  #   #   #  #" ,
                                    "#     ###########  #    #  #   #   #  #" ,
                                    "#x             x#  # x     #   #  x#  #" ,
                                    "#     #         #  #       #   #####  #" ,
                                    "#     #            #x             x   #" ,
                                    "#######################################" };
            char[,] map;
            int playerPositionX = 0;
            int playerPositionY = 0;
            int playerDirectionX = 0;
            int playerDirectionY = 1;
            bool isPlaying = true;
            char playerSymbol = '@';
            char cherrySymbol = '.';
            char emptySymbol = ' ';
            char cherrySpawnPoint = 'x';
            char wallSymbol = '#';
            int allCherrys = 0;
            int collectCherrys = 0;
            int positionScoreTop;
            int potitionScoreLeft = 0;
            int positionWinTextTop;
            int positionWinTextLeft = 0;
            int lineSpacing = 1;
            int delayMiliseconds = 100;
            string winnerMessage = "Вы победили!!!";

            map = ReadMap(mapFile, ref playerPositionX, ref playerPositionY, playerSymbol, ref allCherrys, cherrySymbol, cherrySpawnPoint);

            positionScoreTop = map.GetLength(0) + lineSpacing;
            positionWinTextTop = positionScoreTop + lineSpacing;

            DrawMap(map);

            while (isPlaying == true)
            {
                if (Console.KeyAvailable == true)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);

                    ChangeDirection(key, ref playerDirectionX, ref playerDirectionY);
                }

                if (map[playerPositionX + playerDirectionX, playerPositionY + playerDirectionY] != wallSymbol)
                {
                    Move(ref playerPositionX, ref playerPositionY, playerDirectionX, playerDirectionY, playerSymbol, emptySymbol);

                    TryCollectCherry(map, playerPositionX, playerPositionY, ref collectCherrys, cherrySymbol, emptySymbol);
                }

                Task.Delay(delayMiliseconds).Wait();

                if (collectCherrys == allCherrys)
                {
                    isPlaying = false;
                }

                Console.SetCursorPosition(potitionScoreLeft, positionScoreTop);
                Console.WriteLine($"Собрано: {collectCherrys}/{allCherrys}");
            }

            Console.SetCursorPosition(positionWinTextLeft, positionWinTextTop);
            Console.WriteLine(winnerMessage);

        }

        static void TryCollectCherry(char[,] map, int playerPositionX, int playerPositionY, ref int collectCherry, char characterSymbol, char emptySymbol)
        {
            if (map[playerPositionX, playerPositionY] == characterSymbol)
            {
                collectCherry++;
                map[playerPositionX, playerPositionY] = emptySymbol;
            }
        }

        static void Move(ref int positionX, ref int positionY, int directionX, int directionY, char characterSymbol, char emptySymbol)
        {
            Print(positionX, positionY, emptySymbol);

            positionX += directionX;
            positionY += directionY;

            Print(positionX, positionY, characterSymbol);
        }

        static void Print(int positionX, int positionY, char symbol)
        {
            Console.SetCursorPosition(positionY, positionX);
            Console.Write(symbol);
        }

        static void ChangeDirection(ConsoleKeyInfo key, ref int directionX, ref int directionY)
        {
            const ConsoleKey KeyUp = ConsoleKey.UpArrow;
            const ConsoleKey KeyDown = ConsoleKey.DownArrow;
            const ConsoleKey KeyLeft = ConsoleKey.LeftArrow;
            const ConsoleKey KeyRight = ConsoleKey.RightArrow;

            switch (key.Key)
            {
                case KeyUp:
                    directionX = -1;
                    directionY = 0;
                    break;

                case KeyDown:
                    directionX = 1;
                    directionY = 0;
                    break;

                case KeyLeft:
                    directionX = 0;
                    directionY = -1;
                    break;

                case KeyRight:
                    directionX = 0;
                    directionY = 1;
                    break;
            }
        }

        static char[,] ReadMap(string[] templateMap, ref int playerX, ref int playerY, char characterSymbol, ref int allCherry, char cherry, char cherrySpawnPoint)
        {
            char[,] map = new char[templateMap.GetLength(0), templateMap[0].Length];

            for (int i = 0; i < templateMap.GetLength(0); i++)
            {
                for (int j = 0; j < templateMap[0].Length; j++)
                {
                    map[i, j] = templateMap[i][j];

                    if (map[i, j] == characterSymbol)
                    {
                        playerX = i;
                        playerY = j;
                    }
                    else if (map[i, j] == cherrySpawnPoint)
                    {
                        map[i, j] = cherry;
                        allCherry++;
                    }
                }
            }

            return map;
        }

        static void DrawMap(char[,] map)
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