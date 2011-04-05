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

        private const string bullet_looking_down = "bullet-UD";
        private const string bullet_looking_up = "bullet-DU";
        private const string bullet_looking_right = "bullet-LR";
        private const string bullet_looking_left = "bullet-RL";


        public const int alturaTextura = 44;
        public const int larguraTextura = 50;

        //Baseado no quadrante vai pegar uma imagem diferente
        public static string GetSprite(Vector2 InitialPosition, int maxMatrix)
        {
            // hack maravilhoso pra descobrir em que setor o vetor esta.
            // esse primeiro teste divide a matriz como se fosse uma matriz diagonal comum
            // a reta que divide e na verdade a funcao Y = X (funcao identidade)
            bool diagonalInferior = (InitialPosition.Y > InitialPosition.X);
            bool diagonalSuperior = (InitialPosition.X > InitialPosition.Y);

            // aqui a funcao que vai cortar a matriz esta espelhada!
            // caso fique em duvida plote a funcao Y = ( -X + 900 )  
            // Esse 900 existe porque e o limitante da matriz.... nossa matriz imaginaria e de 900x900
            bool diagonalInferiorInvertida = (InitialPosition.X > ((InitialPosition.Y * -1) + maxMatrix));
            bool diagonalSuperiorInvertida = (InitialPosition.X < ((InitialPosition.Y * -1) + maxMatrix));

            string result = bullet_looking_up; // so pra inicializar

            if (diagonalInferior && diagonalInferiorInvertida) // ele esta no quadrante inferior
                result = bullet_looking_up;
            if (diagonalInferior && diagonalSuperiorInvertida) // ele esta no quadrante esquerdo
                result = bullet_looking_right;
            if (diagonalSuperior && diagonalSuperiorInvertida) // ele esta no quadrante superior
                result = bullet_looking_down;
            if (diagonalSuperior && diagonalInferiorInvertida) // ele esta no quadrante direito
                result = bullet_looking_left;

            return result;
        }

        public void AtualizaPosicao()
        {
            float unit = 1.5f;

            if (position.X > direction.X)
                position = new Vector2(position.X - unit, position.Y);
            else
                position = new Vector2(position.X + unit, position.Y);

            if (position.Y > direction.Y)
                position = new Vector2(position.X, position.Y - unit);
            else
                position = new Vector2(position.X, position.Y + unit);

            if (position.Y == direction.Y && position.X == direction.Y)
                position = new Vector2(InitialPosition.X, InitialPosition.Y);

        }
    }
}
