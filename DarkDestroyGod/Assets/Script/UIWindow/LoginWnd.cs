/****************************************************
    文件：LoginWnd.cs
	作者：Rabbitcocoa
    邮箱: 1085750968@qq.com
    日期：2021/1/4 16:18:48
	功能：登录注册界面
*****************************************************/
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

        //TODO 更新账号密码
    }
}
