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
using System.Xml;

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
        InitRDNameCfg();
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

    #region InitCfgs
    private List<string> surnameList = new List<string>();
    private List<string> mennameList = new List<string>();
    private List<string> womennameList = new List<string>();

    private void InitRDNameCfg()
    {
        TextAsset xml = Resources.Load<TextAsset>(PathDefine.RDNameCfg);
        if (!xml)
        {
            PECommon.Log("xml file:" + PathDefine.RDNameCfg + " not exist");
        } else
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml.text);

            XmlNodeList nodLst = doc.SelectSingleNode("root").ChildNodes;
            for(int i = 0; i < nodLst.Count; i++) {
                XmlElement ele = nodLst[i] as XmlElement;
                //如果ID不存在 读下一条
                if (ele.GetAttributeNode("ID") == null)
                {
                    continue;
                }
               int id=Convert.ToInt32(ele.GetAttributeNode("ID").InnerText);
                foreach(XmlElement e in nodLst[i].ChildNodes)
                {
                    switch (e.Name)
                    {
                        case "surname":
                            surnameList.Add(e.InnerText);
                            break;
                        case "man":
                            mennameList.Add(e.InnerText);
                            break;
                        case "woman":
                            womennameList.Add(e.InnerText);
                            break;
                    }
                }
            }
        }
    }

    public string GetRDNameData(bool man=true)
    {
        System.Random rd = new System.Random();
        string rdName = surnameList[PETools.RDInt(0, surnameList.Count - 1)];
        if (man)
        {
            rdName+= mennameList[PETools.RDInt(0, mennameList.Count - 1)];
        }else
        {
            rdName += womennameList[PETools.RDInt(0, womennameList.Count - 1)];
        }

        return rdName;
    }
    #endregion
}


