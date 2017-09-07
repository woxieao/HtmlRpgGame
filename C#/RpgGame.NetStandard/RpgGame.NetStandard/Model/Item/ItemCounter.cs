using System;
using RpgGame.NetStandard.GameInit;
using RpgGame.NetStandard.Model.DataBase;
using RpgGame.NetStandard.Model.Enums;

namespace RpgGame.NetStandard.Model.Item
{
    public class ItemHandler
    {
        private readonly GameData _gameData = Startup.MyGameData;
        public ItemHandler(Action<object, int> useItemAct, ItemEntity item)
        {
            ItemType = item;
            UseItemAct += useItemAct;
            UseItemAct += (a, b) => { Startup.MyDataHandler.SaveData(Startup.MyGameData); };

        }
        public ItemEntity ItemType { get; }
        public Action<object, int> UseItemAct { get; }

        public int Count
        {
            get
            {
                return _gameData.ItemCounter.TryGetValue(ItemType, out var count) ? count : 0;
            }
            internal set
            {
                if (_gameData.ItemCounter.ContainsKey(ItemType))
                {
                    _gameData.ItemCounter[ItemType] = value;
                }
                else
                {
                    _gameData.ItemCounter.Add(ItemType, value);
                }
            }
        }
    }
}
