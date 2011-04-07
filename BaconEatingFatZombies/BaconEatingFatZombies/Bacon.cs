﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace BaconEatingFatZombies
{
    class bacon : baseSprite
    {
        public bool comido { get; set; }

        public bacon(Texture2D newTexture, Vector2 newPosition, Vector2 newSize, int screensizeWidth, int screensizeHeight)
        {
            texture = newTexture;
            position = newPosition;
            InitialPosition = position;
            size = newSize;
            screensize = new Vector2(screensizeWidth, screensizeHeight);
        }

        public void MorreDiabo()
        {
            position = new Vector2(-10f, -10f);
        }
    }
}