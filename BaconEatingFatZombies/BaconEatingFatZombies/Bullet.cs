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

        //nomes dos arquivos das bullets usando coordenadas cartesianas ( Norte, Sul, blablabla )
        public const string bullet_N = "bullet-N";
        public const string bullet_S = "bullet-S";
        public const string bullet_E = "bullet-E";
        public const string bullet_W = "bullet-W";
        public const string bullet_SW = "bullet-SW";
        public const string bullet_SE = "bullet-SE";
        public const string bullet_NW = "bullet-NW";
        public const string bullet_NE = "bullet-NE";
        
        public const int alturaTextura = 44;
        public const int larguraTextura = 50;

        public void AtualizaPosicao()
        {
            if (!colidiuZumbi)
                position = proximaPosicaoPelaEquacaoReta();        }
    }
}
