using System;
using System.Collections.Generic;
using System.IO;
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
        private void AppendOrWrite<T, T2>(string keyName, T data, Func<T2, T2> appendFunc = null)
        {
            var dataDict = (Dictionary<string, object>)DeserializeObject(File.ReadAllText(Config.GameData.GameDataFilePath));
            if (dataDict.TryGetValue(keyName, out var exitData))
            {
                if (appendFunc != null)
                {
                    dataDict[keyName] = appendFunc((T2)exitData);
                }
                else
                {
                    dataDict[keyName] = data;
                }
            }
            else
            {
                dataDict[keyName] = data;
            }
        }

        private void ReadOrCreate()
        {
            AppendOrWrite<ItemEntity, List<ItemEntity>>("123", ItemEntity.ForgeStone, (list) => { list.Add(ItemEntity.Chest1); return list; });

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
