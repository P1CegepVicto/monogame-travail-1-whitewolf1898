using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace Projet1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameObject mario;
        Rectangle fenetre;
        GameObject chicken;
        GameObject egg;
        GameObject eggplosion;
        GameObject missile;
        GameObject flare;
        GameObject space;
        Random de = new Random();
        GameObject game;
        public int nbEnnemy = 0;

        
        SoundEffect son;
        SoundEffectInstance bombe;

        public object Break { get; private set; }

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
            this.graphics.PreferredBackBufferWidth = graphics.GraphicsDevice.DisplayMode.Width;
            this.graphics.PreferredBackBufferHeight = graphics.GraphicsDevice.DisplayMode.Height;
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
            mario = new Projet1.GameObject();
            chicken = new GameObject();
            egg = new GameObject();
            eggplosion = new GameObject();
            missile = new GameObject();
            flare = new GameObject();
            space = new GameObject();

            flare.estVivant = true;
            flare.position = chicken.position;
            egg.estVivant = true;
            egg.vitesse.Y = 25;
            mario.estVivant = true;
            mario.position.X = 750;
            mario.position.Y = 750;
            chicken.estVivant = true;
            chicken.position.X = 50;
            chicken.position.Y = 100;
            chicken.vitesse.X = -3;
            egg.position.X = chicken.position.X;
            egg.position.Y = chicken.position.Y;
            missile.position.X = mario.position.X;
            missile.position.Y = mario.position.Y;
            missile.vitesse.Y = -10;
            missile.estVivant = false;

            eggplosion.sprite = Content.Load<Texture2D>("eggplosion.png");
            missile.sprite = Content.Load<Texture2D>("missile.png");
            flare.sprite = Content.Load<Texture2D>("flare.png");
            chicken.sprite = Content.Load<Texture2D>("chicken.png");
            egg.sprite = Content.Load<Texture2D>("egg.png");
            mario.sprite = Content.Load<Texture2D>("st6.png");
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
            
            if(Keyboard.GetState().IsKeyDown(Keys.A))
            {
                mario.position.X -= 4;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                mario.position.X += 4;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                mario.position.Y += 4;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                mario.position.Y -= 4;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.PrintScreen))
            {
                
               
            }
            UpdateChicken();
            UpdateMario();
            UpdateEgg();
            UpdateMissile();
            
      
            
            // TODO: Add your update logic here

            base.Update(gameTime);
        }
        public void UpdateMario()
        {
            if (mario.GetRect().Intersects(egg.GetRect()))
            {
                mario.estVivant = false;
            }
            
            if (mario.position.X < fenetre.Left)
            {
                mario.position.X = fenetre.Left;
            }
            if (mario.position.X > fenetre.Right - 150)
            {
                mario.position.X = fenetre.Right - 150;
            }
            if (mario.position.Y < fenetre.Top)
            {
                mario.position.Y = fenetre.Top;
            }
            if (mario.position.Y > fenetre.Bottom - 150)
            {
                mario.position.Y = fenetre.Bottom - 150;
            }
                mario.position += mario.vitesse;
        } 
        public void UpdateEgg()
        {
            if (egg.estVivant == true)
            {
                egg.vitesse.Y = 25;
                if (egg.position.Y > fenetre.Bottom)
                {
                    egg.position.X = chicken.position.X + 59;
                    egg.position.Y = chicken.position.Y + 100;
                }

                egg.position += egg.vitesse;
            }
        }
        
        public void UpdateChicken()
        {
            if (chicken.GetRect().Intersects(missile.GetRect()))
            {
                chicken.estVivant = false;
            }
            if (chicken.position.X < fenetre.Left)
            {
                chicken.vitesse.X = 5;
            }
            if(chicken.position.X > fenetre.Right - 119)
            {
                chicken.vitesse.X = -5;
                
                
            }
            if( chicken.estVivant == false)
            {
                chicken.vitesse.Y = 5;
            }
            chicken.position += chicken.vitesse;

        }
        public void UpdateMissile()
        {
            
            
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                missile.estVivant = true;
                missile.position.Y = mario.position.Y;
                missile.position.X = mario.position.X;
                missile.vitesse.Y = -25;
                if(missile.position.Y < fenetre.Top)
                {
                    missile.position.Y = mario.position.Y;
                    missile.position.X = mario.position.X;
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
            spriteBatch.Draw(space.sprite, fenetre, Color.White);

            if (egg.estVivant == true)
            {
                spriteBatch.Draw(egg.sprite, egg.position, Color.White);
            }
            if (missile.estVivant == true)
            {
              
                    spriteBatch.Draw(missile.sprite, missile.position += missile.vitesse, Color.White);
                   
                
            }
            if ( mario.estVivant == true)
            {
                spriteBatch.Draw(mario.sprite, mario.position, Color.White);
            }
            else if(mario.estVivant == false)
            {
                
                spriteBatch.Draw(eggplosion.sprite, mario.position, Color.White);
                if(Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    Exit();
                }
                               
            }
            if( chicken.estVivant == true)
            {
                spriteBatch.Draw(chicken.sprite, chicken.position, Color.White);
            }
            else if( chicken.estVivant == false)
            {

                egg.estVivant = false;
                spriteBatch.Draw(flare.sprite, chicken.position, Color.White);

                
            }
           









            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
