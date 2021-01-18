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
        InitMapCfg();
        InitGuideCfg();
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


    private Dictionary<string, GameObject> objDic = new Dictionary<string, GameObject>();
   
    public GameObject LoadPrefab(string path, bool cache = false)
    {
        GameObject prefab = null;
        if (!objDic.TryGetValue(path, out prefab))
        {
            prefab = Resources.Load<GameObject>(path);
            if (cache)
            {
                objDic.Add(path, prefab);
            }
        }

        GameObject go = null;
        if (prefab != null)
            go = Instantiate(prefab);

        return go;
    }

    //图片缓存
    private Dictionary<string, Sprite> imgDic = new Dictionary<string, Sprite>();
    public Sprite LoadSprite(string path,bool iscache = false)
    {
        Sprite sp = null;
        if(!imgDic.TryGetValue(path,out sp))
        {
            sp = Resources.Load<Sprite>(path);
            if (iscache)
                imgDic.Add(path, sp);
        }
        return sp;
    }


    #region InitCfgs




    #region 随机姓名
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

    #region 地图配置
    private Dictionary<int, MapCfg> mapCfgDic = new Dictionary<int, MapCfg>();
    private void InitMapCfg()
    {
        TextAsset xml = Resources.Load<TextAsset>(PathDefine.MapCfg);
        if (!xml)
        {
            PECommon.Log("xml file:" + PathDefine.MapCfg + " not exist");
        }
        else
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml.text);

            XmlNodeList nodLst = doc.SelectSingleNode("root").ChildNodes;
            for (int i = 0; i < nodLst.Count; i++)
            {
                XmlElement ele = nodLst[i] as XmlElement;
                //如果ID不存在 读下一条
                if (ele.GetAttributeNode("ID") == null)
                {
                    continue;
                }
                int id = Convert.ToInt32(ele.GetAttributeNode("ID").InnerText);
                MapCfg mc = new MapCfg
                {
                    ID = id
                };
                foreach (XmlElement e in nodLst[i].ChildNodes)
                {
                    switch (e.Name)
                    {
                    case "mapName":
                        mc.mapName = e.InnerText;
                        break;
                    case "sceneName":
                        mc.sceneName = e.InnerText;
                        break;

                        case "mainCamPos":
                            {
                                string[] valArr = e.InnerText.Split(',');
                                mc.mainCamPos = new Vector3
                               (float.Parse(valArr[0]), float.Parse(valArr[1]),
                                 float.Parse(valArr[2]));
                                break;
                            }
                        case "mainCamRote":
                            {
                                string[] valArr = e.InnerText.Split(',');
                                mc.mainCamRote = new Vector3
                               (float.Parse(valArr[0]), float.Parse(valArr[1]),
                                 float.Parse(valArr[2]));
                                break;
                            }
                        case "playerBornPos":
                            {
                                string[] valArr = e.InnerText.Split(',');
                                mc.playerBornPos = new Vector3
                               (float.Parse(valArr[0]), float.Parse(valArr[1]),
                                 float.Parse(valArr[2]));
                                break;
                            }
                        case "playerBornRote":
                            {
                                string[] valArr = e.InnerText.Split(',');
                                mc.playerBornRote = new Vector3
                               (float.Parse(valArr[0]), float.Parse(valArr[1]),
                                 float.Parse(valArr[2]));
                                break;
                            }
                    }

                }
                mapCfgDic.Add(id, mc);
            }
        }
    }

    public MapCfg GetMapCfgData(int id)
    {
        MapCfg data;
        if(mapCfgDic.TryGetValue(id,out data))
        {
            return data;
        }
        return null;
    }

    #endregion

    #region 自动引导配置
    private Dictionary<int, AutoGuideCfg> autoGuideCfgDic = new Dictionary<int, AutoGuideCfg>();
    private void InitGuideCfg()
    {
        TextAsset xml = Resources.Load<TextAsset>(PathDefine.AutoGuideCfg);
        if (!xml)
        {
            PECommon.Log("xml file:" + PathDefine.AutoGuideCfg + " not exist");
        }
        else
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml.text);

            XmlNodeList nodLst = doc.SelectSingleNode("root").ChildNodes;
            for (int i = 0; i < nodLst.Count; i++)
            {
                XmlElement ele = nodLst[i] as XmlElement;
                //如果ID不存在 读下一条
                if (ele.GetAttributeNode("ID") == null)
                {
                    continue;
                }
                int id = Convert.ToInt32(ele.GetAttributeNode("ID").InnerText);
                AutoGuideCfg mc = new AutoGuideCfg
                {
                    ID = id
                };
                foreach (XmlElement e in nodLst[i].ChildNodes)
                {
                    switch (e.Name)
                    {
                        case "npcID":
                            mc.npcID = int.Parse(e.InnerText);
                            break;
                        case "actID":
                            mc.actID = int.Parse(e.InnerText);
                            break;
                        case "coin":
                            mc.coin = int.Parse(e.InnerText);
                            break;
                        case "exp":
                            mc.exp = int.Parse(e.InnerText);
                            break;
                        case "dilogArr":
                            mc.dilogArr = e.InnerText;
                            break;
                    }
                }
                autoGuideCfgDic.Add(id, mc);
            }
        }
    }
    public AutoGuideCfg GetGuideCfg(int id)
    {
        AutoGuideCfg cfg = null;
        autoGuideCfgDic.TryGetValue(id, out cfg);

        return cfg;
    }
    #endregion
    #endregion
}


