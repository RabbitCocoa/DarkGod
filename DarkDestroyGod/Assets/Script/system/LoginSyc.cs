/****************************************************
    文件：LoginSyc.cs
	作者：Rabbitcocoa
    邮箱: 1085750968@qq.com
    日期：2021/1/4 15:16:17
	功能：登录业务系统
*****************************************************/
using PEProtocol;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginSyc : SystemRoot
{
    public static LoginSyc Instance = null;
   public LoginWnd loginWnd;
    public CreateWnd createWnd;

    public override void InitSys()
    {
        base.InitSys();
        PECommon.Log("登录初始化");
        Instance = this;
    }
    /// <summary>
    /// 进入登录场景
    /// </summary>
    public void EnterLogin()
    {
        //异步加载登录场景
        //在加载完成后打开注册登录界面，并播放背景音乐
        ResSvc.instance.AsyncLoadScene(Constants.SceneLoginStr,OpenLoginWnd);
    }

    /// <summary>
    /// 打开登录界面
    /// </summary>
    public void OpenLoginWnd()
    {
        loginWnd.SetWndState();
        audioSvc.PlayBGMusic(Constants.LoginBgm,true);
    }

    
    public void RspLogin(GameMsg  msg)
    {
        GameRoot.AddTips("登录成功");
        GameRoot.instance.SetPlayerData(msg.rspLogin);

        if (msg.rspLogin.playerData.name == "")
        {
            //打开角色创建界面
            createWnd.SetWndState(true);
        }else
        {
            //进入主城TODO
           MainCitySyc.Instance.EnterMainCity();
        }

        //关闭登录界面
        loginWnd.SetWndState(false);
    }

    public void ReName(GameMsg msg)
    {
        GameRoot.instance.SetPlayerName(msg.rename.name);

        //TODO
        //跳转场景到主城
        MainCitySyc.Instance.EnterMainCity();

        //关闭创建场景
        createWnd.SetWndState(false);
    }
}
