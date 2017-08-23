using System;
using RpgGame.NetStandard.Core.GameLogic;
using RpgGame.NetStandard.Model.DataBase;

namespace RpgGame.NetStandard.GameInit
{
    public class Startup
    {
        public static readonly Random Ran = new Random();
        public static readonly GameData MyGameData = new GameData();
        public static IInteractive MyInteractiver;
        public static IDataHandler MyDataHandler;
        public Startup(IInteractive interactiver, IDataHandler dataHandler)
        {
            MyInteractiver = interactiver;
            MyDataHandler = dataHandler;
        }
    }
}
