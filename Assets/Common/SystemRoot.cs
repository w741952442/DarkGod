/****************************************************
    文件：SystemRoot.cs
	作者：Mouse
    日期：2021/6/18 11:48:39
	功能：业务功能基类
*****************************************************/

using UnityEngine;

public class SystemRoot : MonoBehaviour 
{
    protected ResSvc resSvc;
    protected AudioSvc audioSvc;

    public virtual void InitSys()
    { 
        resSvc = ResSvc.Instance;
        audioSvc = AudioSvc.Instance;
    }
}