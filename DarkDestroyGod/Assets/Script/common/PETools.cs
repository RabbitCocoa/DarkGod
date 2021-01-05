/****************************************************
    文件：PETools.cs
	作者：Rabbitcocoa
    邮箱: 1085750968@qq.com
    日期：2021/1/5 15:40:48
	功能 工具类
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PETools 
{
   public static int RDInt(int min,int max,System.Random rd=null)
    {
        if(rd==null)
           rd = new System.Random();
        int val = rd.Next(min, max + 1);
        return val;
    }
}
