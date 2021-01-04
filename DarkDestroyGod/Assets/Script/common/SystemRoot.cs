/****************************************************
    文件：SystemRoot.cs
	作者：Rabbitcocoa
    邮箱: 1085750968@qq.com
    日期：2021/1/4 17:40:15
	功能：业务系统
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemRoot : MonoBehaviour
{
    protected ResSvc resSvc;
    protected AudioSvc audioSvc;

    public virtual void InitSys()
    {
        resSvc = ResSvc.instance;
        audioSvc = AudioSvc.instance;
    }
}
