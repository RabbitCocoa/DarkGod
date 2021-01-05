using PENet;
using Protocal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class ClientSession : PENet.PESession<NetMsg> 
    {
    /// <summary>
    /// 建立连接
    /// </summary>
    protected override void OnConnected()
    {
        Debug.Log("Server Connect");
    }
    /// <summary>
    /// 接受消息
    /// </summary>
    protected override void OnReciveMsg(NetMsg msg)
    {
        Debug.Log("Server Res:" + msg.text);
    }

    /// <summary>
    /// 断开连接
    /// </summary>
    protected override void OnDisConnected()
    {
        Debug.Log("Server Dis Connect");
    }
}

