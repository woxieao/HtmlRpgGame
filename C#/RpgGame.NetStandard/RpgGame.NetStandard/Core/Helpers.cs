﻿using System;
using System.Linq;
using System.Reflection;
using RpgGame.NetStandard.Core.GameLogic;
using RpgGame.NetStandard.GameInit;
using RpgGame.NetStandard.Model.Attributes;
using RpgGame.NetStandard.Model.Enums;
using RpgGame.NetStandard.Model.Exceptions;

namespace RpgGame.NetStandard.Core
{
    public static class Helpers
    {
        public static ItemIntroAttribute GetItemAttr(this ItemEntity item)
        {
            return item.GetAttribute<ItemIntroAttribute>();
        }
        public static T GetAttribute<T>(this object enty, bool throwOrReturnNull = true) where T : Attribute
        {
            if (enty.GetType().BaseType == typeof(Enum))
            {
                var member = enty.GetType().GetMember(enty.ToString());
                if (member.Length == 0)
                {
                    throw new MsgException("该对象无枚举元素");
                }
                var attr = member[0].GetCustomAttribute<T>();
                if (attr == null && throwOrReturnNull)
                {
                    throw new MsgException($"对象[{enty}]未包含[{typeof(T)}]的属性");
                }
                else
                {
                    return attr;
                }
            }
            else
            {
                var attr = enty.GetType().GetTypeInfo().GetCustomAttribute<T>();
                if (attr == null && throwOrReturnNull)
                {
                    throw new MsgException($"对象[{enty}]未包含[{typeof(T)}]的属性"); ;
                }
                return attr;
            }
        }
        //framework ValueTuple 有bug
        //public static (int Min, int Max) GetEnumFirstLast<T>() where T : struct, IConvertible
        //{
        //    var enumList = Enum.GetValues(typeof(T)).Cast<Enum>();
        //    var enumerable = enumList as Enum[] ?? enumList.ToArray();
        //    return new ValueTuple<int, int>(enumerable.First().GetHashCode(), enumerable.Last().GetHashCode()); ;
        //}
        public static Tuple<int, int> GetEnumFirstLast<T>() where T : struct, IConvertible
        {
            var enumList = Enum.GetValues(typeof(T)).Cast<Enum>();
            var enumerable = enumList as Enum[] ?? enumList.ToArray();
            return new Tuple<int, int>(enumerable.First().GetHashCode(), enumerable.Last().GetHashCode()); ;
        }

        public static void AddItem(this ItemEntity item, int count, bool showMsg = true)
        {
            var currentItemInfo = item.GetItemInfo();
            currentItemInfo.Count += count;
            Startup.MyDataHandler.SaveData(Startup.MyGameData);
            if (showMsg)
            {
                Startup.MyInteractiver.Pop($"[{item.GetItemAttr().Name}] {count}");
            }
        }
    }
}
