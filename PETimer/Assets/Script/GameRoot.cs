/****************************************************
    文件：GameRoot.cs
	作者：Rabbitcocoa
    邮箱: 1085750968@qq.com
    日期：2021/1/18 10:26:0
	功能：入口
*****************************************************/

using UnityEngine;

public class GameRoot : MonoBehaviour 
{
    private void Start()
    {

        TimerSys timerSys = GetComponent<TimerSys>();
        timerSys.Init();

        AddTimerTask();
    }

    public void AddTimerTask()
    {
        TimerSys.Instance.AddTimeTask(()=>
        {
            Debug.Log("AAA");
        },1000,0);

    }
}