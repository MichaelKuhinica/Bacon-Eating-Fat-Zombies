using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BaconEatingFatZombies
{
    class bullet : baseSprite
    {
        private Vector2 direction;
        public bullet(Texture2D newTexture, Vector2 newPosition, Vector2 newSize, Vector2 newScreensize, Vector2 newDirection)
        {
            texture = newTexture;
            position = newPosition;
            InitialPosition = newPosition;
            size = newSize;
            screensize = newScreensize;
            direction = newDirection;
        }

        public const int alturaTextura = 55;
        public const int larguraTextura = 55;

        public void AtualizaPosicao()
        {
            Vector2 positionBacon = new Vector2(direction.X, direction.Y);
           
            float unit = 1.5f;
                   

            if (position.X > positionBacon.X)
                position = new Vector2(position.X - unit, position.Y);
            else
                position = new Vector2(position.X + unit, position.Y);

            if (position.Y > positionBacon.Y)
                position = new Vector2(position.X, position.Y - unit);
            else
                position = new Vector2(position.X, position.Y + unit);
        
        }
    }
}
