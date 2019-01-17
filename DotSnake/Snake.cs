using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace DotSnake
{
    public class Snake
    {
        private readonly LinkedList<Point> _snakePoints;
        private bool _isFed = false;
        public Direction SnakeDirection { get; set; } = Direction.Right;

        public Snake()
        {
            _snakePoints = new LinkedList<Point>();
            _snakePoints.AddFirst(new Point(2, 2));
            _snakePoints.AddLast(new Point(1, 1)); 
        }

        public Point GetHeadPoint()
        {
            return _snakePoints.First.Value;
        }

        public void UpdatePosition(Point newHeadPoint, bool isFed = false)
        {
            _snakePoints.AddFirst(newHeadPoint);

            if (!_isFed)
            {
                _snakePoints.RemoveLast();
            }

            _isFed = isFed;
        }

        public bool IsPointSnake(Point toCheck)
        {
            return _snakePoints.Any(t => t == toCheck);
        }

        public bool IsCollided()
        {
            return _snakePoints.Any(t => _snakePoints.Count(k => k == t) > 1);
        }

    }
}