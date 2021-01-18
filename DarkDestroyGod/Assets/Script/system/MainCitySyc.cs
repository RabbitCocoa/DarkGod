/****************************************************
    文件：MainCitySyc.cs
	作者：Rabbitcocoa
    邮箱: 1085750968@qq.com
    日期：2021/1/11 15:56:41
	功能：主城业务系统
*****************************************************/

using PEProtocol;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class MainCitySyc : SystemRoot 
{
    public static MainCitySyc Instance = null;
    public MainCityWnd mainCityWnd;
    public DialogWnd dialogWnd;
    private PlayerController playerCtrl;

    private AutoGuideCfg curtTaskData;
    //人物相机
    private Transform charshowTran;
    public override void InitSys()
    {
        base.InitSys();
        Instance = this;
    }

    private Transform[] npcPosTrans;
    private NavMeshAgent navAgent;
    bool isNav = false;

    #region 导航
    //执行引导任务
    public void RunTask(AutoGuideCfg cfg)
    {
        if (cfg != null)
            curtTaskData = cfg;

        navAgent.enabled = true;
        //解析任务
        if (curtTaskData.npcID != -1)
        {
            //寻路
            float dis = Vector3.Distance(playerCtrl.transform.position, npcPosTrans[cfg.npcID].position);
            if (dis < 0.5f)
            {
                IsArriveNavPos();
            }
            else
            {
                playerCtrl.ctl.enabled = false;
                isNav = true;
            
                navAgent.speed = Constants.PlayerModeSpeed;
                navAgent.SetDestination(npcPosTrans[cfg.npcID].position);
                playerCtrl.SetBlend(Constants.BlendWalk);
            }

        }else
        {
            OpenGuideWnd();
        }
    }
    //打开引导界面
    private void OpenGuideWnd()
    {
        dialogWnd.SetWndState();
    }

    private void IsArriveNavPos()
    {
        float dis = Vector3.Distance(playerCtrl.transform.position, npcPosTrans[curtTaskData.npcID].position);
        if (dis < 0.5f)
        {
            isNav = false;
            playerCtrl.ctl.enabled = true;
            navAgent.isStopped = true;
            playerCtrl.SetBlend(Constants.BlendIdle);
            navAgent.enabled = false;
            OpenGuideWnd();
        }
    }
    private void StopNav()
    {
        if (isNav)
        {
            isNav = false;
            playerCtrl.ctl.enabled = true;
            navAgent.isStopped = true;
            playerCtrl.SetBlend(Constants.BlendIdle);
            navAgent.enabled = false;
        }
    }
    #endregion

    private void Update()
    {
        if (isNav)
        {
            playerCtrl.SetCamera();
            IsArriveNavPos();
        }
    }


    public void ResGuide(GameMsg msg)
    {
        ResGuide res = msg.resGuide;

        GameRoot.AddTips(Constants.Color( "金币+" + curtTaskData.coin,TxtColor.blue));
        GameRoot.AddTips(Constants.Color("经验+" + curtTaskData.exp,TxtColor.yellow));
        switch (curtTaskData.actID)
        {
            case 0:
                //对话
                break;
            case 1:
                //@TODO副本
                break;
            case 2:
                //TODO强化
                break;
            case 3:
                //TODO体力
                break;
            case 4:
                break;
            case 5:
                break;
        }
        GameRoot.instance.SetPlayerDataByGuide(res);
        mainCityWnd.RefreshUI();
    }
    #region 进入主城
    /// <summary>
    /// 进入主城
    /// </summary>
    public void EnterMainCity()
    {
        MapCfg mapData = resSvc.GetMapCfgData(Constants.MainCityMapID);

        resSvc.AsyncLoadScene(mapData.sceneName, () => {
            PECommon.Log("Enter MainCity...");

            //TODO加载主角
            LoadPlayer(mapData);
            //打开主城UI
            mainCityWnd.SetWndState(true);
           // mainCityWnd.RefreshUI();

            //播放主城音乐
            audioSvc.PlayBGMusic(Constants.MainCityBgm);

            //TODO 设置人物相机
            if (charshowTran != null)
            {
                charshowTran.gameObject.SetActive(false);
            }

            GameObject map = GameObject.FindGameObjectWithTag("MapRoot");
            MainCityNPC mcm = map.GetComponent<MainCityNPC>();
            npcPosTrans = mcm.npcPos;
        });

       
        
    }

    private void LoadPlayer(MapCfg mapData)
    {
        GameObject player = null;
        player=resSvc.LoadPrefab(PathDefine.AssissnCityPlayerPrefab,true);
        player.transform.position = mapData.playerBornPos;
        player.transform.localEulerAngles = mapData.playerBornRote;
        player.transform.localScale=new  Vector3(1.5f, 1.5f, 1.5f);
        playerCtrl = player.GetComponent<PlayerController>();
        playerCtrl.Init();

        navAgent = player.GetComponent<NavMeshAgent>();
        //相机初始化
        Camera.main.transform.position = mapData.mainCamPos;
        Camera.main.transform.localEulerAngles = mapData.mainCamRote ;
    }

    #endregion

    #region 移动
    public void SetMoveDir(Vector2 dir)
    {
        StopNav();
        if (dir == Vector2.zero)
            playerCtrl.SetBlend(Constants.BlendIdle);
        else
            playerCtrl.SetBlend(Constants.BlendWalk);

        playerCtrl.Dir = dir;

    }

    #endregion

    #region 信息面板
    /// <summary>
    /// 信息界面
    /// </summary>
    public InfoWnd infownd;
    public void OpenInfoWnd()
    {
        StopNav();
        if (charshowTran == null)
        {
            charshowTran = GameObject.FindGameObjectWithTag("CharShowCam").transform;
        }
        //设置人物展示相机相对位置
        charshowTran.localPosition = playerCtrl.transform.position + playerCtrl.transform.forward * 6.0f+new Vector3(0,1.2f,0);
        charshowTran.localEulerAngles = new Vector3(0, 180 + playerCtrl.transform.localEulerAngles.y, 0);
        charshowTran.gameObject.SetActive(true);

        infownd.SetWndState();
    }
    public void CloseInfoWnd()
    {
        charshowTran.gameObject.SetActive(false);
    }
    private float startRotate = 0;
    public void SetStartRotate( )
    {
        startRotate = playerCtrl.transform.localEulerAngles.y;
    }
    public void ResetRotate()
    {
        playerCtrl.transform.localEulerAngles = new Vector3(0, startRotate, 0);
    }
    public void SetPlayerRotate(float rotate)
    {
        //playerCtrl.transform.localEulerAngles += new Vector3(0, rotate, 0);
        playerCtrl.transform.localEulerAngles = new Vector3(0, startRotate + rotate, 0);

    }
    #endregion

    #region 聊天
    public ChatWnd chatWnd;
   
    public void PshChat(GameMsg msg)
    {
        chatWnd.AddChatMsg(msg.pndChat.name, msg.pndChat.chat);
    }
    public void OpenChatWnd(bool isChat)
    {
        chatWnd.SetWndState(isChat);
    }
    #endregion 
}