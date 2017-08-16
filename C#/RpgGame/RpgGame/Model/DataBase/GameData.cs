using System;
using System.Collections.Generic;
using System.Linq;
using RpgGame.Model.Exceptions;
using RpgGame.Model.Language;

namespace RpgGame.Model.DataBase
{
    public class Counter
    {
        public Counter()
        {
            Count = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item2Add"></param>
        /// <param name="count">minus/plus count</param>
        public void AddItem<T>(T item2Add, int count)
        {
            if (Base != null)
            {
                if (Count - count < 0)
                {
                    throw new MsgException("物品数量不足".L());
                }
                Count += count;
            }
            else
            {
                if (count <= 0)
                {
                    throw new MsgException("物品数量需为正数".L());
                }
                Base = item2Add;
                Count = count;
            }
        }
        public dynamic Base { private get; set; }
        public int Count { get; private set; }
    }
    public sealed class GameData
    {
        public static double Gold;
        public static LanguageType LanType;
        public static readonly List<Counter> ItemList;
        static GameData()
        {
            ItemList = new List<Counter>();
        }
        public static Counter GetItem<T>(T item2Get)
        {
            var itemInfo = ItemList.SingleOrDefault(i => i.GetType() == item2Get.GetType());
            if (itemInfo == null)
            {
                itemInfo = new Counter
                {
                    Base = item2Get
                };
                ItemList.Add(itemInfo);
            }
            return itemInfo;
        }
    }
}
