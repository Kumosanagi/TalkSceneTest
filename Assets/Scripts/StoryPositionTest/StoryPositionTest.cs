using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StoryPositionTest : MonoBehaviour
{
    [SerializeField] GameObject slashObject;

    [SerializeField] GameObject leftGroup, rightGroup;
    CharaGroupTest leftCharaGroup, rightCharaGroup;

    private void Start()
    {
        leftCharaGroup = leftGroup.GetComponent<CharaGroupTest>();
        rightCharaGroup = rightGroup.GetComponent<CharaGroupTest>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            slashObject.transform.DOLocalMoveX(-10, 0.5f);
            leftCharaGroup.transform.DOMoveX(-2, 0.5f);
            leftCharaGroup.appearNum = 0;
            leftCharaGroup.Appear("");
            rightCharaGroup.appearNum = 1;
            rightCharaGroup.Disappear("");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            slashObject.transform.DOLocalMoveX(-10, 0.5f);
            leftCharaGroup.transform.DOMoveX(-2, 0.5f);
            leftCharaGroup.appearNum = 1;
            leftCharaGroup.Appear("");
            rightCharaGroup.appearNum = 1;
            rightCharaGroup.Disappear("");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            slashObject.transform.DOLocalMoveX(-10, 0.5f);
            leftCharaGroup.transform.DOMoveX(-2, 0.5f);
            leftCharaGroup.appearNum = 2;
            leftCharaGroup.Appear("");
            rightCharaGroup.appearNum = 1;
            rightCharaGroup.Disappear("");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            slashObject.transform.DOLocalMoveX(0, 0.5f);
            leftCharaGroup.transform.DOMoveX(3, 0.5f);
            leftCharaGroup.appearNum = 2;
            leftCharaGroup.Appear("");
            rightCharaGroup.appearNum = 0;
            rightCharaGroup.Appear("");
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            slashObject.transform.DOLocalMoveX(0, 0.5f);
            leftCharaGroup.transform.DOMoveX(3, 0.5f);
            leftCharaGroup.appearNum = 2;
            leftCharaGroup.Appear("");
            rightCharaGroup.appearNum = 1;
            rightCharaGroup.Appear("");
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            slashObject.transform.DOLocalMoveX(0, 0.5f);
            leftCharaGroup.transform.DOMoveX(3, 0.5f);
            leftCharaGroup.appearNum = 2;
            leftCharaGroup.Appear("");
            rightCharaGroup.appearNum = 2;
            rightCharaGroup.Appear("");
        }
    }
}
