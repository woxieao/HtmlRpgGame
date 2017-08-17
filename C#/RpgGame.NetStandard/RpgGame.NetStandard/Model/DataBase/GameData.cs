using System;
using System.Collections.Generic;
using System.Linq;
using RpgGame.NetStandard.Model.Language;

namespace RpgGame.NetStandard.Model.DataBase
{
    public class Counter
    {
        public Counter()
        {
            Count = 0;
        }

        public object Base { get; set; }
        public int Count { get; internal set; }
    }
    public sealed class GameData
    {
        public static double Gold;
        public static LanguageType LanType;
        public static readonly List<Counter> ItemList;
        static GameData()
        {
            ItemList = new List<Counter>();
            LanType = LanguageType.Cn;
            Gold = 0;
        }
        public static void ChangeLanguage(LanguageType lanType)
        {
            LanType = lanType;
        }

        public static Counter GetItem<T>(T item2Get)
        {
            var itemInfo = ItemList.SingleOrDefault(i => i.Base.GetType() == item2Get.GetType());
            if (itemInfo == null)
            {
                itemInfo = new Counter
                {
                    Base = item2Get,
                };
                ItemList.Add(itemInfo);
            }
            return itemInfo;
        }
    }
}
