/****************************************************
    文件：DialogWnd.cs
	作者：Rabbitcocoa
    邮箱: 1085750968@qq.com
    日期：2021/1/14 17:5:49
	功能：Nothing
*****************************************************/

using PEProtocol;
using UnityEngine;
using UnityEngine.UI;
public class DialogWnd : WindowRoot 
{
    public Image imgIcon;
    public Text txtName;
    public Text txtDialog;
    private AutoGuideCfg curtTaskData;

    private PlayerData pd;
    private string[] dialogArr;
    private int index;
    protected override void InitWnd()
    {
        base.InitWnd();
        pd = GameRoot.instance.PlayerData;
        curtTaskData = MainCityWnd.curtTaskData;
        dialogArr = curtTaskData.dilogArr.Split('#');
        index = 1;
        SetTalk();
    }

    private void SetTalk()
    {
        string[] talkArr = dialogArr[index].Split('|');
        if (talkArr[0] == "0")
        {
            //自己
            SetSprite(imgIcon,PathDefine.SelfIcon);
            SetText(txtName,pd.name);
        }else
        {
            //NPC
            switch (curtTaskData.npcID)
            {
                case 0:
                    SetSprite(imgIcon, PathDefine.NpcWiseHeadIcon);
                    SetText(txtName, "智者");
                    break;
                case 1:
                    SetSprite(imgIcon, PathDefine.NpcGeneralHeadIcon);
                    SetText(txtName, "将军");
                    break;
                case 2:
                    SetSprite(imgIcon, PathDefine.NpcArtisanHeadIcon);
                    SetText(txtName, "工匠");
                    break;
                case 3:
                    SetSprite(imgIcon, PathDefine.NpcTraderHeadIcon);
                    SetText(txtName, "商人");
                    break;
                default:
                    SetSprite(imgIcon, PathDefine.NpcguideHeadIcon);
                    SetText(txtName, "包菜");
                    break;

            }
        }
        imgIcon.SetNativeSize();
        SetText(txtDialog, talkArr[1].Replace("$name", pd.name));
    }

    public void ClickNextBtn()
    {
        audioSvc.PlayUIAudio(Constants.UIClickBtn);
        index++;
        if (index < dialogArr.Length)
            SetTalk();
        else
        {
            GameMsg msg = new GameMsg
            {
                cmd = (int)CMD.ReqGuide,
                reqGuide = new ReqGuide
                {
                    guideId = curtTaskData.ID
                }
            };
            netSvc.SendMsg(msg);
            SetWndState(false);
        }
    }
}