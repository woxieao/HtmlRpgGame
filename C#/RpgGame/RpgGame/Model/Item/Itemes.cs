using System;
using RpgGame.Model.Player;

namespace RpgGame.Model.Item
{
    [ItemIntro("红药水", "回复{0}HP", 0.3, 50, "P1")]
    public class RedMedicine : ItemInfo
    {
        private static readonly Func<dynamic, bool> IsUseSuccess = (p =>
          {
              var player = (PlayerBase)p;
              if (player.CurrentHp <= player.MaxHp)
              {
                  return false;
              }
              else
              {
                  player.CurrentHp += player.MaxHp * Effect;
                  return true;
              }
          });
        public RedMedicine() : base(IsUseSuccess)
        {

        }
    }

    [ItemIntro("蓝药水", "回复{0}MP", 0.3, 50, "P1")]
    public class BlueMedicine : ItemInfo
    {
        private static readonly Func<dynamic, bool> IsUseSuccess = (p =>
         {
             var player = (PlayerBase)p;
             if (player.CurrentMp <= player.MaxMp)
             {
                 return false;
             }
             else
             {
                 player.CurrentMp += player.MaxMp * Effect;
                 return true;
             }
         });
        public BlueMedicine() : base(IsUseSuccess)
        {

        }
    }
}
