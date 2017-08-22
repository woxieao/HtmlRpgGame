using System;
using RpgGame.NetStandard.Core;
using RpgGame.NetStandard.Model.Wepon;
using RpgGame.NetStandard.StartUp;

namespace RpgGame.NetStandard.Model.Attributes
{
    public class WeponTypeAttribute : Attribute
    {
        public WeponTypeAttribute(string name, PropValue.EffectType effectType)
        {
            Name = name;
            EffectValue = effectType;

        }
        public string Name { get; private set; }
        public string Description => $"{EffectValue.GetAttribute<WeponEffectAttribute>().Name}提升{Config.WeponImprove.WeponTypeImprove:P1}";
        public PropValue.EffectType EffectValue { get; private set; }
    }
}
