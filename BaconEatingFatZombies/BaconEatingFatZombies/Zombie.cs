﻿using System;using System.Collections.Generic;using System.Linq;using System.Text;using Microsoft.Xna.Framework;using Microsoft.Xna.Framework.Graphics;namespace BaconEatingFatZombies{    class zombie : baseSprite    {        private const string zombie_looking_down = "Z_DOWN_PNG";        private const string zombie_looking_up = "Z_UP_PNG";        private const string zombie_looking_right = "Z_RIGHT_PNG";        private const string zombie_looking_left = "Z_LEFT_PNG";        public const int alturaTextura = 86;        public const int larguraTextura = 62;        //Esse vai guardar a posicao do bacon...        public Vector2 positionBacon { get; set; }        private bool possoMorrer { get; set; }        public zombie(Texture2D newTexture, Vector2 newPosition, Vector2 newSize, int screensizeWidth, int screensizeHeight, Vector2 pPosBacon)        {            texture = newTexture;            position = newPosition;            size = newSize;            screensize = new Vector2(screensizeWidth, screensizeHeight);            positionBacon = pPosBacon;        }        public void MoveDireita()        {            velocity = new Vector2(velocity.X, 0);            position += velocity;        }        //Baseado no quadrante vai pegar uma imagem diferente        public static string GetSprite( Vector2 InitialPosition, int maxMatrix )        {            // hack maravilhoso pra descobrir em que setor o vetor esta.            // esse primeiro teste divide a matriz como se fosse uma matriz diagonal comum            // a reta que divide e na verdade a funcao Y = X (funcao identidade)            bool diagonalInferior = (InitialPosition.Y > InitialPosition.X);            bool diagonalSuperior = (InitialPosition.X > InitialPosition.Y);            // aqui a funcao que vai cortar a matriz esta espelhada!            // caso fique em duvida plote a funcao Y = ( -X + 900 )              // Esse 900 existe porque e o limitante da matriz.... nossa matriz imaginaria e de 900x900            bool diagonalInferiorInvertida = (InitialPosition.X > ( ( InitialPosition.Y * -1 ) + maxMatrix ));            bool diagonalSuperiorInvertida = (InitialPosition.X < ( ( InitialPosition.Y * -1 ) + maxMatrix ));            string result = zombie_looking_up; // so pra inicializar            if (diagonalInferior && diagonalInferiorInvertida) // ele esta no quadrante inferior                result = zombie_looking_up;            if (diagonalInferior && diagonalSuperiorInvertida) // ele esta no quadrante esquerdo                result = zombie_looking_right;            if (diagonalSuperior && diagonalSuperiorInvertida) // ele esta no quadrante superior                result = zombie_looking_down;            if (diagonalSuperior && diagonalInferiorInvertida) // ele esta no quadrante direito                result = zombie_looking_left;            return result;        }        public void MorreDiabo(Texture2D newTexture, Vector2 newPosition) {
            texture = newTexture;            position = newPosition;        }         public void AtualizaPosicao()        {            float unit = 0.55f;            if (position.X > positionBacon.X)                position = new Vector2(position.X - unit, position.Y);            else                position = new Vector2(position.X + unit, position.Y);            if (position.Y > positionBacon.Y)                position = new Vector2(position.X, position.Y - unit);            else                position = new Vector2(position.X, position.Y + unit);            if (position.Equals(positionBacon))            {                possoMorrer = true;            }        }    }}