/****************************************************
    文件：ClientSession.cs
	作者：Rabbitcocoa
    邮箱: 1085750968@qq.com
    日期：2021/1/6 14:21:29
	功能：客户端连接
*****************************************************/
using PEProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSession : PENet.PESession<GameMsg>
{
    protected override void OnConnected()
    {
        GameRoot.AddTips("连接服务器成功");
        PECommon.Log("Server Connect Success");
     }

    protected override void OnDisConnected()
    {
        GameRoot.AddTips("服务器断开");
        PECommon.Log("Server DisConnect");
    }

    protected override void OnReciveMsg(GameMsg msg)
    {
        PECommon.Log("Server Res:" + ((CMD)msg.cmd).ToString());
        NetSvc.instance.AddMsg(msg);
    }
}
