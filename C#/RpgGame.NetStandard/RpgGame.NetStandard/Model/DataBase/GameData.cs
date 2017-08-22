using System.Collections.Generic;
using RpgGame.NetStandard.Core;
using RpgGame.NetStandard.Model.Enums;
using RpgGame.NetStandard.Model.Item;
using RpgGame.NetStandard.Model.Player;
using RpgGame.NetStandard.Model.Wepon;
using RpgGame.NetStandard.StartUp;

namespace RpgGame.NetStandard.Model.DataBase
{
    public class GameData
    {
        public static double Gold;
        public static LanguageType LanType;

        public static readonly Dictionary<ItemEntity, ItemHandler> ItemList
            = new Dictionary<ItemEntity, ItemHandler>
            {
                {
                    ItemEntity.RedMedicine, new ItemHandler((p, count) =>
                        {
                            var player = (PlayerBase) p;
                            for (var i = 0; i < count && player.CurrentHp <= player.MaxHp; i++)
                            {
                                ItemEntity.RedMedicine.AddItem(-1);
                                player.CurrentHp += player.MaxHp * ItemEntity.RedMedicine.GetItemAttr().Data;
                            }
                        })
                },
                {
                    ItemEntity.ForgeStone,
                    new ItemHandler((p, count) =>
                        {
                            var prop = (Prop) p;
                            for (var i = 0;i < count &&prop.ForgeProbability(i) < 100;i++)
                            {
                                ItemEntity.ForgeStone.AddItem( -1);
                            }
                        })
                },
                {
                    ItemEntity.Chest1, new ItemHandler((gameData,count)=>
                    {

                    })
                }
            };
        public static PlayerBase Player;
        public static int PlayerExp;
        public static int PlayerLevel => PlayerExp / Config.PersonLevelUp.EveryLevelNeedsExp;
        static GameData()
        {
            LanType = LanguageType.Cn;
            Gold = 0;
            Player = new Lee();
            PlayerExp = 0;
        }

        public static void ChangeLanguage(LanguageType lanType)
        {
            LanType = lanType;
        }
    }
}
