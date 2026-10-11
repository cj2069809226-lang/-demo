using UnityEngine;
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
        });
    }
}
