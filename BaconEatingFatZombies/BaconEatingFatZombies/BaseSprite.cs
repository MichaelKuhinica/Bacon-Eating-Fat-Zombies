using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BaconEatingFatZombies
{
    class baseSprite
    {

        public Vector2 screensize { get; set; }
        public Vector2 velocity { get; set; }

        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle(
                    (int)position.X,
                    (int)position.Y,
                    texture.Width,
                    texture.Height);
            }
        }

        public Texture2D texture { get; set; }
        public Vector2 position { get; set; }
        public Vector2 size { get; set; }
        protected Vector2 InitialPosition { get; set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, GetCenter(), Color.White);
        }

        public Vector2 GetCenter()
        {
            // O sprite do zumbi e um 62x86 entao o centro fica em 31x43
            Vector2 inc = new Vector2(position.X - (texture.Width / 2), position.Y - (texture.Height / 2));
            return inc;
        }
    }
}
