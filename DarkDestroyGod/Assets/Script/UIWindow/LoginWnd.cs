/****************************************************
    文件：LoginWnd.cs
	作者：Rabbitcocoa
    邮箱: 1085750968@qq.com
    日期：2021/1/4 16:18:48
	功能：登录注册界面
*****************************************************/
using PEProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginWnd : WindowRoot
{
    public InputField iptAcct;
    public InputField iptPsw;
    public Button btnEnter;
    public Button btnNotice;


   protected override void InitWnd()
    {
        base.InitWnd();
        //获取本地存储的账号密码
        if (PlayerPrefs.HasKey("Account"))
        {
            iptAcct.text = PlayerPrefs.GetString("Account");
        }else
        {
            iptAcct.text = "";
        }
        if (PlayerPrefs.HasKey("passwd"))
        {
            iptPsw.text = PlayerPrefs.GetString("passwd");
        }
        else
        {
            iptPsw.text = "";
        }

     
    }


    ///点击进入游戏
    public void ClickEnterBtn()
    {
        audioSvc.PlayUIAudio(Constants.LoginUi);

        string acctd = iptAcct.text;
        string pswd = iptPsw.text;
        if (acctd!=""  && pswd != "")
        {
            PlayerPrefs.SetString("Account", acctd);
            PlayerPrefs.SetString("passwd", pswd);

            //TODO 发送网络消息 请求登录
            GameMsg msg = new GameMsg()
            {
                cmd = (int)CMD.ReqLogin, 
                reqLogin = new ReqLogin
                {
                    acct = acctd,
                    pass=pswd
                }
            };
            netSvc.SendMsg(msg);
            //TOREMOVE
        }
        else
        {
            GameRoot.AddTips("账号或密码为空");
        }
    }

    public void ClickNoticeBtn()
    {
        audioSvc.PlayUIAudio(Constants.UIClickBtn);
        GameRoot.AddTips("功能正在开发");
    }
    
}
