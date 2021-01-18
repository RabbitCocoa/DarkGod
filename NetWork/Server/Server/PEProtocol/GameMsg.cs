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
		public ReqLogin reqLogin;
		public RspLogin rspLogin;
		public Rename rename;

		public ReqGuide reqGuide;
		public ResGuide resGuide;

		public SndChat sndChat;
		public PshChat pndChat;
    }
	public enum CMD
    {
		None=0,
		//登录相关
		ReqLogin=101,
		RspLogin=102,
		ReName=103,

			//主城相关
			ReqGuide=201,
			ResGuied=202,


			SndChat=206,
		PshChat = 207
    }

	public enum ErrorCode
    {
		None=0,//无错误
		AcctIsOnline=501,//账号已上线
		WrongPass=502,//账号或密码错误
		NameRepeat,//名字已存在
		UpdateDbError,//更新数据库失败
		ServerDataError
    }

    #region 登录相关
    [Serializable]
	public class ReqLogin
    {
		public string acct;
		public string pass;
    }
	
	[Serializable]
	public class RspLogin
    {
		//TODO
		public PlayerData playerData;
    }
    [Serializable]
	public class PlayerData
    {
		public int id;
		public string name;
		public int lv;
		public int exp;
		public int power;
		public int coin;
		public int diamond;
		//@TOADD

		public int hp;
		public int ad;
		public int ap;
		public int addef;
		public int apdef;
		public int dodge; //闪避
		public int pierce; //穿透
		public int critical; //暴击
		public int guideid;

    }

	[Serializable]
	public class Rename
    {
		public string name;
    }
    #endregion

    #region 引导相关
    #endregion


    #region 聊天相关
	[Serializable]
	public class SndChat
    {
		public string chat;
    }
	[Serializable]
	public class PshChat
    {
		public string name;
		public string chat;
    }
    #endregion
    [Serializable]
    public class ReqGuide
    {
		public int guideId;
    }
	[Serializable]
	public class ResGuide
    {
		public int guideId;
		public int coin;
		public int lv;
		public int exp;

    }

    

    public class SrvCfg
    {
		public const string srvIP = "127.0.0.1";
		public const int srvPort = 17666;
    }
}
