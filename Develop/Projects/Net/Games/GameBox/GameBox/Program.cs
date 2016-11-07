using System;

namespace GameBox
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (GameBox game = new GameBox())
            {
                game.Run();
            }
        }
    }
#endif
}

