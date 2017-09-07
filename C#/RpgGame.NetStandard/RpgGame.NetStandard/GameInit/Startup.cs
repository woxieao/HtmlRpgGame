using System;
using Newtonsoft.Json;
using RpgGame.NetStandard.Core.GameLogic;
using RpgGame.NetStandard.Model.DataBase;

namespace RpgGame.NetStandard.GameInit
{
    public class Startup
    {
        public static readonly Random Ran = new Random();
        public static IInteractive MyInteractiver = new SimpleInteractive();
        public static DataHandler MyDataHandler = new DataHandler();
        public static GameData MyGameData = MyDataHandler.InitGameData();
    }
}
