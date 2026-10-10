using System.Collections.Generic;
using UnityEngine;

public class UImanager
{
   private static UImanager instance=new UImanager();
    public static UImanager Instance => instance;

    //存储面板的容器
    private Dictionary<string,BasePanel> panelDic = new Dictionary<string,BasePanel>();
    //应该一开始就得到canvas对象
    private Transform canvasTrans;
    private UImanager()
    {
        //得到场景上创建好的canvas对象
        canvasTrans=GameObject.Find("Canvas").transform;
        //让canvas对象过场景不移除
        //我们都是通过动态创建和动态删除俩显示隐藏面板的，所以删除它影响不大
        GameObject.DontDestroyOnLoad(canvasTrans.gameObject);
    }

    //显示面板
    public T ShowPanel<T>() where T : BasePanel
    {
        //我们只需要保证泛型T的类型和面板名一致定一个这样的规则就非常方便使用
        string panelName=typeof(T).Name;

        //是否已经有显示着的面板了，如果有，不用创建直接返回
        if(panelDic.ContainsKey(panelName))
        {
            return (T)panelDic[panelName];
        }
        //显示面板就是创建面板预设体 设置父对象
        //根据得到的类名就是我们的预设头面板名，直接动态创建他即可
        GameObject panelObj = GameObject.Instantiate(Resources.Load<GameObject>("UI/" + panelName));
        panelObj.transform.SetParent(canvasTrans,false);

        //接着就是得到对应的面板脚本存储起来
        T panel=panelObj.GetComponent<T>();
        //把面板脚本存储到对应容器中，之后方便我们获取他
        panelDic.Add(panelName, panel);
        //调用显示自己的逻辑
        panel.ShowMe();
        return panel;
    }

    //隐藏面板
    //参数一：如果希望淡出就默认传true，直接删除传false
    public void HidePanel<T>(bool isFade=true) where T : BasePanel
    {
        //根据泛型得到面板名字
        string panelName = typeof(T).Name;

        if(panelDic.ContainsKey(panelName) )
        {
            if (isFade)
            {
                panelDic[panelName].HideMe(() =>
                {
                    //面板淡出成功后希望删除面板
                    GameObject.Destroy(panelDic[panelName].gameObject);
                    //删除面板后从字典中移除
                    panelDic.Remove(panelName);
                });
            }
            else
            {
                //删除面板
                GameObject.Destroy(panelDic[panelName].gameObject);
                //删除面板后从字典中移除
                panelDic.Remove(panelName);
            }
        }
    }

    //获得面板
    public T GetPanel<T>() where T : BasePanel
    {
        string panelName=typeof(T).Name;
        if( panelDic.ContainsKey(panelName))
        {
            return panelDic[panelName] as T;
        }
        return null;
    }

    public bool IsPanelActive<T>() where T : BasePanel
    {
        string panelName = typeof(T).Name;
        if (panelDic.ContainsKey(panelName))
        {
            return panelDic[panelName].gameObject.activeSelf;
        }
        return false;
    }
}
