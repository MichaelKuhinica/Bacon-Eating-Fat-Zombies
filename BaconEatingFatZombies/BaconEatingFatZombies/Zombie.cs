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

        public void MoveDireita()
        {
            velocity = new Vector2(velocity.X, 0);
            position += velocity;

        }

        public void AtualizaPosicao()
        {

        }


    }
}
