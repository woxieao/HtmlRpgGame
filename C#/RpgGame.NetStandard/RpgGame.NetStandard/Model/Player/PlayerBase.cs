using RpgGame.NetStandard.Model.Enums;
using RpgGame.NetStandard.StartUp;

namespace RpgGame.NetStandard.Model.Player
{
    public abstract class PlayerBase : IPlayBehaviour
    {

        private double GetValue(double baseValue, bool changeWithLv)
        {
            return baseValue * (int)PropLevel * (changeWithLv ? Level : 1);
        }

        public PropType PropLevel { get; protected set; }
        private double _currentHp;
        public int Level => (int)(Exp / Config.EveryLevelExp);
        /// <summary>
        /// 经验
        /// </summary>
        public double Exp { get; set; }
        /// <summary>
        /// 血
        /// </summary>
        public double MaxHp => GetValue(10, true);

        /// <summary>
        /// 当前血量
        /// </summary>
        public double CurrentHp
        {
            get { return _currentHp >= MaxHp ? MaxHp : _currentHp; }
            set { _currentHp = value; }
        }

        /// <summary>
        /// 每回合回血百分百
        /// </summary>
        public double HpRecover => GetValue(0.01, false);

        /// <summary>
        /// 防御力
        /// </summary>
        public double Defensive => GetValue(1, true);

        /// <summary>
        /// 掉落,暴击
        /// </summary>
        public double Lucky => GetValue(0.05, false);
        /// <summary>
        /// 伤害
        /// </summary>
        public double Strength => GetValue(1, true);

        public void EveryTurnStart()
        {
            throw new System.NotImplementedException();
        }

        public void EveryTurnEnd()
        {
            throw new System.NotImplementedException();
        }

        public bool Fight(PlayerBase monster)
        {
            throw new System.NotImplementedException();
        }
    }
    internal interface IPlayBehaviour
    {
        void EveryTurnStart();
        void EveryTurnEnd();
        bool Fight(PlayerBase monster);
    }
}
