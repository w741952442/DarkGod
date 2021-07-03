/****************************************************
    文件：WindowsRoot.cs
	作者：Mouse
    日期：2021/6/17 16:57:11
	功能：UI窗口基类
*****************************************************/

using UnityEngine;
using UnityEngine.UI;

public class WindowRoot : MonoBehaviour 
{
    protected ResSvc resSvc;
    protected AudioSvc audioSvc;

    public void SetWndState(bool isActive)
    {
        if(gameObject.activeSelf != isActive)
        {
            gameObject.SetActive(isActive);
        }
        if (isActive)
        {
            InitWnd();
        }
        else
        {
            CleanWnd();
        }
    }

    protected virtual void InitWnd()
    {
        resSvc = ResSvc.Instance;
        audioSvc = AudioSvc.Instance;
    }

    protected virtual void CleanWnd()
    {
        resSvc = null;
        audioSvc = null;
    }

    #region
    protected void SetText(Text widget,string content = "")
    {
        widget.text = content;
    }

    protected void SetText(Transform widget, int num = 0)
    {
        SetText(widget.GetComponent<Text>(), num);
    }

    protected void SetText(Transform widget,string content = "")
    {
        SetText(widget.GetComponent<Text>(), content);
    }

    protected void SetText(Text widget ,int num = 0)
    {
        SetText(widget, num.ToString());
    }
    #endregion

    #region
    protected void SetActive(GameObject widget,bool isActive = true)
    {
        widget.SetActive(isActive);
    }

    protected void SetActive(Transform widget,bool isActive = true)
    {
        widget.gameObject.SetActive(isActive);
    }

    protected void SetActive(RectTransform widget, bool isActive = true)
    {
        widget.gameObject.SetActive(isActive);
    }

    protected void SetActive(Image widget, bool isActive = true)
    {
        widget.transform.gameObject.SetActive(isActive);
    }

    protected void SetActive(Text widget, bool isActive = true)
    {
        widget.transform.gameObject.SetActive(isActive);
    }
    #endregion
}