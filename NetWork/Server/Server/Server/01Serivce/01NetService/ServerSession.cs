
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
 class ServerSession:PESession<GameMsg>
 {
    protected override void OnConnected()
    {
        PETool.LogMsg("Client Connect");
        SendMsg(new GameMsg
        {
            text = "Welcome to connect"
        });

    }

    protected override void OnDisConnected()
    {
        PETool.LogMsg("Client DisConnect");
    }

    protected override void OnReciveMsg(GameMsg msg)
    {
        PETool.LogMsg("Client Req:"+msg.text);
        SendMsg(new GameMsg
        {
            text = "SrvRsp" + msg.text
        }); ;
    }
}

