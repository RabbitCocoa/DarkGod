/****************************************************
    文件：GameRoot.cs
	作者：Rabbitcocoa
    邮箱: 1085750968@qq.com
    日期：2021/1/4 15:6:15
	功能：游戏启动入口
*****************************************************/
using PEProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoot : MonoBehaviour
{
    public static GameRoot instance = null;
    //加载界面API
    public LoadingWnd loadingWnd;
    public DynamicWnd dynamicWnd;

    //玩家数据
    private PlayerData playerData = null;
    public PlayerData PlayerData
    {
        get
        {
            return playerData;
        }    
    }


    public void SetPlayerData(RspLogin data)
    {
        playerData = data.playerData;
    }
    public void SetPlayerName(string name)
    {
        playerData.name = name;
    }
    private void Start()
    {
        instance = this;
        DontDestroyOnLoad(this);
        ClearUIRoot();
        Init();
    }
    
    private void ClearUIRoot()
    {
        Transform canvas = transform.Find("Canvas");
        for(int i = 0; i < canvas.childCount; i++)
        {
            canvas.GetChild(i).gameObject.SetActive(false);
        }
        dynamicWnd.SetWndState();
    }

   private void Init()
    {
        //服务模块初始化
        NetSvc netSvc = GetComponent<NetSvc>();
        netSvc.InitSvc();

        ResSvc res = GetComponent<ResSvc>();
        res.InitSvc();

        AudioSvc audioSvc = GetComponent<AudioSvc>();
        audioSvc.InitSvc();


        //业务系统初始化
        LoginSyc logSyc = GetComponent<LoginSyc>();
        logSyc.InitSys();
        MainCitySyc mainCitySyc = GetComponent<MainCitySyc>();
        mainCitySyc.InitSys();
   
       
        //窗口初始化
        //进入登录场景并加载UI
        logSyc.EnterLogin();
    }




    //添加tips调用此
    public  static void AddTips(string tips)
    {
        instance.dynamicWnd.AddTips(tips);
    }

    public void SetPlayerDataByGuide(ResGuide data )
    {
        playerData.coin = data.coin;
        playerData.lv = data.lv;
        playerData.exp = data.exp;
        playerData.guideid = data.guideId;
    }
}
