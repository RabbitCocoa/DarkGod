/****************************************************
	文件：PECommon.cs
	作者：rabbitcocoa
	邮箱: 1085750968@qq.com
	日期：2021/01/06 14:36   	
	功能：客户端服务端共用工具类
*****************************************************/
using PENet;
using PEProtocol;

public enum LogType
{
    Log = 0,
    Warn = 1,
    Error = 2,
    Info = 3
}
public class PECommon
{
  
    public static void Log(string msg="", LogType type = LogType.Log)
	{
		LogLevel lv = (LogLevel)type;
		PETool.LogMsg(msg, lv);	
	}
	//计算战斗力
	public static int GetFightByProps(PlayerData data)
    {

		return data.lv * 100 + data.ad + data.ap + data.addef + data.apdef;
    }

	//计算经验值
	public static int GetExpUpVaByLv(int lv)
    {
		return 100 * lv * lv;
    }
}

