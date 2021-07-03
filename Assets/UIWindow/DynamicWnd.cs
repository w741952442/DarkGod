/****************************************************
    文件：DynamicWnd.cs
	作者：Mouse
    日期：2021/6/19 10:47:11
	功能：动态元素UI界面
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicWnd : WindowRoot 
{
    public Text TextTips;
    public Animation TipAnimation;
    private Queue<string> TipsQue = new Queue<string>();
    private bool IsTipsShow;

    protected override void InitWnd()
    {
        base.InitWnd();
        SetActive(TextTips, false);     //初始化的时候默认界面关闭
        IsTipsShow = false;             //初始化的时候提示的显示状态默认为false
    }

    public void AddTips(string tips)   //将新的提示信息加入队列，并给其加上线程锁
    {
        lock (TipsQue)
        {
            TipsQue.Enqueue(tips);      //加入队列
        }
    }

    private void Update()           //更新提示信息
    {
        if (TipsQue.Count > 0 && IsTipsShow == false)      //判断提示信息队列的数量是否大于0，也就是队列中是否有消息存在 并且没有正在显示提示的状态
        {
            lock (TipsQue)          //锁定线程
            {
                string tip = TipsQue.Dequeue();     //将队列消息提取出来
                IsTipsShow = true;                  //将正在显示的状态改成true
                SetTips(tip);                       //调用显示提示消息
            }
        }
    }

    private void SetTips(string tips)    //弹窗显示文字接口
    {
        SetActive(TextTips, true);      //先将弹窗界面显示开启
        SetText(TextTips, tips);        //设置界面文字内容

        AnimationClip clip = TipAnimation.GetClip("TipsShowAni");       //获取弹窗的动画片段
        TipAnimation.Play();        //播放动画
        StartCoroutine(AniPlayDone(clip.length,()=> {           //启动一个协程在播放动画完成之后关闭弹窗界面(延时关闭激活)
            SetActive(TextTips, false);
            IsTipsShow = false;                                 //将提示的显示状态重新改成false
        }));
    }

    private IEnumerator AniPlayDone(float second,Action callback)       //协程在完成之后等待一段时间并开启回调
    {
        yield return new WaitForSeconds(second);
        if (callback != null)
        {
            callback();
        }
    }
}