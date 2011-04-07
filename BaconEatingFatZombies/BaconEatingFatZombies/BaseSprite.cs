using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BaconEatingFatZombies
{
    class baseSprite
    {

        public Vector2 screensize { get; set; }
        public Vector2 velocity { get; set; }

        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle(
                    (int)position.X,
                    (int)position.Y,
                    texture.Width,
                    texture.Height);
            }
        }

        public Texture2D texture { get; set; }
        public Vector2 position { get; set; }
        public Vector2 destination { get; set; }
        public Vector2 size { get; set; }
        protected Vector2 InitialPosition { get; set; }
        
        //Componente da equacao da reta
        private float m { get; set; }

        //Outro componente da equacao da reta
        private float b { get; set; }

        public baseSprite()
        { 
            //apenas valores padrao
            velocity = new Vector2(1, 1);
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, GetRealPositionTopLeft(), Color.White);

            // lembrar de usar outra assinatura do metodo DRAW pra poder especificar que o Bacon fica por baixo do zumbi...
        }


        // O esquema e tratar `position` como a posicao do centro do objeto e na hora de desenhar eu diminuo esses
        // valores de position pra pegar a posicao real (necessaria para Draw) top left da imagem...
        public Vector2 GetRealPositionTopLeft()
        {
            Vector2 inc = new Vector2(position.X - (texture.Width / 2), position.Y - (texture.Height / 2));
            return inc;
        }

        protected void calculaEquacaoReta()
        {
            if (destination.X == 0 && InitialPosition.X == 0)
                throw new Exception("Essa merda ta de treta...");

            float numerador = (destination.Y - InitialPosition.Y);
            float denominador = (destination.X - InitialPosition.X);

            if (denominador != 0)
            {
                this.m = (numerador / denominador);
                //calculando B a partir da initialPosition
                this.b = ((m * InitialPosition.X) * -1) + InitialPosition.Y;
            }
            else
            {
                this.m = 1;  // elemento neutro da multiplicacao
                this.b = 0;  // elemento neutro da adicao
            }


            //Atualizando a posicao inicial do sprite!
            position = new Vector2(InitialPosition.X, InitialPosition.Y);
        }

        protected void determinaVelocidade()
        {
            float vConst = 0.4f;
            float incX = position.X < destination.X ? vConst : vConst*-1;
            float incY = position.Y < destination.Y ? vConst : vConst*-1;


            // Pode ser que eu tenha que DECREMENTAR a posicao pela velocidade... dependendo de onde se encontra o sprite
            velocity = new Vector2(velocity.X * incX, velocity.Y * incY);
        }

        public Vector2 proximaPosicaoPelaEquacaoReta()
        {
            Vector2 returnVector;

            // ja chegou ao destino? entao o resultado e o proprio destino
            if (position.X == destination.X && position.Y == destination.Y)
                returnVector = position;
            else
            {
                float newX = 0;

                if (this.m == 1 && this.b == 0)
                {
                    newX = position.X;  // nao altera
                }
                else
                    newX = position.X + velocity.X;  // vamos comecar incrementando com 1...

                
                float newY = ( m * newX ) + b;

                returnVector = new Vector2( newX, newY );
            }

            return returnVector;
        }

        protected void MoveTo(Vector2 newPosition) {            if (!ColidiuParede(newPosition))            {                position = newPosition;            }        }

        public void MoveLeste()
        {
            MoveTo(new Vector2(position.X + velocity.X, position.Y));
        }

        public void MoveOeste()
        {
            MoveTo(new Vector2(position.X - velocity.X, position.Y));
        }

        public void MoveSul()
        {
            MoveTo(new Vector2(position.X, position.Y + velocity.Y));
        }

        public void MoveNorte()
        {
            MoveTo(new Vector2(position.X, position.Y - velocity.Y));
        }

        protected virtual bool ColidiuParede(Vector2 newPosition) {
            return !(newPosition.X > size.X / 2 && newPosition.X < screensize.X - (size.X / 2)
                && newPosition.Y > size.X / 2 && newPosition.Y < screensize.Y - (size.X / 2));
        }
    }
}
