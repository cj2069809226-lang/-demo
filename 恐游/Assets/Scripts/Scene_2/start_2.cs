using Kongyou.Dialogue;
using UnityEditor;
using UnityEngine;

public class start_2 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DialogueSystem.Show(new DialogueLine("旁白", "你躺在床上玩着手机，玩着玩着你睡着了。"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
