using System;

namespace RpgGame.NetStandard.Model.Wepon
{
    public class WeponEffectAttribute : Attribute
    {
        public WeponEffectAttribute(string description, double effectValue)
        {
            Description = description;
            EffectValue = effectValue;
        }
        public string Description { get; private set; }
        public double EffectValue { get; private set; }
    }
}
