/****************************************************
	文件：PECommon.cs
	作者：rabbitcocoa
	邮箱: 1085750968@qq.com
	日期：2021/01/06 14:36   	
	功能：客户端服务端共用工具类
*****************************************************/
using PENet;
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
}

