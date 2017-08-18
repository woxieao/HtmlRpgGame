
using System;

namespace RpgGame.NetStandard.Model.Item
{
    public class ItemHandler
    {
        public ItemHandler()
        {
            Count = 0;
        }
        public Action<object, int> UseItemAct { get; set; }
        public int Count { get; internal set; }
    }
}
