using System;
using System.Collections.Generic;
using RpgGame.NetStandard.Model.DataBase;
using RpgGame.NetStandard.Model.Enums;
using RpgGame.NetStandard.Model.Exceptions;
using RpgGame.NetStandard.Model.Wepon;
using RpgGame.NetStandard.StartUp;

namespace RpgGame.NetStandard.Core
{

    public class Draw
    {

        public class ChestResult
        {
            public ChestResult()
            {
                WeponList = new List<Wepon>();
                ItemList = new List<ItemEntity>();
            }
            public List<Wepon> WeponList { get; set; }
            public List<ItemEntity> ItemList { get; set; }
        }
        public static readonly Dictionary<PropType, Dictionary<PropType, double>> ChestProbabilityList = new Dictionary<PropType, Dictionary<PropType, double>>();
        public static readonly Dictionary<PropType, List<ItemEntity>> ItemPropList = new Dictionary<PropType, List<ItemEntity>>();
        static Draw()
        {
            foreach (PropType prob in Enum.GetValues(typeof(PropType)))
            {
                ChestProbabilityList[prob] = PropDrawLogic(prob);
            }

            foreach (ItemEntity item in Enum.GetValues(typeof(ItemEntity)))
            {
                var itemProb = item.GetItemAttr().PropLevel;
                if (ItemPropList.TryGetValue(itemProb, out var itemList))
                {
                    itemList.Add(item);
                }
                else
                {
                    ItemPropList[itemProb] = new List<ItemEntity> { item };
                }
            }
        }
        private const double MaxNum = 1000000000;
        private static double GetProbability(int n)
        {
            return 1.0 / Math.Pow(2, n);
        }
        private static Dictionary<PropType, double> PropDrawLogic(PropType propLevel)
        {
            var probList = new Dictionary<PropType, double>();
            var minMaxPropType = Helpers.GetEnumFirstLast<PropType>();
            var minProp = minMaxPropType.Item1;
            var maxProp = minMaxPropType.Item2;
            var currentPropLevelProbability = 1.0;
            var propLevelHash = propLevel.GetHashCode();
            var baseN = propLevelHash == minProp || propLevelHash == maxProp ? 2 : 3;
            var nIndex = baseN;
            for (var i = propLevelHash + 1; i <= maxProp; i++)
            {
                var problility = GetProbability(nIndex++);
                currentPropLevelProbability -= problility;
                probList[(PropType)i] = problility;
            }
            nIndex = baseN;
            for (var i = propLevelHash - 1; i >= minProp; i--)
            {
                var problility = GetProbability(nIndex++);
                currentPropLevelProbability -= problility;
                probList[(PropType)i] = problility;
            }
            probList[propLevel] = currentPropLevelProbability;
            return probList;
        }

        public static PropType GetChectResult(PropType probList)
        {
            double probility = 0;
            var ranNum = Singleton.Ran.Next(1, (int)MaxNum + 1);
            foreach (var prob in ChestProbabilityList[probList])
            {
                probility += prob.Value * MaxNum;
                if (ranNum <= probility)
                {
                    return prob.Key;
                }
            }
            throw new MsgException("计算概率时出错");
        }

        public static ChestResult OpenChest(PropType propLevel)
        {
            var chestResult = new ChestResult();
            var result = GetChectResult(propLevel);
            var isMulti = Singleton.Ran.Next(3) == 1;

            //炫酷的诅咒宝箱
            if (propLevel == PropType.Lv10)
            {
                result = result == PropType.Lv10 ? PropType.Lv10 : PropType.Lv1;
                isMulti = false;
            }
            if (isMulti)
            {
                var maxProbLevel = result.GetHashCode();
                while (maxProbLevel > 0)
                {
                    var item = Singleton.Ran.Next(1, maxProbLevel + 1);
                    maxProbLevel -= item;
                    chestResult.ItemList.Add(ItemPropList[(PropType)item][Singleton.Ran.Next(0, ItemPropList[(PropType)item].Count)]);
                }
            }
            else
            {
                chestResult.WeponList.Add(new Wepon(result, GameData.PlayerLevel));
            }
            return chestResult;
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
            itemInfo.UseItemAct(target, useCount);
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
                item.AddItem(-sellCount);
                GameData.Gold += sellCount * itemAttr.SellPrice;
            }
            else
            {
                throw new MsgException("物品数量不足");
            }
        }
    }
    public class BattleLogic
    {
        //普通关卡
        //经验关卡,金币关卡,宝箱关卡=>需要10点体力,体力可以喝体力药水来获得 药水开宝箱获得或者每分钟恢复一点,体力上限 每级+1

    }
}



