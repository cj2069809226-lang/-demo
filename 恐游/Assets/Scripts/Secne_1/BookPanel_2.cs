using UnityEngine;
using UnityEngine.UI;
public class BookPanel_2 : BasePanel
{
    public Button btnSure;
    public override void Init()
    {
        btnSure.onClick.AddListener(() =>
        {
            
            BackGroundManager.Instance.SetBackGround(4f, "black_room");
            BackGroundManager.Instance.SetBackGroundwithVFX(6f, "door_close",0);
            UImanager.Instance.ShowPanelLater<CheckPanel>(6.1f);
            UImanager.Instance.HidePanel<BookPanel_2>();
        });
    }
}
