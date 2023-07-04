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
            StartGame();
            Console.ReadLine();
        }

        private static void StartGame()
        {
            string[] mapLevelOne = {"#######################################" ,
                                    "#     #         #          #          #" ,
                                    "#     ######    #  #########   ########" ,
                                    "#     #         #  #       #   #      #" ,
                                    "####  #  ########  #  ######   ## #####" ,
                                    "#     x            @       #   #      #" ,
                                    "#     x    x               #   #      #" ,
                                    "#     ######### #  ######  #   #  #####" ,
                                    "#     #         #       #  #   #      #" ,
                                    "#     #         #  #    #  #   #   #  #" ,
                                    "#     #         #  #    #  #   #   #  #" ,
                                    "#     ###########  #    #  #   #   #  #" ,
                                    "#               #  #       #   #   #  #" ,
                                    "#     #         #  #       #   #####  #" ,
                                    "#     #            #                  #" ,
                                    "#######################################" };
            char[,] map;
            int playerX = 0;
            int playerY = 0;
            int moveDirectionX = 0;
            int moveDirectionY = 1;
            bool isPlaying = true;
            char player = '@';
            char cherry = '.';
            char empty = ' ';
            char cherrySpawnPoint = 'x';
            char wall = '#';
            int allCherry = 0;
            int collectCherry = 0;
            int positionScoreTop = 20;
            int potitionScoreLeft = 0;
            int positionWinTextTop = 25;
            int positionWinTextLeft = 0;
            string winnerMessage = "Вы победили!!!";

            map = ReadMap(mapLevelOne, ref playerX, ref playerY, player, ref allCherry, cherry, cherrySpawnPoint);

            DrowMap(map);

            while (isPlaying == true)
            {
                if (Console.KeyAvailable == true)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);

                    ChangeDirection(key, ref moveDirectionX, ref moveDirectionY);
                }

                if (map[playerX + moveDirectionX, playerY + moveDirectionY] != wall)
                {
                    Move(ref playerX, ref playerY, moveDirectionX, moveDirectionY, player, empty);

                    CollectCherry(map, playerX, playerY, ref collectCherry, cherry, empty);
                }

                Task.Delay(100).Wait();

                if (collectCherry == allCherry)
                {
                    isPlaying = false;
                }

                Console.SetCursorPosition(potitionScoreLeft, positionScoreTop);
                Console.WriteLine($"Собрано: {collectCherry}/{allCherry}");
            }

            if (collectCherry == allCherry)
            {
                Console.SetCursorPosition(positionWinTextLeft, positionWinTextTop);
                Console.WriteLine(winnerMessage);
            }
        }

        static void CollectCherry(char[,] map, int playerX, int playerY, ref int collectCherry, char cherry, char empty)
        {
            if (map[playerX, playerY] == cherry)
            {
                collectCherry++;
                map[playerX, playerY] = empty;
            }
        }

        static void Move(ref int playerX, ref int playerY, int moveDirectionX, int moveDirectionY, char player, char empty)
        {
            Console.SetCursorPosition(playerY, playerX);
            Console.Write(empty);

            playerX += moveDirectionX;
            playerY += moveDirectionY;

            Console.SetCursorPosition(playerY, playerX);
            Console.Write(player);
        }

        static void ChangeDirection(ConsoleKeyInfo key, ref int directionX, ref int directionY)
        {
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    directionX = -1;
                    directionY = 0;
                    break;

                case ConsoleKey.DownArrow:
                    directionX = 1;
                    directionY = 0;
                    break;

                case ConsoleKey.LeftArrow:
                    directionX = 0;
                    directionY = -1;
                    break;

                case ConsoleKey.RightArrow:
                    directionX = 0;
                    directionY = 1;
                    break;
            }
        }

        static char[,] ReadMap(string [] mapTemplate, ref int playerX, ref int playerY, char characterSymbol, ref int allCherry, char cherry, char cherrySpawnPoint)
        {
            char[,] map = new char[mapTemplate.GetLength(0), mapTemplate[0].Length];

            for (int i = 0; i < mapTemplate.GetLength(0); i++)
            {
                for (int j = 0; j < mapTemplate[0].Length; j++)
                {
                    map[i, j] = mapTemplate[i][j];

                    if (map[i, j] == characterSymbol)
                    {
                        playerX = i;
                        playerY = j;
                        break;
                    }

                    if (map[i, j] == cherrySpawnPoint)
                    {
                        map[i, j] = cherry;
                        allCherry++;
                    }
                }
            }

            return map;
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