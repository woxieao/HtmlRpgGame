﻿using RpgGame.NetStandard.Core;
using RpgGame.NetStandard.GameInit;
using RpgGame.NetStandard.Model.Attributes;
using RpgGame.NetStandard.Model.Enums;
using RpgGame.NetStandard.Model.Prop;

namespace RpgGame.NetStandard.Model.Wepon
{

    public class WeponInfo : PropBase
    {
        private WeponType _weponKind;
        public WeponType WeponKind
        {
            get { return _weponKind; }
            set
            {
                _weponKind = value;
                InitSpecEffect();
                typeof(PropValue)
                    .GetProperty(WeponKind
                    .GetAttribute<WeponTypeAttribute>().EffectValue.ToString())
                    .SetValue(SpecEffect, (double)typeof(PropValue)
                    .GetProperty(WeponKind.GetAttribute<WeponTypeAttribute>().EffectValue.ToString())
                    .GetValue(SpecEffect) + Config.WeponImprove.WeponTypeImprove);
            }
        }

        public WeponInfo(PropType propLevel, long level) : base(propLevel, level)
        {
            var minMax = Helpers.GetEnumFirstLast<WeponType>();
            WeponKind = (WeponType)Startup.Ran.Next(minMax.Item1, minMax.Item2 + 1);
        }
    }
}
