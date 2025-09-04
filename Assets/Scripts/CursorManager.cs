using System;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField]
    private Texture2D defaultImage;

    [SerializeField]
    private Texture2D aim;

    [SerializeField]
    private Texture2D[] watchFrames;

    public float frameRate = 0.4f;

    private int currentFrame = 0;

    private float timer = 0f;

    private Action action;

    void Start()
    {
        ChangeCursorToDefault();
    }

    void Update()
    {
        action?.Invoke();
    }

    public void ChangeCursorToAim()
    {
        if (action != Watch)
        {
            Cursor.SetCursor(aim, Vector2.zero, CursorMode.Auto);
        }
    }

    public void ChangeCursorToDefault()
    {
        if (action != Watch)
        {
            Cursor.SetCursor(defaultImage, Vector2.zero, CursorMode.Auto);
        }
    }

    public void StartWtach()
    {
        action = Watch;
    }

    private void Watch()
    {
        timer += Time.deltaTime;
        if (timer >= frameRate)
        {
            currentFrame = (currentFrame + 1) % watchFrames.Length;
            Cursor.SetCursor(watchFrames[currentFrame], Vector2.zero, CursorMode.Auto);
            timer -= frameRate;
        }
    }

    public void StopWatch()
    {
        action = () => { };
        ChangeCursorToDefault();
    }

}
