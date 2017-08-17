namespace RpgGame.Model.Player
{
    public abstract class PlayerBase : IPlayBehaviour
    {
        private double _currentHp;

        /// <summary>
        /// 经验
        /// </summary>
        public double Exp { get; set; }
        /// <summary>
        /// 血
        /// </summary>
        public double MaxHp { get; set; }

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
        public double HpRecover { get; set; }
        /// <summary>
        /// 每回合回蓝百分百
        /// </summary>
        public double MpRecover { get; set; }
        /// <summary>
        /// 蓝
        /// </summary>
        public double MaxMp { get; set; }
        /// <summary>
        /// 当前蓝量 
        /// </summary>
        public double CurrentMp { get; set; }
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
