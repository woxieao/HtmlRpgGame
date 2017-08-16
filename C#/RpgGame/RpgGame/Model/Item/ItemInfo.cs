using System;
using System.Reflection;
using RpgGame.Model.DataBase;
using RpgGame.Model.Exceptions;
using RpgGame.Model.Language;

namespace RpgGame.Model.Item
{

    public abstract class ItemInfo
    {
        protected ItemInfo(Func<dynamic, bool> isUseSuccess)
        {
            var typeInfo = this.GetType().GetTypeInfo().GetCustomAttribute<ItemIntroAttribute>();
            Name = typeInfo.Name;
            Description = typeInfo.Description;
            Price = typeInfo.Price;
            Effect = typeInfo.Effect;
            _isUseSuccess = isUseSuccess;
            Me = GameData.GetItem(this);
        }
        private readonly Func<dynamic, bool> _isUseSuccess;
        public Counter Me;
        public string Name { get; private set; }
        public string Description { get; private set; }
        public double Price { get; private set; }
        public static double Effect;
        public double SellPrice => Price / 2;
        public bool IsLocked { get; set; }
        public void UseItem(int useCount, object target)
        {
            if (useCount <= 0)
            {
                throw new MsgException("使用数量不能小于等于0".L());
            }
            if (Me.Count > 0)
            {
                for (var i = 0; i < useCount && _isUseSuccess(target); i++)
                {
                    Me.AddItem(this, -useCount);
                }
            }
            else
            {
                throw new MsgException("物品不存在".L());
            }
        }
        protected void SellItem(int sellCount)
        {
            if (sellCount <= 0)
            {
                throw new MsgException("出售数量不能小于等于0".L());
            }
            if (Me.Count >= sellCount)
            {
                Me.AddItem(this, -sellCount);
                GameData.Gold += sellCount * SellPrice;
            }
            else
            {
                throw new MsgException("物品不存在".L());
            }
        }
    }
}