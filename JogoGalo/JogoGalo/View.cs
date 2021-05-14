using JogoGalo.GameUtil;
using System;
using System.Collections.Generic;

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
                    WriteErrorMsgs(CodeError.InputError);
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
                            WriteErrorMsgs(CodeError.MoveImpossible);
                            ShowMoveList(coords);
                        }
                    }
                    else
                    {
                        WriteErrorMsgs(CodeError.InputError);
                    }
                }
            }
        }

        public void WriteWelcomeMsg()
        {
            Console.WriteLine("Welcome to the game of Tic-Tac-Toe!");
            Console.WriteLine("Press Enter to Continue!");
            Console.ReadLine();
        }

        public void ViewBoard()
        {
            //PlayerType[,] board = gameBoard.GetBoard();

            for(int line = 0; line < gameBoard.GetLineLength(); line++)
            {
                for(int col = 0; col < gameBoard.GetColLength(); col++)
                {
                    if (col == 1 || col == 2)
                    {
                        Console.Write("||");
                    }

                    if (gameBoard.GetStatusAt(line, col) == PlayerType.Player1)
                        Console.Write("  X  ");
                    else if (gameBoard.GetStatusAt(line, col) == PlayerType.Player2)
                        Console.Write("  O  ");
                    else
                        Console.Write("     ");

                }
                Console.Write("\n");
                if(line == 0 || line == 1)
                    for (int col = 0; col < gameBoard.GetColLength(); col++)
                    {
                        if (col == 1)
                            Console.Write(" ===== ");
                        else
                            Console.Write(" ==== ");
                    }
                Console.Write("\n");
                
            }
        }

        public void ViewAvailableMoves(PlayerType player)
        {
            List<BoardCoord> coords = gameBoard.CheckPossibleActions();
            Console.WriteLine("It is " + player.ToString() + "'s Turn");
            Console.WriteLine("Available Moves: ");
            ShowMoveList(coords);
            Console.WriteLine("\nType the Coordinates: ");
            ReadPlayerMoveInput(coords);
        }

        public void WriteDrawGame()
        {
            Console.WriteLine("No One Wins!! The Game Has Ended in a Draw!");
            Console.WriteLine("Press Q to Quit, or C to Continue Playing another Match!");
            
        }

        public void WriteWinner(PlayerType player)
        {
            Console.WriteLine("TIC-TAC-TOE! " + player + " WINS!");
            Console.WriteLine("Press Q to Quit, or C to Continue Playing another Match!");
            
        }

        public void WriteGoodbyeMsg()
        {
            Console.WriteLine("Thank You for Playing!");
        }

        public void ShowMoveList(List<BoardCoord> coords)
        {
            foreach (BoardCoord c in coords)
            {
                Console.Write(c.ToString() + " | ");
            }
        }

        public void WriteErrorMsgs(CodeError code)
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

        public bool QuitorRestart()
        {
            string read = (Console.ReadLine()).ToLower();
            char[] values = read.ToCharArray();

            bool isActionSet = false;
            bool toContinue = false;

            while (!isActionSet)
            {
                if (values.Length != 1)
                {
                    WriteErrorMsgs(CodeError.QuitInputError);
                }
                else
                {
                    if (values[0] == 'c')
                    {
                        toContinue = true;
                        isActionSet = true;
                    }
                    else if (values[0] == 'q')
                    {
                        toContinue = false;
                        isActionSet = true;
                    }
                    else
                    {
                        WriteErrorMsgs(CodeError.QuitInputError);
                    }
                }
            }
            return toContinue;
        }

    }
}
