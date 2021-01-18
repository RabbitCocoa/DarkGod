/****************************************************
    文件：WindowRoot.cs
	作者：Rabbitcocoa
    邮箱: 1085750968@qq.com
    日期：2021/1/4 16:39:21
	功能：窗口基类
*****************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WindowRoot : MonoBehaviour
{
    protected ResSvc res=null;
    protected AudioSvc audioSvc = null;
    protected NetSvc netSvc = null;
   public void SetWndState(bool isAcive = true){
        if (gameObject.activeSelf!=isAcive){
            gameObject.SetActive(isAcive);
        }

        if (gameObject.activeSelf)
        {
            InitWnd();
        }
        else {
            ClearWnd();
        }
    }
    protected virtual void InitWnd(){
        res = ResSvc.instance;
        audioSvc = AudioSvc.instance;
        netSvc = NetSvc.instance;
    }

    protected virtual void ClearWnd(){
        res = null;
        audioSvc = null;
        netSvc = null;
    }

    #region Tool Function
    /// <summary>
    /// 给文本框设置文字
    /// </summary>
    /// <param name="txt">文本框组件</param>
    /// <param name="context">文字/或数字 已重载</param>
    protected void SetText(Text txt , string context="")
    {
        txt.text = context;
    }
    protected void SetText(Text txt, int num = 0)
    {
        txt.text = num.ToString();
    }

    protected void SetText(Transform trans, string context = "")
    {
        SetText(trans.GetComponent<Text>(), context);
    }
    protected void SetText(Transform trans, int num = 0)
    {
        SetText(trans.GetComponent<Text>(), num.ToString());
      
    }

   /// <summary>
   /// 设置物体是否启用(已重载)
   /// </summary>
   /// <param name="obj">物体</param>
   /// <param name="isActive">是否启用</param>
    protected void SetActive(GameObject obj,bool isActive = true)
    {
        obj.SetActive(isActive);
    }
    protected void SetActive(Transform obj, bool isActive = true)
    {
        SetActive(obj.gameObject, isActive);
    }
    protected void SetActive(RectTransform obj, bool isActive = true)
    {
        SetActive(obj.gameObject, isActive);
    }
    protected void SetActive(Image obj, bool isActive = true)
    {
        SetActive(obj.gameObject, isActive);
    }
    protected void SetActive(Text obj, bool isActive = true)
    {
        SetActive(obj.gameObject, isActive);
    }
    #endregion


    protected T GetOrAddComponent<T>(GameObject go) where T:Component
    {
        T t = go.GetComponent<T>();
        if (t == null)
            t = go.AddComponent<T>();

        return t;
    }

    #region clickEvts
    protected void OnClickDown(GameObject go,Action<PointerEventData> evt)
    {
        PEListener listener = GetOrAddComponent<PEListener>(go);
        listener.onClickDown = evt;
    }
    protected void OnClickUp(GameObject go, Action<PointerEventData> evt)
    {
        PEListener listener = GetOrAddComponent<PEListener>(go);
        listener.onClickUp = evt;
    }
    protected void OnClickDrag(GameObject go, Action<PointerEventData> evt)
    {
        PEListener listener = GetOrAddComponent<PEListener>(go);
        listener.onClickDrag = evt;
    }

    protected void SetSprite(Image img,string path)
    {
        Sprite sp = res.LoadSprite(path, true);
        img.sprite = sp;
    }
    #endregion
}
