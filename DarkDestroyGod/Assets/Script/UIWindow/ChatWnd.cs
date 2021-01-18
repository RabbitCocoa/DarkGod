/****************************************************
    文件：ChatWnd.cs
	作者：Rabbitcocoa
    邮箱: 1085750968@qq.com
    日期：2021/1/15 16:6:39
	功能：Nothing
*****************************************************/

using PEProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChatWnd : WindowRoot {

    public Button imgworld;
    private int chatType;
    public Text txtChat;

    public InputField iptMessagge;
    protected override void InitWnd()
    {
        base.InitWnd();
        chatType = 0;
        imgworld.Select();
        RefreshUI();

    }
    private List<string> chatList = new List<string>();


    public void RefreshUI()
    {
        if (chatType == 0)
        {
            string chatMsg = "";
            for(int i = 0; i < chatList.Count; i++) {
                chatMsg += chatList[i] + "\n";
            }
            SetText(txtChat, chatMsg);
        }else  if(chatType == 1){
            SetText(txtChat, "未加入工会");
        }
        else if(chatType == 2)
        {
            SetText(txtChat, "暂无好友信息");
        }

    }

    #region clickEvet
    public void ClickWorldBtn()
    {
        chatType = 0;
        RefreshUI();
    }
    public void ClickGuildBtn()
    {
        chatType = 1;
        RefreshUI();
    }

    public void ClickFriendBtn()
    {
        chatType = 2;
        RefreshUI();
    }

    private bool canSend = true;
    public void ClickSend()
    {
        if (!canSend)
        {
            GameRoot.AddTips("先喝口茶再发送吧");
            return;
        }

        if(iptMessagge!=null && iptMessagge.text.Trim() != "")
        {
            if (iptMessagge.text.Length > 24)
            {
                GameRoot.AddTips("输入信息长度过长");
            }else
            {
                //发送网络消息
                GameMsg msg = new GameMsg
                {
                    cmd = (int)CMD.SndChat,
                    sndChat = new SndChat
                    {
                        chat = iptMessagge.text
                   }
                };
                 iptMessagge.text = "";
                netSvc.SendMsg(msg);
            }

            //@TODO 定时器
            canSend = false;
            StartCoroutine(MsgTimer());


        }
        else
        {
            GameRoot.AddTips("尚未输入聊天信息");

        }
    }
    IEnumerator   MsgTimer()
    {
        yield return new WaitForSeconds(5.0f);
        canSend = true;
    }
    public void AddChatMsg(string name,string msg)
    {
        chatList.Add(Constants.Color(name+":", TxtColor.blue)+ msg);
        if (chatList.Count > 12)
        {
            chatList.RemoveAt(0);
        }
        if(gameObject.activeSelf)
          RefreshUI();
    }

    #endregion
}