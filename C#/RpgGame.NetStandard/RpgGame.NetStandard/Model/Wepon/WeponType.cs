using System;

namespace RpgGame.NetStandard.Model.Wepon
{
    public class WeponEffectAttribute : Attribute
    {
        public WeponEffectAttribute(string description, double effect)
        {
            Description = description;
            Effect = effect;
        }
        public string Description { get; private set; }
        public double Effect { get; private set; }
    }
}
