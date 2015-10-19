using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

// WARNING: DO NOT code like this. Please. EVER! 
//          "Aaaargh!" 
//          "My eyes bleed!" 
//          "I facepalmed my facepalm." 
//          Etc.
//          I had a lot of fun obfuscating this code though! And I can now (proudly?) say that this is the uggliest short piece of code I've ever worked with! :-)
//          (And yes, it could have been a lot ugglier! But the idea wasn't to make it fuggly-uggly, just funny-uggly, sweet-uggly, or whatever you want to call it.)
//
//          -Tomas
//
namespace SnakeMess
{

	class SnakeMain
	{
        private bool gameOver, pause, inUse;
        private Direction dir;
        private Snake snake;
        private Random rand;
        private Coord egg;
        private Coord tail;
        private Coord head;
        private Coord newH;
        private GameWindow window;

        /**
            function Init with the initialization of all the main variables
        */
        public void Init() 
        {
            pause = inUse = false;
            dir = new Direction();
            window = new GameWindow();
            snake = new Snake();

            window.setTitle("Westerdals Oslo ACT - SNAKE");
           
            rand = new Random();
            egg = new Coord();

            addEgg();

            Stopwatch t = new Stopwatch();
            t.Start();
            

            
            while (!gameOver)
            {
                dir.newDir = dir.getNewDir(gameOver, pause);
                if (!pause)
                {
                    if (t.ElapsedMilliseconds < 100)
                        continue;
                    t.Restart();
                    tail = new Coord(snake.getFirst());
                    head = new Coord(snake.getLast());
                    newH = new Coord(head);
                    switch (dir.newDir)
                    {
                        case 0:
                            newH.Y -= 1;
                            break;
                        case 1:
                            newH.X += 1;
                            break;
                        case 2:
                            newH.Y += 1;
                            break;
                        default:
                            newH.X -= 1;
                            break;
                    }
                    gameOver = death();
                    if (!gameOver)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.SetCursorPosition(head.X, head.Y);
                        Console.Write("0");
                        if (!inUse)
                        {
                            Console.SetCursorPosition(tail.X, tail.Y);
                            Console.Write(" ");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.SetCursorPosition(egg.X, egg.Y);
                            Console.Write("$");
                            inUse = false;
                        }
                        snake.add(newH);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.SetCursorPosition(newH.X, newH.Y);
                        Console.Write("@");
                        dir.lastDir = dir.newDir;
                    }
                }
            }
            
        }

        public void addEgg()
        {
            while (true)
            {
                egg.X = rand.Next(0, window.getBoardW());
                egg.Y = rand.Next(0, window.getBoardH());
                bool spot = true;
                foreach (Coord i in snake.getSnake())
                    if (i.X == egg.X && i.Y == egg.Y)
                    {
                        spot = false;
                        break;
                    }
                if (spot)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(egg.X, egg.Y);
                    Console.Write("$");
                    break;
                }
            }
        }
        public bool death() {
            if (newH.X < 0 || newH.X >= window.getBoardW())
                return true;
            else if (newH.Y < 0 || newH.Y >= window.getBoardH())
                return true;
            if (newH.X == egg.X && newH.Y == egg.Y) {
                if (snake.getCount() + 1 >= window.getBoardW() * window.getBoardH())
                    // No more room to place eggs -- game over.
                    return true;
                else {
                    while (true) {
                        egg.X = rand.Next(0, window.getBoardW()); egg.Y = rand.Next(0, window.getBoardH());
                        bool found = true;
                        foreach (Coord i in snake.getSnake())
                            if (i.X == egg.X && i.Y == egg.Y) {
                                found = false;
                                break;
                            }
                        if (found) {
                            inUse = true;
                            break;
                        }
                    }
                }
            }
            if (!inUse) {
                snake.remove();
                foreach (Coord x in snake.getSnake()) {
                    if (x.X == newH.X && x.Y == newH.Y) {
                        // Death by accidental self-cannibalism.
                        return true;
                    }
                }
            }
            return false;
        }
		public static void Main(string[] arguments)
		{
            SnakeMain snakemain = new SnakeMain();
            snakemain.Init();
		}
	}
}