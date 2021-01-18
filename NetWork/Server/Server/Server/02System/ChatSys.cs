using PEProtocol;
using System;
using System.Collections.Generic;
/****************************************************
文件：ChatSys.cs
作者：rabbitcocoa
邮箱: 1085750968@qq.com
日期：2021/01/15 16:44   	
功能：聊天业务系统
*****************************************************/


public class ChatSys
{
    private static ChatSys instance = null;
    public static ChatSys Instance
    {
        get
        {
            if (instance == null)
                instance = new ChatSys();

            return instance;
        }
    }
    private CacheSvc cacheSvc = null;
    public void Init()
    {
        PECommon.Log("ChatSys Init Done.");
        cacheSvc = CacheSvc.Instance;
    }

    public void SndChat(MsgPack pack)
    {
        PlayerData pd = cacheSvc.GetPlayeDataBySession(pack.session);
        SndChat chat = pack.msg.sndChat;

        GameMsg msg = new GameMsg
        {
            cmd = (int)CMD.PshChat,
            pndChat = new PshChat
            {
                name = pd.name,
                chat = chat.chat
            }
        };

        //广播所有在线客户端
        List<ServerSession> lst = cacheSvc.GetOnlineSeverSession();
        byte[] bytes = PENet.PETool.PackNetMsg(msg);
        for(int i = 0; i < lst.Count; i++)
        {
            lst[i].SendMsg(bytes);
        }


    }
}

