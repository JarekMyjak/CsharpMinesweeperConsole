using System;
using System.Collections.Generic;
using System.Linq;


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

        const int tileSize = 3;
        GameState State = GameState.running;
        int cursorPosx = 0;
        int cursorPosy = 0;
        Board board = new Board(8,8,10);
/*
        int sizex = 8;
        int sizey = 8;
        int bombNum = 10;
        const int tileSize = 3;
        //Tile[,] board;
        int revealedCount = 0;
        int freeTiles;*/
        



        public Game()
        {
            Console.Clear();
            Console.CursorVisible = false;

            //while gra sie toczy
            while (State == GameState.running)
            {
                board.displayBoard();
                displayCursor();
                handleKeypress();
                if (board.revealedCount >= board.freeTiles)
                {
                    State = GameState.won;
                } else if (board.blown)
                {
                    State = GameState.lost;
                }
            }
            Console.WriteLine(State.ToString());

            Console.ReadLine();
        }

        private void handleKeypress()
        {

            string keyPressed = Console.ReadKey(true).Key.ToString();
            switch (keyPressed)
            {
                case "LeftArrow":
                case "A":
                    cursorPosx = Math.Clamp(cursorPosx - 1, 0, board.sizex - 1);
                    break;
                case "RightArrow":
                case "D":
                    cursorPosx = Math.Clamp(cursorPosx + 1, 0, board.sizex - 1);
                    break;
                case "UpArrow":
                case "W":
                    cursorPosy = Math.Clamp(cursorPosy - 1, 0, board.sizey - 1);
                    break;
                case "DownArrow":
                case "S":
                    cursorPosy = Math.Clamp(cursorPosy + 1, 0, board.sizey - 1);
                    break;
                case "Enter":
                    board.revealTile(cursorPosx, cursorPosy);
                    break;
                case "F":
                    board.flagTile(cursorPosx, cursorPosy);
                    break;
                default:
                    break;
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
