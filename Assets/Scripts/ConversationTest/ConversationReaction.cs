using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ConversationReaction : MonoBehaviour
{
    Vector3 defaultPos;
    Vector3 defaultScale;

    bool isAppearing = true;
    bool isSpeaking = true;

    private void Start()
    {
        defaultPos = transform.position;//これやるとappearが機能しなくなるな…
        defaultScale = transform.localScale;
    }

    public void Speaker(bool isSpeaker)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (isSpeaker)
        {
            spriteRenderer.color = new Color(1, 1, 1);
            transform.localScale = defaultScale * 1.2f;
        }
        else
        {
            spriteRenderer.color = new Color(0.6f, 0.6f, 0.6f);
            transform.localScale = defaultScale;
        }
    }

    //以下のコルーチンにまたDG.Tweening適用する
    public IEnumerator Appear()
    {
        const float time = 0.5f;
        transform.DOMove(defaultPos, time).SetEase(Ease.OutBack);
        isAppearing = true;
        yield break;
    }

    public IEnumerator Disappear()
    {
        if (isAppearing)
        {
            const float time = 0.5f;
            transform.DOMove(defaultPos + (new Vector3(10, 0, 0) * Mathf.Sign(transform.position.x)), time).SetEase(Ease.InCubic);
            isAppearing = false;
        }
        yield break; ;
    }

    public IEnumerator Jump()
    {
        transform.DOMoveY(2, 0.2f).SetEase(Ease.OutCubic).SetLoops(6, LoopType.Yoyo).SetRelative();
        yield break;
        //イージングかける
    }

    public Vector3 DefaultPos
    {
        get { return defaultPos; }
        set
        {
            defaultPos = value;
            transform.position = defaultPos;
        }
    }
    public Vector3 DefaultScale
    {
        get { return defaultScale; }
        set { defaultScale = value; }
    }
}
