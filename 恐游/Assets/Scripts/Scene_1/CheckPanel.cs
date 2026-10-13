using UnityEngine;
using UnityEngine.UI;

public class CheckPanel : BasePanel
{
    public Button button;
    public override void Init()
    {
        button.onClick.AddListener(() =>
        {
            BackGroundManager.Instance.SetBackGroundwithVFX(0, "fish_eye", 1);
            UImanager.Instance.ShowPanel<BackButtonPanel>();
            UImanager.Instance.HidePanel<CheckPanel>();
        });
    }

}
