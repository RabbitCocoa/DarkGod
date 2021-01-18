/****************************************************
    文件：GameRoot.cs
	作者：Rabbitcocoa
    邮箱: 1085750968@qq.com
    日期：#CreateTime#
	功能：物体生命周期测试
*****************************************************/

using UnityEngine;
using System.Collections;
public class Test : MonoBehaviour 
{

    ResourceRequest request;
    GameObject go=null;
    private void Start()
    {
        //      Coroutine cot= StartCoroutine(FuncA());
        //StopCoroutine(cot);

        //注册入底层
        //        StartCoroutine("FuncA"); //StopCoroutine("FuncA")
        StartCoroutine(AsyncGetRes());
        Debug.Log("Loading...");

    }
    //在主线程中进行 类似通路 类似中断询问法
    IEnumerator FuncA()
    {
        yield return null;
    }

    IEnumerator AsyncGetRes()
    {
        request = Resources.LoadAsync("Test");
        yield return request;
        Debug.Log("Done");

        go = request.asset as GameObject;
        if (go != null)
            Instantiate(go);
        else
            Debug.Log("Load Fail");
    }
    private void Update()
    {
        if (request != null)
            if (go == null)
                Debug.Log(request.progress);
    }

}