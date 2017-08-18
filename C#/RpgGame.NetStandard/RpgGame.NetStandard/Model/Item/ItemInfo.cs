//using System;
//using System.Linq;
//using System.Reflection;
//using RpgGame.NetStandard.Core;
//using RpgGame.NetStandard.Model.DataBase;
//using RpgGame.NetStandard.Model.Exceptions;
//using RpgGame.NetStandard.Model.Language;

//namespace RpgGame.NetStandard.Model.Item
//{

//    public abstract class ItemInfo
//    {
//        protected ItemInfo()
//        {
//            var typeInfo = this.GetAttribute<ItemIntroAttribute>();
//            _name = typeInfo.Name;
//            _description = typeInfo.Description;
//            Price = typeInfo.Price;
//            SellInMarket = typeInfo.Price > 0;
//            Effect = typeInfo.Data;
//            _me = GetItem();
//        }

//        protected ItemInfo(Func<object, ItemInfo, bool> needUse) : this()
//        {
//            _needUse = needUse ?? ((target, me) => true);
//        }
//        protected ItemInfo(Func<ItemInfo, bool> needUse) : this()
//        {
//            _needUse = (target, me) => needUse?.Invoke(me) ?? true;
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="count">minus/plus count</param>
//        public void AddItem(int count)
//        {
//            _me.Count += count;
//        }

//        private readonly Func<object, ItemInfo, bool> _needUse;
//        private readonly ItemCounter _me;
//        public int Count => _me.Count;
//        private readonly string _name;
//        private readonly string _description;

//        public string Name => _name.L();

//        public string Description => _description.L(Effect);

//        public double Price { get; private set; }
//        public double Effect;
        
        

//        public ItemCounter GetItem()
//        {
//            if (!GameData.ItemList.TryGetValue(this.GetType(), out var itemInfo))
//            {
//                itemInfo = new ItemCounter
//                {
//                    Base = this,
//                };
//                GameData.ItemList.Add(this.GetType(), itemInfo);
//            }
//            return itemInfo;
//        }
//    }
//}