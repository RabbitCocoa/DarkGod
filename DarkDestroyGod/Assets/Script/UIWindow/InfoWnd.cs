/****************************************************
    文件：InfoWnd.cs
	作者：Rabbitcocoa
    邮箱: 1085750968@qq.com
    日期：2021/1/13 16:8:16
	功能：Nothing
*****************************************************/

using PEProtocol;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class InfoWnd : WindowRoot
{

    #region UI
    public Text txInfo;
    public Text txExp;
    public Text txPower;
    public Image imgPower;
    public Image imgExp;

    public Text txOccp;
    public Text txBattle;
    public Text txHp;
    public Text txHurt;
    public Text txDefense;
    public Button btnClose;
    #endregion
    protected override void InitWnd()
    {
        base.InitWnd();
        RefreshUI();
        RegTouchEvts();
    }

    private void RefreshUI()
    {
        PlayerData data = GameRoot.instance.PlayerData;
        SetText(txInfo, data.name + "LV." + data.lv);
        SetText(txExp, data.exp + "/" + PECommon.GetExpUpVaByLv(data.lv));
        SetText(txPower, data.power + "/" + 150);

        imgExp.fillAmount = data.exp * 1.0f / PECommon.GetExpUpVaByLv(data.lv);
        imgPower.fillAmount = data.power * 1.0f / 150;
        SetText(txOccp, "暗夜刺客");
        SetText(txHp, data.hp);
        SetText(txHurt, data.ad);
        SetText(txDefense, data.addef);
        SetText(txBattle, PECommon.GetFightByProps(data));

        //detail
    }
    public void ClickCloseBtn()
    {
        audioSvc.PlayUIAudio(Constants.UIClickBtn);
        MainCitySyc.Instance.CloseInfoWnd();
        SetWndState(false);
        
    }

    public RawImage imgCharacter;

    private Vector2 startPos;
    private void RegTouchEvts()
    {
        OnClickDown(imgCharacter.gameObject, (PointerEventData evt) =>
        {
            startPos = evt.position;
            MainCitySyc.Instance.SetStartRotate();

        });
        OnClickDrag(imgCharacter.gameObject, (PointerEventData evt) =>
         {
             float rotate = (evt.position.x - startPos.x)*0.4f;
             MainCitySyc.Instance.SetPlayerRotate(rotate);

         });
    }
}