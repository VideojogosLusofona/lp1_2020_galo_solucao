using System.Collections.Generic;

namespace JogoGalo.GameUtil
{
    class GameBoard
    {

        private PlayerType[,] board;


        // Constants
        private const int MAX_TILES = 3;
        private const int TICTAC_WIN = 3;

        public GameBoard()
        {
            // KickStart Board
            KickstartGame();
        }

        public void KickstartGame()
        {
            board = new PlayerType[MAX_TILES, MAX_TILES];
            for (int i = 0; i < board.GetLength(0); i++)
                for(int j = 0; j < board.GetLength(1); j++)
                    board[i,j] = PlayerType.Null;
        }

        public bool CommitAction(BoardCoord coord, PlayerType player)
        {
            switch (player)
            {
                case PlayerType.Player1:
                    board[coord.line, coord.col] = PlayerType.Player1;
                    return CheckWinningCondition(coord, player);
                case PlayerType.Player2:
                    board[coord.line, coord.col] = PlayerType.Player2;
                    return CheckWinningCondition(coord, player);
                default:
                    return false;
            }
        }

        public List<BoardCoord> CheckPossibleActions()
        {
            List<BoardCoord> boardPos = new List<BoardCoord>();
            for(int i = 0; i < board.GetLength(0); i++)
            {
                for(int j = 0; j < board.GetLength(1); j++)
                {
                    if(board[i,j] == PlayerType.Null)
                    {
                        boardPos.Add(new BoardCoord(i, j));
                    }
                }
            }
            return boardPos;
        }

        public bool CheckWinningCondition(BoardCoord lastPlay, PlayerType player)
        {
            // This function checks if player has won based on the last played action
            int[] cArr = lastPlay.ConvertCoord();
            int l = cArr[0];
            int c = cArr[1];

            if(c == l)
            {
                return CheckLine(l, player) || CheckCol(c, player) || CheckDiagonal(player);
            }
            else
            {
                return CheckLine(l, player) || CheckCol(c, player);
            }
        }

        public bool CheckBoardFull()
        {
            int emptySpace = 0;

            for(int i = 0; i < board.GetLength(0); i++)
            {
                for(int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == PlayerType.Null)
                        emptySpace++;
                }
            }

            if (emptySpace < 1)
                return true;
            else
                return false;
        }

        private bool CheckLine(int LineAnchor, PlayerType player)
        {
            int tictac = 0;

            // Check Lines
            for (int col = 0; col < board.GetLength(1); col++)
            {
                if(board[LineAnchor, col] == player)
                {
                    tictac++;
                }
            }
            if(tictac >= TICTAC_WIN)
            {
                return true;
            }
            return false;
        }

        private bool CheckCol(int ColAnchor, PlayerType player)
        {
            int tictac = 0;

            // Check Columns
            for (int line = 0; line < board.GetLength(0); line++)
            {
                if (board[line, ColAnchor] == player)
                {
                    tictac++;
                }
            }
            if (tictac >= TICTAC_WIN)
            {
                return true;
            }
            return false;
        }

        private bool CheckDiagonal(PlayerType player)
        {
            int tictac = 0;

            // Check Diagonal
            for(int i = 0; i < board.GetLength(0); i++)
            {
                if (board[i, i] == player)
                {
                    tictac++;
                }
            }
            if (tictac >= TICTAC_WIN)
            {
                return true;
            }
            return false;
        }

        // Util 
        public PlayerType GetStatusAt(int row, int col)
        {
            return board[row, col];
        }

        public int GetLineLength()
        {
            return board.GetLength(0);
        }

        public int GetColLength()
        {
            return board.GetLength(1);
        }
    }
}
