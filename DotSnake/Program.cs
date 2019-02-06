using DotSnake.Logic;

namespace DotSnake
{
    class Program
    {
        static void Main(string[] args)
        {
            var gameBoard = new GameBoard((20,20));
            var ui = new UI();
            var userController = new UserController();

            var game = new Game(gameBoard, ui, userController);
            game.Start();
        }
    }
}
