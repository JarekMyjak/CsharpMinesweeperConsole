using System;
using System.Threading;

namespace mineSweeper
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Console.Beep(262, 125);
            Console.Beep(294, 125);
            Console.Beep(311, 125);
            Thread.Sleep(125);
            Console.Beep(262, 125);
            Console.Beep(349, 125);
            Console.Beep(370, 125);*/

            bool sound = true;
            Game game = displayMenu();
            while (true) game = displayMenu();

            Game displayMenu()
            {
                Console.Clear();
                Console.WriteLine("\n\"Saper myli sie tylko raz\"\n\nWybierz poziom trudności:");
                Console.WriteLine("1 - łatwy");
                Console.WriteLine("2 - średni");
                Console.WriteLine("3 - trudny");
                Console.WriteLine("4 - własny");
                Console.WriteLine("");
                Console.Write("0 - Dźwięk [");
                Console.Write(sound ? "x" : " ");
                Console.WriteLine("]");

                while (true)
                {
                    string choice = Console.ReadKey(true).KeyChar.ToString();
                    switch (choice)
                    {
                        case "1":
                            return new Game(8, 8, 10, sound);
                        case "2":
                            return new Game(16, 16, 40, sound);
                        case "3":
                            return new Game(30, 16, 99, sound);
                        case "4":
                            Console.WriteLine("Podaj Szerokość");
                            int w = int.Parse(Console.ReadLine());
                            w = Math.Clamp(w, 8, 30);
                            Console.WriteLine("Podaj Wysokość");
                            int h = int.Parse(Console.ReadLine());
                            h = Math.Clamp(h, 8, 24);
                            Console.WriteLine("Podaj Ilość Bomb");
                            int b = int.Parse(Console.ReadLine());
                            b = Math.Clamp(b, 10, ((w - 1) * (h - 1)));
                            return new Game(w, h, b, sound);
                        case "0":
                            sound = !sound;
                            displayMenu();
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
