/****************************************************
	文件：Class1.cs
	作者：rabbitcocoa
	邮箱: 1085750968@qq.com
	日期：2021/01/05 17:32   	
	功能：网络通信协议(客户服务共用)
*****************************************************/
using PENet;
using System;
namespace PEProtocol
{
	[Serializable]
    public class GameMsg:PEMsg
    {
		public string text;
    }

	public class SrvCfg
    {
		public const string srvIP = "127.0.0.1";
		public const int srvPort = 17666;
    }
}
