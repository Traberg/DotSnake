using System;
using System.Drawing;

namespace DotSnake.Logic
{
    public class GameBoard
    {
        public event EventHandler GameStateChanged;

        public (int HorizontalLength, int VerticalHeight) BoardSize { get; }

        public Snake Snake { get; set; }
        public Point FoodPosition { get; set; }


        public GameBoard((int HorizontalLength, int VerticalHeight) boardSize)
        {
            BoardSize = boardSize;
            Reset();
        }

        internal void Reset()
        {
            Snake = new Snake();
            SpawnFood();
        }

        internal Result Tick()
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

            bool isFed = newHeadPoint.X == FoodPosition.X && newHeadPoint.Y == FoodPosition.Y;
            bool died = Snake.IsCollided();

            if (isFed)
            {
                SpawnFood();
            }            
            Snake.UpdatePosition(newHeadPoint, isFed);

            GameStateChanged?.Invoke(this, EventArgs.Empty);


            return new Result(didEat: isFed, died: died);
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