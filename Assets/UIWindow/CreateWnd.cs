/****************************************************
    文件：CreateWnd.cs
	作者：Mouse
    日期：2021/6/19 16:18:39
	功能：创建角色窗口
*****************************************************/

using UnityEngine;
using UnityEngine.UI;

public class CreateWnd : WindowRoot
{
    //定义需要用到的各个组件
    public Image ImgChar;
    public Text DesTitle;
    public Text Description;
    public InputField InputName;
    public Button RandomBtn;
    public Button EnterBtn;

    protected override void InitWnd()
    {
        base.InitWnd();
        
        //显示一个随机名字
        InputName.text = resSvc.GetRandomNameData(false);
    }

    //点击随机名按钮函数
    public void ClickRandomBtn()
    {
        audioSvc.PlayUIAudio(Constants.UIClickBtn);
        string randomName = resSvc.GetRandomNameData(false);
        InputName.text = randomName;
    }

    //点击进入游戏按钮函数
    public void ClickEnterBtn()
    {
        audioSvc.PlayUIAudio(Constants.UIClickBtn);
        if (InputName.text != "")
        {
            //TODO 发送网络连接登录游戏
        }
        else
        {
            GameRoot.AddTips("输入的名字不合规范，请重新输入！");
        }
    }
}