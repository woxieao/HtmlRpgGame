using System;
using RpgGame.Model.Language;

namespace RpgGame.Model.Item
{
    public class ItemIntroAttribute : Attribute
    {
        public ItemIntroAttribute(string name, string description, double effect, double price = 0, string toSting = "")
        {
            Name = name.L();
            Description = string.Format(description, effect.ToString(toSting)).L();
            Price = price;
            Effect = effect;
        }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public double Price { get; private set; }
        public double Effect { get; private set; }
    }
}
