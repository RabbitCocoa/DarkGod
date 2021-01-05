/****************************************************
    文件：GameStart.cs
	作者：Rabbitcocoa
    邮箱: 1085750968@qq.com
    日期：#CreateTime#
	功能：Nothing
*****************************************************/
using Protocal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    PENet.PESocket<ClientSession, NetMsg> client = new PENet.PESocket<ClientSession, NetMsg>();

    private void Start()
    {
        client.StartAsClient(IPCf.srvIP, IPCf.srvPort);

        client.SetLog(true, (string msg,int level) =>
        {
            switch (level)
            {
                case 0:
                    msg = "Log:" + msg;
                    Debug.Log(msg);
                    break;
                case 1:
                    msg = "warn:" + msg;
                    Debug.Log(msg);
                    break;
                case 2:
                    msg = "error:" + msg;
                    Debug.Log(msg);
                    break;
                case 3:
                    msg = "info:" + msg;
                    Debug.Log(msg);
                    break;
           
            }
        });
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            client.session.SendMsg(new NetMsg
            {
                text = "hello unity"
            });
        }
    }
}
