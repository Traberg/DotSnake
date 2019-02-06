using System;
using System.Drawing;

namespace DotSnake.Logic
{
    public class UI
    {
        public UI()
        {
            Console.CursorVisible = false;
        }

        public void Render(object sender, EventArgs e)
        {
            Console.SetCursorPosition(0, 0);
            var game = (GameBoard) sender;
            string toBeRendered = string.Empty;

            for (int i = game.BoardSize.VerticalHeight; i >= 1; i--)
            {
                for (int j = 1; j <= game.BoardSize.HorizontalLength; j++)
                {
                    var point = new Point(j, i);
                    toBeRendered += game.Snake.IsPointSnake(point) ? "S "
                        : point == game.FoodPosition ? "F "
                        : "  ";
                }

                toBeRendered += Environment.NewLine;
            }

            Console.SetCursorPosition(0, 0);
            Console.Write(toBeRendered);
        }

        public void ClearScreen()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
        }

        public void Died()
        {
            Console.WriteLine("You died ): ..");
        }

        public void EnterMenu()
        {
            Console.WriteLine("Press any key to start playing!");
        }
    }
}