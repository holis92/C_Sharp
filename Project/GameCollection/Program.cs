using System;

namespace GameCollection
{
    class Program
    {
        static void Main(string[] args)
        {
            // public void Launch() {
            BaseBall baseball = new BaseBall();
            while (baseball.Lobby() != 3) // 3 means EXIT the game
            {
                baseball.Initialize();
                if (baseball.mode == BaseBall.MODE.Solo)
                {
                    baseball.SoloPlay();
                }
                else
                {
                    //baseball.vsAIPlay();
                }
            }
            // }
        }
    }
}
