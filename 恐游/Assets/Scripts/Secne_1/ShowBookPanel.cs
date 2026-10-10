using UnityEngine;
using UnityEngine.UI;

public class ShowBookPanel : BasePanel
{
    public Button btnShowBookPanel;

    public override void Init()
    {
        btnShowBookPanel.onClick.AddListener(() =>
        {
            UImanager.Instance.ShowPanel<BookPanel_1>();
            UImanager.Instance.HidePanel<ShowBookPanel>();
        });
    }

 
}
