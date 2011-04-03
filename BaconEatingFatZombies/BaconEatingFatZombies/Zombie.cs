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
            spriteBatch.Draw(texture, GetCenter(), Color.White);
        }

        public Vector2 GetCenter()
        {
            // O sprite do zumbi e um 62x86 entao o centro fica em 31x43
            Vector2 inc = new Vector2(position.X - (texture.Width / 2), position.Y - (texture.Height / 2));
            return inc;
        }

        public void MoveDireita()
        {
            velocity = new Vector2(velocity.X, 0);
            position += velocity;

        }

        public Vector2 getRandomInitialLocation()
        {
            //Random random = new Random();
            //int y = random.Next(-100, 600);
            //int x = 0;

            //if (y > 0 && y < 501)
            //{
            //    int lado = random.Next(2);  // exclusive upperBound
            //    if (lado == 1)
            //    {

            //    }

            //}
            //else
            //{
            //}

            return new Vector2(0f, 0f);

        }


        public void AtualizaPosicao()
        {
            float unit = 0.25f;

            if (position.X > positionBacon.X)
                position = new Vector2(position.X - unit, position.Y);
            else
                position = new Vector2(position.X + unit, position.Y);

            if (position.Y > positionBacon.Y)
                position = new Vector2(position.X, position.Y - unit);
            else
                position = new Vector2(position.X, position.Y + unit);

            if (position.Equals(positionBacon))
            {
                possoMorrer = true;
            }

        }


    }
}
