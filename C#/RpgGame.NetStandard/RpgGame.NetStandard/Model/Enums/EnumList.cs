using System.ComponentModel;
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
        LvMax = 10
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
}
