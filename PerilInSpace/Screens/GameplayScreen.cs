using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Newtonsoft.Json;
using PerilInSpace.Collisions;
using PerilInSpace.Sprites;
using PerilInSpace.StateManagement;

namespace PerilInSpace.Screens
{
    
    public class GameplayScreen : GameScreen
    {
        //META GAME SERVICES
        private Game _game;
        private ContentManager _content;
        private SpriteBatch _spriteBatch;
        private Settings _settings;

        //FONTS
        SpriteFont font;
        SpriteFont endGamefont;

        //SOUNDS
        private SoundEffect playerHitSound;
        private SoundEffect firingLasersSound;
        public Song backgroundMusic;

        //TEXTURES
        private Texture2D _purpleGameBackground;
        private Texture2D _blueGameBackground;
        private Texture2D _darkPurpleBackground;
        private Texture2D parallaxBG;

        public Texture2D asteroidTexture1;
        public Texture2D asteroidTexture2;
        public Texture2D asteroidTexture3;
        public Texture2D asteroidTexture4;

        //List<Texture2D> asteroidTextures = new List<Texture2D>();
        public Texture2D boundingTexture;


        //VARIABLES
        public bool resetFlag = false;
        public bool running = false;

        public MouseState _currentMouse;
        public MouseState _previousMouse;

        private KeyboardState _keyboardState;
        public bool previousCollision;
        public bool currentCollision;
        private List<Sprite> _sprites;
        //List<Sprite> asteroids = new List<Sprite>();
        List<Asteroid> asteroidList = new List<Asteroid>();
        public float distance;
        public int score = 0;
        public DateTime targetTime;
        private bool isMainScreen = false;
        private bool buffer = true;


        //PARTICLE SYSTEMS
        RainParticleSystem _rain;
        ExplosionParticleSystem _explosions;
        FireworkParticleSystem _fireworks;


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

            _settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(Globals.FILE_NAME));


            

            //CREATE PARTICLE SYSTEMS AND GAME BACKGROUND
            //_rain = new RainParticleSystem(_game, new Rectangle(0, -20, ScreenManager.GraphicsDevice.Viewport.Width, 10));
            //_game.Components.Add(_rain);
            _explosions = new ExplosionParticleSystem(_game, 20);
            _game.Components.Add(_explosions);
            _explosions.Visible = false;
            _fireworks = new FireworkParticleSystem(_game, 20);
            _game.Components.Add(_fireworks);


            //LOAD TEXTURES & SOUNDS
            font = _content.Load<SpriteFont>("myfont");
            endGamefont = _content.Load<SpriteFont>("endGameFont");
            boundingTexture = _content.Load<Texture2D>("bounding");
            backgroundMusic = _content.Load<Song>("music");

            playerHitSound = _content.Load<SoundEffect>("sfx_lose");
            firingLasersSound = _content.Load<SoundEffect>("sfx_laser1");
            parallaxBG = _content.Load<Texture2D>("parallax_bg");
            _blueGameBackground = _content.Load<Texture2D>("Backgrounds/blue");
            _purpleGameBackground = _content.Load<Texture2D>("Backgrounds/purple");
            _darkPurpleBackground = _content.Load<Texture2D>("Backgrounds/darkPurple");

            asteroidTexture1 = _content.Load<Texture2D>("asteroid1");
            asteroidTexture2 = _content.Load<Texture2D>("asteroid2");
            asteroidTexture3 = _content.Load<Texture2D>("asteroid3");
            asteroidTexture4 = _content.Load<Texture2D>("asteroid4");
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


            DateTime currentTime = DateTime.Now;
            _keyboardState = Keyboard.GetState();

            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();



            if (!isMainScreen)
            {
                if (_keyboardState.IsKeyDown(Keys.Space))
                {

                    isMainScreen = true;
                    running = true;

                    startGame();
                }
                buffer = false;
            }

