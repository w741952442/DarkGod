/****************************************************
    文件：LodingWnd.cs
	作者：Mouse
    日期：2021/6/17 10:58:25
	功能：加载进度界面
*****************************************************/

using UnityEngine;
using UnityEngine.UI;

public class LoadingWnd : WindowRoot 
{
    public Text TextTips;
    public Image LoadingFg;
    public Image ImagePoint;
    public Text TextPrg;
    private float FgWidth;

    protected override void InitWnd()
    {
        base.InitWnd();

        //获取这个图片矩形的宽度/1090
        FgWidth = LoadingFg.GetComponent<RectTransform>().sizeDelta.x;
        SetText(TextTips, "Tips:这是一个游戏提示");
        SetText(TextPrg, "0%");
        LoadingFg.fillAmount = 0;
        ImagePoint.transform.localPosition = new Vector3(-545, 0, 0);
    }

    public void setProgress(float prg)
    {
        SetText(TextPrg, (int)(100 * prg) + "%");
        LoadingFg.fillAmount = prg;
        float posX = prg * FgWidth - 545;
        ImagePoint.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX, 0);
    }
}