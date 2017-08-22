namespace RpgGame.NetStandard.StartUp
{
    internal sealed class Config
    {
        public class PersonLevelUp
        {
            public const int NeedsExp = 100;
            public const int HpRecover = 0;
            public const int MaxHp = 10;
            public const int EveryLevelNeedsExp = 100;
            //public const int EveryLevelNeedsExp = 100;
            //public const int EveryLevelNeedsExp = 100;
            //public const int EveryLevelNeedsExp = 100;
            //public const int EveryLevelNeedsExp = 100;
            //public const int EveryLevelNeedsExp = 100;
        }
        public class PropLevelUp
        {
            public const int Strength = 1;
            public const int Defensive = 1;
            public const int Hp = 10;
            public const int BaseForgeGold = 5;
        }
        public class WeponImprove
        {
            public const double WeponTypeImprove = 0.3;
        }

    }
}
