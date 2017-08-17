using System;
using System.Reflection;
using RpgGame.NetStandard.Model.DataBase;
using RpgGame.NetStandard.Model.Exceptions;
using RpgGame.NetStandard.Model.Language;

namespace RpgGame.NetStandard.Model.Item
{

    public abstract class ItemInfo
    {
        protected ItemInfo(Func<object, ItemInfo, bool> isUseSuccess)
        {
            var typeInfo = this.GetType().GetTypeInfo().GetCustomAttribute<ItemIntroAttribute>();
            _name = typeInfo.Name;
            _description = typeInfo.Description;
            Price = typeInfo.Price;
            Effect = typeInfo.Effect;
            _isUseSuccess = isUseSuccess;
            Me = GameData.GetItem(this);
        }
        private readonly Func<object, ItemInfo, bool> _isUseSuccess;
        public Counter Me;
        private readonly string _name;
        private readonly string _description;

        public string Name => _name.L();

        public string Description => _description.L(Effect);

        public double Price { get; private set; }
        public double Effect;
        public double SellPrice => Price / 2;
        public bool IsLocked { get; set; }
        public void UseItem(int useCount, object target)
        {
            if (useCount <= 0)
            {
                throw new MsgException("使用数量不能小于等于0");
            }
            if (Me.Count > 0)
            {
                for (var i = 0; i < useCount && _isUseSuccess(target, this); i++)
                {
                    Me.AddItem(this, -useCount);
                }
            }
            else
            {
                throw new MsgException("物品不存在");
            }
        }
        protected void SellItem(int sellCount)
        {
            if (sellCount <= 0)
            {
                throw new MsgException("出售数量不能小于等于0");
            }
            if (Me.Count >= sellCount)
            {
                Me.AddItem(this, -sellCount);
                GameData.Gold += sellCount * SellPrice;
            }
            else
            {
                throw new MsgException("物品不存在");
            }
        }
    }
}