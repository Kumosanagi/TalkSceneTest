using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationManager : MonoBehaviour
{
    ConversationEmotion  cEmotion;
    ConversationReaction cReaction;

    private void Start()
    {
        cEmotion = GetComponent<ConversationEmotion>();
        cReaction = GetComponent<ConversationReaction>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) { StartCoroutine(cReaction.Appear()); }
        if (Input.GetKeyDown(KeyCode.D)) { StartCoroutine(cReaction.Disappear()); }
        if (Input.GetKeyDown(KeyCode.J)) { cReaction.Jump(); }

        if (Input.GetKeyDown(KeyCode.S)) { cReaction.Speaker(true); }
        if (Input.GetKeyDown(KeyCode.W)) { cReaction.Speaker(false); }

        if (Input.GetKeyDown(KeyCode.N)) { cEmotion.EmotionChange(ConversationEmotion.Emotion.Normal); }
        if (Input.GetKeyDown(KeyCode.H)) { cEmotion.EmotionChange(ConversationEmotion.Emotion.Happy); }
        //if (Input.GetKeyDown(KeyCode.A)) { cEmotion.EmotionChange(ConversationEmotion.Emotion.Angry); }
        //if (Input.GetKeyDown(KeyCode.S)) { cEmotion.EmotionChange(ConversationEmotion.Emotion.Sad); }

        //if (Input.GetKeyDown(KeyCode.Space)) { cEmotion}
    }
}
