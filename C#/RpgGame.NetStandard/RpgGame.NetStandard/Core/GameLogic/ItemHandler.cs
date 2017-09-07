using System.Collections.Generic;
using System.Linq;
using RpgGame.NetStandard.GameInit;
using RpgGame.NetStandard.Model.Enums;
using RpgGame.NetStandard.Model.Exceptions;
using RpgGame.NetStandard.Model.Item;
using RpgGame.NetStandard.Model.Player;
using RpgGame.NetStandard.Model.Prop;

namespace RpgGame.NetStandard.Core.GameLogic
{
    public static class ItemHelpers
    {
        public static ItemHandler GetItemInfo(this ItemEntity item)
        {
            return ItemListHandler.Single(i => i.ItemType == item);
        }

        private static readonly List<ItemHandler> ItemListHandler = new List<ItemHandler>
        {
            new ItemHandler((p, count) =>
            {
                var player = (PlayerBase) p;
                for (var i = 0; i < count && player.CurrentHp <= player.MaxHp; i++)
                {
                    ItemEntity.RedMedicine.AddItem(-1);
                    player.CurrentHp += player.MaxHp * ItemEntity.RedMedicine.GetItemAttr().Data;
                }
            }, ItemEntity.RedMedicine),
            new ItemHandler((p, count) =>
            {
                var prop = (PropBase) p;
                for (var i = 0; i < count && prop.ForgeProbability(i) < 100; i++)
                {
                    ItemEntity.ForgeStone.AddItem(-1);
                }
            }, ItemEntity.ForgeStone),
            new ItemHandler((gameData, count) => { Draw.OpenChestResult(ItemEntity.Chest1, count); }, ItemEntity.Chest1),
            new ItemHandler((gameData, count) => { Draw.OpenChestResult(ItemEntity.Chest2, count); }, ItemEntity.Chest2),
            new ItemHandler((gameData, count) => { Draw.OpenChestResult(ItemEntity.Chest3, count); }, ItemEntity.Chest3),
            new ItemHandler((gameData, count) => { Draw.OpenChestResult(ItemEntity.Chest4, count); }, ItemEntity.Chest4),
            new ItemHandler((gameData, count) => { Draw.OpenChestResult(ItemEntity.Chest10, count); },
                ItemEntity.Chest10),
            new ItemHandler((gameData, count) => { ItemEntity.ChestKey.AddItem(-count); }, ItemEntity.ChestKey)
            ,
            new ItemHandler((gameData, count) => { }, ItemEntity.PurseLv3)
            ,
            new ItemHandler((gameData, count) => { }, ItemEntity.PurseLv4)
            ,
            new ItemHandler((gameData, count) => { }, ItemEntity.PurseLv5)
            ,
            new ItemHandler((gameData, count) => { }, ItemEntity.WaitForCreate6)
            ,
            new ItemHandler((gameData, count) => { }, ItemEntity.WaitForCreate7)
            ,
            new ItemHandler((gameData, count) => { }, ItemEntity.WaitForCreate8)
            ,
            new ItemHandler((gameData, count) => { }, ItemEntity.WaitForCreate9)
            ,
            new ItemHandler((gameData, count) => { }, ItemEntity.WaitForCreate10)
        };

        /// <summary>
        /// 使用物品并校验
        /// </summary>
        /// <param name="item"></param>
        /// <param name="useCount"></param>
        /// <param name="target"></param>
        public static void UseItemActAndCheck(this ItemEntity item, int useCount, object target)
        {
            if (useCount <= 0)
            {
                throw new MsgException("使用数量不能小于等于0");
            }
            var itemInfo = item.GetItemInfo();
            if (itemInfo.Count < useCount)
            {
                throw new MsgException("道具数量不足");
            }
            itemInfo.UseItemAct(target, useCount);
        }
        public static void SellItem(this ItemEntity item, int sellCount)
        {
            var itemAttr = item.GetItemAttr();
            if (!itemAttr.SellInMarket)
            {
                throw new MsgException("物品不可出售");
            }
            if (sellCount <= 0)
            {
                throw new MsgException("出售数量不能小于等于0");
            }
            var itemInfo = item.GetItemInfo();
            if (itemInfo.Count >= sellCount)
            {
                item.AddItem(-sellCount);
                Startup.MyGameData.Gold += sellCount * itemAttr.SellPrice;
            }
            else
            {
                throw new MsgException("物品数量不足");
            }
        }
    }
}
