using JogoGalo.GameUtil;
using System;
using System.Collections.Generic;
using System.Text;

namespace JogoGalo
{
    class Model
    {
        private GameBoard board;
        private bool isRunning;
        private bool continuePlay;

        private PlayerType currentTurn;
        private PlayerType lastWinner;

        private View gameView;

        private BoardCoord lastMove = null;
        
        public Model()
        {
            board = new GameBoard();
            gameView = new View(this);

            currentTurn = PlayerType.Null;
            lastWinner = PlayerType.Null;

            isRunning = false;
            KickStartGame();
        }

        private void KickStartGame()
        {
            gameView.PrintWelcomeMsg();
        }

        public void StartGame()
        {
            board.KickstartGame();
            PickFirstTurnPlayer();
            GameLoop();
        }

        public void QuitGame()
        {
            gameView.PrintGoodbyeMsg();
            System.Environment.Exit(0);
        }

        public void GameLoop()
        {
            isRunning = true;
            while (isRunning)
            {
                gameView.PrintBoard(board);
                gameView.PrintAvailableMoves(board.CheckPossibleActions(), currentTurn);
                if (board.CheckBoardFull())
                {
                    // The Game has ended in a Draw
                    isRunning = false;
                    gameView.PrintDraw();
                }
                else if (board.CheckWinningCondition(lastMove, currentTurn))
                {
                    // Player has met the winning condition!
                    isRunning = false;
                    lastWinner = currentTurn;
                    gameView.PrintBoard(board);
                    gameView.PrintWinner(currentTurn);
                }
                else
                {
                    // We continue playing
                    if (currentTurn == PlayerType.Player1)
                        currentTurn = PlayerType.Player2;
                    else
                        currentTurn = PlayerType.Player1;
                }
            }
            if (continuePlay)
            {
                StartGame();
            }
            else
            {
                QuitGame();
            }
        }

        public void PickFirstTurnPlayer()
        {
            if(lastWinner != PlayerType.Null)
            {
                if(lastWinner == PlayerType.Player1)
                {
                    currentTurn = PlayerType.Player2;
                }
                else
                {
                    currentTurn = PlayerType.Player1;
                }
            }
            else
            {
                // Random Selection
                Random rand = new Random();
                if (rand.NextDouble() < 0.5)
                    currentTurn = PlayerType.Player1;
                else
                    currentTurn = PlayerType.Player2;
            }
        }

        public void CommitMove(BoardCoord newMove, Controller player)
        {
            board.CommitAction(newMove, player.getPlayer());
            lastMove = newMove;
        }

        public void SetAction(bool continuePlay)
        {
            this.continuePlay = continuePlay;
        }
    }
}
