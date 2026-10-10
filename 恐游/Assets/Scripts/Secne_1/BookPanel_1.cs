using UnityEngine;
using UnityEngine.UI;

public class BookPanel_1 : BasePanel
{
    public Button btnSure;
    
    public override void Init()
    {
        BackGroundManager.Instance.setBackGround("black_room");
        Debug.Log("BookPanel_1 Init");
        btnSure.onClick.AddListener(() =>
        {
            UImanager.Instance.HidePanel<BookPanel_1>();
            UImanager.Instance.ShowPanel<BookPanel_2>();
        });
    }
   

}
