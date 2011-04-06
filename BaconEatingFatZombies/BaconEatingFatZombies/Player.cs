using System;using System.Collections.Generic;using System.Linq;using System.Text;using Microsoft.Xna.Framework;using Microsoft.Xna.Framework.Graphics;using Microsoft.Xna.Framework.Input;namespace BaconEatingFatZombies{    class player : baseSprite    {
        private Dictionary<string, Texture2D> directionTextures;

        public player(Dictionary<string, Texture2D> newDirectionTextures, Vector2 newPosition, Vector2 newSize, Vector2 newScreensize)        {
            directionTextures = newDirectionTextures;                        position = newPosition;            InitialPosition = position;            size = newSize;            screensize = newScreensize;
            velocity = new Vector2(2f, 2f);
            direcaoCarteseana = "E";
            texture = GetSprite();                    }

        public Texture2D GetSprite()
        {
            return directionTextures[direcaoCarteseana];
        }

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

                if (keyboard.IsKeyDown(Keys.Right))
                {
                    direcaoCarteseana = "NE";
                }
                else if (keyboard.IsKeyDown(Keys.Left))
                {
                    direcaoCarteseana = "NW";
                }
            }
            if (keyboard.IsKeyDown(Keys.Down))
            {
                direcaoCarteseana = "S";
                if (keyboard.IsKeyDown(Keys.Right))
                {
                    direcaoCarteseana = "SE";
                }
                else if (keyboard.IsKeyDown(Keys.Left))
                {
                    direcaoCarteseana = "SW";
                }
                MoveSul();
            }
            texture = GetSprite();        }    }}