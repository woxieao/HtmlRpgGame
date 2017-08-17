using System;

namespace RpgGame.NetStandard.Model.Item
{
    public class ItemIntroAttribute : Attribute
    {
        public ItemIntroAttribute(string name, string description, double effect, double price = 0)
        {
            Name = name;
            Description = description;
            Price = price;
            Effect = effect;
        }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public double Price { get; private set; }
        public double Effect { get; private set; }
    }
}
