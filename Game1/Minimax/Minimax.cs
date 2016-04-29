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
            int aux = -1000, idx = 0;

            for (int i = 0; i < boards.Count; i++)
            {
                int auxTemp = run(boards[i].board, 8, player == 1 ? 2 : 1);
                if (auxTemp > aux)
                {
                    aux = auxTemp;
                    idx = i;
                }
            }
            if (boards.Count > 0)
            {
                return boards[idx].board;
            }
            else {
                return board;
            }
        }

        public static int run(int[] board, int depth, int player) {
            int value;
            Board table = new Board((int[])board.Clone());
            if (depth == 0 || Board.getState(table.board) != 'p') {
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

        public static int score(Board table) {
            char ch = Board.getState(table.board);

            switch (ch) {
                case 'x': return 1;
                case 'o': return -1;
                case 'd': return 0;
                default: return 0;
            }
        }

        public static int min(int a, int b) {
            if (a <= b)
            {
                return a;
            }
            else {
                return b;
            }
        }

        public static int max(int a, int b)
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
