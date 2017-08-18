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
                        UseItemAct = p =>
                        {
                            var player = (PlayerBase) p;
                            if (player.CurrentHp <= player.MaxHp)
                            {
                                AddItem(ItemEntity.RedMedicine, 1);
                            }
                            else
                            {
                                player.CurrentHp += player.MaxHp * ItemEntity.RedMedicine.GetItemAttr().Data;
                            }
                        }
                    }
                },
                {
                    ItemEntity.ForgeStone,
                    new ItemHandler
                    {
                        UseItemAct = p =>
                        {
                            var prop = (Prop) p;
                            if (prop.ForgeProbability >= 100)
                            {
                                AddItem(ItemEntity.ForgeStone, 1);
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
