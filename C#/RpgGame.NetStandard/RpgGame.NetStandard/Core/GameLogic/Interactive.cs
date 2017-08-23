using System;

namespace RpgGame.NetStandard.Core.GameLogic
{
    public interface IInteractive
    {
        void Pop(string msg);
        void Alert(string msg);
        bool Confirm(string msg);

        string Prompt(string msg);

    }
    public class SimpleInteractive : IInteractive
    {
        public void Pop(string msg)
        {
            Console.WriteLine(msg);
        }

        public void Alert(string msg)
        {
            Console.WriteLine(msg);
        }

        public bool Confirm(string msg)
        {
            Console.WriteLine(msg, "\n是(输入Y回车)\n否(任意键回车)");
            return Console.ReadLine().ToLower() == "Y";
        }

        public string Prompt(string msg)
        {
            Console.WriteLine(msg);
            return Console.ReadLine();
        }
    }
}



