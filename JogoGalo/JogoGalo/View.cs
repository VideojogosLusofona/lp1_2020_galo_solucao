using JogoGalo.GameUtil;
using System;
using System.Collections.Generic;
using System.Text;

namespace JogoGalo
{
    class View
    {
        private Controller player1;
        private Controller player2;

        public View(Model model)
        {
            player1 = new Controller(PlayerType.Player1, model);
            player2 = new Controller(PlayerType.Player2, model);
        }

        public void PrintWelcomeMsg()
        {
            Console.WriteLine("Welcome to the game of Tic-Tac-Toe!");
            Console.WriteLine("Press Enter to Continue!");

            player1.PlayerStartTrigger();
        }

        public void PrintBoard(GameBoard boardObj)
        {
            TileType[,] board = boardObj.GetBoard();

            for(int line = 0; line < board.GetLength(0); line++)
            {
                for(int col = 0; col < board.GetLength(1); col++)
                {
                    if (col == 1 || col == 2)
                    {
                        Console.Write("||");
                    }

                    if (board[line, col] == TileType.Player1)
                        Console.Write("  X  ");
                    else if (board[line, col] == TileType.Player2)
                        Console.Write("  O  ");
                    else
                        Console.Write("     ");

                }
                Console.Write("\n");
                if(line == 0 || line == 1)
                    for (int col = 0; col < board.GetLength(1); col++)
                    {
                        if (col == 1)
                            Console.Write(" ===== ");
                        else
                            Console.Write(" ==== ");
                    }
                Console.Write("\n");
                
            }
        }

        public void PrintAvailableMoves(List<BoardCoord> coords, PlayerType player)
        {
            Console.WriteLine("It is " + player.ToString() + "'s Turn");
            Console.WriteLine("Available Moves: ");
            PrintMoveList(coords);
            Console.WriteLine("\nType the Coordinates: ");
            PlayerTurn(player).ReadPlayerMoveInput(coords);
        }

        public void PrintDraw()
        {
            Console.WriteLine("No One Wins!! The Game Has Ended in a Draw!");
            Console.WriteLine("Press Q to Quit, or C to Continue Playing another Match!");
            player1.QuitorRestart();
        }

        public void PrintWinner(PlayerType player)
        {
            Console.WriteLine("TIC-TAC-TOE! " + player + " WINS!");
            Console.WriteLine("Press Q to Quit, or C to Continue Playing another Match!");
            player1.QuitorRestart();
        }

        public void PrintGoodbyeMsg()
        {
            Console.WriteLine("Thank You for Playing!");
        }

        public static void PrintMoveList(List<BoardCoord> coords)
        {
            foreach (BoardCoord c in coords)
            {
                Console.Write(c.GetLine() + "," + c.GetCol() + " | ");
            }
        }

        // Util
        private Controller PlayerTurn(PlayerType player)
        {
            if (player == PlayerType.Player1)
                return this.player1;
            else
                return this.player2;
        }


    }
}
