
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

    protected override void OnConnected()
    {
        PECommon.Log("Client Connect");
        

    }

    protected override void OnDisConnected()
    {
        PECommon.Log("Client DisConnect");
    }

    protected override void OnReciveMsg(GameMsg msg)
    {
        PECommon.Log("Client Req:"+((CMD)msg.cmd).ToString());
        NetSvc.Instance.AddMsgQue(this, msg);

    }


}

