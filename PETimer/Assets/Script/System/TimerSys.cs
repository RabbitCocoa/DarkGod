/****************************************************
    文件：TimerSyc.cs
	作者：Rabbitcocoa
    邮箱: 1085750968@qq.com
    日期：2021/1/18 10:25:14
	功能：Nothing
*****************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;

public class TimerSys : MonoBehaviour 
{
    public static TimerSys Instance;
    private static readonly string obj = "lock";
    private static int tid;

    private List<PETimeTask> timeTaskLst = new List<PETimeTask>();

    //缓存列表
    private List<PETimeTask> tempTaskLst = new List<PETimeTask>();
    public void Init()
    {
        Instance = this;
        tid = 0;
    }


    private void Update()
    {
        for (int index = 0; index < tempTaskLst.Count; index++)
            timeTaskLst.Add(tempTaskLst[index]);
        tempTaskLst.Clear();

        for(int index = 0; index < timeTaskLst.Count; index++)
        {
            PETimeTask task = timeTaskLst[index];
            if (Time.realtimeSinceStartup*1000 >= task.destTime)
            {
                if(task.callBack!=null)
                     task.callBack();

        
                    if (task.repeat_count == 1)
                    {
                        timeTaskLst.RemoveAt(index);
                        index--;
                    }
                    else
                    {
                    if(task.repeat_count!=0)
                        task.repeat_count--;
                        task.UpdateTime();
                    }
                
            }

          

        }
    }

    public int AddTimeTask(Action action,float delayTime,PETime timeType=PETime.Millisecond,int repeat=1)
    {
        switch (timeType)
        {
            case PETime.Second:
                delayTime = delayTime * 1000;
                break;

            case PETime.Minute:
                delayTime = delayTime * 1000*60;
                break;
            case PETime.Hour:
                delayTime = delayTime * 1000*60*60;
                break;
            case PETime.Day:
                delayTime = delayTime * 1000*60*60*24;
                break;
        }

        int tid=GetTid();
        float desTimer = Time.realtimeSinceStartup*1000 + delayTime;
        PETimeTask timeTask = new PETimeTask
        (tid,
            action,
            desTimer,
            delayTime,
             repeat
        );
        tempTaskLst.Add(timeTask);

        return tid;
    }
    public int AddTimeTask(Action action, float delayTime, int repeat )
    {
        return AddTimeTask(action, delayTime, PETime.Millisecond, repeat);
    }

    private HashSet<int> tidSet = new HashSet<int>();
    private int GetTid()
    {
        lock (obj)
        {
            tid++;
            if (tid == Int32.MaxValue)
                tid = 0;

            while (tidSet.Contains(tid))
            {
                tid++;
                if (tid == Int32.MaxValue)
                    tid = 0;
            }
        }
        return tid;
    }

    public bool DeleteTime(int tid)
    {
        bool isSuccess = false;

        for(int i = 0; i < timeTaskLst.Count; i++)
        {
            PETimeTask task = timeTaskLst[i];
            if (task.tid == tid)
            {
                timeTaskLst.RemoveAt(i);
                tidSet.Remove(i);
                isSuccess = true;
                break;
            }
        }
        if (!isSuccess)
        {
            for (int i = 0; i < tempTaskLst.Count; i++)
            {
                PETimeTask task = tempTaskLst[i];
                if (task.tid == tid)
                {
                    tempTaskLst.RemoveAt(i);
                    tidSet.Remove(i);
                    isSuccess = true;
                    break;
                }
            }
        }

        return isSuccess;
    }
}