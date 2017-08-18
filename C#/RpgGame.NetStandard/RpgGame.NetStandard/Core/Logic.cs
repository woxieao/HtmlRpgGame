using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using RpgGame.NetStandard.Model.DataBase;
using RpgGame.NetStandard.Model.Enums;
using RpgGame.NetStandard.Model.Exceptions;
using RpgGame.NetStandard.Model.Item;

using RpgGame.NetStandard.StartUp;

namespace RpgGame.NetStandard.Core
{
    public class Draw
    {
        public static void OpenChest(PropType propLevel)
        {
            var kind = Singleton.Ran.Next(1, 100);
            var resultKind = 0;
            var minMax = Helpers.GetEnumFirstLast<PropType>();
            for (var i = 1; i < propLevel.GetHashCode(); i++)
            {
                resultKind += propLevel.GetHashCode() >= Singleton.Ran.Next(minMax.Item1, minMax.Item2) ? 1 : 0;
            }
            //70% wepon
            if (kind < 70)
            {

            }
            else
            {

                //var itemList = Assembly.GetAssembly(typeof(ItemInfo)).GetTypes().Where(t => t.IsSubclassOf(typeof(ItemInfo)));
                ////GetAttribute<ItemIntroAttribute>()
                //var dict = new Dictionary<PropType, List<ItemInfo>>();

                //foreach (var item in itemList)
                //{
                //    var itemAttr = item.GetAttribute<ItemIntroAttribute>();
                //    if (dict.TryGetValue(itemAttr.PropLevel, out var rewardList))
                //    {
                //        rewardList.Add(item.in);
                //    }
                //}
            }

        }
    }
    public class DataLogic
    {
        public static void SaveGame()
        {
        }
    }
    public class ItemLogic
    {
        public static void UseItem(ItemEntity item, int useCount, object target)
        {
            if (useCount <= 0)
            {
                throw new MsgException("使用数量不能小于等于0");
            }
            var itemInfo = GameData.ItemList[item];

            if (itemInfo.Count < useCount)
            {
                throw new MsgException("物品数量不足");
            }
            GameData.AddItem(item, -useCount);
            for (var i = 0; i < useCount; i++)
            {
                itemInfo.UseItemAct(target);
            }
        }
        protected void SellItem(ItemEntity item, int sellCount)
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
            var itemInfo = GameData.ItemList[item];
            if (itemInfo.Count >= sellCount)
            {
                GameData.AddItem(item, -sellCount);
                GameData.Gold += sellCount * itemAttr.SellPrice;
            }
            else
            {
                throw new MsgException("物品数量不足");
            }
        }
    }
}
