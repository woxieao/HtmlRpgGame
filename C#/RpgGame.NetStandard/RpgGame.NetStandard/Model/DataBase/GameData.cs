using System;
using System.Collections.Generic;
using System.IO;
using RpgGame.NetStandard.Core;
using RpgGame.NetStandard.Core.GameLogic;
using RpgGame.NetStandard.GameInit;
using RpgGame.NetStandard.Model.Enums;
using RpgGame.NetStandard.Model.Item;
using RpgGame.NetStandard.Model.Player;
using RpgGame.NetStandard.Model.Prop;
using RpgGame.NetStandard.Model.Wepon;

namespace RpgGame.NetStandard.Model.DataBase
{
    public class GameData
    {
    
        internal GameData()
        {
            Startup.MyDataHandler.InitGameData(this);
            LanType = LanguageType.Cn;
            Player = new Lee();
            PlayerExp = 0;
            ItemEntity.ChestKey.AddItem(999999999);
            ItemEntity.Chest1.AddItem(999999999);
            ItemEntity.Chest2.AddItem(999999999);
            ItemEntity.Chest3.AddItem(999999999);
            ItemEntity.Chest4.AddItem(999999999);
            ItemEntity.Chest10.AddItem(999999999);

        }
        public long Gold
        {
            get { return _gold; }
            set
            {
                _gold = value;
                Startup.MyDataHandler.SaveGoldData(Gold);
                Startup.MyInteractiver.Pop("[金币]" + value);
            }
        }

        public LanguageType LanType
        {
            get { return _lanType; }
            set
            {
                _lanType = value;
                Startup.MyDataHandler.SaveLanguageType(LanType);
            }
        }

        public readonly List<WeponInfo> WeponList = new List<WeponInfo>();

        public readonly Dictionary<ItemEntity, ItemHandler> ItemList
            = new Dictionary<ItemEntity, ItemHandler>
            {
                {
                    ItemEntity.RedMedicine, new ItemHandler((p, count) =>
                    {
                        var player = (PlayerBase) p;
                        for (var i = 0; i < count && player.CurrentHp <= player.MaxHp; i++)
                        {
                            ItemEntity.RedMedicine.AddItem(-1);
                            player.CurrentHp += player.MaxHp * ItemEntity.RedMedicine.GetItemAttr().Data;
                        }
                    })
                },
                {
                    ItemEntity.ForgeStone,
                    new ItemHandler((p, count) =>
                    {
                        var prop = (PropBase) p;
                        for (var i = 0; i < count && prop.ForgeProbability(i) < 100; i++)
                        {
                            ItemEntity.ForgeStone.AddItem(-1);
                        }
                    })
                },

                #region 宝箱相关
                {
                    ItemEntity.Chest1,
                    new ItemHandler((gameData, count) => { Draw.OpenChestResult(ItemEntity.Chest1, count); })
                },
                {
                    ItemEntity.Chest2,
                    new ItemHandler((gameData, count) => { Draw.OpenChestResult(ItemEntity.Chest2, count); })
                },
                {
                    ItemEntity.Chest3,
                    new ItemHandler((gameData, count) => { Draw.OpenChestResult(ItemEntity.Chest3, count); })
                },
                {
                    ItemEntity.Chest4,
                    new ItemHandler((gameData, count) => { Draw.OpenChestResult(ItemEntity.Chest4, count); })
                },
                {
                    ItemEntity.Chest10,
                    new ItemHandler((gameData, count) => { Draw.OpenChestResult(ItemEntity.Chest10, count); })
                },
                {
                    ItemEntity.ChestKey, new ItemHandler((gameData, count) => { ItemEntity.ChestKey.AddItem(-count); })
                },

                #endregion

                #region 钱袋相关
                {
                    ItemEntity.PurseLv3, new ItemHandler((gameData, count) => { })
                },

                {
                    ItemEntity.PurseLv4, new ItemHandler((gameData, count) => { })
                },

                {
                    ItemEntity.PurseLv5, new ItemHandler((gameData, count) => { })
                },
                #endregion

                #region 待补充
                
                {
                    ItemEntity.WaitForCreate6, new ItemHandler((gameData, count) => { })
                },
                {
                    ItemEntity.WaitForCreate7, new ItemHandler((gameData, count) => { })
                },
                {
                    ItemEntity.WaitForCreate8, new ItemHandler((gameData, count) => { })
                },
                {
                    ItemEntity.WaitForCreate9, new ItemHandler((gameData, count) => { })
                },
                {
                    ItemEntity.WaitForCreate10, new ItemHandler((gameData, count) => { })
                },
                #endregion

            };

        private LanguageType _lanType;

        public static PlayerBase Player;

        public long PlayerExp
        {
            get { return _playerExp; }
            set
            {
                _playerExp = value;
                Startup.MyDataHandler.SaveExp(PlayerExp);
            }
        }

        private long _gold;
        private long _playerExp;
        public long PlayerLevel => PlayerExp / Config.PersonLevelUp.EveryLevelNeedsExp;




        public void ChangeLanguage(LanguageType lanType)
        {
            LanType = lanType;
        }
    }
}
