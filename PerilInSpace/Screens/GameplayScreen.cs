using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using PerilInSpace.StateManagement;

namespace PerilInSpace.Screens
{
    
    public class GameplayScreen : GameScreen
    {
        //META GAME SERVICES
        private Game _game;
        private ContentManager _content;
        private SpriteBatch _spriteBatch;

        //SOUNDS
        private SoundEffect playerHitSound;
        private SoundEffect firingLasersSound;
        public Song backgroundMusic;

        //TEXTURES
        private Texture2D _purpleGameBackground;
        private Texture2D _blueGameBackground;
        private Texture2D _darkPurpleBackground;
        private Texture2D parallaxBG;
        private Texture2D fixedBG;
        
        List<Texture2D> asteroidTextures = new List<Texture2D>();
        public Texture2D boundingTexture;


        //VARIABLES
        public bool resetFlag = false;
        public bool running = true;
        public static bool controlsScreen = false;
        public bool objectiveScreen = false;
        public MouseState _currentMouse;
        public MouseState _previousMouse;
        public bool previousCollision;
        public bool currentCollision;
        private GameBackground gameBackground;
        //private List<Sprite> _sprites;
        //List<Sprite> asteroids = new List<Sprite>();
        public float distance;
        public int score = 0;
        public DateTime targetTime;
        public int _volume;
        public static bool isMainScreen;

        //PARTICLE SYSTEMS
        //public RainParticleSystem _rain;
        //ExplosionParticleSystem _explosions;
        //FireworkParticleSystem _fireworks;


        public GameplayScreen()
        {
            
        }

        // Load graphics content for the game
        public override void Activate()
        {

            
            // Add this to access the .Game properties
            //ScreenManager.Game

            //SETUP OUR META GAME SERVICES
            _game = ScreenManager.Game;

            if (_content == null)
            {
                _content = new ContentManager(ScreenManager.Game.Services, "Content");
            }


            //CREATE PARTICLE SYSTEMS AND GAME BACKGROUND

            //gameBackground = new GameBackground(_game);
            //_rain = new RainParticleSystem(this, new Rectangle(0, -20, ScreenManager.GraphicsDevice.Viewport.Width, 10));
            //_game.Components.Add(_rain);
            //_explosions = new ExplosionParticleSystem(this, 20);
            //_game.Components.Add(_explosions);
            //_explosions.Visible = false;
            //_fireworks = new FireworkParticleSystem(this, 20);
            //_game.Components.Add(_fireworks);


            //LOAD TEXTURES & SOUNDS
            //gameBackground.LoadContent(_content);
            //font = _content.Load<SpriteFont>("myfont");
            //endGamefont = _content.Load<SpriteFont>("endGameFont");
            boundingTexture = _content.Load<Texture2D>("bounding");
            backgroundMusic = _content.Load<Song>("music");

            playerHitSound = _content.Load<SoundEffect>("sfx_lose");
            firingLasersSound = _content.Load<SoundEffect>("sfx_laser1");
            parallaxBG = _content.Load<Texture2D>("parallax_bg");
            _blueGameBackground = _content.Load<Texture2D>("Backgrounds/blue");
            _purpleGameBackground = _content.Load<Texture2D>("Backgrounds/purple");
            _darkPurpleBackground = _content.Load<Texture2D>("Backgrounds/darkPurple");




            //startGame();

        }


        public override void Deactivate()
        {
            base.Deactivate();
        }

        public override void Unload()
        {
            //_content.Unload();
        }

        // This method checks the GameScreen.IsActive property, so the game will
        // stop updating when the pause menu is active, or if you tab away to a different application.
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);

            if (IsActive)
            {



            }
        }

        // Unlike the Update method, this will only be called when the gameplay screen is active.


        public override void Draw(GameTime gameTime)
        {
            // This game has a blue background. Why? Because!
            //ScreenManager.GraphicsDevice.Clear(ClearOptions.Target, Color.CornflowerBlue, 0, 0);

            // Our player and enemy are both actually just text strings.
            var spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin(samplerState: SamplerState.PointWrap);
            Rectangle source = new Rectangle(0, 0, ScreenManager.GraphicsDevice.Viewport.Width, ScreenManager.GraphicsDevice.Viewport.Height);
            spriteBatch.Draw(_darkPurpleBackground, Vector2.Zero, source, Color.White, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);
            spriteBatch.End();

        }
    }
}
