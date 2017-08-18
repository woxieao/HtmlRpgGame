using System.Collections.Generic;
using RpgGame.NetStandard.Core;
using RpgGame.NetStandard.Model.Enums;
using RpgGame.NetStandard.Model.Item;
using RpgGame.NetStandard.Model.Player;
using RpgGame.NetStandard.Model.Wepon;

namespace RpgGame.NetStandard.Model.DataBase
{
    public class GameData
    {
        public static double Gold;
        public static LanguageType LanType;

        public static void AddItem(ItemEntity item, int count)
        {
            ItemList[item].Count += count;
        }

        public static readonly Dictionary<ItemEntity, ItemHandler> ItemList
            = new Dictionary<ItemEntity, ItemHandler>
            {
                {
                    ItemEntity.RedMedicine, new ItemHandler
                    {
                        UseItemAct = (p, count) =>
                        {
                            var player = (PlayerBase) p;
                            for (var i = 0; i < count && player.CurrentHp <= player.MaxHp; i++)
                            {
                                AddItem(ItemEntity.RedMedicine, -1);
                                player.CurrentHp += player.MaxHp * ItemEntity.RedMedicine.GetItemAttr().Data;
                            }
                        }
                    }
                },
                {
                    ItemEntity.ForgeStone,
                    new ItemHandler
                    {
                        UseItemAct = (p, count) =>
                        {
                            var prop = (Prop) p;
                            for (var i = 0;i < count &&prop.ForgeProbability(i) < 100;i++)
                            {
                                AddItem(ItemEntity.ForgeStone, -1);
                            }
                        }
                    }
                },
            };

        static GameData()
        {
            LanType = LanguageType.Cn;
            Gold = 0;
        }

        public static void ChangeLanguage(LanguageType lanType)
        {
            LanType = lanType;
        }
    }
}
