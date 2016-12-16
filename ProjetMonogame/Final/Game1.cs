using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Threading;

namespace Final
{

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SoundEffect son;
        SoundEffectInstance boom;

        protected Song song;
        Rectangle fenetre;
        GameObject ship;
        GameObject[] chick;
        GameObject space;
        GameObject[] bullet;
        GameObject[] egg;
        GameObject eggplosion;
        GameObject flare;
        GameObject space2;
        GameObject[] novaBlast;
        GameObject[] shadowball;
        GameObject[] thunderbolt;
        GameObject boost;
        GameObject enter;


        int b = 3;
        int nbEnnemy = 0;
        int i = 0;
        public int maxChick = 15;
        public int maxBullet = 5;
        int compteur = 0;
        int compteurG = 0;
        public SpriteFont font;

        Random de = new Random();
        // essayé public si private ne marche pas

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

            // TODO: Add your initialization logic here
            this.graphics.PreferredBackBufferHeight = graphics.GraphicsDevice.DisplayMode.Height;
            this.graphics.PreferredBackBufferWidth = graphics.GraphicsDevice.DisplayMode.Width;
            this.graphics.ApplyChanges();
            fenetre = new Rectangle(0, 0, graphics.GraphicsDevice.DisplayMode.Width, graphics.GraphicsDevice.DisplayMode.Height);
            // get the mouse state
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
            boost = new GameObject();  
            space.position.X = 0;
            space.position.Y = 0;
            space2 = new GameObject();
            bullet = new GameObject[maxBullet];
            egg = new GameObject[maxChick];
            eggplosion = new GameObject();
            flare = new GameObject();
            enter = new GameObject();

            Song song = Content.Load<Song>("Son//song");
            MediaPlayer.Play(song);

            ship.isAlive = true;
            ship.vie = 100;
            ship.position.X = 750;
            ship.position.Y = 750;
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
                egg[i].position = chick[i].position;
                egg[i].vitesse.Y = de.Next(8, 15);
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
            space2.sprite = Content.Load<Texture2D>("space.jpg");
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

                UpdateChick(gameTime);
                UpdateCrash();
                UpdateBackground();
                UpdateShip();
                UpdateEgg();
                UpdateBullet(gameTime);
          

          
            // TODO: Add your update logic here
             base.Update(gameTime);
            // TODO: Add your update logic here
        }

        public void UpdateShip()
        {
         
            if (eggplosion.isAlive == false)
            {
                Exit();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                ship.position.X -= 4;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                ship.position.X += 4;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                ship.position.Y += 4;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                ship.position.Y -= 4;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.PrintScreen))
            {


            }
        }
        public void UpdateBackground()
        {
            if(Keyboard.GetState().IsKeyDown(Keys.W))
            {
                space.vitesse.Y = 4;
            }
            if(Keyboard.GetState().IsKeyUp(Keys.W))
            {
                space.vitesse.Y = 3;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                space.vitesse.Y = 2;
            }          
            if (space.position.Y < 0)
            {
                space2.position.Y = space.position.Y + space.sprite.Height;
            }
            if (space.position.Y >= 0)
            {
                space2.position.Y = space.position.Y - space.sprite.Height;
            }
            if (space2.position.Y > 0)
            {
                space.position.Y = space2.position.Y - space2.sprite.Height;
            }
            if (space2.position.Y <= 0)
            {
                space.position.Y = space2.position.Y + space2.sprite.Height;
            }

            space.position.Y += space.vitesse.Y;
            
        }
        public void UpdateChick(GameTime gameTime)
        {
            //faire apparaitre un ennemi a la fois   
            if (nbEnnemy * 2 < gameTime.TotalGameTime.Seconds && nbEnnemy < maxChick)
            {
                chick[nbEnnemy].isAlive = true;
                nbEnnemy++;
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
                    egg[i].vitesse.Y = de.Next(8, 15);
                    if (egg[i].position.Y > fenetre.Bottom || egg[i].isAlive == false)
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
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
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
                    if(bullet[i].getRect().Intersects(chick[j].getRect()))
                    {
                        
                    }
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
                    egg[i].position = chick[i].position;
                    ship.vie = ship.vie -20;
                    if (ship.vie == 0)
                    {
                        ship.isAlive = false;
                    }
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
            spriteBatch.Draw(space.sprite, space.position, Color.White);
            spriteBatch.Draw(space2.sprite, space2.position, Color.White);
                for (int i = 0; i < bullet.Length; i++)
                {
                    if (ship.isAlive == true)
                    {
                        spriteBatch.Draw(ship.sprite, ship.position, Color.White);
                        if (bullet[i].isAlive == true)
                        {
                            spriteBatch.Draw(bullet[i].sprite, bullet[i].position += bullet[i].vitesse);
                        }
                    }
                }
            
            //faire en sorte que le jeu se termine après un certains temps
            if (ship.isAlive == false)
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
            spriteBatch.DrawString(font ,ship.vie.ToString() + " LP" , new Vector2( 100,100), Color.Crimson);     
            // TODO: Add your drawing code here
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
