using System;

namespace Heal
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            using (HealGame game = new HealGame())
            {
                HealGame.Game = game;
                game.Run();
            }
        }
    }
}

