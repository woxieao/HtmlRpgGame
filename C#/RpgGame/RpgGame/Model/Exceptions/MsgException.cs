using System;

namespace RpgGame.Model.Exceptions
{
    internal sealed class MsgException : Exception
    {
        public MsgException() : base() { }
        public MsgException(string msg) : base(msg) { }
    }
}
