using UnityEngine;
using UnityEngine.Events;

public abstract class BasePanel : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    public float ShowSpeed = 10;
    public float HideSpeed = 10;
    private bool isShow;

    //当淡出成功时调用委托函数
    private UnityAction hideCallBack;
    protected virtual void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = this.gameObject.AddComponent<CanvasGroup>();
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        Init();
    }
    /// <summary>
    /// 主要用于初始化 按钮事件监听等等内容
    /// </summary>
    public abstract void Init();

    public virtual void ShowMe()
    {
        isShow = true;
        canvasGroup.alpha = 0;
    }

    public virtual void HideMe(UnityAction callback)
    {
        isShow = false;
        canvasGroup.alpha = 1;
        //记录传入的当淡出成功后会执行的函数
        hideCallBack = callback;
    }
    // Update is called once per frame
    void Update()
    {
        //淡入
        if (isShow&&canvasGroup.alpha!=1)
        {
            canvasGroup.alpha += ShowSpeed * Time.deltaTime;
            if (canvasGroup.alpha >= 1)
            {
                canvasGroup.alpha = 1;
            }
        }
        //淡出
        else if (!isShow)
        {
            canvasGroup.alpha-=HideSpeed * Time.deltaTime;
            if(canvasGroup.alpha <= 0)
            {
                canvasGroup.alpha = 0;
                //应该让管理器删除自己
                hideCallBack?.Invoke();
            }
        }
    }
}
