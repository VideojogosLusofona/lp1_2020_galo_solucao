using JogoGalo.GameUtil;
using System;

namespace JogoGalo
{
    class Program
    {
        private static void Main(string[] args)
        {
            GameBoard board = new GameBoard();
            Controller controller = new Controller(board);
            
            View gameView = new View(controller, board);
            controller.Run(gameView);
        }
    }
}
