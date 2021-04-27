using JogoGalo.GameUtil;
using System;
using System.Collections.Generic;
using System.Text;

namespace JogoGalo
{
    class Controller
    {
        private PlayerType pID { set; get; }
        private Model model;

        public Controller(PlayerType pID, Model model)
        {
            this.pID = pID;
            this.model = model;
        }

        public PlayerType getPlayer()
        {
            return this.pID;
        }

        public void PlayerStartTrigger()
        {
            Console.ReadLine();
            model.StartGame();
        }

        public void ReadPlayerMoveInput(List<BoardCoord> coords)
        {
            bool goodinput = false;
            BoardCoord new_move = null;

            while (!goodinput)
            {
                string val = Console.ReadLine();
                string[] parser = val.Split(',');

                if (parser.Length != 2)
                {
                    Console.WriteLine("Input Error. Please input as the following: LineIndex,ColIndex");
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
                            goodinput = true;
                        }
                        else
                        {
                            Console.WriteLine("Not a Possible Move!\nPlease Input a Possible Move from the List");
                            View.PrintMoveList(coords);
                        }

                    }
                    else
                    {
                        Console.WriteLine("Input Error!\nPlease input as the following: LineIndex,ColIndex");
                    }
                }
            }
            model.CommitMove(new_move, this);
        }

        public void QuitorRestart()
        {
            string read = (Console.ReadLine()).ToLower();
            char[] values = read.ToCharArray();

            if(values.Length != 1)
            {
                Console.WriteLine("Input Error. Press Q to Quit or C to Continue!");
                QuitorRestart();
            }
            else
            {
                if(values[0] == 'c')
                {
                    model.SetAction(true);
                }
                else if(values[0] == 'q')
                {
                    model.SetAction(false);
                }
                else
                {
                    Console.WriteLine("Input Error. Press Q to Quit or C to Continue!");
                    QuitorRestart();
                }
            }
        }
    }
}
