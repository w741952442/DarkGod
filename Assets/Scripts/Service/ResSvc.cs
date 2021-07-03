/****************************************************
    文件：ResSvc.cs
	作者：Mouse
    日期：2021/6/16 16:1:55
	功能：资源加载服务
*****************************************************/

using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResSvc : MonoBehaviour
{
    //将资源服务类变成单例
    public static ResSvc Instance = null;
    private Action prgCb;
    private Dictionary<string, AudioClip> audioDictionary = new Dictionary<string, AudioClip>(); //创建一个通过路径引用的字典
    private List<string> SurnameList = new List<string>();
    private List<string> ManList = new List<string>();
    private List<string> WomanList = new List<string>();

    //将资源服务类初始化
    public void InitSvc()
    {
        //将此单例赋值给与自己
        Instance = this;
        prgCb = null;
        InitRandomNameCfg();
        Debug.Log("Init ResSvc...");
    }

    //异步加载函数
    public void AsyncLoadScene(string sceneName, Action loaded)
    {
        GameRoot.Instance.loadingWnd.SetWndState(true);
        AsyncOperation SceneAsync = SceneManager.LoadSceneAsync(sceneName);
        prgCb = () =>
        {
            float val = SceneAsync.progress; //获取异步加载进度（结果为浮点数0-1之间）
            GameRoot.Instance.loadingWnd.setProgress(val); //显示加载进度百分比
            if (val == 1) //判断其异步操作的进度是否已经完成
            {
                if (loaded != null)
                {
                    loaded();
                }

                prgCb = null; //若完成，则将回调赋值为空
                SceneAsync = null; //若完成，则将异步操作赋值为空
                GameRoot.Instance.loadingWnd.SetWndState(false); //关闭登录界面
            }
        };
    }

    private void Update()
    {
        if (prgCb != null)
        {
            prgCb();
        }
    }

    //加载音乐音效文件并缓存
    public AudioClip LoadAudio(string path, bool cache = false)
    {
        AudioClip audio = null;
        if (!audioDictionary.TryGetValue(path, out audio)) //判断字典中是否存在这个路径的音乐音效
        {
            audio = Resources.Load<AudioClip>(path); //根据传入的音乐路径参数来加载音乐音效文件(不存在的情况下)
            if (cache) //判断是否需要将其对应的音乐音效进行缓存
            {
                audioDictionary.Add(path, audio); //需要缓存，以key：path和value：audio的形式加入到字典中
            }
        }

        return audio;
    }

    #region InitCfgs

    //先定义函数接口
    public void InitRandomNameCfg()
    {
        //通过文件管理器加载本地随机名配置未见
        TextAsset xml = Resources.Load<TextAsset>(PathDefine.RandomNameConfig);
        if (!xml) //判断文件是否存在
        {
            Debug.LogError("xml file :" + PathDefine.RandomNameConfig + "not exist..."); //如不存在则弹出错误打印            
        }
        else
        {
            XmlDocument doc = new XmlDocument(); //创建一个新的xml格式文档
            doc.LoadXml(xml.text); //将配置的文件内容加载到xml文档中
            XmlNodeList nodeList = doc.SelectSingleNode("root").ChildNodes; //查找root节点内容并将其赋值给nodeList
            for (int i = 0; i < nodeList.Count; i++)
            {
                XmlElement ele = nodeList[i] as XmlElement; //实例化节点对象并将每个子节点赋值给对象
                if (ele.GetAttributeNode("ID") != null) //判断子节点对象中是否存在ID属性
                {
                    int ID = Convert.ToInt32(ele.GetAttributeNode("ID").InnerText); //提取ID属性并转化为int类型赋予变量
                }

                foreach (XmlElement e in nodeList[i].ChildNodes) //遍历子节点中的相同字段的值并将其放入对应的list之中
                {
                    switch (e.Name)
                    {
                        case "surname":
                            SurnameList.Add(e.InnerText);
                            break;
                        case "man":
                            ManList.Add(e.InnerText);
                            break;
                        case "woman":
                            WomanList.Add(e.InnerText);
                            break;
                    }
                }
            }
        }
    }

    //通过性别获取随机名字接口
    //@man 男性名字
    public string GetRandomNameData(bool man)
    {
        System.Random random = new System.Random();     //创建新的随机数体
        string randomName = SurnameList[PETools.RandomInt(0, SurnameList.Count - 1)];       //获取姓
        if (man)                                                                            //判断是否为男性
        {
            randomName += ManList[PETools.RandomInt(0, ManList.Count - 1)];                 //获取男性名
        }
        else
        {
            randomName += WomanList[PETools.RandomInt(0, WomanList.Count - 1)];             //获取女性名
        }
        return randomName;
    }

    #endregion
}