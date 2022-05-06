using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ConversationSpeak : MonoBehaviour
{
    Canvas canvas;

    [SerializeField] Vector3 mergin = Vector3.zero;

    [SerializeField] GameObject fukidashiPrefab;
    GameObject fukidashiInstance = null;

    bool isSpeaking;

    // Start is called before the first frame update
    void Start()
    {
        canvas = transform.root.GetComponentInChildren<Canvas>();
        isSpeaking = false;
        //Debug.Log(name + ":" + transform.root + "," + canvas);
    }

    public void Speak(string text)
    {
        Debug.Log(name + "ÅF" + text + "Ç∆íùÇËÇ‹Å[Ç∑");
        if(fukidashiInstance == null)
        {
            fukidashiInstance = Instantiate(fukidashiPrefab, transform);
        }
        Text textObject = fukidashiInstance.GetComponentInChildren<Text>();
        textObject.text = text;
        if (GetComponentInParent<CharaGroup>().transform.localScale.x < 0) {
            textObject.transform.localScale = new Vector3(-Mathf.Abs(textObject.transform.localScale.x), textObject.transform.localScale.y);
        }
        if (!isSpeaking)
        {
            transform.DOScale(transform.localScale * 1.1f, 0.5f);
            GetComponent<SpriteRenderer>().color = Color.white;
            isSpeaking = true;
        }
        fukidashiInstance.transform.localPosition = mergin;
    }

    public void ShutUp()
    {
        if (fukidashiInstance != null)
        {
            Destroy(fukidashiInstance);
        }
        if (isSpeaking)
        {
            transform.DOScale(transform.localScale / 1.1f, 0.5f);
            GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f, 1);
            isSpeaking = false;
        }
    }

    public bool IsSpeaking
    {
        get { return isSpeaking; }
    }
}
