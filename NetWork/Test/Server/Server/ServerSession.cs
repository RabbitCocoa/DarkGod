using System;
using Protocal;
using PENet;


class ServerSession:PENet.PESession<NetMsg>{
    /// <summary>
    /// 建立连接
    /// </summary>
    protected override void OnConnected()
    {
        PETool.LogMsg("Client Connect");
        SendMsg(new NetMsg
        {
            text = "welcome to connect:"
        }) ;
    }
    /// <summary>
    /// 接受消息
    /// </summary>
    protected override void OnReciveMsg(NetMsg msg)
    {
        PETool.LogMsg("Client Req:"+msg.text);
        SendMsg(new NetMsg
        {
            text = "SrvRsp:" + msg.text
        });

    }

    /// <summary>
    /// 断开连接
    /// </summary>
    protected override void OnDisConnected()
    {
        PETool.LogMsg("Client Dis Connect");
    }

}