            if (isMainScreen)
            {
                if (currentTime.TimeOfDay > targetTime.TimeOfDay)
                {
                    running = false;
                }

                //Checking to See if the game has passed its time
                if (running)
                {

                    //Exits the game if escape is pressed
                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                        ExitScreen();

                    UpdateAsteroids();

                    foreach (var sprite in _sprites.ToArray())
                    {
                        sprite.Update(gameTime, _sprites);
                    }

                    for (int i = 0; i < _sprites.Count; i++)
                    {

                        for (int j = 0; j < asteroidList.Count; j++)
                        {
                            if (i > 0 && CollisionHelper.Collides(_sprites[i].bounds, asteroidList[j].bounds))
                            {
                                _explosions.PlaceExplosion(asteroidList[j].position);
                                _sprites.RemoveAt(i);
                                i--;
                                asteroidList.RemoveAt(j);
                                j--;
                                MakeOneAsteroid();
                                score += _settings.pointsPerAsteroid;
                            }

                        }
                        if (_sprites[i].IsRemoved)
                        {
                            _sprites.RemoveAt(i);
                            i--;
                        }
                    }
                }
                if (!running)
                {
                    if ((int)gameTime.TotalGameTime.TotalSeconds % 3 == 0)
                    {
                        var width = ScreenManager.GraphicsDevice.Viewport.Width * 0.90;
                        var height = ScreenManager.GraphicsDevice.Viewport.Height * 0.90;
                        _fireworks.PlaceFirework(new Vector2(RandomHelper.Next(50, (int)width), RandomHelper.Next(50, (int)height)));
                        _fireworks.PlaceFirework(new Vector2(RandomHelper.Next(50, (int)width), RandomHelper.Next(50, (int)height)));
                    }
                    if (_keyboardState.IsKeyDown(Keys.R))
                    {
                        running = true;
                        startGame();
                    }
                }
            }

            
        }

        // Unlike the Update method, this will only be called when the gameplay screen is active.


        public override void Draw(GameTime gameTime)
        {
            // Our player and enemy are both actually just text strings.
            var spriteBatch = ScreenManager.SpriteBatch;
            Matrix transform;
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target, Color.White, 0, 0);

            if (!isMainScreen)
            {
                ScreenManager.SpriteBatch.Begin(samplerState: SamplerState.PointWrap);
                Rectangle source = new Rectangle(0, 0, ScreenManager.GraphicsDevice.Viewport.Width, ScreenManager.GraphicsDevice.Viewport.Height);
                ScreenManager.SpriteBatch.Draw(_darkPurpleBackground, Vector2.Zero, source, Color.White, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);
                int stringHeight = 24;
                int stringWidth = 220;
                spriteBatch.DrawString(font, "Press Space to Start", new Vector2(Globals.SCREEN_WIDTH / 2 - stringWidth, Globals.SCREEN_HEIGHT / 2 - stringHeight), Color.White, 0, new Vector2(0, 0), 3.0f, SpriteEffects.None, 0f);
                ScreenManager.SpriteBatch.End();
            }


            if (isMainScreen)
            {
                if (_sprites != null)
                {
                    transform = Matrix.CreateTranslation((_sprites[0].Position.X) * 0.10f, (_sprites[0].Position.Y) * 0.10f, 0);
                    spriteBatch.Begin(transformMatrix: transform, samplerState: SamplerState.PointWrap);
                    spriteBatch.Draw(parallaxBG, new Rectangle(-1000, -1000, 3000, 3000), Color.White);
                    spriteBatch.End();
                }

                if (running)
                {
                    //THIS SPRITE BATCH BEGIN/END DRAWS THE BACKGROUND AND TEXT
                    spriteBatch.Begin(samplerState: SamplerState.PointWrap);
                    Rectangle source = new Rectangle(0, 0, ScreenManager.GraphicsDevice.Viewport.Width, ScreenManager.GraphicsDevice.Viewport.Height);
                    double showTime = (targetTime - DateTime.Now).TotalSeconds;
                    spriteBatch.DrawString(font, "Score: " + score, new Vector2(5, 5), Color.White, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.5f);
                    spriteBatch.DrawString(font, "Time Remaining: " + showTime.ToString("#.00"), new Vector2(5, 25), Color.White, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.5f);
                    spriteBatch.End();

                    //THIS SPRITE BATCH BEGIN/END DRAWS THE SPRITES
                    spriteBatch.Begin();
                    foreach (Asteroid a in asteroidList)
                    {
                        spriteBatch.Draw(a.texture, a.position, null, Color.White, a.rotation, new Vector2(a.radius / 2, a.radius / 2), a.scale, SpriteEffects.None, 1.0f);
                    }

                    foreach (var sprite in _sprites)
                    {
                        sprite.Draw(spriteBatch);
                    }

                    spriteBatch.End();
                }
                if (!running)
                {
                    spriteBatch.Begin();
                    var viewport = ScreenManager.GraphicsDevice.Viewport;
                    Rectangle source = new Rectangle(0, 0, viewport.Width, viewport.Height);
                    spriteBatch.Draw(_purpleGameBackground, Vector2.Zero, source, Color.White, 0, Vector2.Zero, 3.0f, SpriteEffects.None, 0.01f);
                    spriteBatch.DrawString(endGamefont, "Score: " + score, new Vector2(Globals.SCREEN_WIDTH / 2 - endGamefont.MeasureString("Score: " + score.ToString()).X, 150), Color.White, 0, new Vector2(0, 0), 2.0f, SpriteEffects.None, 0f);
                    spriteBatch.DrawString(font, "Press R to restart game", new Vector2(Globals.SCREEN_WIDTH / 2 - 246, 250), Color.White, 0, new Vector2(0, 0), 3.0f, SpriteEffects.None, 0f);

                    spriteBatch.End();
                }
            }


        }

        public void startGame()
        {
            //UPDATE SETTINGS TO MATCH SETTINGS FILE
            
            //Set the music to the volume of the other menu
            MediaPlayer.Volume = (float)_settings.volume / 10;
            //Create the ship and the bullet
            
            var shipTexture = _content.Load<Texture2D>("playerShip");
            _sprites = new List<Sprite>()
            {
                new Ship(shipTexture, _content, _settings.volume)
                {
                    Position = new Vector2(400,250),
                    Bullet = new Bullet(_content.Load<Texture2D>("bullet")),
                    Volume = _settings.volume
                    
                }
            };

            asteroidList.Clear();
            InitializeAsteroids();
            score = 0;

            //Grab the target time
            targetTime = DateTime.Now.AddSeconds(_settings.timeLimit);

            //Set the background music
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(backgroundMusic);
            _explosions.Visible = true;
        }

        private void MakeOneAsteroid()
        {
            Random random = new Random();
            int textureChooser = random.Next(1, 4);
            Asteroid temp;
            if (textureChooser == 1)
            {
                temp = new Asteroid(asteroidTexture1, 1);
            }
            else if (textureChooser == 2)
            {
                temp = new Asteroid(asteroidTexture2, 2);
            }
            else if (textureChooser == 3)
            {
                temp = new Asteroid(asteroidTexture3, 3);
            }
            else
            {
                temp = new Asteroid(asteroidTexture4, 4);
            }

            asteroidList.Add(temp);

        }
        private void InitializeAsteroids()
        {
            Random random = new Random();

            //Initial Asteroids Creation
            for (int i = 0; i < random.Next(10, 20); i++)
            {
                int textureChooser = random.Next(1, 4);
                Asteroid temp;
                if (textureChooser == 1)
                {
                    temp = new Asteroid(asteroidTexture1, 1);
                }
                else if (textureChooser == 2)
                {
                    temp = new Asteroid(asteroidTexture2, 2);
                }
                else if (textureChooser == 3)
                {
                    temp = new Asteroid(asteroidTexture3, 3);
                }
                else
                {
                    temp = new Asteroid(asteroidTexture4, 4);
                }

                asteroidList.Add(temp);

            }
        }
        public int j = 0;
        private void UpdateAsteroids()
        {
            for (int i = 0; i < asteroidList.Count; i++)
            {

                asteroidList[i].Update();

                if (running && CollisionHelper.Collides(_sprites[0].shipBounds, asteroidList[i].bounds))
                {

                    asteroidList.RemoveAt(i);
                    i--;
                    score -= _settings.pointDeductionIfHit;
                    MakeOneAsteroid();
                    _sprites[0].color = Color.Red;

                    playerHitSound.Play((float)_settings.volume / 10, 0, 0);

                }
            }
            j++;
            if (j == 10 && running)
            {
                _sprites[0].color = Color.White;
                j = 0;
            }

        }
    }
}
