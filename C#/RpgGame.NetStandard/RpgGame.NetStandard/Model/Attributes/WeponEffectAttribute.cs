using System;

namespace RpgGame.NetStandard.Model.Attributes
{
    public class WeponEffectAttribute : Attribute
    {
        public WeponEffectAttribute (string name, string description, double effectValue)
        {
            Name = name;
            Description = description;
            EffectValue = effectValue;
        }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public double EffectValue { get; private set; }
    }
}
