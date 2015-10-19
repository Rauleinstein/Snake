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
	class Coord
	{
		public const string Ok = "Ok";

		public int X; public int Y;
		public Coord(int x = 0, int y = 0) { X = x; Y = y; }
		public Coord(Coord input) { X = input.X; Y = input.Y; }
	}

    class Direction 
    {
        public ConsoleKeyInfo key;
        public short lastDir = 2;
        public short newDir = 1;

        public short getNewDir(ref bool gg, ref bool pause) {
            if (Console.KeyAvailable) {
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Escape)
                    gg = true;
                else if (key.Key == ConsoleKey.Spacebar)
                    pause = !pause;
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
    }

	class SnakeMain
	{
        static bool gameOver, pause, inUse; //SnakeMain
        static Direction dir;  //Snake
        static int boardWidth, boardHeight; //GameWindow
        static List<Coord> snake; //Snake
        static Random rand; //SnakeMain
        static Coord egg;  //SnakeMain
        static Coord tail; //Snake
        static Coord head; //Snake
        static Coord newHead; //Snake

        /**
            function Init with the initialization of all the main variables
        */
        public void Init() 
        {
            gameOver = false;
            pause = false;
            inUse = false;
            dir = new Direction();
            dir.newDir = 2;
            dir.lastDir = 2;
            boardWidth = Console.WindowWidth;
            boardHeight = Console.WindowHeight;
            snake = new List<Coord>();
            for(int i = 0; i<4; i++) {
                snake.Add(new Coord(10, 10));
            }

            Console.CursorVisible = false;
            Console.Title = "Westerdals Oslo ACT - SNAKE";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(10, 10);
            Console.Write("@");
            rand = new Random();
            egg = new Coord();
            while (true) {
                egg.X = rand.Next(0, boardWidth);
                egg.Y = rand.Next(0, boardHeight);
                bool spot = true;
                foreach (Coord i in snake)
                    if (i.X == egg.X && i.Y == egg.Y) {
                        spot = false;
                        break;
                    }
                if (spot) {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(egg.X, egg.Y);
                    Console.Write("$");
                    break;
                }
            }
        }
        public bool death() {
            if (newHead.X < 0 || newHead.X >= boardWidth)
                return true;
            else if (newHead.Y < 0 || newHead.Y >= boardHeight)
                return true;
            if (newHead.X == egg.X && newHead.Y == egg.Y) {
                if (snake.Count + 1 >= boardWidth * boardHeight)
                    // No more room to place eggs -- game over.
                    return true;
                else {
                    while (true) {
                        egg.X = rand.Next(0, boardWidth); egg.Y = rand.Next(0, boardHeight);
                        bool found = true;
                        foreach (Coord i in snake)
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
                snake.RemoveAt(0);
                foreach (Coord x in snake) {
                    if (x.X == newHead.X && x.Y == newHead.Y) {
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
            //Variables 
			Stopwatch t = new Stopwatch();
			t.Start();
			while (!gameOver) {
                dir.newDir = dir.getNewDir(ref gameOver, ref pause);
				if (!pause) {
					if (t.ElapsedMilliseconds < 100)
						continue;
					t.Restart();
					tail = new Coord(snake.First());
					head = new Coord(snake.Last());
					newHead = new Coord(head);
					switch (dir.newDir) {
						case 0:
							newHead.Y -= 1;
							break;
						case 1:
							newHead.X += 1;
							break;
						case 2:
							newHead.Y += 1;
							break;
						default:
							newHead.X -= 1;
							break;
					}
                    gameOver = snakemain.death();
					if (!gameOver) {
						Console.ForegroundColor = ConsoleColor.Yellow;
						Console.SetCursorPosition(head.X, head.Y);
                        Console.Write("0");
						if (!inUse) {
							Console.SetCursorPosition(tail.X, tail.Y);
                            Console.Write(" ");
						} else {
							Console.ForegroundColor = ConsoleColor.Green;
                            Console.SetCursorPosition(egg.X, egg.Y);
                            Console.Write("$");
							inUse = false;
						}
						snake.Add(newHead);
						Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.SetCursorPosition(newHead.X, newHead.Y);
                        Console.Write("@");
						dir.lastDir = dir.newDir;
					}
				}
			}
		}
	}
}