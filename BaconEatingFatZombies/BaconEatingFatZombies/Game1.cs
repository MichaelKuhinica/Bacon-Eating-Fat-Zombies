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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D background;
        List<zombie> listaZumbis = new List<zombie>();
        KeyboardState keyboard;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 500;
            graphics.PreferredBackBufferWidth = 500;

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
//            background = Content.Load<Texture2D>("foto2");
            listaZumbis.Add( new zombie(Content.Load<Texture2D>("Z_DOWN_PNG"), new Vector2(250f, 0f), new Vector2(64f, 64f), graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, new Vector2(0f, 0f)));
            listaZumbis.Add(new zombie(Content.Load<Texture2D>("Z_UP_PNG"), new Vector2(250f, 490f), new Vector2(64f, 64f), graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, new Vector2(0f, 0f)));
            listaZumbis.Add(new zombie(Content.Load<Texture2D>("Z_RIGHT_PNG"), new Vector2(0f, 100f), new Vector2(64f, 64f), graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, new Vector2(0f, 0f)));
            listaZumbis.Add(new zombie(Content.Load<Texture2D>("Z_RIGHT_PNG"), new Vector2(0f, 150f), new Vector2(64f, 64f), graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, new Vector2(0f, 0f)));
            listaZumbis.Add(new zombie(Content.Load<Texture2D>("Z_LEFT_PNG"), new Vector2(490f, 300f), new Vector2(64f, 64f), graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, new Vector2(0f, 0f)));
            //sprite1.velocity = new Vector2(1, 1);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
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

            // TODO: Add your update logic here
            //sprite1.Move();

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
    }
}
