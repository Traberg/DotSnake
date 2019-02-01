namespace DotSnake
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game((20,20));
            var ui = new UI();
            var userController = new UserController();

            userController.NewUserInput += game.DirectionChange;

            while (true)
            {
                game.Tick();
                ui.Render(game);
                System.Threading.Thread.Sleep(50);
                userController.RegisterInput();
            }
        }
    }
}
