using System;

namespace SoWeDig
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using var game = new SoWeDig();
            game.Run();
        }
    }
}
