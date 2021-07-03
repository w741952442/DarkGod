/****************************************************
    文件：GameRoot.cs
	作者：Mouse
    日期：2021/6/16 15:57:19
	功能：游戏的启动入口
*****************************************************/

using UnityEngine;

public class GameRoot : MonoBehaviour 
{
    public static GameRoot Instance = null;
    public LoadingWnd loadingWnd;
    public DynamicWnd dynamicWnd;
    //游戏开始
    private void Start()
    {
        Instance = this;
        DontDestroyOnLoad(this);
        Debug.Log("Game Start......");
        Init();
    }

    private void CleanUIRoot()
    {
        Transform Canvas = transform.Find("Canvas");
        for (int i = 0; i < Canvas.childCount; i++)
        {
            Canvas.GetChild(i).gameObject.SetActive(false);
        }
        dynamicWnd.SetWndState(true);
    }

    //初始化各个模块
    private void Init()
    {   //服务模块初始化
        ResSvc res = GetComponent<ResSvc>();
        res.InitSvc();

        //声音播放初始化
        AudioSvc audio = GetComponent<AudioSvc>();
        audio.InitSvc();

        //业务系统初始化
        LoginSys login = GetComponent<LoginSys>();
        login.InitSys();

        //进入登录场景并加载UI
        login.EnterLogin();
    }

    public static void AddTips(string tip)
    {
        Instance.dynamicWnd.AddTips(tip);
    }
}