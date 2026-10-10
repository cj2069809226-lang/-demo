using System;
using UnityEngine;
using UnityEngine.UI;

public class BlackImage : BasePanel
{
    public Button button;
    //public float fadeDuration = 1f; //淡出持续时间
    public override void Init()
    {
        
        button.onClick.AddListener(() =>
        {
            UImanager.Instance.HidePanel<BlackImage>();
            /*while (UImanager.Instance.IsPanelActive<BlackImage>())
            {
                //等待黑色图片淡出完成
            }*/
            ShowBookPanel showBookPanel = UImanager.Instance.ShowPanel<ShowBookPanel>();
        });
    }


}
