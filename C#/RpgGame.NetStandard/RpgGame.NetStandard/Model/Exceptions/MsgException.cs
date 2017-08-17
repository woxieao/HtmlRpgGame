using System;
using RpgGame.NetStandard.Model.Language;

namespace RpgGame.NetStandard.Model.Exceptions
{
    internal sealed class MsgException : Exception
    {
        public MsgException() : base() { }
        public MsgException(string msg) : base(msg.L()) { }
    }
}
