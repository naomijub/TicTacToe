using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Minimax
{
    public class Tree
    {
        public Node root { get; set; }

        public Tree(int[] board) {
            BoardState state = new BoardState();
            state.board = board;
            state.player = 2;
            state.setTable();

            root = new Node(state, 0);
        }

        public int points(Node node) {
            if (node.nodes == null)
            {
                return node.state.points;
            }
            else {
                int sum = 0;
                for (int i = 0; i < node.nodes.Count; i++) {
                    node.points[i] = points(node.nodes[i]);
                }
                for (int i = 0; i < node.points.Length; i++) {
                    sum += node.points[i];
                }
                return sum;
            }
        }

        public int min(Node node) {
            int value = 10, idx = 0;
            for (int i = 0; i < node.points.Length; i++) {
                if (value > node.points[i]) {
                    value = node.points[i];
                    idx = i;
                }
            }
            return idx;
        }

        public int max(Node node)
        {
            int value = -10, idx = 0;
            for (int i = 0; i < node.points.Length; i++)
            {
                if (value < node.points[i])
                {
                    value = node.points[i];
                    idx = i;
                }
            }
            return idx;
        }

    }
}
