using System.Collections.Generic;
using System.Linq;
using RpgGame.NetStandard.Model.DataBase;
using RpgGame.NetStandard.Model.Exceptions;

namespace RpgGame.NetStandard.Model.Language
{
    public enum LanguageType
    {
        Cn = 1,
        En = 2,
    }

    public class LanguageInfo
    {
        public LanguageInfo(LanguageType lanType, string value)
        {
            Value = value;
            LanType = lanType;
        }
        public LanguageType LanType { get; }
        public string Value { get; }
    }
    public static class LanguageCore
    {
        static LanguageCore()
        {
            InitLanguage();
        }
        private static void PushToLanList(string key, params string[] valList)
        {
            if (LanData.ContainsKey(key))
            {
                throw new MsgException($"已包含{key}的定义");
            }
            else
            {
                valList = valList ?? new string[] { };
                var lanList = new List<LanguageInfo> { new LanguageInfo(LanguageType.Cn, key) };
                for (var i = 0; i < valList.Length; i++)
                {
                    lanList.Add(new LanguageInfo((LanguageType)(i + 2), valList[0]));
                }
                LanData.Add(key, lanList);
            }

        }
        private static void InitLanguage()
        {
            PushToLanList("红药水", "RedMedicine");
            PushToLanList("回复{0:P1}HP", "Recover{0:P1}HP");
            PushToLanList("蓝药水", "BlueMedicine");
            PushToLanList("回复{0:P1}MP", "Recover{0:P1}MP");
        }
        public static readonly Dictionary<string, List<LanguageInfo>> LanData = new Dictionary<string, List<LanguageInfo>>();
        public static string L(this string keyName, double num = 0)
        {
            if (LanData.TryGetValue(keyName, out var lanList))
            {
                var lan = lanList.SingleOrDefault(i => i.LanType == GameData.LanType);
                return string.Format(lan == null ? keyName : lan.Value, num);
            }
            else
            {
                var inDevelop = true;
                if (!inDevelop)
                {
                    throw new MsgException($"未包含[{keyName}]的定义");
                }
                else
                {
                    return keyName;
                }
            }
        }
    }
}
