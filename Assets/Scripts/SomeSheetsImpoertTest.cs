using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomeSheetsImpoertTest : MonoBehaviour
{
    [SerializeField] Entity_SomeSheetsTestData entity;

    // Start is called before the first frame update
    void Start()
    {
        System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
        sw.Start();
        try
        {
            for (int i = 0; i < 1;i++)//entity.sheets.Count; i++)
            {
                for (int j = 0; j < entity.sheets[i].list.Count; j++)
                {
                    //Debug.Log((i + 1) + "�s�ڂ�number:" + entity.sheets[0].list[i].number);
                    //Debug.Log((i + 1) + "�s�ڂ�text:" + entity.sheets[0].list[i].text);
                    Debug.Log(entity.sheets[i].list[j].number + "�Ԗڂ�text:" + entity.sheets[i].list[j].text);
                }
            }
        }
        catch { }
        sw.Stop();
        Debug.Log("����ԁF" + sw.ElapsedMilliseconds + "ms");
    }
}
