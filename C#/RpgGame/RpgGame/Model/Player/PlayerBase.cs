using RpgGame.Model.Item;
using RpgGame.Model.Monster;

namespace RpgGame.Model.Player
{
    internal abstract class PlayerBase : IPlayBehaviour, IItem
    {
        /// <summary>
        /// 经验
        /// </summary>
        public double Exp { get; set; }
        /// <summary>
        /// 血
        /// </summary>
        public double Hp { get; set; }
        /// <summary>
        /// 每回合回血百分百
        /// </summary>
        public double HpRecover { get; set; }
        /// <summary>
        /// 每回合回蓝百分百
        /// </summary>
        public double MpRecover { get; set; }
        /// <summary>
        /// 蓝
        /// </summary>
        public double Mp { get; set; }
        /// <summary>
        /// 防御力
        /// </summary>
        public double Defensive { get; set; }
        /// <summary>
        /// 攻击速度
        /// </summary>
        public double Speed { get; set; }
        /// <summary>
        /// 掉落,暴击
        /// </summary>
        public double Lucky { get; set; }
        /// <summary>
        /// 伤害
        /// </summary>
        public double Strength { get; set; }

        public void EveryTurnStart()
        {
            throw new System.NotImplementedException();
        }

        public void EveryTurnEnd()
        {
            throw new System.NotImplementedException();
        }

        public bool Fight(MonsterBase monster)
        {
            throw new System.NotImplementedException();
        }
    }
    internal interface IPlayBehaviour
    {
        void EveryTurnStart();
        void EveryTurnEnd();
        bool Fight(MonsterBase monster);
    }
}
