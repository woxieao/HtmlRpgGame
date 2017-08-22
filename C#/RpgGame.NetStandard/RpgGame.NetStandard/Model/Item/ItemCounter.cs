
using System;

namespace RpgGame.NetStandard.Model.Item
{
    public class ItemHandler
    {
        public ItemHandler(Action<object, int> useItemAct)
        {
            Count = 0;
            UseItemAct = useItemAct ?? ((a, b) => { });
        }
        public Action<object, int> UseItemAct { get; }
        public int Count { get; internal set; }
    }
}
