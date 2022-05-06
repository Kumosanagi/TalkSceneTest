using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationEmotion : MonoBehaviour
{
    [SerializeField] Sprite normalSprite;
    [SerializeField] Sprite happySprite;
    [SerializeField] Sprite angrySprite;
    [SerializeField] Sprite sadSprite;

    SpriteRenderer spriteRenderer;

    public enum Emotion
    {
        Normal,
        Happy,
        Angry,
        Sad
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void EmotionChange(Emotion emotion)
    {
        switch (emotion)
        {
            case Emotion.Normal: spriteRenderer.sprite = normalSprite; break;
            case Emotion.Happy: spriteRenderer.sprite = happySprite; break;
            case Emotion.Angry: spriteRenderer.sprite = angrySprite; break;
            case Emotion.Sad: spriteRenderer.sprite = sadSprite; break;
            default: break;
        }
    }
}
