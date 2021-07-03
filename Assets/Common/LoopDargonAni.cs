/****************************************************
    文件：LoopDargonAni.cs
	作者：Mouse
    日期：2021/6/19 10:2:56
	功能：飞龙循环动画
*****************************************************/

using UnityEngine;

public class LoopDargonAni : MonoBehaviour 
{
    private Animation DargonAnimate;

    private void Awake()
    {
        DargonAnimate = GetComponent<Animation>();
    }

    private void Start()
    {
        if (DargonAnimate != null)
        {
            InvokeRepeating("PlayDargonAni", 0, 10);
        }
    }

    private void PlayDargonAni()
    {
        if (DargonAnimate != null)
        {
            DargonAnimate.Play();
        }
    }
}