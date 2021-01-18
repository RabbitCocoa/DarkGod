/****************************************************
    文件：PEListener.cs
	作者：Rabbitcocoa
    邮箱: 1085750968@qq.com
    日期：2021/1/12 15:14:59
	功能：UI事件监听
*****************************************************/

using System;
using UnityEngine;
using UnityEngine.EventSystems;
public class PEListener : MonoBehaviour,IPointerDownHandler,IPointerUpHandler,IDragHandler
{
    public Action<PointerEventData> onClickDown;
    public Action<PointerEventData> onClickUp;
    public Action<PointerEventData> onClickDrag;

    public void OnPointerUp(PointerEventData eventData)
    {
        if (onClickUp != null)
            onClickUp(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (onClickDrag != null)
            onClickDrag(eventData);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (onClickDown != null)
            onClickDown(eventData);
    }


}