using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RpgGame.NetStandard.GameInit;
using RpgGame.NetStandard.Model.DataBase;
using RpgGame.NetStandard.Model.Enums;

namespace RpgGame.NetStandard.Core.GameLogic
{
    public interface IDataHandler
    {
        void SaveItemData(ItemEntity itemEntity, int count);
        void SaveGoldData(long gold);
        void SaveLanguageType(LanguageType lan);
        void SaveExp(long exp);
        void InitGameData(GameData gameData);
    }

    public class DataHandler : IDataHandler
    {
        private Func<object, string> SerializeObject;
        private Func<string, object> DeserializeObject;

        public DataHandler(Func<object, string> serializeObjectFunc, Func<string, object> deserializeObjectFunc)
        {
            SerializeObject = serializeObjectFunc;
            DeserializeObject = deserializeObjectFunc;
        }
        private void AppendOrWrite<T>(string keyName, T data, bool appendToList)
        {
            {
                var dataDict = (Dictionary<string, object>)DeserializeObject(File.ReadAllText(Config.GameData.GameDataFilePath));
                if (dataDict.TryGetValue(keyName, out var exitData))
                {
                    if (appendToList)
                    {
                        var existList = exitData as IList<T>;
                        if (existList != null)
                        {
                            existList.Add(data);
                            dataDict[keyName] = existList;
                            return;
                        }
                    }
                }
                dataDict[keyName] = data;
            }
        }
        private void AppendOrWrite<T>(string keyName, T data, Func<IDictionary<string, T>, T> func)
        {
            var dataDict = (Dictionary<string, object>)DeserializeObject(File.ReadAllText(Config.GameData.GameDataFilePath));
            if (dataDict.TryGetValue(keyName, out var exitData))
            {
                if (func != null)
                {
                    var existDict = exitData as IDictionary<string, T>;
                    if (existDict != null)
                    {
                        dataDict[keyName] = func(existDict);
                        return;
                    }
                }
            }
            dataDict[keyName] = data;
        }

        private void ReadOrCreate()
        {
            AppendOrWrite("123", ItemEntity.ForgeStone, true);

        }
        public void InitGameData(GameData gameData)
        {

        }

        public void SaveItemData(ItemEntity itemEntity)
        {
            throw new System.NotImplementedException();
        }

        public void SaveGoldData(long gold)
        {
            throw new System.NotImplementedException();
        }

        public void SaveItemData(ItemEntity itemEntity, int count)
        {
            throw new System.NotImplementedException();
        }

        public void SaveLanguageType(LanguageType lan)
        {
            throw new System.NotImplementedException();
        }

        public void SaveExp(long exp)
        {
            throw new System.NotImplementedException();
        }
    }
}
