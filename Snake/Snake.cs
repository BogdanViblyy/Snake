using System.Collections.Generic;
using System.Drawing;
using NAudio.Wave;
using System.Threading;

namespace Snake
{
    class Snake : Figure
    {
        Direction direction;
        Score score;

        public Snake(Point tail, int length, Direction _direction, Score _score)
        {
            direction = _direction;
            score = _score;
            pList = new List<Point>();
            for (int i = 0; i < length; i++)
            {
                Point p = new Point(tail);
                p.Move(i, direction);
                pList.Add(p);
            }
        }

        public void Move()
        {
            Point tail = pList[0];
            pList.RemoveAt(0);
            Point head = GetNextPoint();
            pList.Add(head);

            tail.Clear();
            head.Draw();
        }

        public Point GetNextPoint()
        {
            Point head = pList[pList.Count - 1];
            Point nextPoint = new Point(head);
            nextPoint.Move(1, direction);
            return nextPoint;
        }
        public void Shrink()
        {
            if (pList.Count > 3)
            {
                Point tail = pList[0];
                pList.RemoveAt(0); 
                tail.Clear(); 
            }
        }
        public void Grow()
        {
            
            Point head = pList[pList.Count - 1];
            Point newHead = new Point(head);
            newHead.Move(1, direction);  
            pList.Add(newHead);     
        }


        public bool IsHitTail()
        {
            Point head = pList[pList.Count - 1];
            for (int i = 0; i < pList.Count - 2; i++)
            {
                if (head.IsHit(pList[i]))
                    return true;
            }
            return false;
        }

        public void HandleKey(ConsoleKey key)
        {
            if (key == ConsoleKey.LeftArrow && direction != Direction.RIGHT)
                direction = Direction.LEFT;
            else if (key == ConsoleKey.RightArrow && direction != Direction.LEFT)
                direction = Direction.RIGHT;
            else if (key == ConsoleKey.DownArrow && direction != Direction.UP)
                direction = Direction.DOWN;
            else if (key == ConsoleKey.UpArrow && direction != Direction.DOWN)
                direction = Direction.UP;
        }

        public bool Eat(Point food)
        {
            Point head = GetNextPoint();
            if (head.IsHit(food))  
            {
                food.sym = head.sym; 
                return true;
            }
            return false;
        }


    }
}