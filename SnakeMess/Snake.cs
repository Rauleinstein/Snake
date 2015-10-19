using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnakeMess
{
    class Snake
    {
        private List<Coord> snake;
        public Snake()
        {
            snake = new List<Coord>();
            snake.Add(new Coord(10, 10)); snake.Add(new Coord(10, 10)); snake.Add(new Coord(10, 10)); snake.Add(new Coord(10, 10));
            Console.ForegroundColor = ConsoleColor.Green; Console.SetCursorPosition(10, 10); Console.Write("@");
        }

        public void add(Coord newCoord)
        {
            snake.Add(newCoord);
        }

        public void remove()
        {
            snake.RemoveAt(0);
        }

        public List<Coord> getSnake()
        {
            return snake;
        }

        public Coord getFirst()
        {
            return snake.First();
        }

        public Coord getLast()
        {
            return snake.Last();
        }

        public int getCount()
        {
            return snake.Count;
        }
    }
}
