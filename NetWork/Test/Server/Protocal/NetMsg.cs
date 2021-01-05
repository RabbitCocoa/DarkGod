using PENet;
using System;
namespace Protocal
{
    //序列化
    [Serializable]
    public class NetMsg:PEMsg
    {
        public string text;
    }

    public class IPCf
    {
        public const string srvIP = "127.0.0.1";
        public const int srvPort = 17666;
    }
}
