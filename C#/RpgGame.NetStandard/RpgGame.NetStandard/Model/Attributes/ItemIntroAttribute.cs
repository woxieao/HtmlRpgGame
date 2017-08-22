using System;
using RpgGame.NetStandard.Model.Enums;

namespace RpgGame.NetStandard.Model.Attributes
{
    public class ItemIntroAttribute : Attribute
    {
        public ItemIntroAttribute(string name, string description, double data, PropType propLevel, double price = 0)
        {
            Name = name;
            Description = description;
            Price = price;
            Data = data;
            PropLevel = propLevel;
        }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public double Price { get; private set; }
        public double Data { get; private set; }
        public double SellPrice => Price / 2;
        public PropType PropLevel { get; private set; }
        public bool SellInMarket => Price > 0;
    }
}
