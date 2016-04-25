using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Minimax
{
    public class Minimax
    {

        public Tree tree { get; set; }

        public Minimax(int[] board) {
            tree = new Tree(board);
            tree.points(tree.root);
        }

        public int run(Node node) {
            if (node.state.player == 2 && node.nodes != null)
            {
                return tree.max(node);
            }
            else {
                if (node.state.player == 1 && node.nodes != null)
                {
                    return tree.min(node);
                }
                else { return 0; } //console readings pls
            }
        }
    }
}
