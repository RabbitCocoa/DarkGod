/****************************************************
    文件：LoadingWnd.cs
	作者：Rabbitcocoa
    邮箱: 1085750968@qq.com
    日期：2021/1/4 15:54:35
	功能：加载界面脚本
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingWnd : WindowRoot
{
    public Text txtTips;
    public Image imgFg;
    public Image imgPoint;
    public Text txtPercent;

    //前景宽度
    private float fgWidth;
    

    protected override void InitWnd()
    {
        base.InitWnd();
        fgWidth = imgFg.GetComponent<RectTransform>().sizeDelta.x;
        SetText(txtTips, "游戏tips");
        SetText(txtPercent, "0%");

        imgFg.fillAmount = 0;
        imgPoint.transform.localPosition = new Vector3(-fgWidth / 2,0,0);


    }
    /// <summary>
    /// 设置进度 传入进度
    /// </summary>
    /// <param name="prg"></param>
    public void SetProgress(float prg)
    {
        SetText(txtPercent, (int)(prg * 100) + "%");
        imgFg.fillAmount = prg;
        imgPoint.transform.localPosition = new Vector3(-fgWidth/2+prg* fgWidth, 0, 0);
    }

}
