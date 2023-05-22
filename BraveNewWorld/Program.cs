namespace BraveNewWorld
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool isPlaying = true;
            string mapName = "map1";
            char player = '+';
            char wall = '#';
            int playerPositionX = 0;
            int playerPositionY = 0;
            int playerPositionDirectionX = 0;
            int playerPositionDirectionY = 0;
            char[,] map;
            ConsoleKeyInfo key;

            ReadMap(out map, mapName, player, ref playerPositionX, ref playerPositionY);
            DrawMap(map);

            Console.SetCursorPosition(playerPositionY, playerPositionX);

            while (isPlaying)
            {
                if(Console.KeyAvailable)
                {
                    key = Console.ReadKey();

                    DirectionalChoice(key, ref playerPositionDirectionX, ref playerPositionDirectionY);

                    if (map[playerPositionX + playerPositionDirectionX, playerPositionY + playerPositionDirectionY] != wall)
                    {
                        Move(ref playerPositionX, ref playerPositionY, playerPositionDirectionX, playerPositionDirectionY, player);
                    }
                }

                Thread.Sleep(200);
            }
        }

        static void Move(ref int playerX, ref int playerY, int playerDirectionX, int playerDirectionY, char player)
        {
            Console.Write(" ");

            playerX += playerDirectionX;
            playerY += playerDirectionY;
            
            Console.SetCursorPosition(playerY, playerX);
            Console.Write(player);
            Console.SetCursorPosition(playerY, playerX);
        }

        static void DirectionalChoice(ConsoleKeyInfo key, ref int playerDirectionX, ref int playerDirectionY)
        {
            const ConsoleKey pressUpButton = ConsoleKey.UpArrow;
            const ConsoleKey pressDownButton = ConsoleKey.DownArrow;
            const ConsoleKey pressLeftButton = ConsoleKey.LeftArrow;
            const ConsoleKey pressRightButton = ConsoleKey.RightArrow;

            switch (key.Key)
            {
                case pressUpButton:
                    playerDirectionX = -1; playerDirectionY = 0;
                    break;

                case pressDownButton:
                    playerDirectionX = 1; playerDirectionY = 0;
                    break;

                case pressLeftButton:
                    playerDirectionX = 0; playerDirectionY = -1;
                    break;

                case pressRightButton:
                    playerDirectionX = 0; playerDirectionY = 1;
                    break;
            }    
        }

        static void DrawMap(char[,] map)
        {
            for(int i = 0; i < map.GetLength(0); i++)
            {
                for(int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[i, j]);
                }

                Console.WriteLine();
            }
        }

        static void ReadMap(out char[,] map, string mapName, char player, ref int playerX, ref int playerY)
        {
            string[] mapFile = File.ReadAllLines($"Maps/{mapName}.txt");
            map = new char[mapFile.Length, mapFile[0].Length];

            for(int i = 0; i < map.GetLength(0); i++)
            {
                for(int j = 0; j < map.GetLength(1); j++)
                {
                    map[i,j] = mapFile[i][j];

                    if (mapFile[i][j] == player)
                    {
                        playerX = i;
                        playerY = j;
                    }
                }
            }
        }
    }
}