using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace BaconEatingFatZombies
{
    class zombie
    {
        public Texture2D texture { get; set; }
        public Vector2 position { get; set; }
        public Vector2 size { get; set; }
        public Vector2 screensize { get; set; }
        public Vector2 velocity { get; set; }

        //Esse vai guardar a posicao do bacon...
        public Vector2 positionBacon { get; set; }

        private bool possoMorrer { get; set; }

        public zombie(Texture2D newTexture, Vector2 newPosition, Vector2 newSize, int screensizeWidth, int screensizeHeight, Vector2 pPosBacon)
        {
            texture = newTexture;
            position = newPosition;
            size = newSize;
            screensize = new Vector2(screensizeWidth, screensizeHeight);
            positionBacon = pPosBacon;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public Vector2 GetCenter()
        { 
            // O sprite do zumbi e um 62x86 entao o centro fica em 31x43
            Vector2 inc = new Vector2(31,43);
            return position += inc;
        }

        public void MoveDireita()
        {
            velocity = new Vector2(velocity.X, 0);
            position += velocity;

        }

        public void AtualizaPosicao()
        {
            if (position.X > positionBacon.X)
                position = new Vector2(position.X - 1, position.Y);
            else
                position = new Vector2(position.X + 1, position.Y);

            if ( position.Y > positionBacon.Y )
                position = new Vector2(position.X, position.Y - 1);
            else
                position = new Vector2(position.X, position.Y + 1);

            if (position.Equals(positionBacon))
            {
                possoMorrer = true;
            }

        }


    }
}
