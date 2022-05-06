using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryCharacter : MonoBehaviour
{
    [SerializeField]private string charaName = "chara";
    [SerializeField]private bool isAppear;

    private void Start()
    {
        isAppear = false;
    }

    public string CharaName
    {
        get { return charaName; }
    }
    public bool IsAppear
    {
        get { return isAppear; }
        set { isAppear = value; }
    }
}
