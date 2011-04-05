using System;using System.Collections.Generic;using System.Linq;using Microsoft.Xna.Framework;using Microsoft.Xna.Framework.Audio;using Microsoft.Xna.Framework.Content;using Microsoft.Xna.Framework.GamerServices;using Microsoft.Xna.Framework.Graphics;using Microsoft.Xna.Framework.Input;using Microsoft.Xna.Framework.Media;using Microsoft.Xna.Framework.Net;using Microsoft.Xna.Framework.Storage;namespace BaconEatingFatZombies{    public class Game1 : Microsoft.Xna.Framework.Game    {        private GraphicsDeviceManager graphics;        private SpriteBatch spriteBatch;        private Texture2D background;        private List<zombie> listaZumbis = new List<zombie>();        private List<bacon> listaBacon = new List<bacon>();
        private player player;

        //private List<zombie> listaPixels = new List<zombie>();
        private List<bullet> listaBalas = new List<bullet>();
        private KeyboardState keyboard;        private Random random = new Random();        private Vector2 centro;        public int matrixLow { get; set; }        public int matrixHigh { get; set; }        public Game1()        {            graphics = new GraphicsDeviceManager(this);            graphics.PreferredBackBufferHeight = 500;            graphics.PreferredBackBufferWidth = 500;            Content.RootDirectory = "Content";        }        protected override void Initialize()        {            base.Initialize();        }        protected override void LoadContent()        {            centro = new Vector2((Window.ClientBounds.Width / 2), (Window.ClientBounds.Height / 2));            matrixLow = -200;  // esse vai ser o menor valor da matrix            matrixHigh = 700;  // maior valor            // significa que nossa matrix imaginaria vai ser de 900x900            spriteBatch = new SpriteBatch(GraphicsDevice);            //            background = Content.Load<Texture2D>("foto2");            Vector2 initialPosition = new Vector2(0,0);



            player = new player(Content.Load<Texture2D>("link"),
                    new Vector2(centro.X+50f, centro.Y),
                    new Vector2(34f, 42f),
                    new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight)
                );

            listaBacon.Add(new bacon(Content.Load<Texture2D>("bacon_peq"),
                        new Vector2(centro.X, centro.Y - 15),
                        new Vector2(62f, 86f),
                        graphics.PreferredBackBufferWidth,
                        graphics.PreferredBackBufferHeight));

            listaBacon.Add(new bacon(Content.Load<Texture2D>("bacon_peq"),
                        new Vector2(centro.X - 15, centro.Y),
                        new Vector2(62f, 86f),
                        graphics.PreferredBackBufferWidth,
                        graphics.PreferredBackBufferHeight));

            listaBacon.Add(new bacon(Content.Load<Texture2D>("bacon_peq"),
                        new Vector2(centro.X + 15, centro.Y),
                        new Vector2(62f, 86f),
                        graphics.PreferredBackBufferWidth,
                        graphics.PreferredBackBufferHeight));            for (int i = 0; i < 20; i++)            {                //Obtendo uma posicao inicial aleatoria                initialPosition = this.GetRandomInitialLocation(zombie.larguraTextura, zombie.alturaTextura);                //Centralizando a posicao do bicho (descontando o tamanho da imagem)                initialPosition = new Vector2(initialPosition.X, initialPosition.Y);                Texture2D texture = Content.Load<Texture2D>( zombie.GetSprite(initialPosition , this.matrixHigh ) );                listaZumbis.Add( new zombie(texture, initialPosition, new Vector2(62f, 86f), graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, centro) );            }



            //for (int i = 0; i < 3; i++)
            //{
            //    initialPosition = this.GetRandomInitialLocation(bullet.larguraTextura, bullet.alturaTextura);
            //    Texture2D texture = Content.Load<Texture2D>( bullet.bullet_E );
            //    listaBalas.Add(new bullet(
            //        texture,
            //        initialPosition,
            //        new Vector2(62f, 86f),
            //        new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight),
            //        centro
            //    ));
            //}            //sprite1.velocity = new Vector2(1, 1);        }        protected override void UnloadContent()        {            // TODO: Unload any non ContentManager content here            foreach (zombie sprite in listaZumbis)            {                sprite.texture.Dispose();            }

            foreach (bullet sprite in listaBalas)
            {
                sprite.texture.Dispose();
            }            spriteBatch.Dispose();        }        /// <summary>        /// Allows the game to run logic such as updating the world,        /// checking for collisions, gathering input, and playing audio.        /// </summary>        /// <param name="gameTime">Provides a snapshot of timing values.</param>        protected override void Update(GameTime gameTime)        {            // Allows the game to exit            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)                this.Exit();            KeyboardState keyboard = Keyboard.GetState();

            player.AtualizaPosicao(keyboard);



            //primeiro desenha a porra toda
            foreach (zombie sprite in listaZumbis)
            {
                sprite.AtualizaPosicao();
            }

            foreach (bullet bala in listaBalas)
            {
                bala.AtualizaPosicao();
            }



            // agora testa colisao com as balas
            foreach (zombie sprite in listaZumbis)
            {
                foreach (bullet bala in listaBalas)
                {
                    if (bala.BoundingBox.Intersects(sprite.BoundingBox))
                    {
                        Vector2 newLocation = this.GetRandomInitialLocation(zombie.larguraTextura, zombie.alturaTextura);
                        sprite.MorreDiabo(Content.Load<Texture2D>(zombie.GetSprite(newLocation, matrixHigh)), newLocation);
                    }
                }
            }
            // agora testa colisao com os bacon!
            foreach (zombie sprite in listaZumbis)
            {
                foreach (bacon tira in listaBacon)
                {
                    if ( tira.BoundingBox.Intersects(sprite.BoundingBox))
                    {
                        sprite.colidiuBacon = true;
                    }
                }
            }            //foreach (bullet sprite in listaBalas)            //{            //    sprite.AtualizaPosicao();            //}            base.Update(gameTime);        }        /// <summary>        /// This is called when the game should draw itself.        /// </summary>        /// <param name="gameTime">Provides a snapshot of timing values.</param>        protected override void Draw(GameTime gameTime)        {            GraphicsDevice.Clear(Color.CornflowerBlue);            spriteBatch.Begin();            //spriteBatch.Draw(background, new Rectangle(0, 0, graphics.GraphicsDevice.DisplayMode.Width, graphics.GraphicsDevice.DisplayMode.Height), Color.LightGray);            //sprite1.Draw(spriteBatch);            foreach (zombie sprite in listaZumbis)            {                sprite.Draw(spriteBatch);            }
            foreach (bacon sprite in listaBacon)
            {
                sprite.Draw(spriteBatch);
            }            //foreach (bullet sprite in listaBalas)            //{            //    sprite.Draw(spriteBatch);            //}

            player.Draw(spriteBatch);            spriteBatch.End();            base.Draw(gameTime);        }        public Vector2 GetRandomInitialLocation( int larguraXImagem, int alturaYImagem )        {            int y = 0;            int x = 0;            y = random.Next(matrixLow, matrixHigh);            if (y > 0 && y < 501)            {                int lado = random.Next(2);  // exclusive upperBound                if (lado == 0)                {                    x = random.Next(matrixLow , 0 );                }                else                {                    x = random.Next(500 , matrixHigh );                }            }            else            {                x = random.Next(matrixLow, matrixHigh);            }            Console.WriteLine("x {0} y {1} ", x, y);            return new Vector2(x, y);        }    }}