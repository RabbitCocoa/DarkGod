/****************************************************
    文件：CreateWnd.cs
	作者：Rabbitcocoa
    邮箱: 1085750968@qq.com
    日期：2021/1/5 14:46:14
	功能：角色创建界面
*****************************************************/
using PEProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateWnd : WindowRoot
{
    public InputField iptName;

    protected override void InitWnd()
    {
        base.InitWnd();
        //TODO
        //显示随机名字
        iptName.text = res.GetRDNameData(false);
    }

    public void ClickChangeNameBtn()
    {
        audioSvc.PlayUIAudio(Constants.UIClickBtn);
        string rdName = res.GetRDNameData(false);
        iptName.text = rdName;
    }

    public void ClickEnterBtn()
    {
        audioSvc.PlayUIAudio(Constants.UIClickBtn);
        if (iptName.text != "")
        {
            //@TODO 
            GameMsg msg = new GameMsg
            {
                cmd = (int)CMD.ReName,
                rename = new Rename
                {
                    name = iptName.text
                }
            };
            //发送网络消息
            netSvc.SendMsg(msg);
        }else
        {
            GameRoot.AddTips("当前名字不合法");
        }
    }
}
