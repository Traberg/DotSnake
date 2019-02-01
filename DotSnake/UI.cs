using System;
using System.Drawing;

namespace DotSnake
{
    public class UI
    {
        public UI()
        {
            Console.CursorVisible = false;
        }

        public void Render(object sender, EventArgs e)
        {
            var game = (Game) sender;
            string toBeRendered = string.Empty;
            
            for (int i = game.BoardSize.VerticalHeight; i >= 1; i--)
            {
                for (int j = 1; j <= game.BoardSize.HorizontalLength; j++)
                {
                    var point = new Point(j, i);
                    toBeRendered +=  game.Snake.IsPointSnake(point) ? "S " : 
                        point == game.FoodPosition ? "F " 
                        : "  ";
                }
                toBeRendered += Environment.NewLine;
            }
            
            Console.SetCursorPosition(0, 0);
            Console.Write(toBeRendered);
        }
    }
}