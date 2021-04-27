﻿using System;
using System.Collections.Generic;
using System.Text;

namespace JogoGalo.GameUtil
{
    class GameBoard
    {

        private TileType[,] board;


        // Constants
        private const int MAX_TILES = 3;

        public GameBoard()
        {
            // KickStart Board
            KickstartGame();
        }

        public void KickstartGame()
        {
            board = new TileType[MAX_TILES, MAX_TILES];
            for (int i = 0; i < board.GetLength(0); i++)
                for(int j = 0; j < board.GetLength(1); j++)
                    board[i,j] = TileType.None;
        }

        public bool CommitAction(BoardCoord coord, PlayerType player)
        {
            switch ((int)player)
            {
                case 1:
                    board[coord.GetLine(), coord.GetCol()] = TileType.Player1;
                    return CheckWinningCondition(coord, player);
                case 2:
                    board[coord.GetLine(), coord.GetCol()] = TileType.Player2;
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
                    if(board[i,j] == TileType.None)
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
                    if (board[i, j] == TileType.None)
                        emptySpace++;
                }
            }

            if (emptySpace < 1)
                return true;
            else
                return false;
        }

        public bool CheckLine(int LineAnchor, PlayerType player)
        {
            int tictac = 0;

            // Check Lines
            for (int col = 0; col < board.GetLength(1); col++)
            {
                if((int)board[LineAnchor, col] == (int)player)
                {
                    tictac++;
                }
            }
            if(tictac >= 3)
            {
                return true;
            }
            return false;
        }

        public bool CheckCol(int ColAnchor, PlayerType player)
        {
            int tictac = 0;

            // Check Columns
            for (int line = 0; line < board.GetLength(0); line++)
            {
                if ((int)board[line, ColAnchor] == (int)player)
                {
                    tictac++;
                }
            }
            if (tictac >= 3)
            {
                return true;
            }
            return false;
        }

        public bool CheckDiagonal(PlayerType player)
        {
            int tictac = 0;

            // Check Diagonal
            for(int i = 0; i < board.GetLength(0); i++)
            {
                if ((int)board[i, i] == (int)player)
                {
                    tictac++;
                }
            }
            if (tictac >= 3)
            {
                return true;
            }
            return false;
        }

        // Util 
        public TileType[,] GetBoard()
        {
            return board;
        }
    }
}