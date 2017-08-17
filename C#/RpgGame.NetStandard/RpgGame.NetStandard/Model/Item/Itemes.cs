using System;
using RpgGame.NetStandard.Model.Player;

namespace RpgGame.NetStandard.Model.Item
{
    [ItemIntro("红药水", "回复{0:P1}HP", 0.3, 50)]
    public class RedMedicine : ItemInfo
    {
        private static readonly Func<object, ItemInfo, bool> IsUseSuccess = (p, me) =>
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
        public RedMedicine() : base(IsUseSuccess)
        {

        }
    }

    [ItemIntro("蓝药水", "回复{0:P1}MP", 0.3, 50)]
    public class BlueMedicine : ItemInfo
    {
        private static readonly Func<object, ItemInfo, bool> IsUseSuccess = (p, me) =>
        {
            var player = (PlayerBase)p;
            if (player.CurrentMp <= player.MaxMp)
            {
                return false;
            }
            else
            {
                player.CurrentMp += player.MaxMp * me.Effect;
                return true;
            }
        };
        public BlueMedicine() : base(IsUseSuccess)
        {

        }
    }
}
