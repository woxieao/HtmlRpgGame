using System.ComponentModel;
using RpgGame.NetStandard.Model.Attributes;
using RpgGame.NetStandard.Model.Item;
using RpgGame.NetStandard.Model.Prop;
using RpgGame.NetStandard.Model.Wepon;

namespace RpgGame.NetStandard.Model.Enums
{
    public enum LanguageType
    {
        Cn = 1,
        En,
    }
    public enum PropType
    {
        [Description("白")]
        Lv1 = 1,
        [Description("绿")]
        Lv2,
        [Description("蓝")]
        Lv3,
        [Description("紫")]
        Lv4,
        [Description("橙")]
        Lv5,
        [Description("红")]
        Lv6,
        [Description("黑")]
        Lv7,
        [Description("白银")]
        Lv8,
        [Description("铂金")]
        Lv9,
        [Description("钻石")]
        Lv10
    }

    public enum ItemEntity
    {
        [ItemIntro("红药水", "回复{0:P1}HP", 0.3, PropType.Lv1, 200)]
        RedMedicine = 1,
        [ItemIntro("锻造石", "增加锻造{0:P1}成功概率", 0.05, PropType.Lv4)]
        ForgeStone,

        #region 宝箱相关
        [ItemIntro("宝箱钥匙", "开启宝箱", 0, PropType.Lv2, 1000)]
        ChestKey,
        [ItemIntro("宝箱Lv1", "破旧的箱子散发出一股奇怪的味道", 1, PropType.Lv1)]
        Chest1,
        [ItemIntro("宝箱Lv2", "箱子上刻着:这里没有宝物", 2, PropType.Lv2)]
        Chest2,
        [ItemIntro("宝箱Lv3", "质感极佳的金色宝箱,想必不是凡品", 3, PropType.Lv3)]
        Chest3,
        [ItemIntro("宝箱Lv4", "流光溢彩的宝箱不安地震动着,仿佛有什么要逃出来", 4, PropType.Lv4)]
        Chest4,
        [ItemIntro("诅咒宝箱", "猩红的盒子传来一阵恶寒", 20, PropType.Lv10)]
        Chest10,
        #endregion

        #region 金币相关
        [ItemIntro("钱袋", "沉甸甸的钱袋子", 1000, PropType.Lv3)]
        PurseLv3,

        [ItemIntro("钱箱", "一箱金子", 2000, PropType.Lv4)]
        PurseLv4,

        [ItemIntro("宝库", "金晃晃的宝藏令人窒息", 10000, PropType.Lv5)]
        PurseLv5,
        #endregion

        #region 待补充
        [ItemIntro("占位6", "待开发...Lv6物品", 0, PropType.Lv6)]
        WaitForCreate6,
        [ItemIntro("占位7", "待开发...Lv7物品", 0, PropType.Lv7)]
        WaitForCreate7,
        [ItemIntro("占位8", "待开发...Lv8物品", 0, PropType.Lv8)]
        WaitForCreate8,
        [ItemIntro("占位9", "待开发...Lv9物品", 0, PropType.Lv9)]
        WaitForCreate9,
        [ItemIntro("占位10", "待开发...Lv10物品", 0, PropType.Lv10)]
        WaitForCreate10,
        #endregion

    }
    public enum WeponType
    {
        [WeponType("血刃", PropValue.EffectType.Bloodthirsty)]
        Sword = 1,
        [WeponType("神箭", PropValue.EffectType.AgileImprovePercent)]
        Arch,
        [WeponType("阔斧", PropValue.EffectType.StrengthImprovePercent)]
        Axe,
        [WeponType("长矛", PropValue.EffectType.HpImprovePercent)]
        Spear,
        [WeponType("大棒", PropValue.EffectType.DefensiveImprovePercent)]
        Rod
    }
}
