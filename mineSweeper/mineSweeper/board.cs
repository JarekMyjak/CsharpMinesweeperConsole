using System;
using System.Collections.Generic;
using System.Linq;


namespace mineSweeper
{
    class Board
    {

        public int sizex = 8;
        public int sizey = 8;
        int bombNum = 10;
        const int tileSize = 3;
        Tile[,] boardArr;
        public int revealedCount = 0;
        public int freeTiles;
        public bool blown = false;
        public bool bombsGenerated = false;

        public Board(int sizeX, int sizeY, int bombNumber)
        {
            sizex = sizeX;
            sizey = sizeY;
            bombNum = bombNumber;
            boardArr = InitializeBoard(sizex, sizey);
            freeTiles = (sizex * sizey) - bombNum;
        }

        private Tile[,] InitializeBoard(int sizex, int sizey)
        {
            //Initialize empty board
            Tile[,] t = new Tile[sizex, sizey];
            for (int i = 0; i < sizex; i++)
            {
                for (int j = 0; j < sizey; j++)
                {
                    t[i, j] = new Tile(i, j);
                }
            }
            return t;
        }

        private Tile[,] InitializeBoard(int sizex, int sizey, int bombNum, int startX, int startY)
        {
            //initialize all tiles empty
            Tile[,] t = new Tile[sizex, sizey];
            for (int i = 0; i < sizex; i++)
            {
                for (int j = 0; j < sizey; j++)
                {
                    t[i, j] = new Tile(i, j);
                }
            }

            //add bombs
            //make list of possible locations, shuffle it, take bombs
            List<Tile> bombPosibilities = new List<Tile> { };
            for (int i = 0; i < sizex; i++)
            {
                for (int j = 0; j < sizey; j++)
                {
                    if (!(i == startX && j == startY))
                    {
                        bombPosibilities.Add(new Tile(i, j));
                    }
                    
                }
            }

            //not the best shuffle for not the best saper
            List<Tile> shuffledBombPosibilities = bombPosibilities.OrderBy(a => Guid.NewGuid()).ToList();

            int bombIndex = 0;
            while (bombIndex < bombNum)
            {
                int tx = shuffledBombPosibilities[bombIndex].xpos;
                int ty = shuffledBombPosibilities[bombIndex].ypos;
                t[tx, ty].bomb = true;
                bombIndex++;
            }

            //initialize neighbor counts, not really proud of implementation, but works!
            for (int i = 0; i < sizex; i++)
            {
                for (int j = 0; j < sizey; j++)
                {
                    int neighbourCount = 0;

                    for (int xOffset = -1; xOffset <= 1; xOffset++)
                    {
                        for (int yOffset = -1; yOffset <= 1; yOffset++)
                        {
                            if (!(xOffset == 0 && yOffset == 0))
                            {
                                Tile neighbor = null;

                                try { neighbor = t[i + xOffset, j + yOffset]; } catch (Exception) { }

                                if (neighbor != null)
                                {
                                    neighbourCount += neighbor.bomb ? 1 : 0;
                                }
                            }
                        }
                    }
                    t[i, j].neighborNumber = neighbourCount;
                }
            }

            return t;
        }

        public void displayBoard()
        {
            
            for (int i = 0; i < sizex; i++)
            {
                for (int j = 0; j < sizey; j++)
                {
                    boardArr[i, j].display();

                }
            }
        }

        public void revealBoard()
        {

            for (int i = 0; i < sizex; i++)
            {
                for (int j = 0; j < sizey; j++)
                {
                    boardArr[i, j].reveal();
                    boardArr[i, j].display();

                }
            }
        }
        public void displayCursor(int x, int y)
        {
            Console.SetCursorPosition(x * tileSize, y * tileSize);
            Console.Write("╔═╗");
            Console.SetCursorPosition(x * tileSize, (y * y) + 1);
            Console.Write("║");
            Console.SetCursorPosition(x * tileSize + 2, y * tileSize + 1);
            Console.Write("║");
            Console.SetCursorPosition(x * tileSize, y * tileSize + 2);
            Console.Write("╚═╝");

        }

        public void flagTile(int x, int y)
        {
            Tile tile = boardArr[x, y];
            if (!tile.revealed)
            {
                tile.flaged = !tile.flaged;
            }

        }

        public void revealTile(int x, int y)
        {
            // first make sure that bombs are generated;
            // if not generate bombs without the field we checked

            if (!bombsGenerated)
            {
                boardArr = InitializeBoard(sizex, sizey, bombNum, x, y);
                bombsGenerated = true;
                
            }

            Tile tile = boardArr[x, y];
            int neighborNumber = tile.neighborNumber;
            if (tile.bomb)
            {
                blown = true;
            }

            if (!tile.bomb && !tile.flaged && !tile.revealed)
            {
                revealedCount++;
                tile.reveal();
                if (neighborNumber == 0)
                {
                    for (int xOffset = -1; xOffset <= 1; xOffset++)
                    {
                        for (int yOffset = -1; yOffset <= 1; yOffset++)
                        {
                            if (!(xOffset == 0 && yOffset == 0))
                            {
                                Tile neighbor = null;

                                try { neighbor = boardArr[x + xOffset, y + yOffset]; } catch (Exception) { }
                                if (neighbor != null)
                                {
                                    revealTile(x + xOffset, y + yOffset);
                                }

                            }
                        }
                    }
                }
            }


        }
    }
}
