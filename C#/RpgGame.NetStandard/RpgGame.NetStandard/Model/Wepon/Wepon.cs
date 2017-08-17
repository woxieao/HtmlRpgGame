using System.Collections.Generic;
using RpgGame.NetStandard.Model.Enums;
using RpgGame.NetStandard.StartUp;

namespace RpgGame.NetStandard.Model.Wepon
{
  
    public abstract class Prop
    {
        protected Prop(PropType propLevel, int level)
        {
            EffectList = new List<EffectType>();
            for (var i = 0; i < (int)propLevel; i++)
            {
                EffectList.Add((EffectType)Singleton.Ran.Next((int)EffectType.Bloodthirsty, (int)EffectType.Gold));
            }
            PropLevel = propLevel;
            Level = level;
        }
        public PropType PropLevel { get; protected set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseValue">每级增长的属性值</param>
        /// <returns></returns>
        private double GetValue(double baseValue)
        {
            return Level * baseValue * (int)PropLevel;
        }
        public double Strength => GetValue(1);
        public double Defensive => GetValue(1);
        public double Hp => GetValue(10);

        public int Level { get; protected set; }
        public List<EffectType> EffectList { get; set; }
    }
    public class Wepon : Prop
    {
        public Wepon(PropType propLevel, int level) : base(propLevel, level)
        {
        }

    }
}
