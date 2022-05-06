using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StoryManager : MonoBehaviour
{
    const float defaultTextScale = 0.5f;
    const int defaultTextSize = 64;

    List<GameObject> instancedCharacterObjects;

    [SerializeField] Entity_TestStory entity;

    [SerializeField] GameObject slashObject;
    [SerializeField] CharaGroup leftGroup, rightGroup;

    bool isWait;

    //[SerializeField] Fukidashi

    const int story = 1;//�Q�Ƃ���V�[�g,�Ƃ肠�����萔

    // Start is called before the first frame update
    void Start()
    {
        /*
        GameObject[] characterPrefabs = (GameObject[])Resources.LoadAll("Character");
        for (int row = 0; row < entity.sheets[story].list.Count; row++)
        {
            for (int i = 0; i < characterPrefabs.Length; i++) 
            {
                var record = entity.sheets[story].list[row];
                if (record.speaker == characterPrefabs[i].GetComponent<StoryCharacter>().Name)
                {
                    bool wasInstance = false;
                    for (int j = 0; j < instancedCharacterObjects.Count; j++)
                    {
                        if (record.speaker == instancedCharacterObjects[j].GetComponent<StoryCharacter>().Name)
                        {
                            wasInstance = true;
                        }
                    }
                    if (!wasInstance)
                    {
                        instancedCharacterObjects.Add(Instantiate(characterPrefabs[i], transform));
                    }
                }
            }
        }
        */
        isWait = false;
        /*
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<ConversationReaction>().Disappear();
        }
        */
        //����
        //��Z���͊e�ϐ��^�̏����l�Ƃ��Ĉ�����悤�Ȃ̂ŁA1�s�S����Ƃ����Ȃ���Α��v����
        StartCoroutine(StoryCoroutine());
    }

    IEnumerator StoryCoroutine()
    {
        //Debug.Log("sheet" + entity.sheets.Count + "��,List" + entity.sheets[story].list.Count + "��");
        const float moveTime = 0.5f;
        for (int i = 0; i < entity.sheets[story].list.Count; i++)
        {
            var row = entity.sheets[story].list[i];

            if (row.eventName == "") { }
            else
            {
                switch (row.eventName)
                {
                }
            }
            //text�ǉ��O�C�x���g,����Enter��C�x���g�ɕ����ł��񂩁H

            StoryCharacter[] characters = GetComponentsInChildren<StoryCharacter>();

            foreach(var chara in characters)
            {
                ConversationEmotion emotion = chara.GetComponent<ConversationEmotion>();
                ConversationReaction reaction = chara.GetComponent<ConversationReaction>();

                if (row.speaker == "everyone" || row.speaker.Contains(chara.CharaName))
                {
                    chara.GetComponent<ConversationSpeak>().Speak(row.text);
                }
                else if(row.speaker == "")//Speaker���󂩂�text����łȂ�(=�����Ă�L���������̂܂ܒ���Â���)
                {
                    if (row.text != "")
                    {
                        ConversationSpeak speak = chara.GetComponent<ConversationSpeak>();
                        if (speak.IsSpeaking) speak.Speak(row.text);
                        else speak.ShutUp();
                    }
                }
                else
                {
                    chara.GetComponent<ConversationSpeak>().ShutUp();
                }

                if(row.who == "everyone" || row.who.Contains(chara.CharaName))
                {
                    switch (row.emotion)
                    {
                        case "normal": emotion.EmotionChange(ConversationEmotion.Emotion.Normal); break;
                        case "happy": emotion.EmotionChange(ConversationEmotion.Emotion.Happy); break;
                        case "angry": emotion.EmotionChange(ConversationEmotion.Emotion.Angry); break;
                        case "sad": emotion.EmotionChange(ConversationEmotion.Emotion.Sad); break;
                        default: break;
                    }
                    switch (row.reaction)
                    {
                        case "appear":
                            {
                                if (leftGroup.Appear(row.who))
                                {
                                    if (leftGroup.AppearNum == 1)
                                    {
                                        if (rightGroup.AppearNum == 0) leftGroup.transform.DOLocalMoveX(0, moveTime);//���̂ւ��enum-Dictionary�ɂ���ׂ��H
                                        else
                                        {
                                            leftGroup.transform.DOLocalMoveX(-3, moveTime);
                                            rightGroup.transform.DOLocalMoveX(3, moveTime);
                                            slashObject.transform.DOLocalMoveX(0, moveTime);
                                            Debug.Log("SlashObject�ړ�");
                                        }
                                    }
                                }
                                if (rightGroup.Appear(row.who))
                                {
                                    if (rightGroup.AppearNum == 1)
                                    {
                                        if (leftGroup.AppearNum == 0) rightGroup.transform.DOLocalMoveX(0, moveTime);
                                        else
                                        {
                                            leftGroup.transform.DOLocalMoveX(-3, moveTime);
                                            rightGroup.transform.DOLocalMoveX(3, moveTime);
                                            slashObject.transform.DOLocalMoveX(0, moveTime);
                                            Debug.Log("SlashObject�ړ�");
                                        }
                                    }
                                }
                            }
                            break;
                        case "disappear":
                            {
                                if (leftGroup.Disappear(row.who))
                                {
                                    if (leftGroup.AppearNum == 0)
                                    {
                                        if (rightGroup.AppearNum != 0)
                                        {
                                            rightGroup.transform.DOLocalMoveX(0, moveTime);
                                            slashObject.transform.DOLocalMoveX(-10, moveTime);
                                            Debug.Log("SlashObject�ړ�");
                                        }
                                    }
                                }
                                if (rightGroup.Disappear(row.who))
                                {
                                    if (rightGroup.AppearNum == 0)
                                    {
                                        if (leftGroup.AppearNum != 0)
                                        {
                                            leftGroup.transform.DOLocalMoveX(0, moveTime);
                                            slashObject.transform.DOLocalMoveX(10, moveTime);
                                            Debug.Log("SlashObject�ړ�");
                                        }
                                    }
                                }
                            }
                            break;
                        case "jump": StartCoroutine(reaction.Jump()); break;
                        default: break;
                    }//���������������₵������΂�����case���₹�΂���
                }
            }

            if (row.text == "") { }
            else
            {
                //text.text += (row.text + "\n");
            }//�e�L�X�g�̒ǋL

            for (int j = 0; j < transform.childCount; j++)
            {
                GameObject child = transform.GetChild(j).gameObject;

                //StoryCharacter character = child.GetComponent<StoryCharacter>();
                //ConversationEmotion cEmotion = child.GetComponent<ConversationEmotion>();
                //ConversationReaction cReaction = child.GetComponent<ConversationReaction>();

                /*if (row.speaker == "") { }
                else
                {
                    if (row.speaker == "everyone" || row.speaker == character.CharaName)
                    {
                        cReaction.Speaker(true);
                        //speakerNameText.text = row.speaker;
                    }
                    else
                    {
                        cReaction.Speaker(false);
                    }
                }
                */
            }
                

            if (row.eventName == "") { }
            else
            {
                switch (row.eventName)
                {
                    case "wait": yield return new WaitForSeconds(row.eventValue); break;
                    case "br": FontSizeChange(); isWait = true; break;
                    case "waitEnter":
                    case "glyph": isWait = true; break;//waitEnter,glyph,br�C�x���g�ŃG���^�[�҂�
                    case "fontSize":
                        {
                            FontSizeChange(row.eventValue);
                        }
                        break;
                    default: break;
                }
            }//�e�L�X�g�ǋL��C�x���g

            while (isWait)
            {
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Return)) isWait = false;
                yield return null;
            }//Enter�҂��t���O

            yield return null;

            if (row.eventName == "br") { /*text.text = "";*/ }//�����ɖ߂�
        }
    }

    void FontSizeChange()
    {
        FontSizeChange(defaultTextSize);
    }
    void FontSizeChange(int fontSize)
    {
        float magni;
        magni = fontSize / defaultTextSize;
        //text.transform.localScale = Vector3.one * defaultTextScale * magni;
    }
}


