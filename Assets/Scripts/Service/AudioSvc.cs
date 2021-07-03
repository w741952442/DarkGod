/****************************************************
    文件：AudioSvc.cs
	作者：Mouse
    日期：2021/6/18 10:24:8
	功能：声音播放服务
*****************************************************/

using UnityEngine;

public class AudioSvc : MonoBehaviour 
{
    public static AudioSvc Instance;
    public AudioSource BGAudio;
    public AudioSource UIAudio;

    public void InitSvc()
    {
        Instance = this;
        Debug.Log("Init AudioSvc...");
    }

    public void PlayBGAudio(string name,bool isLoop = false)
    {
        AudioClip audio = ResSvc.Instance.LoadAudio("ResAudio/" + name,true);
        if(BGAudio.clip == null || BGAudio.clip.name != audio.name)
        {
            BGAudio.clip = audio;
            BGAudio.loop = isLoop;
            BGAudio.Play();
        }
    }

    public void PlayUIAudio(string name)
    {
        AudioClip audio = ResSvc.Instance.LoadAudio("ResAudio/" + name);
        UIAudio.clip = audio;
        UIAudio.loop = false;
        UIAudio.Play();
    }
}