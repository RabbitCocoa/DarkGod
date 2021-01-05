/****************************************************
    文件：DynamicWnd.cs
	作者：Rabbitcocoa
    邮箱: 1085750968@qq.com
    日期：2021/1/5 14:17:34
	功能：动态面板
*****************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicWnd : WindowRoot
{
    public Animation anim;
    public Text tipsText;

    private Queue<string> tipsQue = new Queue<string>();
    private bool isPlay = false;
    protected override void InitWnd()
    {
        base.InitWnd();

        SetActive(tipsText, false);
    }

    public void AddTips(string tips)
    {
        lock (tipsQue)
        {
            tipsQue.Enqueue(tips);
        }
    }

    private void Update()
    {
        if (tipsQue.Count > 0 && !isPlay)
        {
            lock (tipsQue)
            {
                string tips = tipsQue.Dequeue();
                SetTips(tips);
            }
        }
    }
    private void  SetTips(string tips)
    {
        SetActive(tipsText, true);
        SetText(tipsText, tips);
        isPlay = true;
        AnimationClip clip = anim.GetClip("AmshowTips");
        anim.Play();
        //延时关闭

        StartCoroutine(AniPlayDone(clip.length, () =>
        {
            SetActive(tipsText, false);
            isPlay = false;
        }
        ));
    }

    private IEnumerator AniPlayDone(float setc,Action ac)
    {
        yield return new WaitForSeconds(setc);
        if (ac != null)
        {
            ac();
        }
    }
}
