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

        public bool colidiuZumbi { get; set; }

        public bullet(Texture2D newTexture, Vector2 newPosition, Vector2 newSize, Vector2 newScreensize, Vector2 newDirection)
        {
            texture = newTexture;
            position = newPosition;
            InitialPosition = newPosition;
            size = newSize;
            screensize = newScreensize;
            destination = newDirection;

            colidiuZumbi = false;

            determinaVelocidade();
            calculaEquacaoReta();        }

        private const string bullet_looking_down = "bullet-UD";
        private const string bullet_looking_up = "bullet-DU";
        private const string bullet_looking_right = "bullet-LR";
        private const string bullet_looking_left = "bullet-RL";


        public const int alturaTextura = 44;
        public const int larguraTextura = 50;

        public void AtualizaPosicao()
        {
            if (!colidiuZumbi)
                position = proximaPosicaoPelaEquacaoReta();        }
    }
}
