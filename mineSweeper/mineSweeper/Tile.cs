using System;
using System.Collections.Generic;
using System.Text;

namespace mineSweeper
{
    class Tile
    {
        public int xpos;
        public int ypos;
        public bool revealed = false;
        public bool bomb = false;
        public bool flaged = false;
        const int tileSize = 3;
        public int neighborNumber = 0;

        public Tile(int x, int y)
        {
            xpos = x;
            ypos = y;
        }

        public void reveal() { revealed = true; }

        public void display()
        {
            switch (revealed)
            {
                case true:
                    Console.SetCursorPosition(xpos * tileSize, ypos * tileSize);
                    Console.Write("   ");
                    Console.SetCursorPosition(xpos * tileSize, ypos * tileSize + 1);
                    Console.Write(" ");
                    Console.Write(neighborNumber == 0 ? " " : neighborNumber.ToString());
                    Console.Write(" ");
                    Console.SetCursorPosition(xpos * tileSize, ypos * tileSize + 2);
                    Console.Write("   ");
                    break;
                case false:
                    Console.SetCursorPosition(xpos * tileSize, ypos * tileSize);
                    Console.Write("┌─┐");
                    Console.SetCursorPosition(xpos * tileSize, ypos * tileSize + 1);
                    Console.Write("│" + " " + "│");
                    Console.SetCursorPosition(xpos * tileSize, ypos * tileSize + 2);
                    Console.Write("└─┘");
                    break;
            }


            if (flaged)
            {

                Console.SetCursorPosition(xpos * tileSize + 1, ypos * tileSize + 1);
                Console.Write("F");

            }

            if (bomb && revealed)
            {
                Console.SetCursorPosition(xpos * tileSize, ypos * tileSize);
                Console.Write("┌─┐");
                Console.SetCursorPosition(xpos * tileSize, ypos * tileSize + 1);
                Console.Write("│" + "B" + "│");
                Console.SetCursorPosition(xpos * tileSize, ypos * tileSize + 2);
                Console.Write("└─┘");
            }
        }
    }
}
