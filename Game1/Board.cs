using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1
{
    public class Board
    {
        public int[] board { get; set; }
        //public enum State { xWins, oWins, draw, playing};

        public Board() {
            board = new int[9];
            reset();
        }

        public Board(int[] board) {
            this.board = (int[])board.Clone(); ;
        }

        public void reset() {
            for (int i = 0; i < board.Length; i++)
            {
                board[i] = 0;
            }
        }

        public void changeState(int xMouse, int yMouse, int player) {
            int x, y;
            if (yMouse > 449)
            {
                y = yMouse - 10;
            }
            else { y = yMouse; }
            if (xMouse > 449)
            {
                x = xMouse - 10;
            }
            else { x = xMouse; }

            int index = (int)(x / 150) + (3 * (int)(y / 150));
            //Console.WriteLine("index: " + index + " player: " + player);
            board[index] = player;
            
        }

        //adicioanr possibildiade de teste para o minmax
        public static char getState(int[] gameBoard) {
            char x = gameState(gameBoard, 1);
            char o = gameState(gameBoard, 2);

            if (x == 'w')
            {
                return 'x';
            }
            else if (o == 'w')
            {
                return 'o';
            }
            else if (x == 'd' && o == 'd')
            {
                return 'd';
            }
            else {
                return 'p';
            }
        }

        public static char gameState(int[] gameBoard, int player) {
            if ((gameBoard[0] == player && gameBoard[1] == player && gameBoard[2] == player) ||
                (gameBoard[3] == player && gameBoard[4] == player && gameBoard[5] == player) ||
                (gameBoard[6] == player && gameBoard[7] == player && gameBoard[8] == player))
            {
                return 'w';

            }
            else if ((gameBoard[0] == player && gameBoard[3] == player && gameBoard[6] == player) ||
                   (gameBoard[1] == player && gameBoard[4] == player && gameBoard[7] == player) ||
                   (gameBoard[2] == player && gameBoard[5] == player && gameBoard[8] == player))
            {
                return 'w';
            }
            else if ((gameBoard[0] == player && gameBoard[4] == player && gameBoard[8] == player) ||
                    (gameBoard[2] == player && gameBoard[4] == player && gameBoard[6] == player))
            {
                return 'w';
            }
            else if (gameBoard[0] == 0 || gameBoard[1] == 0 || gameBoard[2] == 0 || gameBoard[3] == 0 ||
                   gameBoard[4] == 0 || gameBoard[5] == 0 || gameBoard[6] == 0 || gameBoard[7] == 0 ||
                   gameBoard[8] == 0)
            {
                return 'p';
            }
            else {
                return 'd';
            }
        }

        public int emptyPos() {
            int empty = 0;
            for (int i = 0; i < board.Length; i++) {
                if (board[i] == 0) {
                    empty++;
                }
            }
            return empty;
        }

        //neo method
        public IList<Board> getPossibilities(int player) {
            IList<Board> boards = new List<Board>();
            for (int i = 0; i < board.Length; i++) {
                if (board[i] == 0) {
                    int[] auxBoard = new int[9];
                    //for (int j = 0; j < board.Length; j++) {
                    //    auxBoard[j] = board[j];
                    //}
                    auxBoard = (int[])board.Clone();
                    auxBoard[i] = player;
                    Board aux = new Board(auxBoard);
                    boards.Add(aux);
                }
             }
            return boards;
        }
    }
}
