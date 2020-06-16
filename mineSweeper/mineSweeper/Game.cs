using System;
using System.Collections.Generic;
using System.Linq;


namespace mineSweeper
{
    class Game
    {
        public enum GameState
        {
            running,
            lost,
            won,
            completed
        }

        const int tileSize = 3;
        public GameState State = GameState.running;
        int cursorPosx = 0;
        int cursorPosy = 0;
        Board board;
        bool sound;
/*
        int sizex = 8;
        int sizey = 8;
        int bombNum = 10;
        const int tileSize = 3;
        //Tile[,] board;
        int revealedCount = 0;
        int freeTiles;*/
        



        public Game(int width, int height, int bombNum, bool sound)
        {
            this.sound = sound;
            Console.SetWindowSize(width * tileSize, height * tileSize);
            Console.SetBufferSize(width * tileSize, height * tileSize);
            board = new Board(width, height, bombNum);

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
                    board.revealBoard();
                } else if (board.blown)
                {
                    State = GameState.lost;
                    board.revealBoard();
                }
            }
            Console.BufferHeight = Console.BufferHeight + 3;
            Console.WindowHeight = Console.WindowHeight + 3;
            Console.SetCursorPosition(0,Console.BufferHeight-2);
            switch (State)
            {
                case GameState.lost:
                    Console.WriteLine("Przegrana :(");
                    break;
                case GameState.won:
                    Console.WriteLine("Wygrana \u263B");
                    break;
            }
            Console.ReadKey();
            State = GameState.completed;
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
                case "Q":
                case "Spacebar":
                    if (sound) Console.Beep(440, 100);
                    board.revealTile(cursorPosx, cursorPosy);
                    
                    
                    break;
                case "F":
                case "ALT":
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
