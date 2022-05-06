using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharaGroup : MonoBehaviour
{
    [SerializeField] GameObject pairSlashObject, trioSlashObject;
    [SerializeField] Vector3 disappearSlashPos;
    [SerializeField] Vector3 pairSlashPos;
    [SerializeField] Vector3 trioLowSlashPos, trioHighSlashPos;

    const float MoveTime = 0.5f;
    int appearNum;

    private void Start()
    {
        appearNum = 0;
    }

    public bool Appear(string name)//AppearÇ∑ÇÈëŒè€Ç™Ç¢ÇΩÇ»ÇÁtrue,Ç¢Ç»ÇØÇÍÇŒfalse
    {
        bool isAppearing = false;
        StoryCharacter[] characters = GetComponentsInChildren<StoryCharacter>();
        foreach(var chara in characters)
        {
            if (name.Contains(chara.CharaName))
            {
                chara.IsAppear = true;
                appearNum = CountAppearing();
                isAppearing = true;
                PositionChange();
            }
        }
        return isAppearing;
    }

    public bool Disappear(string name)//DisappearÇ∑ÇÈëŒè€Ç™Ç¢ÇΩÇ»ÇÁtrue,Ç¢Ç»ÇØÇÍÇŒfalse
    {
        bool isDisappearing = false;
        StoryCharacter[] characters = GetComponentsInChildren<StoryCharacter>();
        foreach (var chara in characters)
        {
            if (name.Contains(chara.CharaName))
            {
                chara.IsAppear = false;
                appearNum = CountAppearing();
                isDisappearing = true;
                PositionChange();
            }
        }
        return isDisappearing;
    }

    private int CountAppearing()
    {
        int num = 0;
        StoryCharacter[] characters = GetComponentsInChildren<StoryCharacter>();
        foreach (var chara in characters)
        {
            if (chara.IsAppear)
            {
                num++;
            }
        }
        return num;
    }

    private void PositionChange()
    {
        StoryCharacter[] characters = GetComponentsInChildren<StoryCharacter>();
        int num = 0;//âΩî‘ñ⁄ÇÃìoèÍíÜÉLÉÉÉâÉNÉ^Å[Ç©ÇÃîªífópïœêî
        foreach (var chara in characters)
        {
            CharaPosition charaPosition = new CharaPosition();
            CharaPosition.Position destination = CharaPosition.Position.Disappear;//Ç∆ÇËÇ†Ç¶Ç∏DisappearÇìnÇµÇƒÇ®Ç≠
            if (chara.IsAppear)
            {
                switch (appearNum)
                {
                    case 1:
                        {
                            chara.transform.GetComponentInParent<CharaFrame>().transform.DOScale(charaPosition.scaleDict[CharaPosition.Scale.Solo], MoveTime);
                            destination = CharaPosition.Position.Solo;
                            pairSlashObject.transform.DOLocalMove(disappearSlashPos, MoveTime);
                            trioSlashObject.transform.DOLocalMove(disappearSlashPos, MoveTime);
                        }
                        break;
                    case 2:
                        {
                            chara.transform.GetComponentInParent<CharaFrame>().transform.DOScale(charaPosition.scaleDict[CharaPosition.Scale.Pair], MoveTime);
                            switch (num)
                            {
                                case 0: destination = CharaPosition.Position.PairHigh; break;
                                case 1: destination = CharaPosition.Position.PairLow; break;
                                default:break;
                            }
                            pairSlashObject.transform.DOLocalMove(pairSlashPos, MoveTime);
                            trioSlashObject.transform.DOLocalMove(disappearSlashPos, MoveTime);
                        }
                        break;
                    case 3:
                        {
                            chara.transform.GetComponentInParent<CharaFrame>().transform.DOScale(charaPosition.scaleDict[CharaPosition.Scale.Trio], MoveTime);
                            switch (num)
                            {
                                case 0: destination = CharaPosition.Position.TrioHigh; break;
                                case 1: destination = CharaPosition.Position.TrioMiddle; break;
                                case 2: destination = CharaPosition.Position.TrioLow; break;
                                default: break;
                            }
                            pairSlashObject.transform.DOLocalMove(trioHighSlashPos, MoveTime);
                            trioSlashObject.transform.DOLocalMove(trioLowSlashPos, MoveTime);
                        }
                        break;
                    default: break;
                }
                num++;
            }
            //Debug.Log(name + "ÇÃdestination:" + destination.ToString());
            if (destination == CharaPosition.Position.Disappear) chara.GetComponentInParent<CharaFrame>().transform.DOLocalMove(charaPosition.positionDict[destination], MoveTime).SetRelative();
            else chara.GetComponentInParent<CharaFrame>().transform.DOLocalMove(charaPosition.positionDict[destination], MoveTime);
        }
    }

    public int AppearNum
    {
        get { return appearNum; }
    }
}
