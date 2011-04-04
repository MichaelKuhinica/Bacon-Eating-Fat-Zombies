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
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D background;
        List<zombie> listaZumbis = new List<zombie>();
        KeyboardState keyboard;

        Random random = new Random();

        // aqui fica o bacon
        Vector2 centro;


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

            spriteBatch = new SpriteBatch(GraphicsDevice);
            //            background = Content.Load<Texture2D>("foto2");

            Vector2 initialPosition = new Vector2(0,0);
            for (int i = 0; i < 25; i++)
            {
                initialPosition = this.GetRandomInitialLocation();

                listaZumbis.Add( new zombie(Content.Load<Texture2D>(zombie.GetSprite(initialPosition)), initialPosition, new Vector2(64f, 64f), graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, centro) );
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


            spriteBatch.End();

            base.Draw(gameTime);
        }


        public Vector2 GetRandomInitialLocation()
        {
            
            int y = random.Next(-100, 600);
            int x = 0;

            if (y > 0 && y < 501)
            {
                int lado = random.Next(2);  // exclusive upperBound
                if (lado == 0)
                {
                    x = random.Next(-100, 0);
                }
                else
                {
                    x = random.Next(500, 600);
                }
            }
            else
            {
                x = random.Next(-100, 600);
            }

            Console.WriteLine("x {0} y {1} ", x, y);
            return new Vector2(x, y);


        }

    }
}
