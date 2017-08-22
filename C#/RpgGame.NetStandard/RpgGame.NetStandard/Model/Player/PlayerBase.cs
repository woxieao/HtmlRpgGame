using RpgGame.NetStandard.Model.DataBase;
using RpgGame.NetStandard.Model.Enums;
using RpgGame.NetStandard.Model.Wepon;
using RpgGame.NetStandard.StartUp;

namespace RpgGame.NetStandard.Model.Player
{
    public abstract class PlayerBase : IPlayBehaviour
    {


        private double GetValue(double baseValue, params Prop[] equipList)
        {
            //foreach (var equip in equipList)
            //{
            //    equip.SpecEffect;
            //}
            //return baseValue * (int)PropLevel * ( ? Level : 1);
            return 0;
        }

        public PropType PropLevel { get; protected set; }
        private double _currentHp;
        
        /// <summary>
        /// 经验
        /// </summary>

        /// <summary>
        /// 血
        /// </summary>
        public double MaxHp => GetValue(10);

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
        public double HpRecover => GetValue(0.01);

        /// <summary>
        /// 防御力
        /// </summary>
        public double Defensive => GetValue(1);

        /// <summary>
        /// 掉落,暴击
        /// </summary>
        public double Lucky => GetValue(0.05);
        /// <summary>
        /// 命中/闪避
        /// </summary>
        public double Agile => GetValue(0.05);
        /// <summary>
        /// 伤害
        /// </summary>
        public double Strength => GetValue(1);
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

    public class Lee : PlayerBase
    {
    }

    internal interface IPlayBehaviour
    {
        void EveryTurnStart();
        void EveryTurnEnd();
        bool Fight(PlayerBase monster);
    }
}
