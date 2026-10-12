using UnityEngine;

public class disappear : MonoBehaviour
{
    [Tooltip("开始渐隐前先停留多久")]
    public float delay = 0.8f;

    [Tooltip("渐隐总时长（从 1 淡到 0 需要多久）")]
    public float durationTime = 1.5f;

    [Tooltip("淡完之后要不要销毁这个物体")]
    public bool destroyWhenDone = false;

    private SpriteRenderer _image;
    private Color _color;
    private float _elapsed;

    private void Awake()
    {
        _image = GetComponent<SpriteRenderer>();
        if (_image == null)
        {
            Debug.LogError("[disappear] 身上没有 SpriteRenderer，组件已自动关闭");
            enabled = false;
            return;
        }

        _color = _image.color;
        _color.a = 1f;
        _image.color = _color;
    }

    // Update is called once per frame
    void Update()
    {
        // 原来是 while (delay > 0.001f) { delay -= 0.1f * Time.deltaTime; }，
        // 那等于在一帧里空转，把主线程堵住。现在改成按时间累加。
        // 淡出速度也改成跟 durationTime 挂钩，不再是"每帧固定减 0.01"（那样 30 帧和 144 帧的速度能差 4 倍）。
        _elapsed += Time.deltaTime;

        if (_elapsed < delay) return;

        float k = durationTime > 0f ? (_elapsed - delay) / durationTime : 1f;
        SetAlpha(1f - Mathf.Clamp01(k));

        if (k < 1f) return;

        // 淡完了：先关掉自己，免得每帧继续算
        enabled = false;
        if (destroyWhenDone) Destroy(gameObject);
    }

    void SetAlpha(float a)
    {
        _color.a = a;
        _image.color = _color;
    }
}
