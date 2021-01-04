/****************************************************
    文件：AudioSvc.cs
	作者：Rabbitcocoa
    邮箱: 1085750968@qq.com
    日期：2021/1/4 17:9:29
	功能：Nothing
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSvc : MonoBehaviour
{
    public static AudioSvc instance = null;

    //背景与UI声源
    public AudioSource bgAuSource;
    public AudioSource uiAuSource;
    /// <summary>
    /// 初始化服务
    /// </summary>
    public void InitSvc()
    {
        instance = this;
    }
    /// <summary>
    /// 播放音乐
    /// </summary>
    /// <param name="name">音乐名</param>
    /// <param name="isLoop">是否循环</param>
    public void PlayBGMusic(string name, bool isLoop = true)
    {
        AudioClip audio = ResSvc.instance.LoadAudio(Constants.MusicPath + name, true);
        if (bgAuSource.clip == null || bgAuSource.clip.name != audio.name) {
            bgAuSource.clip = audio;
            bgAuSource.loop = isLoop;
            bgAuSource.Play();
        }          
    }

    public void PlayUIAudio(string name)
    {
        AudioClip audio = ResSvc.instance.LoadAudio(Constants.MusicPath + name,true);
        if (audio)
        {
            uiAuSource.clip = audio;
            uiAuSource.Play();
        }
    }
}
