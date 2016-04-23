using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DoDManager
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class DoDManager : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Player player1;
        private Flag flag1;
        private Texture2D image;

        public DoDManager()
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
            base.Initialize();

            player1 = new Player(0, 5);
            flag1 = new Flag(380, 40);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            image = Content.Load<Texture2D>("dot");
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

            // TODO: Add your update logic here
            base.Update(gameTime);
            player1.updatePosition();
            if (player1.getPosition() >= flag1.getLeft() & flag1.getCapState() == 0)
            {
                player1.setVelocity(0);
                flag1.setCapState(1);
                flag1.incrementCap();
            }

            if (player1.getPosition() >= flag1.getLeft() & flag1.getCapState() == 1)
            {
                if (flag1.getCapTime() == 0)
                {
                    flag1.setCapState(2);                    
                }
                else
                {
                    flag1.incrementCap();
                }
            }

            if (player1.getPosition() >= flag1.getLeft() & flag1.getCapState() == 2)
            {
                player1.setVelocity(5);
            }

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here

            base.Draw(gameTime);

            spriteBatch.Begin();

            spriteBatch.Draw(image, new Rectangle(player1.getPosition(), 200, 10, 10), Color.White);

            spriteBatch.End();
        }
    }

    public class Flag
    {
        private int origin;
        private int size;
        private int captureState;
        private int captureTime;

        public Flag(int topLeft, int Size)
        {
            origin = topLeft;
            size = Size;
            captureState = 0;
            captureTime = 180;
        }

        public int getLeft() { return origin; }
        public int getSize() { return size; }
        public int getCapTime() { return captureTime; }
        public void incrementCap() { captureTime -= 1; }
        public int getCapState() { return captureState; }
        public void setCapState(int value) { captureState = value; }
    }

    public class Player
    {
        private int vel, pos;

        public Player(int initialPosition, int initialVelocity)
        {
            vel = initialVelocity;
            pos = initialPosition;
        }

        public void setVelocity(int value)
        {
            vel = value;
        }

        public void setPosition(int value)
        {
            pos = value;
        }

        public void updatePosition()
        {
            pos += vel;
        }

        public int getPosition()
        {
            return pos;
        }
    }
}
