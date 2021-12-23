using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SoWeDig.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoWeDig.Utilities
{
    public class SpriteEngine
    {
        private readonly Dictionary<Data.Texture, Texture2D> Textures;

        public SpriteEngine(ContentManager content)
        {
            Textures = new Dictionary<Data.Texture, Texture2D>();
            foreach (Data.Texture texture in Enum.GetValues(typeof(Data.Texture)))
            {
                Textures[texture] = content.Load<Texture2D>("Textures/" + texture.ToString());
            }
        }

        public void Draw(Data.Texture texture, SpriteBatch spriteBatch, Vector2 location)
        {
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, Settings.TILE_SIZE, Settings.TILE_SIZE);
            spriteBatch.Draw(Textures[texture], destinationRectangle, Color.White);
        }

        public void Draw(Data.Texture texture, SpriteBatch spriteBatch, Vector2 location, int width, int height)
        {
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);
            spriteBatch.Draw(Textures[texture], destinationRectangle, Color.White);
        }
    }
}
