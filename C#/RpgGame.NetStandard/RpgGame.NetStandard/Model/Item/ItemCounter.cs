
namespace RpgGame.NetStandard.Model.Item
{
    public class ItemCounter
    {
        public ItemCounter()
        {
            Count = 0;
        }
        public ItemInfo Base { get; set; }
        public int Count { get; internal set; }
    }
}
