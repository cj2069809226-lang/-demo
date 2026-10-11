using System;
using System.Collections;
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

    //最普通的设置背景函数
    public void SetBackGround(string BackgroundName,Action onComplete=null)
   {
       Sprite newBackGroundImage = Resources.Load<Sprite>("BackGrounds/" + BackgroundName);
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
    public void SetBackGround(float waitTime, string backgroundName)
    {
        // 协程体写在下面，但由常驻的 MonoBehaviour 来"跑"
        CoroutineRunner.Instance.StartCoroutine(SetBackGroundRoutine(waitTime, backgroundName));
    }

    public void SetBackGroundwithVFX(float  waitTime, string backgroundName,int VFXindex)
    {
        
        CoroutineRunner.Instance.StartCoroutine(SwitchRoutine(waitTime, backgroundName,VFXindex));
    }

    private IEnumerator SetBackGroundRoutine(float waitTime, string backgroundName)
    {
        yield return new WaitForSeconds(waitTime);   // 想暂停时也走，用 WaitForSecondsRealtime
        SetBackGround(backgroundName);               // 复用你不延迟的那个版本
    }
    private IEnumerator SwitchRoutine(float waitTime, string backgroundName,int VFXindex)
    {
        yield return new WaitForSeconds(waitTime);
        if (backGroundImage == null) yield break;

        SetBackGround(backgroundName);// 换图的瞬间
        switch (VFXindex){
            case 0:
                yield return GlitchJitter(backGroundImage.rectTransform, 0.3f);   // 紧接着抖一下
                break;
            case 1:
                yield return WhiteFlash(0.25f, 0.9f);
                break;
        }
        
    }
    //返回一个迭代器为协程等待特效做准备,这是抖动特效
    private IEnumerator GlitchJitter(RectTransform rt, float duration)
    {
        var original = rt.anchoredPosition;
        float t = 0f;
        while (t < duration)
        {
            t += Time.unscaledDeltaTime;
            rt.anchoredPosition = original + new Vector2(
                UnityEngine.Random.Range(-14f, 14f),
                UnityEngine.Random.Range(-6f, 6f));
            yield return null;          // 每帧变一次，千万别用 WaitForSeconds
        }
        rt.anchoredPosition = original;
    }

    /// <summary>闪白：瞬间爆白一帧，然后快速淡出</summary>
    /// <param name="duration">淡出时长（秒）</param>
    /// <param name="peakAlpha">最亮时的透明度，1 = 完全遮住画面</param>
    private IEnumerator WhiteFlash(float duration = 0.25f, float peakAlpha = 0.9f)
    {
        var flashOverlay = Flash;
        if(flashOverlay == null)yield break;


        flashOverlay.gameObject.SetActive(true);

        // 第 1 帧：直接拉满。这一帧刚好用来盖住换图的痕迹
        flashOverlay.color = new Color(1f, 1f, 1f, peakAlpha);
        yield return null;

        // 之后：快速淡出
        float t = 0f;
        while (t < duration)
        {
            t += Time.unscaledDeltaTime;                    // 用 unscaled，暂停时也能正常播完
            float k = Mathf.Clamp01(t / duration);
            flashOverlay.color = new Color(1f, 1f, 1f, peakAlpha * (1f - k));
            yield return null;
        }

        flashOverlay.color = new Color(1f, 1f, 1f, 0f);
        flashOverlay.gameObject.SetActive(false);
    }

    private Image _flash;

    private Image Flash
    {
        get
        {
            if (_flash != null) return _flash;

            var bg = backGroundImage;
            if (bg == null) { Debug.LogError("[BackGround] 背景为空，无法创建闪白层"); return null; }

            var go = new GameObject("FlashOverlay", typeof(RectTransform), typeof(Image));
            go.transform.SetParent(bg.transform.parent, false);
            go.transform.SetSiblingIndex(bg.transform.GetSiblingIndex() + 1);   // 紧贴在背景上面

            var rt = (RectTransform)go.transform;          // 四角拉伸铺满
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.one;
            rt.offsetMin = Vector2.zero;
            rt.offsetMax = Vector2.zero;

            var img = go.GetComponent<Image>();
            img.color = new Color(1f, 1f, 1f, 0f);
            img.raycastTarget = false;                     // 别挡住点击

            _flash = img;
            return _flash;
        }
    }
}
