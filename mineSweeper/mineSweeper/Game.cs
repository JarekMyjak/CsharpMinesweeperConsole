using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace mineSweeper
{
    class Game
    {
        enum GameState
        {
            running,
            lost,
            won,
        }

        int sizex = 10;
        int sizey = 10;
        int bombNum = 10;
        int cursorPosx = 0;
        int cursorPosy = 0;
        const int tileSize = 3;
        Tile[,] board;
        GameState State = GameState.running;

        Random R = new Random();


        public Game()
        {
            Console.Clear();
            Console.CursorVisible = false;
            board = InitializeBoard(sizex, sizey, bombNum);

            //while gra sie toczy
            while (State == GameState.running)
            {
                displayBoard();
                displayCursor();
                handleKeypress();
            }

        }

        private void handleKeypress()
        {

            string keyPressed = Console.ReadKey(true).Key.ToString();
            switch (keyPressed)
            {
                case "LeftArrow":
                case "A":
                    cursorPosx = Math.Clamp(cursorPosx - 1, 0, sizex-1);
                    break;
                case "RightArrow":
                case "D":
                    cursorPosx = Math.Clamp(cursorPosx + 1, 0, sizex-1);
                    break;
                case "UpArrow":
                case "W":
                    cursorPosy = Math.Clamp(cursorPosy + 1, 0, sizey - 1);
                    break;
                case "DownArrow":
                case "S":
                    cursorPosy = Math.Clamp(cursorPosy + 1, 0, sizey - 1);
                    break;
                default:
                    break;
            }
        }

        private Tile[,] InitializeBoard(int sizex, int sizey, int bombNum)
        {
            //initialize all tiles empty
            Tile[,] t = new Tile[sizex, sizey];
            for (int i = 0; i < sizex; i++)
            {
                for (int j = 0; j < sizey; j++)
                {
                    t[i,j] = new Tile(i, j);
                }
            }

            //add bombs
            int bombIndex = 0;
            while (bombIndex < bombNum)
            {
                int tx = R.Next(sizex);
                int ty = R.Next(sizey);
                
                if (!t[tx,ty].bomb)
                {
                    t[tx, ty].bomb = true;
                    bombIndex++;
                }
            }

            //initialize neighbor counts, not really proud of implementation, but works!
            for (int i = 0; i < sizex; i++)
            {
                for (int j = 0; j < sizey; j++)
                {
                    int neighbourCount = 0;

                    for (int xOffset= -1; xOffset <= 1; xOffset++)
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
                    board[i,j].display();

                }
            }
        }

        public void displayCursor()
        {
            Console.SetCursorPosition(cursorPosx * tileSize, cursorPosy * tileSize);
            Console.Write("╔═╗");
            Console.SetCursorPosition(cursorPosx * tileSize, (cursorPosy * tileSize) + 1);
            Console.Write("║");
            Console.SetCursorPosition(cursorPosx * tileSize + 2, cursorPosy * tileSize + 1);
            Console.Write("║");
            Console.SetCursorPosition(cursorPosx * tileSize , cursorPosy * tileSize + 2);
            Console.Write("╚═╝");

        }
    }
}
