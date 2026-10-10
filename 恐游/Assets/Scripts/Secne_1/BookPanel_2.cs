using UnityEngine;
using UnityEngine.UI;
public class BookPanel_2 : BasePanel
{
    public Button btnSure;
    public override void Init()
    {
        btnSure.onClick.AddListener(() =>
        {
            UImanager.Instance.HidePanel<BookPanel_2>();
        });
    }
}
