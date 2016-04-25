using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Minimax
{
    public class BoardState
    {
        public int points { get; set; }
        public Board table { get; set; }
        public int[] board { get; set; }
        public int player { get; set; }

        public BoardState() {
            points = 0;
            table = new Board();
        }

        public void setTable() {
            table.board = board;
        }

        public void setPoints() {
            if (table.gameState(board, 1) == 'w')
            {
                points = 1;
            }
            else if (table.gameState(board, 2) == 'w')
            {
                points = -1;
            }
            else {
                points = 0;
            }
        }

        public BoardState[] getPossibilities( int player)
        {
            int emptyPos = getEmpty(board);
            int count = 0;
            BoardState[] states = new BoardState[emptyPos];

            if (emptyPos == 0)
            {
                for (int i = 0; i < board.Length; i++)
                {
                    if (board[i] == 0)
                    {
                        int[] auxBoard = board;
                        BoardState state = new BoardState();
                        auxBoard[i] = player;
                        state.board = auxBoard;
                        state.player = player;
                        state.setTable();
                        state.setPoints();
                        states.SetValue(state, count);
                        count++;
                    }
                }
                return states;
            }
            else {
                return null;
            }
        }

        public int getEmpty(int[] board)
        {
            Board table = new Board();
            table.board = board;
            return table.emptyPos();
        }

        public int changePlayer(int player)
        {
            if (player == 1)
            {
                return 2;
            }
            if (player == 2)
            {
                return 1;
            }
            return 0;
        }
    }
}
