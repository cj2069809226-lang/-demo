using System;
using Kongyou.Dialogue;
using UnityEngine;
using UnityEngine.UI;

public class BlackImage : BasePanel
{
    public Button button;
    //public float fadeDuration = 1f; //淡出持续时间
    public override void Init()
    {
        DialogueSystem.Show(new DialogueLine("？？？", "这是新生的第一个假期，你成为宿舍里唯一一个留校的人。")); 
        button.onClick.AddListener(() =>
        {
            //DialogueSystem.Show(new DialogueLine("旁白", "书页自己翻到了这一页。")); 
            UImanager.Instance.HidePanel<BlackImage>();
            
            ShowBookPanel showBookPanel = UImanager.Instance.ShowPanel<ShowBookPanel>();
        });
    }


}
