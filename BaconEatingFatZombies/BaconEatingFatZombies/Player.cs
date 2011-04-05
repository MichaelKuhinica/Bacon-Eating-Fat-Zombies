﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BaconEatingFatZombies
{
    class player : baseSprite
    {
        private float velocidade = 3f;
        public player(Texture2D newTexture, Vector2 newPosition, Vector2 newSize, Vector2 newScreensize)        {            texture = newTexture;            position = newPosition;
            InitialPosition = position;            size = newSize;
            screensize = newScreensize;        }

        public void MoveLeste()
        {
            MoveTo(new Vector2(position.X + velocidade, position.Y));
        }

        public void MoveOeste()
        {
            MoveTo(new Vector2(position.X - velocidade, position.Y));
        }

        public void MoveSul()
        {
            MoveTo(new Vector2(position.X, position.Y + velocidade));
        }

        public void MoveNorte()
        {
            MoveTo(new Vector2(position.X, position.Y - velocidade));
        }

        private void MoveTo(Vector2 newPosition) {
            if (newPosition.X > size.X/2 && newPosition.X < screensize.X - (size.X/2)
                && newPosition.Y > size.X/2 && newPosition.Y < screensize.Y - (size.X / 2))
            {
                position = newPosition;
            }
        }

        public void AtualizaPosicao(KeyboardState keyboard)
        {
            if (keyboard.IsKeyDown(Keys.Right))
                MoveLeste();
            if (keyboard.IsKeyDown(Keys.Left))
                MoveOeste();
            if (keyboard.IsKeyDown(Keys.Up))
                MoveNorte();
            if (keyboard.IsKeyDown(Keys.Down))
                MoveSul();
        }
    }
}
