using UnityEngine;
using UnityEngine.UI;

public class disappear : MonoBehaviour
{
    [Tooltip("开始前停留的时间")]
    public float delay = 0.8f;
    [Tooltip("渐隐时长")]
    public float durationTime = 1.5f;


    private SpriteRenderer _image;
    private Color _color;

    private void Awake()
    {
        _image=GetComponent<SpriteRenderer>();
        if( _image == null)
        {
            Debug.Log("error");
        }
        _color=_image.color;
        if( _color == null)
        {
            Debug.Log("error");
        }
        _color.a = 1f;
        _image.color = _color;

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        while (delay > 0.001f)
        {
            delay-=0.1f*Time.deltaTime;
        }
        _color.a -= 0.01f;
        _image.color = _color;
    }
}
