using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Minimax
{
    public class Node
    {
        public BoardState state { get; set; }
        public IList<Node> nodes { get; set; }
        public int depth { get; set; }
        //public int point { get; set; }
        public int[] points { get; set; }

        public Node(BoardState state, int depth) {
            this.state = state;
            this.state.setTable();
            this.state.setPoints();
            nodes = new List<Node>();
            this.depth = depth;
            Console.WriteLine("Get Potato");
            setNodes();
        }

        public void setNodes() {
            BoardState[] states = state.getPossibilities(state.changePlayer(state.player));
            if (states != null)
            {
                points = new int[states.Length];
            }

            if (states != null)
            {
                for (int i = 0; i < states.Length; i++)
                {
                    Node aux = new Node(states[i], depth + 1);
                    nodes.Add(aux);
                }
            }
            else {
                nodes = null;
            }
        }

    }
}
