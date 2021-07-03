/****************************************************
    文件：LoginWnd.cs
	作者：Mouse
    日期：2021/6/17 15:54:5
	功能：登录游戏界面
*****************************************************/

using UnityEngine;
using UnityEngine.UI;

public class LoginWnd : WindowRoot 
{
    //定义需要用到的各个组件
    public InputField AccountInputField;
    public InputField PassWordInputField;
    public Button BtnNotice;
    public Button BtnEnter;

    protected override void InitWnd()
    {
        base.InitWnd();
        //获取需要用到的各个组件（注意需要在unity中将对应的组件附加到对应的变量上面）
        //获取本地储存的账号密码信息
        if(PlayerPrefs.HasKey("Account") && PlayerPrefs.HasKey("Password"))
        {
            AccountInputField.text = PlayerPrefs.GetString("Account");
            PassWordInputField.text = PlayerPrefs.GetString("Password");
        }
        else
        {
            AccountInputField.text = "";
            PassWordInputField.text = "";
        }
    }

    //点击进入游戏
    public void ClickEnterBtn()
    {
        //播放点击登录按钮音效（登录按钮音效）
        audioSvc.PlayUIAudio(Constants.UILoginBtn);

        //获取玩家输入的账号密码信息，并将其缓存至本地
        string account = AccountInputField.text;
        string password = PassWordInputField.text;
        if (account != "" && password != "")        //当玩家输入的账号密码不为空时进行下一步操作
        {
            PlayerPrefs.SetString("Account", account);      //缓存玩家的账号信息
            PlayerPrefs.SetString("Password", password);    //缓存玩家的密码信息
            LoginSys.Instance.RspLogin();        //需要通过网络发送消息给服务器验证账号密码信息（目前模拟调用消息接收后执行操作）
        }
        else
        {
            GameRoot.AddTips("请输入账号或者密码");
        }
    }

    //点击公告按钮
    public void ClickNoticeBtn()
    {
        //播放点击公告按钮音效（常规按钮音效）
        audioSvc.PlayUIAudio(Constants.UIClickBtn);
        GameRoot.AddTips("公告正在开发中");
    }
}