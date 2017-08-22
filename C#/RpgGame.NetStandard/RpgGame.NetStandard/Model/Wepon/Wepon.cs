using System;
using System.Collections.Generic;
using RpgGame.NetStandard.Core;
using RpgGame.NetStandard.Model.Attributes;
using RpgGame.NetStandard.Model.DataBase;
using RpgGame.NetStandard.Model.Enums;
using RpgGame.NetStandard.Model.Exceptions;
using RpgGame.NetStandard.StartUp;

namespace RpgGame.NetStandard.Model.Wepon
{
    public class PropValue
    {
        public enum EffectType
        {
            [WeponEffect("嗜血", "吸取实际伤害值百分比的血量", 0.1)]
            Bloodthirsty = 1,
            [WeponEffect("神勇", "攻击力加成", 0.1)]
            StrengthImprovePercent,
            [WeponEffect("幸运", "幸运加成", 0.05)]
            Lucky,
            [WeponEffect("博学", "经验加成", 1)]
            ExpImprovePercent,
            [WeponEffect("肉山", "生命值加成", 0.1)]
            HpImprovePercent,
            [WeponEffect("神威", "每次攻击额外造成目标当前生命值百分比伤害", 0.05)]
            DamagePercent,
            [WeponEffect("回春", "生命回复提升", 0.05)]
            HpRecoverImprove,
            [WeponEffect("坦克", "防御力加成", 0.1)]
            DefensiveImprovePercent,
            [WeponEffect("神偷", "金币加成", 1)]
            GoldImprovePercent,
            [WeponEffect("霸主", "暴击伤害提升", 0.5)]
            CritDamageImprovePercent,
            [WeponEffect("飞燕", "敏捷加成", 0.1)]
            AgileImprovePercent,
        }
        public double Bloodthirsty { get; set; }
        public double StrengthImprovePercent { get; set; }
        public double Lucky { get; set; }
        public double ExpImprovePercent { get; set; }
        public double HpImprovePercent { get; set; }
        public double DamagePercent { get; set; }
        public double HpRecoverImprove { get; set; }
        public double DefensiveImprovePercent { get; set; }
        public double GoldImprovePercent { get; set; }
        public double CritDamageImprovePercent { get; set; }
        public double AgileImprovePercent { get; set; }
    }

    public abstract class Prop
    {
        protected Prop()
        {
            SpecEffect = new PropValue();
        }
        protected Prop(PropType propLevel, int level) : this()
        {
            EffectList = new List<PropValue.EffectType>();
            for (var i = 0; i < propLevel.GetHashCode(); i++)
            {
                var minMax = Helpers.GetEnumFirstLast<PropValue.EffectType>();
                EffectList.Add((PropValue.EffectType)Singleton.Ran.Next(minMax.Item1, minMax.Item2 + 1));
            }
            PropLevel = propLevel;
            Level = level;
            ForgeLevel = 1;
            InitSpecEffect();
        }
        protected void InitSpecEffect()
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

        public int ForgeProbability(int forgeStoneCount = 0)
        {
            var prob = 100 - ForgeLevel;
            return (int)(ItemEntity.ForgeStone.GetItemAttr().Data * 100 * forgeStoneCount + (prob < 1 ? 1 : prob));
        }

        /// <summary>
        /// 强化物品
        /// </summary>
        /// <param name="forgeCount">强化材料数量</param>
        /// <returns></returns>
        public bool Forge(int forgeCount = 0)
        {
            if (GameData.Gold <= ForgeNeedGold)
            {
                throw new MsgException("金币不足");
            }
            ItemLogic.UseItem(ItemEntity.ForgeStone, forgeCount, this);
            var forgeResult = ForgeProbability(forgeCount) >= Singleton.Ran.Next(1, 101);
            GameData.Gold -= ForgeNeedGold;
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
        public List<PropValue.EffectType> EffectList { get; set; }
    }
    public class Wepon : Prop
    {
        private PropValue _specEffect;
        private WeponType _weponKind;

        public WeponType WeponKind
        {
            get { return _weponKind; }
            set
            {
                _weponKind = value;
                InitSpecEffect();
                typeof(PropValue).GetProperty(WeponKind.GetAttribute<WeponTypeAttribute>().EffectValue.ToString()).SetValue(SpecEffect,
(double)typeof(PropValue).GetProperty(WeponKind.GetAttribute<WeponTypeAttribute>().EffectValue.ToString()).GetValue(SpecEffect) + Config.WeponImprove.WeponTypeImprove);
            }
        }


        public Wepon(PropType propLevel, int level) : base(propLevel, level)
        {
            var minMax = Helpers.GetEnumFirstLast<WeponType>();
            WeponKind = (WeponType)Singleton.Ran.Next(minMax.Item1, minMax.Item2 + 1);
        }
    }
}
