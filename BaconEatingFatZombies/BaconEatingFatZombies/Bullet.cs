using System;using System.Collections.Generic;using System.Linq;using System.Text;using Microsoft.Xna.Framework;using Microsoft.Xna.Framework.Graphics;namespace BaconEatingFatZombies{    class bullet : baseSprite    {        public bool colidiuZumbi { get; set; }
        public string direcaoCartesiana { get; set; }        public bullet(Texture2D newTexture, Vector2 newPosition, Vector2 newScreensize, string newDirecao)        {            texture = newTexture;            position = newPosition;            InitialPosition = newPosition;            size = new Vector2(62f, 86f);            screensize = newScreensize;            //destination = newDirection;
            direcaoCartesiana = newDirecao;
            if ("N".Equals(direcaoCartesiana) || "S".Equals(direcaoCartesiana))
            {
                position = new Vector2(newPosition.X, newPosition.Y + size.Y/2);
            }
            else
            {
                position = new Vector2(newPosition.X + size.X/2, newPosition.Y);
            }            colidiuZumbi = false;
            velocity = new Vector2(5f, 5f);            //determinaVelocidade();            //calculaEquacaoReta();        }

        protected override bool ColidiuParede(Vector2 newPosition)
        {
            bool colidiu = !(newPosition.X < screensize.X + (size.X / 2)
                && newPosition.Y-size.Y < screensize.Y + (size.X / 2));
            if (colidiu)
            {
                //O ideal seria sumir com o obj mas o LINDO do XNA nao deixa. 
                //Enfia o garbage collector vc sabe onde...
            }
            return colidiu;
        }        //nomes dos arquivos das bullets usando coordenadas cartesianas ( Norte, Sul, blablabla )        public const string bullet_N = "bullet-N";        public const string bullet_S = "bullet-S";        public const string bullet_E = "bullet-E";        public const string bullet_W = "bullet-W";        public const string bullet_SW = "bullet-SW";        public const string bullet_SE = "bullet-SE";        public const string bullet_NW = "bullet-NW";        public const string bullet_NE = "bullet-NE";                public const int alturaTextura = 44;        public const int larguraTextura = 50;        public void AtualizaPosicao()        {
            if (!colidiuZumbi)
            {
                if ("N".Equals(direcaoCartesiana))
                {
                    MoveNorte();
                }
                else if ("S".Equals(direcaoCartesiana))
                {
                    MoveSul();
                }
                else if ("E".Equals(direcaoCartesiana))
                {
                    MoveLeste();
                }
                else
                {
                    MoveOeste();
                }
                //Vector2 newPosition = proximaPosicaoPelaEquacaoReta();
                //if (!ColidiuParede(newPosition))
                //    position = newPosition;
            }
            else
            {
                
            }                        }

       
        //Baseado na posicao em que o player esta olhando
        //public static string GetSprite(Vector2 InitialPosition, Vector2 destination, int maxMatrix)
        //{

        //}    }}