﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace BaconEatingFatZombies
{
    class zombie : baseSprite
    {

        private const string zombie_looking_down = "Z_DOWN_PNG";
        private const string zombie_looking_up = "Z_UP_PNG";
        private const string zombie_looking_right = "Z_RIGHT_PNG";
        private const string zombie_looking_left = "Z_LEFT_PNG";

        public const int alturaTextura = 86;
        public const int larguraTextura = 62;
        public bool colidiuBacon { get; set; }
        public bool morto { get; set; }
        public bool jaComeu { get; set; }

        public zombie(Texture2D newTexture, Vector2 newPosition, Vector2 newSize, int screensizeWidth, int screensizeHeight, Vector2 pPosBacon)
        {
            texture = newTexture;
            position = newPosition;
            InitialPosition = position;
            size = newSize;
            screensize = new Vector2(screensizeWidth, screensizeHeight);
            destination = pPosBacon;
            jaComeu = false;
            morto = false;
            determinaVelocidade();
            calculaEquacaoReta();
        }


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

            string result = zombie_looking_up; // so pra inicializar

            if (diagonalInferior && diagonalInferiorInvertida) // ele esta no quadrante inferior
                result = zombie_looking_up;
            if (diagonalInferior && diagonalSuperiorInvertida) // ele esta no quadrante esquerdo
                result = zombie_looking_right;
            if (diagonalSuperior && diagonalSuperiorInvertida) // ele esta no quadrante superior
                result = zombie_looking_down;
            if (diagonalSuperior && diagonalInferiorInvertida) // ele esta no quadrante direito
                result = zombie_looking_left;

            return result;
        }

        public void MorreDiabo(Texture2D newTexture, Vector2 newPosition)
        {
            
            texture = newTexture;
            position = newPosition;
            InitialPosition = newPosition;


            calculaEquacaoReta();
            determinaVelocidade();
        }


        public void AtualizaPosicao()
        {
            if (!colidiuBacon)
                position = proximaPosicaoPelaEquacaoReta();
            else
            {
                jaComeu = true;
                //MorreDiabo(texture, InitialPosition);
            }


        }


    }
}