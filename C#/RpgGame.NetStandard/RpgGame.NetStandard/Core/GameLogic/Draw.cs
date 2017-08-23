using System;
using System.Collections.Generic;
using RpgGame.NetStandard.GameInit;
using RpgGame.NetStandard.Model.DataBase;
using RpgGame.NetStandard.Model.Enums;
using RpgGame.NetStandard.Model.Exceptions;
using RpgGame.NetStandard.Model.Wepon;

namespace RpgGame.NetStandard.Core.GameLogic
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
            var ranNum = Startup.Ran.Next(1, (int)MaxNum + 1);
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
            if (Startup.MyGameData.ItemList[chest].Count < needKeyCount)
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
            Startup.MyGameData.WeponList.AddRange(openResult.WeponList);
            chest.AddItem(-count);
            ItemEntity.ChestKey.UseItemActAndCheck(-(int)chest.GetItemAttr().Data * count, null);
            return openResult;
        }
        private static ChestResult OpenChest(PropType propLevel, ChestResult chestResult)
        {
            var result = GetChectResult(propLevel);
            var isMulti = Startup.Ran.Next(3) == 1;
            Action<PropType> addItem = (itemProb) =>
            {
                var itemType = ItemPropList[itemProb][Startup.Ran.Next(0, ItemPropList[itemProb].Count)];
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
                    var itemProb = (PropType)Startup.Ran.Next(1, maxProbLevel + 1);
                    maxProbLevel -= itemProb.GetHashCode();
                    addItem(itemProb);
                }
            }
            else
            {
                var isWepon = Startup.Ran.Next(2) == 1;
                if (isWepon)
                {
                    chestResult.WeponList.Add(new WeponInfo(result, Startup.MyGameData.PlayerLevel));
                }
                else
                {
                    addItem(result);
                }
            }
            return chestResult;
        }
    }

}



