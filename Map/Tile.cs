using SoWeDig.Data;
using SoWeDig.Map;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoWeDig
{
    public class Tile
    {
        public Foreground Foreground { get; set; }
        public Background Background { get; set; }

        public Tile()
        {
            Foreground = Foreground.Air;
            Background = Background.AirWall;
        }

        public Tile(Foreground foreground, Background background)
        {
            Foreground = foreground;
            Background = background;
        }
    }
}
