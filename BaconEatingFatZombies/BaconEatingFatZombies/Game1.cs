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
        private List<bacon> listaBacon = new List<bacon>();
        private player player;
        private baseSprite centroTela;

        //private List<zombie> listaPixels = new List<zombie>();
        private List<bullet> listaBalas = new List<bullet>();

        private SpriteFont scores;
        private String scoreString;

        private int numberBacons = 3;
        private KeyboardState keyboard;
        private Random random = new Random();
        private Vector2 centro;

        public int matrixLow { get; set; }
        public int matrixHigh { get; set; }

        //Os tiros tem que ter um intervalo entre eles... por isso um cool down.
        private bool coolDownArma { get; set; }
        private bool coolDownSFX { get; set; }

        private TimeSpan intervaloCoolDown = new TimeSpan(0, 0, 0, 0, 450);
        private TimeSpan ultimoTempo;
        private int zombieNumber = 25;
        private int scoreNumber = 0;
        private bool playing = false;
        private bool gameOver = false;
        private SoundEffect sf;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 500;
            graphics.PreferredBackBufferWidth = 500;

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {

            base.Initialize();


        }

        protected override void LoadContent()
        {
            centro = new Vector2((Window.ClientBounds.Width / 2), (Window.ClientBounds.Height / 2));

            matrixLow = -200;  // esse vai ser o menor valor da matrix
            matrixHigh = 700;  // maior valor


            // significa que nossa matrix imaginaria vai ser de 900x900


            sf = Content.Load<SoundEffect>("chomp");


            spriteBatch = new SpriteBatch(GraphicsDevice);
            //            background = Content.Load<Texture2D>("foto2");

            Vector2 initialPosition = new Vector2(0, 0);

            scores = Content.Load<SpriteFont>("sans");
            scoreString = "Pontos: ";

            Dictionary<string, Texture2D> textureDict = new Dictionary<string, Texture2D>();
            textureDict.Add("N", Content.Load<Texture2D>("link-N"));
            textureDict.Add("NE", Content.Load<Texture2D>("link-NE"));
            textureDict.Add("NW", Content.Load<Texture2D>("link-NW"));
            textureDict.Add("W", Content.Load<Texture2D>("link-W"));
            textureDict.Add("S", Content.Load<Texture2D>("link-S"));
            textureDict.Add("SE", Content.Load<Texture2D>("link-SE"));
            textureDict.Add("SW", Content.Load<Texture2D>("link-SW"));
            textureDict.Add("E", Content.Load<Texture2D>("link-E"));
            player = new player(textureDict,
                    new Vector2(centro.X + 50f, centro.Y),
                    new Vector2(34f, 42f),
                    new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight)
                );


            //centroTela = new baseSprite(null, new Vector2(250, 250), new Vector2(20, 20), graphics.PreferredBackBufferWidth,
            //            graphics.PreferredBackBufferHeight);

            //centroTela.BoundingBox = new Rectangle((int)centroTela.position.X, (int)centroTela.position.Y, 20, 20);



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
                        graphics.PreferredBackBufferHeight));


            for (int i = 0; i < 15; i++)
            {
                //Obtendo uma posicao inicial aleatoria
                initialPosition = this.GetRandomInitialLocation(zombie.larguraTextura, zombie.alturaTextura);

                Texture2D texture = Content.Load<Texture2D>(zombie.GetSprite(initialPosition, this.matrixHigh));

                listaZumbis.Add(new zombie(texture, initialPosition, new Vector2(62f, 86f), graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, centro));
            }




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
            //}

            //sprite1.velocity = new Vector2(1, 1);

        }
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            foreach (zombie sprite in listaZumbis)
            {
                sprite.texture.Dispose();
            }

            foreach (bullet sprite in listaBalas)
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
            int count = 0;
            foreach (bacon tira in listaBacon)
            {
                if (tira.comido)
                    count++;
            }
            
                gameOver = count >= 3;
            
            if (!gameOver)
            {
                

                KeyboardState keyboard = Keyboard.GetState();
                scoreString = "Pontos: " + scoreNumber;
                if ((gameTime.TotalGameTime - ultimoTempo) > intervaloCoolDown)
                    coolDownArma = false;

                player.AtualizaPosicao(keyboard);

                if (keyboard.IsKeyDown(Keys.Space) && !coolDownArma)  // so vou disparar novamente quando o cool down acabar
                {

                    coolDownArma = true;
                    ultimoTempo = gameTime.TotalGameTime;

                    bullet minhaBala = new bullet(Content.Load<Texture2D>("bullet-" + player.direcaoCarteseana),
                        player.position,
                        new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight),
                        player.direcaoCarteseana
                    );
                    listaBalas.Add(minhaBala);

                }


                //primeiro desenha a porra toda
                foreach (zombie sprite in listaZumbis)
                {
                    sprite.AtualizaPosicao();
                }

                foreach (bullet bala in listaBalas)
                {
                    bala.AtualizaPosicao();
                }


                bullet pointer = null;

                // agora testa colisao com as balas
                foreach (zombie sprite in listaZumbis)
                {
                    foreach (bullet bala in listaBalas)
                    {
                        if (bala.BoundingBox.Intersects(sprite.BoundingBox))
                        {
                            bala.colidiuZumbi = true;
                            pointer = bala;
                            Vector2 newLocation = this.GetRandomInitialLocation(zombie.larguraTextura, zombie.alturaTextura);
                            //Vector2 newLocation = new Vector2(400,250);
                            sprite.MorreDiabo(Content.Load<Texture2D>(zombie.GetSprite(newLocation, matrixHigh)), newLocation);
                            scoreNumber = scoreNumber + 10;
                        }
                    }
                }
                if (pointer != null)
                {
                    pointer.position = new Vector2(-50f, -50f);
                }



                if ((gameTime.TotalGameTime - ultimoTempo) > intervaloCoolDown)
                    coolDownSFX = false;
                
                // agora testa colisao com os bacon!
                foreach (zombie sprite in listaZumbis)
                {
                    if (numberBacons > 0)
                    {
                        foreach (bacon tira in listaBacon)
                        {
                            if (tira.BoundingBox.Intersects(sprite.BoundingBox))
                            {
                                sprite.colidiuBacon = true;
                                tira.MorreDiabo();

                                if (!coolDownSFX)
                                {
                                    coolDownSFX = true;
                                    sf.Play();
                                }
                                
                                sprite.MorreDiabo(sprite.texture, sprite.InitialPosition);
                                //sprite.jaComeu = true;
                            }

                        }
                    }
                    else
                    {
                        gameOver = true;
                    }



                }


                base.Update(gameTime);
            }
            else
            {
                scoreString = "GAME OVER!";
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            //spriteBatch.Draw(background, new Rectangle(0, 0, graphics.GraphicsDevice.DisplayMode.Width, graphics.GraphicsDevice.DisplayMode.Height), Color.LightGray);
            //sprite1.Draw(spriteBatch);

            foreach (zombie sprite in listaZumbis)
            {
                sprite.Draw(spriteBatch);
            }
            foreach (bacon sprite in listaBacon)
            {
                sprite.Draw(spriteBatch);
            }
            foreach (bullet sprite in listaBalas)
            {
                sprite.Draw(spriteBatch);
            }

            player.Draw(spriteBatch);

            spriteBatch.DrawString(scores, scoreString, new Vector2(5f, 5f), Color.Red);

            spriteBatch.End();

            base.Draw(gameTime);

            
             
            

        }


        public Vector2 GetRandomInitialLocation(int larguraXImagem, int alturaYImagem)
        {
            int y = 0;
            int x = 0;

            y = random.Next(matrixLow, matrixHigh);

            if (y > 0 && y < 501)
            {
                int lado = random.Next(2);  // exclusive upperBound
                if (lado == 0)
                {
                    x = random.Next(matrixLow, 0);
                }
                else
                {
                    x = random.Next(500, matrixHigh);
                }
            }
            else
            {
                int chance = random.Next(2);

                if (chance == 0)
                {
                    x = random.Next(matrixLow, 170);
                }
                else
                {
                    x = random.Next(330, matrixHigh);
                }

                
            }

            //Console.WriteLine("x {0} y {1} ", x, y);
            return new Vector2(x, y);

        }

    }
}