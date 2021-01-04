/****************************************************
    文件：ResSvc.cs
	作者：Rabbitcocoa
    邮箱: 1085750968@qq.com
    日期：2021/1/4 15:16:59
	功能：资源服务
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;
public class ResSvc : MonoBehaviour
{
    //单例
    public static ResSvc instance = null;
    //加载进度回调
    private Action prgCB = null;

    ///服务初始化
    public void InitSvc()
    {
        instance = this;
    }
    /// <summary>
    /// 异步加载场景
    /// </summary>
    /// <param name="sceneName">场景名</param>
    /// <param name="loadfc">回调函数</param>
    public void AsyncLoadScene(string sceneName,Action loadfc)
    {
      AsyncOperation sceneAysnc=  SceneManager.LoadSceneAsync(sceneName);
        GameRoot.instance.loadingWnd.SetWndState();
        
        prgCB = () =>
        {
            float pcs = sceneAysnc.progress;
            GameRoot.instance.loadingWnd.SetProgress(pcs);
            if (pcs==1.0f)
            {
                if (loadfc != null)
                {
                    loadfc();
                }
                prgCB = null;
                sceneAysnc = null;
                GameRoot.instance.loadingWnd.SetWndState(false);
            }
        };
    }

 
    private void Update()
    {
        if (prgCB != null)
            prgCB();
     }

    //背景音乐缓存
    private Dictionary<string, AudioClip> auDic = new Dictionary<string, AudioClip>();
    /// <summary>
    /// 加载音乐资源
    /// </summary>
    /// <param name="path">路径</param>
    /// <returns></returns>
    public AudioClip LoadAudio(string path,bool cache=false)
    {
        AudioClip au = null;
        if (!auDic.TryGetValue(path, out au))
        {
            au = Resources.Load<AudioClip>(path);
            if (cache)
            {
                auDic.Add(path, au);
            }
        }
        return au;
    }
}
