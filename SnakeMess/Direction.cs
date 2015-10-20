using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnakeMess
{
    class Direction
    {
        public ConsoleKeyInfo key;
        public short lastDir = 2;
        public short newDir = 2;
        private bool gameOver = false;
        private bool pause = false;


        public short getNewDir()
        {
            if (Console.KeyAvailable)
            {
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Escape)
                    gameOver = true;
                else if (key.Key == ConsoleKey.Spacebar)
                    setPause();
                else if (key.Key == ConsoleKey.UpArrow && lastDir != 2)
                    newDir = 0;
                else if (key.Key == ConsoleKey.RightArrow && lastDir != 3)
                    newDir = 1;
                else if (key.Key == ConsoleKey.DownArrow && lastDir != 0)
                    newDir = 2;
                else if (key.Key == ConsoleKey.LeftArrow && lastDir != 1)
                    newDir = 3;
            }
            return newDir;
        }

        public void setPause()
        {
            pause = !pause;
        }

        public bool getGameOver()
        {
            return gameOver;
        }

        public bool getPause()
        {
            return pause;
        }
    }
}
