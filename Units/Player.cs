using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SoWeDig.Data;
using SoWeDig.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SoWeDig
{
    class Player
    {
        public Vector2 Position { get; internal set; }
        public int Health { get; }
        public int MaxHealth { get; }
        public string Name { get; }

        private float speed = Settings.PLAYER_SPEED * 50;

        public Player(Vector2 position, string name)
        {
            Position = position;
            Health = 100;
            MaxHealth = 100;
            Name = name;
        }

        public void Update(GameTime gameTime, KeyboardState keyboard)
        {
            UpdatePosition(keyboard, gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, SpriteEngine spriteEngine)
        {
            // spriteEngine.Draw(texture, spriteBatch, Position);
        }

        private void UpdatePosition(KeyboardState keyboard, GameTime gameTime)
        {
            Vector2 deltaPlayerPosition = Vector2.Zero;

            if (keyboard.IsKeyDown(Keys.W))
            {
                deltaPlayerPosition += new Vector2(0, -1);
            }
            if (keyboard.IsKeyDown(Keys.A))
            {
                deltaPlayerPosition += new Vector2(-1, 0);
            }
            if (keyboard.IsKeyDown(Keys.S))
            {
                deltaPlayerPosition += new Vector2(0, 1);
            }
            if (keyboard.IsKeyDown(Keys.D))
            {
                deltaPlayerPosition += new Vector2(1, 0);
            }

            Vector2 displacement = Vector2.Multiply(
                deltaPlayerPosition, 
                speed * Settings.TILE_SIZE * (float)gameTime.ElapsedGameTime.TotalSeconds
            );

            Position += displacement;
            Trace.WriteLine(Position);
        }
    }
}
