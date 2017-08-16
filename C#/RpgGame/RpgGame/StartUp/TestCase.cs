using RpgGame.Model.DataBase;
using RpgGame.Model.Item;

namespace RpgGame.StartUp
{
    public sealed class TestCase
    {
        public static void Test0()
        {
            var mdc = new RedMedicine();
            var xx = mdc.Me.Count;

        }
    }
}
