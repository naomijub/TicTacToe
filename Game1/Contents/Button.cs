﻿using System;
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
    public class Button
    {
        private int x, y;
        private float xStrF, yStrF;
        private string name;
        public Color color { get; set; }
        private SpriteFont font;
        private Texture2D button;

        public Button(int x, int y, string name, Texture2D button, Color color, SpriteFont font) {
            this.x = x;
            this.y = y;
            this.name = name;
            this.button = button;
            this.color = color;
            this.font = font;
            this.xStrF = ((95 - font.MeasureString(name).X) / 2) + x;
            this.yStrF = ((40 - font.MeasureString(name).Y) / 2) + y;
        }

        public void update() { }

        public void draw(SpriteBatch sb, bool selected) {
            sb.Draw(
                button,
                new Vector2(x, y),
                colorize(selected)
                );
            sb.DrawString(font, name, new Vector2(xStrF, yStrF), color, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
        }

        public Color colorize(bool selected)
        {
            if (selected)
            {
                return Color.LightGreen;
            }
            else {
                return Color.White;
            }
        }
    }
}
