/****************************************************
    文件：PETimeTask.cs
	作者：Rabbitcocoa
    邮箱: 1085750968@qq.com
    日期：2021/1/18 10:35:52
	功能: 定时任务类

*****************************************************/
using System;
using UnityEngine;

public enum  PETime
{
    Millisecond,
    Second,
    Minute,
    Hour,
    Day
}
public class PETimeTask 
{
    public int tid;

    public float destTime;
    public Action callBack;
    public float delayTime;
    //执行次数 无线则为0
    public int repeat_count;

    public PETimeTask(int tid,Action cb,float destTime,float delayTime, int repeat_count)
    {
        this.tid = tid;
        this.destTime = destTime;
        this.callBack = cb;
        this.delayTime = delayTime;
        this.repeat_count = repeat_count;
    }

    public void UpdateTime()
    {
        destTime += delayTime;
    }


}