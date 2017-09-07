using System.Collections.Generic;
using RpgGame.NetStandard.GameInit;
using RpgGame.NetStandard.Model.Attributes;
using RpgGame.NetStandard.Model.Enums;
using RpgGame.NetStandard.Model.Player;
using RpgGame.NetStandard.Model.Wepon;

namespace RpgGame.NetStandard.Model.DataBase
{
    public class GameData
    {
        internal GameData()
        {
        }

        [DataField]
        public long Gold { get; set; }

        [DataField]
        public LanguageType LanType { get; set; }

        [DataField] public List<WeponInfo> WeponList = new List<WeponInfo>();

        [DataField]
        public Dictionary<ItemEntity, int> ItemCounter { get; set; } = new Dictionary<ItemEntity, int>();

        [DataField]
        public long PlayerExp { get; set; }

        [DataField]
        public PlayerBase Player { get; set; }

        public long PlayerLevel => PlayerExp / Config.PersonLevelUp.EveryLevelNeedsExp;
    }
}
