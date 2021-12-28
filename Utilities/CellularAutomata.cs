using System;
using System.Collections.Generic;
using System.Text;

namespace SoWeDig.Utilities
{
    class CellularAutomata
    {
        private float initialDensity;
        private int steps;
        private int lonelinessLimit;

        public CellularAutomata(float initialDensity, int steps, int lonelinessLimit)
        {
            this.initialDensity = initialDensity;
            this.steps = steps;
            this.lonelinessLimit = lonelinessLimit;
        }

        public bool[,] GenerateTerrain(int width, int height, int seed)
        {

        }
    }
}
