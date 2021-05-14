using JogoGalo.GameUtil;

namespace JogoGalo
{
    class Controller
    {

        private bool isRunning;
        private bool continuePlay;

        private PlayerType currentTurn;
        private PlayerType lastWinner;

        private View gameView;
        private GameBoard board;

        private BoardCoord lastMove;

        public Controller(GameBoard board)
        {
            this.board = board;
        }

        public void Run(View gameView)
        {
            this.gameView = gameView;
            gameView.WriteWelcomeMsg();

            continuePlay = true;
            while (continuePlay)
            {
                board.KickstartGame();
                PickFirstTurnPlayer();
                GameLoop();
            }
            gameView.WriteGoodbyeMsg();
        }

        public void GameLoop()
        {
            isRunning = true;
            while (isRunning)
            {
                gameView.ViewBoard();
                gameView.ViewAvailableMoves(currentTurn);
                if (board.CheckBoardFull())
                {
                    // The Game has ended in a Draw
                    isRunning = false;
                    gameView.WriteDrawGame();
                    continuePlay = gameView.QuitorRestart();
                }
                else if (board.CheckWinningCondition(lastMove, currentTurn))
                {
                    // Player has met the winning condition!
                    isRunning = false;
                    lastWinner = currentTurn;
                    gameView.ViewBoard();
                    gameView.WriteWinner(currentTurn);
                    continuePlay = gameView.QuitorRestart();
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
        }

        public void PickFirstTurnPlayer()
        {
            if (lastWinner != PlayerType.Null)
            {
                if (lastWinner == PlayerType.Player1)
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
                currentTurn = PlayerType.Player1;
            }
        }

        public void CommitMove(BoardCoord newMove)
        {
            board.CommitAction(newMove, currentTurn);
            lastMove = newMove;
        }

        public void SetAction(bool continuePlay)
        {
            this.continuePlay = continuePlay;
        }

    }
}
