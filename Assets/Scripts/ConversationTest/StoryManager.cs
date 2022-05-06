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

    const int story = 1;//参照するシート,とりあえず定数

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
        //メモ
        //空セルは各変数型の初期値として扱われるようなので、1行全部空とかがなければ大丈夫そう
        StartCoroutine(StoryCoroutine());
    }

    IEnumerator StoryCoroutine()
    {
        //Debug.Log("sheet" + entity.sheets.Count + "枚,List" + entity.sheets[story].list.Count + "列");
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
            //text追加前イベント,これEnter後イベントに併合できんか？

            StoryCharacter[] characters = GetComponentsInChildren<StoryCharacter>();

            foreach(var chara in characters)
            {
                ConversationEmotion emotion = chara.GetComponent<ConversationEmotion>();
                ConversationReaction reaction = chara.GetComponent<ConversationReaction>();

                if (row.speaker == "everyone" || row.speaker.Contains(chara.CharaName))
                {
                    chara.GetComponent<ConversationSpeak>().Speak(row.text);
                }
                else if(row.speaker == "")//Speakerが空かつtextが空でない(=喋ってるキャラがそのまま喋りつづける)
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
                                        if (rightGroup.AppearNum == 0) leftGroup.transform.DOLocalMoveX(0, moveTime);//このへんもenum-Dictionaryにするべき？
                                        else
                                        {
                                            leftGroup.transform.DOLocalMoveX(-3, moveTime);
                                            rightGroup.transform.DOLocalMoveX(3, moveTime);
                                            slashObject.transform.DOLocalMoveX(0, moveTime);
                                            Debug.Log("SlashObject移動");
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
                                            Debug.Log("SlashObject移動");
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
                                            Debug.Log("SlashObject移動");
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
                                            Debug.Log("SlashObject移動");
                                        }
                                    }
                                }
                            }
                            break;
                        case "jump": StartCoroutine(reaction.Jump()); break;
                        default: break;
                    }//もし発動条件増やしたければここのcase増やせばおｋ
                }
            }

            if (row.text == "") { }
            else
            {
                //text.text += (row.text + "\n");
            }//テキストの追記

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
                    case "glyph": isWait = true; break;//waitEnter,glyph,brイベントでエンター待ち
                    case "fontSize":
                        {
                            FontSizeChange(row.eventValue);
                        }
                        break;
                    default: break;
                }
            }//テキスト追記後イベント

            while (isWait)
            {
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Return)) isWait = false;
                yield return null;
            }//Enter待ちフラグ

            yield return null;

            if (row.eventName == "br") { /*text.text = "";*/ }//白紙に戻す
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


