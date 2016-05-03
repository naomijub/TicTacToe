using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace Game1
{
    public class Lines
    {
        public Texture2D lines { get; set; }

        public Lines(Texture2D lines) {
            this.lines = lines;
        }

        public void draw(SpriteBatch sb) {
            sb.Draw(
                    lines,
                    new Rectangle(175, 225, 460, 5),
                    null,
                    Color.White,
                    0f,
                    new Vector2(0, 0),
                    SpriteEffects.None,
                    0f
                    );
            sb.Draw(
                lines,
                new Rectangle(175, 380, 460, 5),
                null,
                Color.White,
                0f,
                new Vector2(0, 0),
                SpriteEffects.None,
                0f
                );
            sb.Draw(
                lines,
                new Rectangle(320, 75, 5, 450),
                null,
                Color.White,
                0f,
                new Vector2(0, 0),
                SpriteEffects.None,
                0f
                );
            sb.Draw(
                lines,
                new Rectangle(475, 75, 5, 450),
                null,
                Color.White,
                0f,
                new Vector2(0, 0),
                SpriteEffects.None,
                0f
                );
        }

        public void playerButtons(SpriteBatch sb, Texture2D compButton, Texture2D persButton) {
            sb.Draw(
                   compButton,
                   new Vector2(240, 570),
                   Color.White
                   );
            sb.Draw(
                persButton,
                new Vector2(480, 570),
                Color.White
                );
        }
    }
}
