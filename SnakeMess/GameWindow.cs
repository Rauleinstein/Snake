using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnakeMess
{
    class GameWindow
    {
        private int boardW, boardH;
        public GameWindow()
        {
            boardW = Console.WindowWidth;
            boardH = Console.WindowHeight;
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(10, 10);
            Console.Write("@");

        }

        public void setTitle(string title)
        {
            Console.Title = title;
        }

        public int getBoardW()
        {
            return boardW;
        }

        public int getBoardH()
        {
            return boardH;
        }
    }
}
