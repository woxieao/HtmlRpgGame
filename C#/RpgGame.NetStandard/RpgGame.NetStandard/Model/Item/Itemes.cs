using System;
using RpgGame.NetStandard.Model.Player;
using RpgGame.NetStandard.Model.Wepon;

namespace RpgGame.NetStandard.Model.Item
{
    [ItemIntro("红药水", "回复{0:P1}HP", 0.3, 50)]
    public class RedMedicine : ItemInfo
    {
        private static readonly Func<object, ItemInfo, bool> NeedUse = (p, me) =>
        {
            var player = (PlayerBase)p;
            if (player.CurrentHp <= player.MaxHp)
            {
                return false;
            }
            else
            {
                player.CurrentHp += player.MaxHp * me.Effect;
                return true;
            }
        };
        public RedMedicine() : base(NeedUse)
        {

        }
    }
    [ItemIntro("锻造石", "增加锻造{0:P1}成功概率", 0.05, 10000)]
    public class ForgeStone : ItemInfo
    {
        private static readonly Func<object, ItemInfo, bool> NeedUse = (p, me) =>
        {
            var player = (Prop)p;
            return player.ForgeProbability < 1;
        };
        public ForgeStone() : base(NeedUse)
        {

        }
    }
}
