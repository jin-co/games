﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoShooting
{
    class Page
    {
        private string _currentPage;
        private string[] pageMainNames = { 
            "PageMain/main_start", "PageMain/main_help", "PageMain/main_about" };
        private int _pageIdx = 0;
        private double _timer = 1;

        SpriteFont gameFont;
        

        public ContentManager Content { get; set; }
        public Texture2D Sprite { get; set; }


        public Page(ContentManager content)
        {
            Content = content;
            _currentPage = "PageMain/main_start";
        }

        public void Load()
        {
            Sprite = Content.Load<Texture2D>($"Assets/{_currentPage}");
            //test
            gameFont = Content.Load<SpriteFont>("Assets/timerFont");
        }

        public void PageUpdate(GameTime gameTime)
        {
            KeyboardState kState = Keyboard.GetState();            
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _timer -= elapsedTime;
            if (_timer < 0.8)
            {
                if (kState.IsKeyDown(Keys.Left))
                {
                    if (_pageIdx > 0)
                    {
                        _pageIdx--;
                    }
                    else
                    {
                        _pageIdx = pageMainNames.Length - 1;
                    }
                    _currentPage = pageMainNames[_pageIdx];                    
                }

                if (kState.IsKeyDown(Keys.Right))
                {
                    if (_pageIdx < pageMainNames.Length - 1)
                    {
                        _pageIdx++;
                    }
                    else
                    {
                        _pageIdx = 0;
                    }                    
                    _currentPage = pageMainNames[_pageIdx];                    
                }

                if (kState.IsKeyDown(Keys.Enter))
                {
                    if (_pageIdx == 0)
                    {
                        GameController.StartGame = true;
                    }
                    else
                    {
                        _currentPage = pageMainNames[_pageIdx].Split('_')[1];                        
                    }
                }
                Sprite = Content.Load<Texture2D>($"Assets/{_currentPage}");
                _timer = 1;
            }           
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, new Vector2(0, 0), Color.White);
            spriteBatch.DrawString(gameFont, _pageIdx.ToString(), new Vector2(0, 0), Color.Black);

        }
    }
}
