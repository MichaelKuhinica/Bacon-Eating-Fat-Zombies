using System;using System.Collections.Generic;using System.Linq;using System.Text;using Microsoft.Xna.Framework;using Microsoft.Xna.Framework.Graphics;using Microsoft.Xna.Framework.Input;namespace BaconEatingFatZombies{    class player : baseSprite    {        public player(Texture2D newTexture, Vector2 newPosition, Vector2 newSize, Vector2 newScreensize)        {            texture = newTexture;            position = newPosition;            InitialPosition = position;            size = newSize;            screensize = newScreensize;
            velocity = new Vector2(2f, 2f);
            direcaoCarteseana = "E";        }

        public string direcaoCarteseana;        public void AtualizaPosicao(KeyboardState keyboard)        {
            if (keyboard.IsKeyDown(Keys.Right))
            {
                MoveLeste();
                direcaoCarteseana = "E";
            }
            if (keyboard.IsKeyDown(Keys.Left))
            {
                MoveOeste();
                direcaoCarteseana = "W";            
            }
            if (keyboard.IsKeyDown(Keys.Up))
            {
                MoveNorte();
                direcaoCarteseana = "N";
            }
            if (keyboard.IsKeyDown(Keys.Down))
            {
                MoveSul();
                direcaoCarteseana = "S";
            }        }    }}