using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using RpgGame.NetStandard.GameInit;
using RpgGame.NetStandard.Model.Attributes;
using RpgGame.NetStandard.Model.DataBase;
using RpgGame.NetStandard.Model.Enums;

namespace RpgGame.NetStandard.Core.GameLogic
{
    public interface IDataHandler
    {
        GameData InitGameData();
        void SaveData(GameData gameData);
    }

    public class DataHandler //: IDataHandler
    {
        private const string GameDataKeyName = "GameData";
        //private readonly Func<object, string> _serializeObject;
        //private readonly Func<string, object> _deserializeObject;

        private void CheckFile()
        {
            if (!File.Exists(Config.GameData.GameDataFilePath))
            {
                using (File.Create(Config.GameData.GameDataFilePath)) { }
            }
        }
        //public DataHandler(Func<object, string> serializeObjectFunc, Func<string, object> deserializeObjectFunc)
        //{
        //    _serializeObject = serializeObjectFunc;
        //    _deserializeObject = deserializeObjectFunc;
        //}
        private enum DataHandleType
        {
            Append = 1,
            Override = 2
        }
        private void AppendOrWrite<T>(string keyName, T data, DataHandleType handleType)
        {
            CheckFile();
            var dataDict = (JsonConvert.DeserializeObject<Dictionary<string, object>>(File.ReadAllText(Config.GameData.GameDataFilePath)) ??
                           new Dictionary<string, object>());
            if (dataDict.TryGetValue(keyName, out var exitData))
            {
                if (handleType == DataHandleType.Append)
                {
                    if (exitData is IList<T> existList)
                    {
                        existList.Add(data);
                        dataDict[keyName] = existList;
                        return;
                    }
                }
            }
            dataDict[keyName] = data;
            File.WriteAllText(Config.GameData.GameDataFilePath, JsonConvert.SerializeObject(dataDict));
        }
        private T ReadOrCreate<T>(string keyName, T defaultValue)
        {
            CheckFile();
            var dataDict = (JsonConvert.DeserializeObject<Dictionary<string, object>>(File.ReadAllText(Config.GameData.GameDataFilePath)) ??
                           new Dictionary<string, object>());
            if (dataDict.TryGetValue(keyName, out var exitData))
            {
                return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(exitData));
            }
            else
            {
                return defaultValue;
            }
        }
        public void SaveData(GameData gameData)
        {
            AppendOrWrite(GameDataKeyName, gameData, DataHandleType.Override);
        }
        public GameData InitGameData()
        {
            return ReadOrCreate(GameDataKeyName, new GameData());
        }
    }
}
