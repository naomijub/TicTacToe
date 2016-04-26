using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Minimax
{
    public class Minimax
    {

        public Minimax() {

        }

        public int[] bestBoard(int[] board, int player) {
            Board table = new Board(board);
            IList<Board> boards = table.getPossibilities(player);
            int aux = -1000, idx = 0; ;

            for (int i = 0; i < boards.Count; i++)
            {
                if (run(boards[i].board, 4, player) > aux)
                {
                    aux = run(boards[i].board, 4, player);
                    idx = i;
                }
            }
            return boards[idx].board;
        }

        public int run(int[] board, int depth, int player) {
            //int[] auxBoard = new int[board.Length];
            int value;
            //for (int i = 0; i < board.Length; i++)
            //{
            //    auxBoard[i] = board[i];
            //}
            Board table = new Board((int[])board.Clone());
            if (depth == 0 || table.getState(table.board) != 'p') {
                return score(table);
            }
            if (player == 2)
            {
                value = 1000;
                IList<Board> boards = table.getPossibilities(player);
                foreach (Board aux in boards)
                {
                    value = min(value, run(aux.board, depth - 1, 1));
                }
            }
            else {
                value = -1000;
                IList<Board> boards = table.getPossibilities(player);
                foreach (Board aux in boards)
                {
                    value = max(value, run(aux.board, depth - 1, 2));
                }
            }
            return value;
        }

        public int score(Board table) {
            char ch = table.getState(table.board);

            switch (ch) {
                case 'x': return 1;
                case 'o': return -1;
                case 'd': return 0;
                default: return 0;
            }
        }

        public int min(int a, int b) {
            if (a <= b)
            {
                return a;
            }
            else {
                return b;
            }
        }

        public int max(int a, int b)
        {
            if (a >= b)
            {
                return a;
            }
            else {
                return b;
            }
        }
    }
}
