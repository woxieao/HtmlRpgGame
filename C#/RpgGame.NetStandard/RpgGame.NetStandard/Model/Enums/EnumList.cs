using System.ComponentModel;
using RpgGame.NetStandard.Model.Wepon;

namespace RpgGame.NetStandard.Model.Enums
{
    public enum LanguageType
    {
        Cn = 1,
        En = 2,
    }
    public enum PropType
    {
        [Description("白")]
        LvMin = 1,
        [Description("绿")]
        Lv2 = 2,
        [Description("蓝")]
        Lv3 = 3,
        [Description("紫")]
        Lv4 = 4,
        [Description("橙")]
        Lv5 = 5,
        [Description("红")]
        Lv6 = 6,
        [Description("黑")]
        LvMax = 10
    }
    public enum EffectType
    {
        [WeponEffect("吸取实际伤害值百分比的血量", 0.1)]
        Bloodthirsty = 1,
        [WeponEffect("攻击力加成", 0.1)]
        Strength = 2,
        [WeponEffect("幸运加成", 0.05)]
        Lucky = 3,
        [WeponEffect("经验额外获取", 1)]
        Exp = 4,
        [WeponEffect("生命值加成", 0.1)]
        Hp = 5,
        [WeponEffect("每次攻击额外造成目标当前生命值百分比伤害", 0.05)]
        DamagePercent = 6,
        [WeponEffect("每回合回复生命值", 0.05)]
        HpRecover = 7,
        [WeponEffect("防御力加成", 0.1)]
        Defensive = 8,
        [WeponEffect("金币额外获取", 1)]
        Gold = 9,
    }
}
