using System.Collections;
using Kongyou.Dialogue;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackButtonPanel : BasePanel
{
    public Button Button;
    public override void Init()
    {
        Button.onClick.AddListener(() =>
        {
            BackGroundManager.Instance.SetBackGround("black_room");
            UImanager.Instance.HidePanel<BackButtonPanel> ();
            DialogueSystem.Instance.Ended+=later;
            //SceneManager.LoadScene("2");
            DialogueSystem.Show(
                new DialogueLine("旁白",".......",0.5f,0.1f),
                new DialogueLine("旁白","你躺在床上玩着手机，<pause=0.8>玩着玩着——<speed=0.8>你睡着了。")
                
            );
            

        });
        void later()
        {
            DialogueSystem.Instance.Ended-=later;
            BackGroundManager.Instance.SetBackGroundwithVFX(0, "black", 0);
            CoroutineRunner.Instance.StartCoroutine(PlayNextAfterBeat());
            BackGroundManager.Instance.SetBackGroundwithVFX(0, "black_room", 0);
        }
        IEnumerator PlayNextAfterBeat()
        {
            yield return new WaitForSeconds(1f);
            DialogueSystem.Show(
                new DialogueLine("旁白","有人在敲门。",0.5f,0.1f),
                new DialogueLine("旁白","<pause=0.5>很轻"),
                new DialogueLine("旁白","<pause=0.5>很有节奏"),
                new DialogueLine("旁白","<pause=0.5>三下，三下"),
                new DialogueLine("旁白","<pause=1>你醒了"),
                new DialogueLine("旁白","<pause=2>打开手机，<pause=0.5>显示23时50分")
            );
            yield return new WaitForSeconds(1f);
            //UImanager.Instance.ShowPanel
        }
    }
}
