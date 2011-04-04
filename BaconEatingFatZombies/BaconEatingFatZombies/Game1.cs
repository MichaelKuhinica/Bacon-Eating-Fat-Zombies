using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace BaconEatingFatZombies
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Texture2D background;
        private List<zombie> listaZumbis = new List<zombie>();
        private List<zombie> listaPixels = new List<zombie>();
        private KeyboardState keyboard;
        private Random random = new Random();
        private Vector2 centro;

        public int matrixLow { get; set; }
        public int matrixHigh { get; set; }


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 500;
            graphics.PreferredBackBufferWidth = 500;

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();


        }

        protected override void LoadContent()
        {
            centro = new Vector2((Window.ClientBounds.Width / 2), (Window.ClientBounds.Height / 2));

            matrixLow = -200;  // esse vai ser o menor valor da matrix
            matrixHigh = 700;  // maior valor


            // significa que nossa matrix imaginaria vai ser de 900x900


            spriteBatch = new SpriteBatch(GraphicsDevice);
            //            background = Content.Load<Texture2D>("foto2");

            Vector2 initialPosition = new Vector2(0,0);



            for (int i = 0; i < 500; i += 2)
            {
                listaPixels.Add(new zombie(Content.Load<Texture2D>("pixel"), new Vector2( i, i ), new Vector2(62f, 86f), graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, centro));
            }

            for (int i = 0; i < 500; i += 2)
            {
                listaPixels.Add(new zombie(Content.Load<Texture2D>("pixel"), new Vector2( ((i * -1) + 500 ), i), new Vector2(62f, 86f), graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, centro));
            }
            


            for (int i = 0; i < 25; i++)
            {
                //Obtendo uma posicao inicial aleatoria
                initialPosition = this.GetRandomInitialLocation(zombie.larguraTextura, zombie.alturaTextura);

                //Centralizando a posicao do bicho (descontando o tamanho da imagem)
                initialPosition = new Vector2(initialPosition.X - (zombie.larguraTextura / 2), initialPosition.Y - (zombie.alturaTextura / 2));

                Texture2D texture = Content.Load<Texture2D>( zombie.GetSprite(initialPosition , this.matrixHigh ) );

                listaZumbis.Add( new zombie(texture, initialPosition, new Vector2(62f, 86f), graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, centro) );
            }

            //sprite1.velocity = new Vector2(1, 1);

        }
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            foreach (zombie sprite in listaZumbis)
            {
                sprite.texture.Dispose();
            }
            foreach (zombie sprite in listaPixels)
            {
                sprite.texture.Dispose();
            }

            spriteBatch.Dispose();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            KeyboardState keyboard = Keyboard.GetState();
            //            if (keyboard.IsKeyDown(Keys.Right))
            //                sprite1.MoveDireita();

            foreach (zombie sprite in listaZumbis)
            {
                sprite.AtualizaPosicao();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            // TODO: Add your drawing code here
            spriteBatch.Begin();
            //spriteBatch.Draw(background, new Rectangle(0, 0, graphics.GraphicsDevice.DisplayMode.Width, graphics.GraphicsDevice.DisplayMode.Height), Color.LightGray);
            //sprite1.Draw(spriteBatch);
            foreach (zombie sprite in listaZumbis)
            {
                sprite.Draw(spriteBatch);
            }
            foreach (zombie sprite in listaPixels)
            {
                sprite.Draw(spriteBatch);
            }


            spriteBatch.End();

            base.Draw(gameTime);
        }


        public Vector2 GetRandomInitialLocation( int larguraXImagem, int alturaYImagem )
        {
            // apesar da matrix ser de matrixLow x MatrixHigh esses limites tem que ser deslocados por 
            // que precisamos centralizar a imagem do sprite....

            int y = 0;
            int x = 0;


            int chance = random.Next(2);  // exclusive upperBound

            // criei uma zona de exclusao onde nao sao criados zumbis em no range em Y (100 ate 400)
            if (chance == 0)
                y = random.Next(matrixLow - larguraXImagem, 100 - alturaYImagem);
            else
                y = random.Next(400 - larguraXImagem, matrixHigh - alturaYImagem);


            if (y > (0 - alturaYImagem) && y < (501 - alturaYImagem))
            {
                int lado = random.Next(2);  // exclusive upperBound
                if (lado == 0)
                {
                    x = random.Next(matrixLow - larguraXImagem, 0 - alturaYImagem);
                }
                else
                {
                    x = random.Next(500 - larguraXImagem, matrixHigh - alturaYImagem);
                }
            }
            else
            {
                chance = random.Next(2);  // exclusive upperBound

                // outra zona de exclusao onde nao sao criados zumbis em no range em X (100 ate 400)
                if (chance == 0)
                    x = random.Next(matrixLow - larguraXImagem, 100 - alturaYImagem);
                else
                    x = random.Next(400 - larguraXImagem, matrixHigh - alturaYImagem);
            }

            Console.WriteLine("x {0} y {1} ", x, y);
            return new Vector2(x, y);


        }

    }
}
