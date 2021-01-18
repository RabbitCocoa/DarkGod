/****************************************************
    文件：MainCityWnd.cs
	作者：Rabbitcocoa
    邮箱: 1085750968@qq.com
    日期：2021/1/11 15:55:43
	功能：Nothing
*****************************************************/
using PEProtocol;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainCityWnd : WindowRoot 
{
    #region UIDefine
    public Text txFight;
    public Text txPower;
    public Image impPowerPrg;
    public Text txLevel;
    public Text txName;
    public Text txExpPrg;

    public Transform expPrgTrans;
    #endregion

    #region MainFunction
    protected override void InitWnd()
    {
        base.InitWnd();
        defaultPos = imgDirBg.transform.position;
        pointDis = Screen.height * 1.0f / Constants.ScreenStandardHeight*Constants.ScreenOPDis; 
        SetActive(imgDirBgPoint, false);
        RegisterTouchEvets();
        RefreshUI();
    }


    public static AutoGuideCfg curtTaskData = null;
    public Button guideBtn;
    public void RefreshUI()
    {
        #region UI显示
        PlayerData playerData = GameRoot.instance.PlayerData;
        SetText(txFight,PECommon.GetFightByProps(playerData));
        SetText(txPower, "体力:" + playerData.power + "/150");
        impPowerPrg.fillAmount = playerData.power * 1.0f / 150;

        SetText(txLevel,playerData.lv);
        SetText(txName, playerData.name);
        #endregion

        //expprg
        #region Expprg自适应
        int expPrgVal =(int)((playerData.exp * 1.0f) / PECommon.GetExpUpVaByLv(playerData.lv) * 100);
        SetText(txExpPrg, expPrgVal+"%");

        int index = expPrgVal / 10;
        for(int i=0;i<expPrgTrans.childCount; i++)
        {

            Image img = expPrgTrans.GetChild(i).GetComponent<Image>();
            if (i < index)
                img.fillAmount = 1;
            else if (i == index)
            {
                img.fillAmount = expPrgVal %10*1.0f/10;
            }else
            {
                img.fillAmount = 0;
            }
                
        }

        GridLayoutGroup grid = expPrgTrans.GetComponent<GridLayoutGroup>();

        float gloablRate = 1.0f * Constants.ScreenStandardHeight / Screen.height;

        float real_width = Screen.width * gloablRate;

        float width = (real_width - 180) / 10;

        grid.cellSize = new Vector2(width, 7);
        #endregion

        #region 任务图标
        curtTaskData = ResSvc.instance.GetGuideCfg(playerData.guideid);
        if (curtTaskData != null)
        {
            SetGuideImage(curtTaskData.npcID);
        }
        else
        {
            SetGuideImage(-1);
        }

        #endregion
    }

    private void SetGuideImage(int npcid)
    {
        string path = "";
        Image img = guideBtn.GetComponent<Image>();
        switch (npcid)
        {
            case Constants.NPCWiseMan:
                path = PathDefine.WiseManHead;
                break;
            case Constants.NPCTrader:
                path = PathDefine.TraderHead;
                break;
            case Constants.NPCArtisan:
                path = PathDefine.ArtisanHead;
                break;
            case Constants.NPCGeneral:
                path = PathDefine.GeneralHead;
                break;
            default:
                path = PathDefine.TaskHead;
                break;

        }
        SetSprite(img, path);
    }

    #endregion

    #region clickEvts
    //菜单
    public Button MenuBtn;
    private bool MenuState = false;
    public Animation menuAni;
    public void ClickMenuBtn()
    {
        audioSvc.PlayUIAudio(Constants.UIExtentBtn);
        MenuState = !MenuState;
        AnimationClip clip = null;
        if (MenuState)
        {
            clip = menuAni.GetClip("OpenMenu");
        }
        else
        {
            clip = menuAni.GetClip("CloseMenu");
        }
        menuAni.Play(clip.name);
    }

    //轮盘
    public Image imgDirBg;
    public Image imgDirBgPoint;
    public Image imgTouch;
    private Vector2 startPos = Vector2.zero;
    private Vector2 defaultPos;
    private float pointDis;
    private void RegisterTouchEvets()
    {
        OnClickDown (imgTouch.gameObject, (PointerEventData evt) => {
            imgDirBg.transform.position = evt.position;
            startPos = evt.position;
            SetActive(imgDirBgPoint);
        });

        OnClickUp(imgTouch.gameObject, (PointerEventData evt) => {
            imgDirBg.transform.position = defaultPos;
            imgDirBgPoint.transform.localPosition = Vector2.zero;
            SetActive(imgDirBgPoint, false);
            MainCitySyc.Instance.SetMoveDir(Vector2.zero);
            //TODO 方向信息传递
        });


        OnClickDrag(imgTouch.gameObject, (PointerEventData evt) =>
        {
            //方向
            Vector2 dir = (evt.position - startPos);
            float len = dir.magnitude;
            if (len > Constants.ScreenOPDis)
            {
               Vector2 clampDir = Vector2.ClampMagnitude(dir, Constants.ScreenOPDis);
                imgDirBgPoint.transform.position = startPos+ clampDir;
            }else
            {
                imgDirBgPoint.transform.position = evt.position;
            }
            //TODO 
            MainCitySyc.Instance.SetMoveDir(dir.normalized);
            
            
        });
    }

   
    public void ClickInfo()
    {
        audioSvc.PlayUIAudio(Constants.UIOpenPage);
        MainCitySyc.Instance.OpenInfoWnd();
    }

    public void ClickGuideBtn() {
        audioSvc.PlayUIAudio(Constants.UIClickBtn);

        if (curtTaskData != null)
        {
            MainCitySyc.Instance.RunTask(curtTaskData);
        }else
        {
            GameRoot.AddTips("当前没有正在执行的任务...");
        }
    }
    bool isChat = false;
    public void ClickChatBtn()
    {
        isChat = !isChat;
        audioSvc.PlayUIAudio(Constants.UIClickBtn);
        MainCitySyc.Instance.OpenChatWnd(isChat);  
    }
    #endregion




}