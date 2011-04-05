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
        private const string bullet_N = "bullet-N";
        private const string bullet_S = "bullet-S";
        private const string bullet_E = "bullet-E";
        private const string bullet_W = "bullet-W";
        private const string bullet_SW = "bullet-SW";
        private const string bullet_SE = "bullet-SE";
        private const string bullet_NW = "bullet-NW";
        private const string bullet_NE = "bullet-NE";
        
        public const int alturaTextura = 44;
        public const int larguraTextura = 50;

        public void AtualizaPosicao()
        {
            if (!colidiuZumbi)
                position = proximaPosicaoPelaEquacaoReta();        }
    }
}
