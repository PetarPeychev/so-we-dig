using SoWeDig.Data;
using SoWeDig.Map;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoWeDig
{
    public class Tile
    {
        public Texture Foreground { get; }
        public Texture Background { get; }

        public Tile()
        {
            Foreground = Texture.Air;
            Background = Texture.Air;
        }

        public Tile(Texture foreground, Texture background)
        {
            Foreground = foreground;
            Background = background;
        }
    }
}
