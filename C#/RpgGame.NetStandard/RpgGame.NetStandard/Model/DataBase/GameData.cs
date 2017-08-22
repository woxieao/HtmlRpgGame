using System.Collections.Generic;
using System.Linq;
using System.Text;
using RpgGame.NetStandard.Core;
using RpgGame.NetStandard.Model.Enums;
using RpgGame.NetStandard.Model.Item;
using RpgGame.NetStandard.Model.Player;
using RpgGame.NetStandard.Model.Prop;
using RpgGame.NetStandard.Model.Wepon;
using RpgGame.NetStandard.StartUp;

namespace RpgGame.NetStandard.Model.DataBase
{
    public class GameData
    {
        public static double Gold;
        public static LanguageType LanType;
        public static readonly List<WeponInfo> WeponList = new List<WeponInfo>();

        public static readonly Dictionary<ItemEntity, ItemHandler> ItemList
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
                {
                    ItemEntity.Chest1, new ItemHandler((gameData, count) =>
                    {
                        var giftList = Draw.OpenChestResult(ItemEntity.Chest1, count);

                        var msg = new StringBuilder();
                        foreach (var item in giftList.ItemList)
                        {
                            var itemInfo = item.Key.GetItemAttr();
                            msg.AppendLine($"[{itemInfo.Name}({itemInfo.PropLevel})]:{item.Value}");
                        }
                        msg.AppendLine(
                            $"[{giftList.WeponList.Count(i => i.WeponKind == WeponType.Axe)}({giftList.WeponList.Count})]");
                        Output.Msg(msg.ToString());
                    })
                },
                {
                    ItemEntity.Chest2, new ItemHandler((gameData, count) =>
                    {
                        var giftList = Draw.OpenChestResult(ItemEntity.Chest2, count);
                    })
                },
                {
                    ItemEntity.Chest3, new ItemHandler((gameData, count) =>
                    {
                        var giftList = Draw.OpenChestResult(ItemEntity.Chest3, count);
                    })
                },
                {
                    ItemEntity.Chest4, new ItemHandler((gameData, count) =>
                    {
                        var giftList = Draw.OpenChestResult(ItemEntity.Chest4, count);
                        var msg = new StringBuilder();
                        foreach (var item in giftList.ItemList)
                        {
                            var itemInfo = item.Key.GetItemAttr();
                            msg.AppendLine($"[{itemInfo.Name}({itemInfo.PropLevel})]:{item.Value}");
                        }
                        msg.AppendLine(
                            $"[{giftList.WeponList.Count(i => i.WeponKind == WeponType.Axe)}({giftList.WeponList.Count})]");
                        Output.Msg(msg.ToString());
                    })
                },
                {
                    ItemEntity.Chest10, new ItemHandler((gameData, count) =>
                    {
                        var giftList = Draw.OpenChestResult(ItemEntity.Chest10, count);
                        var msg = new StringBuilder();
                        foreach (var item in giftList.ItemList)
                        {
                            var itemInfo = item.Key.GetItemAttr();
                            msg.AppendLine($"[{itemInfo.Name}({itemInfo.PropLevel})]:{item.Value}");
                        }
                        msg.AppendLine(
                            $"[{giftList.WeponList.Count(i => i.WeponKind == WeponType.Axe)}({giftList.WeponList.Count})]");
                        Output.Msg(msg.ToString());
                    })
                },
                {
                    ItemEntity.ChestKey, new ItemHandler((gameData, count) => { ItemEntity.ChestKey.AddItem(-count); })
                },

                {
                    ItemEntity.PurseLv3, new ItemHandler((gameData, count) => { })
                },

                {
                    ItemEntity.PurseLv4, new ItemHandler((gameData, count) => { })
                },

                {
                    ItemEntity.PurseLv5, new ItemHandler((gameData, count) => { })
                },
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
            };

        public static PlayerBase Player;
        public static int PlayerExp;
        public static int PlayerLevel => PlayerExp / Config.PersonLevelUp.EveryLevelNeedsExp;

        static GameData()
        {
            LanType = LanguageType.Cn;
            Gold = 0;
            Player = new Lee();
            PlayerExp = 0;
            ItemEntity.ChestKey.AddItem(999999999);
            ItemEntity.Chest1.AddItem(999999999);
            ItemEntity.Chest2.AddItem(999999999);
            ItemEntity.Chest3.AddItem(999999999);
            ItemEntity.Chest4.AddItem(999999999);
            ItemEntity.Chest10.AddItem(999999999);
        }

        public static void ChangeLanguage(LanguageType lanType)
        {
            LanType = lanType;
        }
    }
}
