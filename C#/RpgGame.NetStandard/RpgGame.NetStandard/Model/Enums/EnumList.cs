using System.ComponentModel;
using RpgGame.NetStandard.Model.Item;
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
        LvMin = 1,
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
        LvMax = 7
    }
    public enum EffectType
    {
        [WeponEffect("吸取实际伤害值百分比的血量", 0.1)]
        Bloodthirsty = 1,
        [WeponEffect("攻击力加成", 0.1)]
        StrengthImprovePercent,
        [WeponEffect("幸运加成", 0.05)]
        Lucky,
        [WeponEffect("经验额外获取", 1)]
        ExpImprovePercent,
        [WeponEffect("生命值加成", 0.1)]
        HpImprovePercent,
        [WeponEffect("每次攻击额外造成目标当前生命值百分比伤害", 0.05)]
        DamagePercent,
        [WeponEffect("每回合回复生命值", 0.05)]
        HpRecoverImprove,
        [WeponEffect("防御力加成", 0.1)]
        DefensiveImprovePercent,
        [WeponEffect("金币额外获取", 1)]
        GoldImprovePercent,
    }
    public enum ItemEntity
    {
        [ItemIntro("红药水", "回复{0:P1}HP", 0.3, PropType.LvMin, 50)]
        RedMedicine = 1,
        [ItemIntro("锻造石", "增加锻造{0:P1}成功概率", 0.05, PropType.Lv4)]
        ForgeStone,
        [ItemIntro("宝箱钥匙", "开启宝箱", 0, PropType.Lv2, 1000)]
        ChestKey,
        [ItemIntro("宝箱Lv1", "破旧的箱子散发出一股奇怪的味道", (int)PropType.LvMin, PropType.LvMin)]
        Chest1,
        [ItemIntro("宝箱Lv2", "箱子上刻着:这里没有宝物", (int)PropType.Lv2, PropType.Lv2)]
        Chest2,
        [ItemIntro("宝箱Lv3", "很有质感的金色宝箱", (int)PropType.Lv3, PropType.Lv3)]
        Chest3,
        [ItemIntro("宝箱Lv4", "流光溢彩的宝箱不安地震动着,仿佛有什么要喷涌而出", 5, PropType.Lv4)]
        Chest4,
        [ItemIntro("被诅咒盒子", "深红的盒子传来一阵刺骨的寒意", 10, PropType.Lv6)]
        Chest5,
    }
}
