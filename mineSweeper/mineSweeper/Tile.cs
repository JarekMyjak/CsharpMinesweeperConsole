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

        public int reveal()
        {
            revealed = true;
            return neighborNumber;
        }

        public void display()
        {
            if (revealed)
            {
                Console.SetCursorPosition(xpos * tileSize, ypos * tileSize);
                Console.Write("   ");
                Console.SetCursorPosition(xpos * tileSize, ypos * tileSize + 1);
                Console.Write(" ");
                Console.Write(neighborNumber == 0 ? " " : neighborNumber.ToString());
                Console.Write(" ");
                Console.SetCursorPosition(xpos * tileSize, ypos * tileSize + 2);
                Console.Write("   ");
            } else
            {
                Console.SetCursorPosition(xpos * tileSize, ypos * tileSize);
                Console.Write("┌─┐");
                Console.SetCursorPosition(xpos * tileSize, ypos * tileSize + 1);
                Console.Write("│" + " " + "│");
                Console.SetCursorPosition(xpos * tileSize, ypos * tileSize + 2);
                Console.Write("└─┘");
                
            }
            if (flaged)
            {
                Console.SetCursorPosition(xpos * tileSize, ypos * tileSize);
                Console.Write("┌─┐");
                Console.SetCursorPosition(xpos * tileSize, ypos * tileSize + 1);
                Console.Write("│" + "F" + "│");
                Console.SetCursorPosition(xpos * tileSize, ypos * tileSize + 2);
                Console.Write("└─┘");
            }
            /*if (bomb)
            {
                Console.SetCursorPosition(xpos * tileSize, ypos * tileSize);
                Console.Write("┌─┐");
                Console.SetCursorPosition(xpos * tileSize, ypos * tileSize + 1);
                Console.Write("│" + "B" + "│");
                Console.SetCursorPosition(xpos * tileSize, ypos * tileSize + 2);
                Console.Write("└─┘");
            }*/
        }
    }
}
