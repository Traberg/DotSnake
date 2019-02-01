using System;

namespace DotSnake
{
    public class UserController
    {
        public event EventHandler<Direction> NewUserInput;

        public UserController()
        {
            
        }

        public void RegisterInput()
        {
            if (Console.KeyAvailable)
            {
                var keyPressed = Console.ReadKey().Key;
                switch (keyPressed)
                {
                    case ConsoleKey.UpArrow:
                        OnNewUserInput(Direction.Up);
                        break;
                    case ConsoleKey.DownArrow:
                        OnNewUserInput(Direction.Down);
                        break;
                    case ConsoleKey.RightArrow:
                        OnNewUserInput(Direction.Right);
                        break;
                    case ConsoleKey.LeftArrow:
                        OnNewUserInput(Direction.Left);
                        break;
                }
            }
                
        }

        protected virtual void OnNewUserInput(Direction e)
        {
            NewUserInput?.Invoke(this, e);
        }
    }
}