using UnityEngine;
using UnityEngine.UI;
public class BackGroundManager
{
   private static BackGroundManager _instance=new BackGroundManager();
   public static BackGroundManager Instance=> _instance;

   public Image backGroundImage;

   public BackGroundManager()
   {
       backGroundImage = GameObject.Find("BackGround").GetComponent<Image>();
       if(backGroundImage!=null)
       {
           //Debug.Log("背景图载入成功");
       }
   }

   public void setBackGround(string BackgroundName)
   {
       Sprite newBackGroundImage = GameObject.Instantiate<Sprite>(Resources.Load<Sprite>("BackGrounds/" + BackgroundName));
        if (newBackGroundImage != null)
        {
            backGroundImage.sprite = newBackGroundImage;
            Debug.Log("背景图载入成功");
        }
        else
        {
            Debug.LogError("背景图载入失败");
        }
   }

   
}
