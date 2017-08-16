using System.Collections.Generic;
using System.Linq;
using RpgGame.Model.DataBase;
using RpgGame.Model.Exceptions;
using RpgGame.StartUp;

namespace RpgGame.Model.Language
{
    public enum LanguageType
    {
        Cn = 1,
        En = 2,
    }
    public class LanguageInfo
    {
        public LanguageInfo(LanguageType lanType, string value) { }
        public LanguageType LanType { get; private set; }
        public string Value { get; private set; }
    }
    public static class LanguageCore
    {
        static LanguageCore()
        {
            InitLanguage();
        }
        private static void PushToLanList(string key, params string[] valList)
        {
            key = key.ToLower().Trim();

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
        }
        public static readonly Dictionary<string, List<LanguageInfo>> LanData = new Dictionary<string, List<LanguageInfo>>();
        public static string L(this string keyName)
        {
            if (LanData.TryGetValue(keyName, out var lanList))
            {
                var lan = lanList.SingleOrDefault(i => i.LanType == GameData.LanType);
                return lan == null ? keyName : lan.Value;
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
