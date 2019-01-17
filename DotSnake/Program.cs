using System;

namespace DotSnake
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game((20,20));
            var ui = new UI();

            while (true)
            {
                game.Tick();
                ui.Render(game);
                System.Threading.Thread.Sleep(50);
                if (Console.KeyAvailable)
                    game.RegisterInput(Console.ReadKey().Key);
            }
        }
    }
}
