
/****************************************************
	文件：ServerSession.cs
	作者：rabbitcocoa
	邮箱: 1085750968@qq.com 
	日期：2021/01/05 17:38   	
	功能：网络会话连接
*****************************************************/
using System;
using PEProtocol;
using PENet;
 public class ServerSession:PESession<GameMsg>
 {
    public int sessionID=0;
    protected override void OnConnected()
    {
        sessionID = ServerRoot.Instance.GetSessionID();
        PECommon.Log("Client Connect:sessionID-" + sessionID);
    }

    protected override void OnDisConnected()
    {
        LoginSys.Instance.ClearOffData(this);
        PECommon.Log("Client DisConnect:sessionID-"+sessionID);
    }

    protected override void OnReciveMsg(GameMsg msg)
    {
        PECommon.Log(sessionID+"-Client Req:"+((CMD)msg.cmd).ToString());
        NetSvc.Instance.AddMsgQue(this, msg);

    }

  
}

