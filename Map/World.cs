using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SoWeDig.Data;
using SoWeDig.Map;
using SoWeDig.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoWeDig
{
    class World
    {
        public int Width { get; }
        public int Height { get; }
        public Player Player { get; }

        private Tile[,] tiles;

        public World(int width, int height)
        {
            Width = width;
            Height = height;
            Player = new Player(GenerateSpawnPoint(), "John");
            tiles = GenerateWorld(width, height);
        }

        public void Update(GameTime gameTime, KeyboardState keyboard)
        {
            Player.Update(gameTime, keyboard);
        }

        public void Draw(SpriteBatch spriteBatch, SpriteEngine spriteEngine)
        {
            int playerX = (int)Player.Position.X / Settings.TILE_SIZE;
            int playerY = (int)Player.Position.Y / Settings.TILE_SIZE;

            // Draw background.
            for (int x = playerX - 1000; x < playerX + 1000; x++)
            {
                for (int y = playerY - 700; y < playerY + 700; y++)
                {
                    if (y < 0 || y >= Height || x < 0 || x >= Width)
                    {
                        continue;
                    }
                            
                    Vector2 pixelPosition = new Vector2(x * Settings.TILE_SIZE, y * Settings.TILE_SIZE);
                    spriteEngine.Draw(tiles[Math.Min(y, 0), Math.Min(x, 0)].Background, spriteBatch, pixelPosition);
                }
            }

            // Draw foreground.
            for (int x = playerX - 1000; x < playerX + 1000; x++)
            {
                for (int y = playerY - 700; y < playerY + 700; y++)
                {
                    if (y < 0 || y >= Height || x < 0 || x >= Width)
                    {
                        continue;
                    }

                    Vector2 pixelPosition = new Vector2(x * Settings.TILE_SIZE, y * Settings.TILE_SIZE);
                    spriteEngine.Draw(tiles[y, x].Foreground, spriteBatch, pixelPosition);
                }
            }
 
            // Draw player.
            Player.Draw(spriteBatch, spriteEngine);
        }

        private Vector2 GenerateSpawnPoint()
        {
            int centerY = (Height / 2) * Settings.TILE_SIZE;
            int centerX = (Width / 2) * Settings.TILE_SIZE;

            return new Vector2(centerX, centerY);
        }

        private static Tile[,] GenerateWorld(int width, int height)
        {
            Tile[,] tiles = new Tile[height, width];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    tiles[y, x] = new Tile(Data.Texture.Dirt, Data.Texture.Air);
                }
            }

            return tiles;
        }
    }
}
