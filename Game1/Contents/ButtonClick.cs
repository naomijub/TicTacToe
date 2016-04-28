using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace Game1.Contents
{
    public class ButtonClick
    {
        private int xMin, xMax, yMin, yMax;

        public ButtonClick(int xMin, int xMax, int yMin, int yMax) {
            this.xMax = xMax;
            this.xMin = xMin;
            this.yMax = yMax;
            this.yMin = yMin;
        }

        public bool state(int x, int y, MouseState mouse, MouseState prevState) {
            if ((x >= xMin && x <= xMax) && (y >= yMin && y <= yMax) && (mouse.LeftButton == ButtonState.Pressed && prevState.LeftButton == ButtonState.Released))
            {
                return true;
            }
            else {
                return false;
            }
        }
    }
}
