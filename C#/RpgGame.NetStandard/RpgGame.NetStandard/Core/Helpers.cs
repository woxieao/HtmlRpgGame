using System;
using System.Linq;
using System.Reflection;
using RpgGame.NetStandard.Model.Exceptions;

namespace RpgGame.NetStandard.Core
{
    public static class Helpers
    {
        public static T GetAttribute<T>(this object enty) where T : Attribute
        {
            var attr = enty.GetType().GetTypeInfo().GetCustomAttribute<T>();
            if (attr == null)
            {
                throw new MsgException($"对象[{enty.GetType()}]未包含[{typeof(T)}]的属性");
            }
            else
            {
                return attr;
            }
        }
        public static (int Min, int Max) GetEnumFirstLast<T>() where T : struct, IConvertible
        {

            var enumList = Enum.GetValues(typeof(T)).Cast<Enum>();
            var enumerable = enumList as Enum[] ?? enumList.ToArray();
            return new ValueTuple<int, int>(enumerable.First().GetHashCode(), enumerable.Last().GetHashCode());
        }
    }
}
