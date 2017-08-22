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
                WeponList = new List<WeponInfo>();
                ItemList = new Dictionary<ItemEntity, int>();
            }
            public List<WeponInfo> WeponList { get; set; }
            public Dictionary<ItemEntity, int> ItemList { get; set; }
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

        private static PropType GetChectResult(PropType probList)
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

        public static ChestResult OpenChestResult(ItemEntity chest, int count)
        {
            var itemInfo = chest.GetItemAttr();
            var needKeyCount = (int)itemInfo.Data * count;
            if (GameData.ItemList[chest].Count < needKeyCount)
            {
                throw new MsgException($"开启[{count}]个[{itemInfo.Name}]需要[{needKeyCount}]把钥匙,你的钥匙不足");
            }
            var openResult = new ChestResult();
            for (var i = 0; i < count; i++)
            {
                OpenChest(itemInfo.PropLevel, openResult);
            }
            foreach (var item in openResult.ItemList)
            {
                item.Key.AddItem(item.Value);
            }
            GameData.WeponList.AddRange(openResult.WeponList);
            chest.AddItem(-count);
            ItemEntity.ChestKey.AddItem(-(int)chest.GetItemAttr().Data * count);
            return openResult;
        }
        private static ChestResult OpenChest(PropType propLevel, ChestResult chestResult)
        {
            var result = GetChectResult(propLevel);
            var isMulti = Singleton.Ran.Next(3) == 1;
            Action<PropType> addItem = (itemProb) =>
            {
                var itemType = ItemPropList[itemProb][Singleton.Ran.Next(0, ItemPropList[itemProb].Count)];
                if (chestResult.ItemList.ContainsKey(itemType))
                {
                    ++chestResult.ItemList[itemType];
                }
                else
                {
                    chestResult.ItemList[itemType] = 1;
                }
            };
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
                    var itemProb = (PropType)Singleton.Ran.Next(1, maxProbLevel + 1);
                    maxProbLevel -= itemProb.GetHashCode();
                    addItem(itemProb);
                }
            }
            else
            {
                var isWepon = Singleton.Ran.Next(2) == 1;
                if (isWepon)
                {
                    chestResult.WeponList.Add(new WeponInfo(result, GameData.PlayerLevel));
                }
                else
                {
                    addItem(result);
                }
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
    public static class ItemLogic
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
            var itemInfo = GameData.ItemList[item];
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
    public class Output
    {
        public static void Msg(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}



