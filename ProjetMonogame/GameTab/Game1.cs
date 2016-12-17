using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace GameTab
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle fenetre;
        GameObject ship;
        GameObject[] chick;
        GameObject space;
        GameObject[] bullet;
        GameObject[] egg;
        GameObject eggplosion;
        GameObject flare;
        int nbEnnemy = 0;
        int i = 0;
        public int maxChick = 15;
        public int maxBullet = 5;
        int compteur = 0;
        int compteurG = 0;
        public SpriteFont font;

        Random de = new Random();
        // essayé public si private ne marche pas
        private Vector2 mouseCoordinates;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);           
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
            this.graphics.PreferredBackBufferHeight = graphics.GraphicsDevice.DisplayMode.Height;
            this.graphics.PreferredBackBufferWidth = graphics.GraphicsDevice.DisplayMode.Width;
            this.graphics.ToggleFullScreen();
            fenetre = new Rectangle(0, 0, graphics.GraphicsDevice.DisplayMode.Width, graphics.GraphicsDevice.DisplayMode.Height);          
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
            fenetre = graphics.GraphicsDevice.Viewport.Bounds;
            fenetre.Width = graphics.GraphicsDevice.DisplayMode.Width;
            fenetre.Height = graphics.GraphicsDevice.DisplayMode.Height;

            font = Content.Load<SpriteFont>("Font");
            ship = new GameObject();
            chick = new GameObject[maxChick];
            space = new GameObject();
            bullet = new GameObject[maxBullet];
            egg = new GameObject[maxChick];
            eggplosion = new GameObject();
            flare = new GameObject();

            ship.isAlive = true;
            for (int i = 0; i < chick.Length; i++)
            {
                chick[i] = new GameObject();
                chick[i].isAlive = false;
                chick[i].direction.X = de.Next(-10, -4);

                chick[i].vitesse.X = -10;
                chick[i].sprite = Content.Load<Texture2D>("chicken.png");
                chick[i].position.X = fenetre.Width;
                chick[i].position.Y = de.Next(0, 250);

                egg[i] = new GameObject();
                egg[i].isAlive = false;
                egg[i].position= chick[i].position;
                egg[i].vitesse.Y = de.Next(8,15);
                egg[i].sprite = Content.Load<Texture2D>("egg.png");               
            }
            for (int i = 0; i < bullet.Length; i++)
            {

                bullet[i] = new GameObject();
                bullet[i].isAlive = false;
                bullet[i].position.X = ship.position.X + (ship.position.X / 2);
                bullet[i].position.Y = ship.position.Y;
                bullet[i].vitesse.Y = -25;
                bullet[i].sprite = Content.Load<Texture2D>("missile.png");
            }                                   
            eggplosion.isAlive = true;
            ship.sprite = Content.Load<Texture2D>("st6.png");                              
            flare.sprite = Content.Load<Texture2D>("flare.png");
            eggplosion.sprite = Content.Load<Texture2D>("eggplosion.png");
            space.sprite = Content.Load<Texture2D>("space.jpg");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            var mouseState = Mouse.GetState();
            this.mouseCoordinates = new Vector2(mouseState.X, mouseState.Y);            
            // TODO: Add your update logic here
            UpdateChick(gameTime);
            UpdateCrash();
            UpdateShip();
            UpdateEgg();
            UpdateBullet(gameTime);
            base.Update(gameTime);
        }
        public void UpdateShip()
        {
            ship.position = this.mouseCoordinates;
            if(eggplosion.isAlive == false)
            {
                Exit();
            }            
        }
        public void UpdateChick(GameTime gameTime)
        {
         //faire apparaitre un ennemi a la fois   
            if (nbEnnemy * 2 < gameTime.TotalGameTime.Seconds && nbEnnemy < maxChick)
                {
                    nbEnnemy++;
                    chick[nbEnnemy].isAlive = true;
                    
                }
            for (int i = 0; i < nbEnnemy; i++)
            {
                chick[i].position.X += (int)chick[i].direction.X;
                chick[i].position.Y += (int)chick[i].direction.Y;
                if (chick[i].isAlive == false)
                {
                    chick[i].isAlive = true;

                    chick[i].position.X = fenetre.Width;
                    chick[i].position.Y = de.Next(0, 250);
                }
                if (chick[i].isAlive == true)
                {
                    egg[i].isAlive = true;
                }            
                if (chick[i].position.X < 0)
                {
                    chick[i].isAlive = false;                    
                }
            }
        }            
        public void UpdateEgg()
        {
            for (int i = 0; i < chick.Length; i++)
            {      
                if (egg[i].isAlive == true)
                {
                    egg[i].vitesse.Y = de.Next(8,15);
                    if (egg[i].position.Y > fenetre.Bottom)
                    {
                            egg[i].position.Y = chick[i].position.Y;
                            egg[i].position.X = chick[i].position.X;                     
                    }
                    egg[i].position += egg[i].vitesse;
                }
            }
        }
        public void UpdateBullet(GameTime gameTime)
        {
            //tirer plusieur balle
            compteur++;
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                if (i < bullet.Length)
                {
                    if (compteur >= 15)
                    {
                        bullet[i].isAlive = true;
                        bullet[i].vitesse.Y = -25;
                        bullet[i].position.X = ship.position.X;
                        bullet[i].position.Y = ship.position.Y;
                        if (bullet[i].position.Y < fenetre.Top)
                        {
                            bullet[i].isAlive = false;
                            bullet[i].position = ship.position;
                        }
                        i++;
                        compteur = 0;
                    }
                }
                else
                {
                    i = 0;
                }
            }       
        }
        public void UpdateCrash()
        {
            //pouvoir tuer n'importe qu'elle ennemy avec une balle
            for (int j = 0; j < chick.Length; j++)
            {
                for (int i = 0; i < bullet.Length; i++)
                {
                    //faire disparaître la balle lorsqu'elle rencontre un ennemy
                    if (bullet[i].getRect().Intersects(chick[j].getRect()) && bullet[i].isAlive == true)
                    {
                        chick[j].isAlive = false;
                        bullet[i].isAlive = false;
                        bullet[i].position = ship.position;
                    }
                }
            }
            for (int i = 0; i < chick.Length; i++)
            {                
                if (egg[i].getRect().Intersects(ship.getRect()))
                {
                    egg[i].isAlive = false;
                    ship.isAlive = false;
                }                
            }
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw( space.sprite, fenetre, Color.White);
            for (int i = 0; i < bullet.Length; i++)
            {
                if (ship.isAlive == true)
                {
                    spriteBatch.Draw(ship.sprite, this.mouseCoordinates, Color.White);
                    if (bullet[i].isAlive == true)
                    {
                        spriteBatch.Draw(bullet[i].sprite, bullet[i].position += bullet[i].vitesse);
                    }
                }              
            }
            //faire en sorte que le jeu se termine après un certains temps
            if(ship.isAlive == false)
            {
                ship.vitesse.Y = 5;
                compteurG++;
                if (compteurG <= 60)
                {
                    spriteBatch.Draw(eggplosion.sprite, ship.position);
                }
                else
                {
                    eggplosion.isAlive = false;
                }
            }           
            for (int i = 0; i < chick.Length; i++)
            {               
                if (chick[i].isAlive == true)
                {
                    
                   spriteBatch.Draw(chick[i].sprite, chick[i].position, Color.White);
                   if (egg[i].isAlive == true)
                    {
                        spriteBatch.Draw(egg[i].sprite, egg[i].position, Color.White);
                    }
                }
            }
            // faire apparaître un timer
            spriteBatch.DrawString(font, gameTime.TotalGameTime.Minutes.ToString() + ":" + gameTime.TotalGameTime.Seconds.ToString(), new Vector2(100,100), Color.White );            
            // TODO: Add your drawing code here
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
