using UnityEngine;

public class Main : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BlackImage blackImage = UImanager.Instance.ShowPanel<BlackImage>();
        //BackGroundManager.Instance.setBackGround("");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
