using RpgGame.NetStandard.GameInit;
using RpgGame.NetStandard.Model.DataBase;
using RpgGame.NetStandard.Model.Enums;
using RpgGame.NetStandard.Model.Exceptions;

namespace RpgGame.NetStandard.Core.GameLogic
{
    public static class ItemHelpers
    {
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
            var itemInfo = Startup.MyGameData.ItemList[item];
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
            var itemInfo = Startup.MyGameData.ItemList[item];
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
