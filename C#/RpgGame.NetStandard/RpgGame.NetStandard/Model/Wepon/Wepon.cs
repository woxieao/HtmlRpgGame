using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using RpgGame.NetStandard.Core;
using RpgGame.NetStandard.Model.DataBase;
using RpgGame.NetStandard.Model.Enums;
using RpgGame.NetStandard.Model.Exceptions;
using RpgGame.NetStandard.Model.Item;
using RpgGame.NetStandard.StartUp;

namespace RpgGame.NetStandard.Model.Wepon
{
    public class PropValue
    {
        public double Bloodthirsty { get; set; }
        public double StrengthImprovePercent { get; set; }
        public double Lucky { get; set; }
        public double ExpImprovePercent { get; set; }
        public double HpImprovePercent { get; set; }
        public double DamagePercent { get; set; }
        public double HpRecoverImprove { get; set; }
        public double DefensiveImprovePercent { get; set; }
        public double GoldImprovePercent { get; set; }
    }

    public abstract class Prop
    {
        protected Prop(PropType propLevel, int level)
        {
            EffectList = new List<EffectType>();
            for (var i = 0; i < propLevel.GetHashCode(); i++)
            {
                var minMax = Helpers.GetEnumFirstLast<EffectType>();
                EffectList.Add((EffectType)Singleton.Ran.Next(minMax.Item1, minMax.Item2));
            }
            PropLevel = propLevel;
            Level = level;
            SpecEffect = new PropValue();
            InitSpecEffect();
        }
        private void InitSpecEffect()
        {
            foreach (var effect in EffectList)
            {
                typeof(PropValue).GetProperty(effect.ToString()).SetValue(SpecEffect, effect.GetAttribute<WeponEffectAttribute>().EffectValue);
            }
        }
        public PropValue SpecEffect { get; private set; }
        public int ForgeLevel { get; set; }
        public PropType PropLevel { get; protected set; }
        public double Strength => GetValue(Config.PropLevelUp.Strength);
        public double Defensive => GetValue(Config.PropLevelUp.Defensive);
        public double Hp => GetValue(Config.PropLevelUp.Hp);
        public long ForgeNeedGold => PropLevel.GetHashCode() * Level * Config.PropLevelUp.BaseForgeGold * ForgeLevel * ForgeLevel;

        public double ForgeProbability
        {
            get
            {
                var prob = 100 - ForgeLevel / 100;
                return prob < 1 ? 1 : prob;
            }
        }

        /// <summary>
        /// 强化物品
        /// </summary>
        /// <param name="forgeMaterialList">强化材料</param>
        /// <returns></returns>
        public bool Forge<T>(params T[] forgeMaterialList) where T : ItemInfo
        {
            if (GameData.Gold <= ForgeNeedGold)
            {
                throw new MsgException("金币不足");
            }
            GameData.Gold -= ForgeNeedGold;
            foreach (var forgeMaterial in forgeMaterialList)
            {
                forgeMaterial.UseItem(1, this);
            }
            var forgeResult = (int)((forgeMaterialList.Sum(i => i.Effect) + ForgeProbability) * 100) >= Singleton.Ran.Next(1, 100);
            if (forgeResult)
            {
                ++ForgeLevel;
            }
            return forgeResult;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseValue">每级增长的属性值</param>
        /// <returns></returns>
        private double GetValue(double baseValue)
        {
            return Level * baseValue * PropLevel.GetHashCode() * ForgeLevel;
        }

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
