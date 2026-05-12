using ModestTree;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogWindow : MonoBehaviour
{

    [SerializeField]
    private List<Line> lines;

    [SerializeField]
    private TMP_Text text;

    [SerializeField]
    private AudioSource globalAudioSource;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private Image headImage;

    [SerializeField]
    private Image backgroundImage;

    [SerializeField]
    private Image variants;

    private Sprite[] frames;

    public float timer = 0f;

    public float frameRate = 0.5f;

    public int currentFrame = 0;

    private Action action;

    private float movingDownDuration = 1f;

    private float movingDownDistance = 420f;

    [SerializeField]
    private List<AfterDialogActionResolver> afterDialogActionResolvers = new();

    private string executedResolverName;

    private void Update()
    {
        action?.Invoke();
    }

    public void AddNodeAndShow(DialogNode node)
    {
        ClearAllLines();
        if (globalAudioSource != null)
        {
            globalAudioSource.Pause();
        }

        var frames = node.Frames;
        var audioClip = node.AudioClip;
        var background = node.Background;

        if (background != null)
        {
            backgroundImage.sprite = background;
        }
        if (frames != null)
        {
            this.frames = frames;
            action = PlayHeadAnimation;
        }

        PlayAudioClipOneShot(audioClip);

        text.text = node.Text;
        var variants = node.GetLVariants();

        for (int i = 0; i < variants.Count; i++)
        {
            var variant = variants[i];
            if (lines != null && !lines.IsEmpty())
            {
                var line = lines[i];
                line.AddTextAndNode(variant);
            }
        }
        Enable();
    }

    private void PlayAudioClipOneShot(AudioClip audioClip)
    {
        if (audioClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(audioClip);
            Invoke("OnAudioFinished", audioClip.length);
        }
    }

    private void OnAudioFinished()
    {
        timer = 0;
        currentFrame = 0;
        headImage.sprite = frames[currentFrame];
        DisableAction();
    }

    private void Enable()
    {
        headImage.gameObject.SetActive(true);
        backgroundImage.gameObject.SetActive(true);
        gameObject.SetActive(true);
    }

    private void PlayHeadAnimation()
    {
        timer += Time.deltaTime;
        if (timer >= frameRate)
        {
            currentFrame = (currentFrame + 1) % frames.Length;
            headImage.sprite = frames[currentFrame];
            timer -= frameRate;
        }
    }

    public void EndDialog(string actionName)
    {
        executedResolverName = actionName;
        text.text = string.Empty;
        ClearAllLines();
        StartCoroutine(MoveVariantsDown(variants.rectTransform.anchoredPosition - new Vector2(0, movingDownDistance)));
    }

    private void ExecuteAfterDeialogAction()
    {
        if (executedResolverName != null && executedResolverName != "")
        {
            var resolver = afterDialogActionResolvers.Where(resolver => resolver.ActionName.Equals(executedResolverName))
                .FirstOrDefault();
            if (resolver != null)
            {
                resolver.Execute();
            }
        }
    }

    private void ClearAllLines()
    {
        foreach (Line line in lines)
        {
            line.ClearText();
        }
    }

    private IEnumerator MoveVariantsDown(Vector2 targetPosition)
    {
        Vector2 startPosition = variants.rectTransform.anchoredPosition;
        float elapsed = 0f;

        while (elapsed < movingDownDuration)
        {
            elapsed += Time.deltaTime;
            float speed = elapsed / movingDownDuration;
            float smoothT = Mathf.SmoothStep(0f, 1f, speed);

            variants.rectTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, smoothT);
            yield return null;
        }

        variants.rectTransform.anchoredPosition = targetPosition;
        Disable();

    }

    public void Disable()
    {
        headImage.gameObject.SetActive(false);
        backgroundImage.gameObject.SetActive(false);
        gameObject.SetActive(false);
        ExecuteAfterDeialogAction();
    }

    private void DisableAction() => action = () => { };
}
