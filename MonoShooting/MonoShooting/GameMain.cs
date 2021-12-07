﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

namespace MonoShooting
{
    public class GameMain : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        int screenWidth = 1400;
        int screenHeight = 950;

        // sprites
        Texture2D gameoverSprite;
        Texture2D gameoverBackSprite;       
        Texture2D ground;
        Texture2D bullet;
        Texture2D collisionEffectSprite;        
       
        //test
        Biker biker;
        Page page;
        Ladder ladder;

        //test
        GameController controller = new GameController();
        Vector2 collisionPoint;

        //timer
        SpriteFont timerFont;

        double timer = 1;
        public GameMain()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = screenWidth;
            _graphics.PreferredBackBufferHeight = screenHeight;
            _graphics.ApplyChanges();

            biker = new Biker(Content);
            page = new Page(Content);
            ladder = new Ladder(Content);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            ground = Content.Load<Texture2D>("Assets/ground");
            bullet = Content.Load<Texture2D>("Assets/Enemies/bullet");
            collisionEffectSprite = Content.Load<Texture2D>("Assets/effect_collision");            
            gameoverBackSprite = Content.Load<Texture2D>("Assets/gameover_back");
            gameoverSprite = Content.Load<Texture2D>("Assets/gameover");
            timerFont = Content.Load<SpriteFont>("Assets/timerFont");
            biker.BikerLoad();
            page.Load();
            ladder.Load();
            Sounds.BackgroundMusic = Content.Load<Song>("Assets/Sounds/sound_back");
            Sounds.BackgroundMusicEnd = Content.Load<Song>("Assets/Sounds/sound_back_end");
            Sounds.StageClear = Content.Load<SoundEffect>("Assets/Sounds/sound_clear");
            Sounds.Dead = Content.Load<SoundEffect>("Assets/Sounds/sound_dead");
            Sounds.Hurt = Content.Load<SoundEffect>("Assets/Sounds/sound_hurt_man");
            Sounds.Bullet = Content.Load<SoundEffect>("Assets/Sounds/sound_bullet");
            MediaPlayer.Play(Sounds.BackgroundMusicEnd);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (!GameController.GameStart)
            {
                page.PageUpdate(gameTime);
                MediaPlayer.Play(Sounds.BackgroundMusic);
            }
            else
            {                
                if (!GameController.GameClear)
                {
                    if (!GameController.GameOver)
                    {
                        biker.BikerUpdate(gameTime);
                        controller.Update(gameTime);

                        foreach (var i in controller.bullets)
                        {
                            i.BulletUpdate(gameTime);
                            if (Vector2.Distance(i.position, biker.Position) 
                                >= i.radius + biker.Radius &&
                                Vector2.Distance(i.position, biker.Position) 
                                < (i.radius + biker.Radius) + 5)
                            {
                                Sounds.Bullet.Play();
                            }

                            if (Vector2.Distance(i.position, biker.Position) 
                                < i.radius + biker.Radius)
                            {                                
                                biker.Dead = true;
                                Sounds.Hurt.Play();                                
                                biker.BikerUpdate(gameTime);
                                collisionPoint = new Vector2(
                                    i.position.X - i.radius, i.position.Y - i.radius);
                            }
                        }

                        if (Vector2.Distance(ladder.Position, biker.Position) <= ladder.Radius + biker.Radius)
                        {
                            MediaPlayer.Stop();
                            Sounds.StageClear.Play();
                            GameController.GameLevel++;
                            GameController.GameClear = true;
                        }
                    }
                    else
                    {
                        MediaPlayer.Stop();                        
                        biker.BikerUpdate(gameTime);
                    }
                }
                
            }        
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            // main page
            if (!GameController.GameStart)
            {
                page.Draw(gameTime, _spriteBatch);
            }

            //game play
            if (GameController.GameStart)
            {
                _spriteBatch.Draw(ground, new Vector2(0, 0), Color.White);
                
                // timer
                _spriteBatch.DrawString(timerFont,
                "Time: " + Math.Floor(GameController.TotalTime),
                new Vector2(3, 3), Color.Black);

                // ladder
                ladder.Draw(gameTime, _spriteBatch);

                // plaer
                biker.Draw(gameTime, _spriteBatch);
                foreach (var i in controller.bullets)
                {
                    _spriteBatch.Draw(bullet, new Vector2(i.position.X, i.position.Y + i.radius), Color.White);
                }

                // game over
                if (GameController.GameOver)
                {
                    _spriteBatch.Draw(collisionEffectSprite, collisionPoint, Color.White);

                    timer -= gameTime.ElapsedGameTime.TotalSeconds;
                    if (timer <= 0.5)
                    {
                        _spriteBatch.Draw(gameoverBackSprite, new Vector2(0, 0), Color.White);
                        _spriteBatch.Draw(gameoverSprite, new Vector2((screenWidth / 2) - 250, (screenHeight / 2) - 250), Color.White);
                    }
                }

                // game clear
                if (GameController.GameClear)
                {
                    _spriteBatch.DrawString(
                    timerFont,
                    "Stage" + GameController.GameLevel + " Cleared",
                    new Vector2((screenWidth / 2) - 85, (screenHeight / 2) - 40), Color.Black);
                }
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
