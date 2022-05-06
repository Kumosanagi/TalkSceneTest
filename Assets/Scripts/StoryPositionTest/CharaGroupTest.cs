using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharaGroupTest : MonoBehaviour
{
    public int appearNum;

    private void Start()
    {
        appearNum = 0;
    }

    private void Update()
    {

    }

    public int Appear(string name)
    {
        appearNum++;
        CharaPositionChange(appearNum);
        return appearNum;
    }

    public int Disappear(string name)
    {
        appearNum--;
        CharaPositionChange(appearNum);
        return appearNum;
    }

    void CharaPositionChange(int num)//‚±‚±‚¿‚á‚ñ‚Æ–¼‘O‚É‚æ‚Á‚Ä”»•Ê‚·‚é‚æ‚¤‚É‚·‚é
    {
        float moveTime = 0.5f;
        CharaPosition charaPosition = new CharaPosition();
        //enum‚ğKey‚É‚µ‚½Dictionary‚ÅCharaFrame‚ÌÀ•W‚ğ•ÏX‚·‚é

        switch (num)
        {
            case 0:
                {
                    for (int i = 0; i < transform.childCount; i++)
                    {
                        transform.GetChild(i).transform.DOLocalMove(charaPosition.positionDict[CharaPosition.Position.Disappear], moveTime);
                    }
                }
                break;
            case 1:
                {
                    transform.GetChild(0).transform.DOLocalMove(charaPosition.positionDict[CharaPosition.Position.Solo], moveTime);
                    transform.GetChild(1).transform.DOLocalMove(charaPosition.positionDict[CharaPosition.Position.Disappear], moveTime);
                    transform.GetChild(2).transform.DOLocalMove(charaPosition.positionDict[CharaPosition.Position.Disappear], moveTime);
                    for(int i = 0; i < transform.childCount; i++)
                    {
                        transform.GetChild(i).transform.DOScale(charaPosition.scaleDict[CharaPosition.Scale.Solo], moveTime);
                    }
                }
                break;
            case 2:
                {
                    transform.GetChild(0).transform.DOLocalMove(charaPosition.positionDict[CharaPosition.Position.PairLow], moveTime);
                    transform.GetChild(1).transform.DOLocalMove(charaPosition.positionDict[CharaPosition.Position.PairHigh], moveTime);
                    transform.GetChild(2).transform.DOLocalMove(charaPosition.positionDict[CharaPosition.Position.Disappear], moveTime);
                    for (int i = 0; i < transform.childCount; i++)
                    {
                        transform.GetChild(i).transform.DOScale(charaPosition.scaleDict[CharaPosition.Scale.Pair], moveTime);
                    }
                }
                break;
            case 3:
                {
                    transform.GetChild(0).transform.DOLocalMove(charaPosition.positionDict[CharaPosition.Position.TrioLow], moveTime);
                    transform.GetChild(1).transform.DOLocalMove(charaPosition.positionDict[CharaPosition.Position.TrioMiddle], moveTime);
                    transform.GetChild(2).transform.DOLocalMove(charaPosition.positionDict[CharaPosition.Position.TrioHigh], moveTime);
                    for (int i = 0; i < transform.childCount; i++)
                    {
                        transform.GetChild(i).transform.DOScale(charaPosition.scaleDict[CharaPosition.Scale.Trio], moveTime);
                    }
                }
                break;
            default: break;
        }
    }
}
