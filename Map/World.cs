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
            for (int x = playerX - 100; x < playerX + 100; x++)
            {
                for (int y = playerY - 100; y < playerY + 100; y++)
                {
                    if (y < 0 || y >= Height || x < 0 || x >= Width)
                    {
                        continue;
                    }
                            
                    Vector2 pixelPosition = new Vector2(x * Settings.TILE_SIZE, y * Settings.TILE_SIZE);

                    spriteEngine.Draw(GetBackgroundTexture(x, y), spriteBatch, pixelPosition);
                }
            }

            // Draw foreground.
            for (int x = playerX - 100; x < playerX + 100; x++)
            {
                for (int y = playerY - 100; y < playerY + 100; y++)
                {
                    if (y < 0 || y >= Height || x < 0 || x >= Width)
                    {
                        continue;
                    }

                    Vector2 pixelPosition = new Vector2(x * Settings.TILE_SIZE, y * Settings.TILE_SIZE);

                    spriteEngine.Draw(GetForegroundTexture(x, y), spriteBatch, pixelPosition);
                }
            }
 
            // Draw player.
            Player.Draw(spriteBatch, spriteEngine);
        }

        private Data.Texture GetForegroundTexture(int x, int y)
        {
            Foreground foreground = tiles[y, x].Foreground;

            bool top = tiles[Math.Max(y - 1, 0), x].Foreground == foreground;
            bool bottom = tiles[Math.Min(y + 1, Height - 1), x].Foreground == foreground;
            bool left = tiles[y, Math.Max(x - 1, 0)].Foreground == foreground;
            bool right = tiles[y, Math.Min(x + 1, Width - 1)].Foreground == foreground;

            int type = 16;

            if (top && bottom && left && right) { type = 6; }

            else if (top && bottom && left) { type = 7; }
            else if (top && bottom && right) { type = 5; }
            else if (top && left && right) { type = 10; }
            else if (bottom && left && right) { type = 2; }

            else if (top && bottom) { type = 8; }
            else if (top && left) { type = 11; }
            else if (bottom && left) { type = 3; }

            else if (top && right) { type = 9; }
            else if (bottom && right) { type = 1; }

            else if (left && right) { type = 14; }

            else if (top) { type = 12; }
            else if (bottom) { type = 4; }
            else if (left) { type = 15; }
            else if (right) { type = 13; }


            if (foreground == Foreground.Dirt)
            {
                return Data.Texture.Dirt1 + (type - 1);
            }
            else if (foreground == Foreground.Stone)
            {
                return Data.Texture.Stone1 + (type - 1);
            }
            else if (foreground == Foreground.Granite)
            {
                return Data.Texture.Granite1 + (type - 1);
            }
            else
            {
                return Data.Texture.Air;
            }
        }

        private Data.Texture GetBackgroundTexture(int x, int y)
        {
            Background background = tiles[y, x].Background;

            bool top = tiles[Math.Max(y - 1, 0), x].Background == background;
            bool bottom = tiles[Math.Min(y + 1, Height - 1), x].Background == background;
            bool left = tiles[y, Math.Max(x - 1, 0)].Background == background;
            bool right = tiles[y, Math.Min(x + 1, Width - 1)].Background == background;

            int type = 16;

            if (top && bottom && left && right) { type = 6; }

            else if (top && bottom && left) { type = 7; }
            else if (top && bottom && right) { type = 5; }
            else if (top && left && right) { type = 10; }
            else if (bottom && left && right) { type = 2; }

            else if (top && bottom) { type = 8; }
            else if (top && left) { type = 11; }
            else if (bottom && left) { type = 3; }

            else if (top && right) { type = 9; }
            else if (bottom && right) { type = 1; }

            else if (left && right) { type = 14; }

            else if (top) { type = 12; }
            else if (bottom) { type = 4; }
            else if (left) { type = 15; }
            else if (right) { type = 13; }


            if (background == Background.DirtWall)
            {
                return Data.Texture.DirtWall1 + (type - 1);
            }
            if (background == Background.StoneWall)
            {
                return Data.Texture.StoneWall1 + (type - 1);
            }
            else if (background == Background.GraniteWall)
            {
                return Data.Texture.GraniteWall1 + (type - 1);
            }
            else
            {
                return Data.Texture.Air;
            }
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

            Random r = new Random();

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int randomInt = r.Next(y - 5, y);

                    if (randomInt <= 33)
                    {
                        tiles[y, x] = new Tile(Foreground.Dirt, Background.DirtWall);
                    }
                    else if (randomInt > 33 && randomInt <= 66)
                    {
                        tiles[y, x] = new Tile(Foreground.Stone, Background.StoneWall);
                    }
                    else if (randomInt > 66)
                    {
                        tiles[y, x] = new Tile(Foreground.Granite, Background.GraniteWall);
                    }

                    if (r.Next(0, 10) > 3)
                    {
                        tiles[y, x].Foreground = Foreground.Air;
                    }

                }
            }

            return tiles;
        }
    }
}
