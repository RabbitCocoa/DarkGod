/****************************************************
    文件：LoopDragonAnm.cs
	作者：Rabbitcocoa
    邮箱: 1085750968@qq.com
    日期：2021/1/4 17:55:23
	功能：Nothing
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopDragonAnm : MonoBehaviour
{
    private Animation ani;
    private void Awake()
    {
        ani = transform.GetComponent<Animation>();
    }

    private void Start()
    {
        if (ani != null) {
            InvokeRepeating("PlayDragonAnm", 0, 20);
        }
    }

    private void PlayDragonAnm()
    {
        if (ani != null) {  
        ani.Play();
       }
    }
}
