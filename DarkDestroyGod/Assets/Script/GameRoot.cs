/****************************************************
    文件：GameRoot.cs
	作者：Rabbitcocoa
    邮箱: 1085750968@qq.com
    日期：2021/1/4 15:6:15
	功能：游戏启动入口
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoot : MonoBehaviour
{
    public static GameRoot instance = null;
    //加载界面API
    public LoadingWnd loadingWnd;
    private void Start()
    {
        instance = this;
        DontDestroyOnLoad(this);
        Init();
    }
    

   private void Init()
    {
        //服务模块初始化
        ResSvc res = GetComponent<ResSvc>();
        res.InitSvc();

        AudioSvc audioSvc = GetComponent<AudioSvc>();
        audioSvc.InitSvc();

        //业务系统初始化
        LoginSyc logSyc = GetComponent<LoginSyc>();
        logSyc.InitSys();


        //窗口初始化

        //进入登录场景并加载UI
        logSyc.EnterLogin();

    }
}
