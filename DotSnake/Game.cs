using System;
using System.Drawing;

namespace DotSnake
{
    public class Game
    {
        public event EventHandler GameStateChanged;

        public (int HorizontalLength, int VerticalHeight) BoardSize { get; }

        public Snake Snake { get; set; }
        public Point FoodPosition { get; set; }


        public Game((int HorizontalLength, int VerticalHeight) boardSize)
        {
            Snake = new Snake();
            BoardSize = boardSize;
            SpawnFood();
        }

        public void Tick()
        {
            var newHeadPoint = Snake.GetHeadPoint();
            switch (Snake.SnakeDirection)
            { 
                case Direction.Right:
                    newHeadPoint.X = newHeadPoint.X != BoardSize.HorizontalLength ? newHeadPoint.X + 1 : 1;
                    break;
                case Direction.Left:
                    newHeadPoint.X = newHeadPoint.X != 1 ? newHeadPoint.X - 1 : BoardSize.HorizontalLength;
                    break;
                case Direction.Down:
                    newHeadPoint.Y = newHeadPoint.Y != 1 ? newHeadPoint.Y - 1 : BoardSize.VerticalHeight;
                    break;
                case Direction.Up:
                    newHeadPoint.Y = newHeadPoint.Y != BoardSize.VerticalHeight ? newHeadPoint.Y + 1 : 1;
                    break;
            }

            if (newHeadPoint.X == FoodPosition.X && newHeadPoint.Y == FoodPosition.Y)
            {
                SpawnFood();
                Snake.UpdatePosition(newHeadPoint, true);
            }
            else
            {
                Snake.UpdatePosition(newHeadPoint);
            }

            if(Snake.IsCollided())
                throw new Exception("oi oi oi you fucked up m80");

            GameStateChanged?.Invoke(this, EventArgs.Empty);
        }


        public void DirectionChange(object sender, Direction direction)
        {
            Snake.SnakeDirection = direction;
        }

        public void SpawnFood()
        {
            var rndColumn = new Random().Next(1, BoardSize.Item1);
            var rndRow = new Random().Next(1, BoardSize.Item2);
            FoodPosition = new Point(rndColumn, rndRow);
        }
    }
}