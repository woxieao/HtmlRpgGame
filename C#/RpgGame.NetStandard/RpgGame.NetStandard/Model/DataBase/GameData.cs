using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RpgGame.NetStandard.Model.Item;
using RpgGame.NetStandard.Model.Language;

namespace RpgGame.NetStandard.Model.DataBase
{

    public static class GameData
    {
        public static double Gold;
        public static LanguageType LanType;
        public static readonly Dictionary<Type, ItemCounter> ItemList;
        static GameData()
        {
            ItemList = new Dictionary<Type, ItemCounter>();
            LanType = LanguageType.Cn;
            Gold = 0;
        }
        public static void ChangeLanguage(LanguageType lanType)
        {
            LanType = lanType;
        }

    }
}
