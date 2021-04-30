using JogoGalo.GameUtil;
using System;
using System.Collections.Generic;
using System.Text;

namespace JogoGalo
{
    class View
    {
        private Controller controller;
        private GameBoard gameBoard;

        public View(Controller controller, GameBoard gameBoard)
        {
            this.controller = controller;
            this.gameBoard = gameBoard;
        }

        public void ReadPlayerMoveInput(List<BoardCoord> coords)
        {
            bool goodinput = false;
            BoardCoord new_move;

            while (!goodinput)
            {
                string val = Console.ReadLine();
                string[] parser = val.Split(',');

                if (parser.Length != 2)
                {
                    PrintErrorMsgs(CodeError.InputError);
                }
                else
                {
                    if (int.TryParse(parser[0], out int line) && int.TryParse(parser[1], out int col))
                    {
                        // We have Numbers!
                        // Check if they are in the list of possible moves!

                        new_move = new BoardCoord(line, col);
                        if (coords.Contains(new_move))
                        {
                            controller.CommitMove(new_move);
                            goodinput = true;
                        }
                        else
                        {
                            PrintErrorMsgs(CodeError.MoveImpossible);
                            PrintMoveList(coords);
                        }
                    }
                    else
                    {
                        PrintErrorMsgs(CodeError.InputError);
                    }
                }
            }
        }

        public void PrintWelcomeMsg()
        {
            Console.WriteLine("Welcome to the game of Tic-Tac-Toe!");
            Console.WriteLine("Press Enter to Continue!");
            Console.ReadLine();
            controller.PlayerStartTrigger();
        }

        public void PrintBoard(GameBoard boardObj)
        {
            PlayerType[,] board = boardObj.GetBoard();

            for(int line = 0; line < board.GetLength(0); line++)
            {
                for(int col = 0; col < board.GetLength(1); col++)
                {
                    if (col == 1 || col == 2)
                    {
                        Console.Write("||");
                    }

                    if (board[line, col] == PlayerType.Player1)
                        Console.Write("  X  ");
                    else if (board[line, col] == PlayerType.Player2)
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
            ReadPlayerMoveInput(coords);
        }

        public void PrintDraw()
        {
            Console.WriteLine("No One Wins!! The Game Has Ended in a Draw!");
            Console.WriteLine("Press Q to Quit, or C to Continue Playing another Match!");
            QuitorRestart();
        }

        public void PrintWinner(PlayerType player)
        {
            Console.WriteLine("TIC-TAC-TOE! " + player + " WINS!");
            Console.WriteLine("Press Q to Quit, or C to Continue Playing another Match!");
            QuitorRestart();
        }

        public void PrintGoodbyeMsg()
        {
            Console.WriteLine("Thank You for Playing!");
        }

        public void PrintMoveList(List<BoardCoord> coords)
        {
            foreach (BoardCoord c in coords)
            {
                Console.Write(c.line + "," + c.col + " | ");
            }
        }

        public void PrintErrorMsgs(CodeError code)
        {
            switch (code)
            {
                case CodeError.InputError:
                    Console.WriteLine("Input Error. Please input as the following: LineIndex,ColIndex");
                    break;
                case CodeError.MoveImpossible:
                    Console.WriteLine("Not a Possible Move!\nPlease Input a Possible Move from the List");
                    break;
                case CodeError.QuitInputError:
                    Console.WriteLine("Input Error. Press Q to Quit or C to Continue!");
                    break;
            }
        }

        public void QuitorRestart()
        {
            string read = (Console.ReadLine()).ToLower();
            char[] values = read.ToCharArray();

            if (values.Length != 1)
            {
                PrintErrorMsgs(CodeError.QuitInputError);
                QuitorRestart();
            }
            else
            {
                if (values[0] == 'c')
                {
                    controller.SetAction(true);
                }
                else if (values[0] == 'q')
                {
                    controller.SetAction(false);
                }
                else
                {
                    PrintErrorMsgs(CodeError.QuitInputError);
                    QuitorRestart();
                }
            }
        }

    }
}
