/****************************************************
    文件：LoginSys.cs
	作者：Mouse
    日期：2021/6/16 16:0:49
	功能：登录注册业务系统
*****************************************************/

using UnityEngine;

public class LoginSys : SystemRoot 
{
    public static LoginSys Instance = null;
    public LoginWnd LoginWnd;
    public CreateWnd CreateWnd;
    public override void InitSys()
    {
        base.InitSys();
        Instance = this;
        Debug.Log("Init LoginSys...");
    }

    //加载登录场景
    //并显示加载进度
    //加载完成之后再显示注册登录界面并播放登录界面背景音乐
    public void EnterLogin()
    {
        resSvc.AsyncLoadScene(Constants.SceneLogin,() => {
            LoginWnd.SetWndState(true);
            audioSvc.PlayBGAudio(Constants.BGLogin);
        });
    }

    //通过网络接收消息验证账号密码正确之后执行程序(目前模拟调用)
    public void RspLogin()
    {
        GameRoot.AddTips("登录成功");
        //打开创建角色页面
        CreateWnd.SetWndState(true);
        //关闭登录界面
        LoginWnd.SetWndState(false);
    }
}