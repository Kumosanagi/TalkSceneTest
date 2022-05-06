using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class ExcelImporterTest : MonoBehaviour
{
    [SerializeField] Entity_TestData entity;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            for(int i = 0; i < entity.sheets[0].list.Count; i++)
            {
                //Debug.Log((i + 1) + "行目のnumber:" + entity.sheets[0].list[i].number);
                //Debug.Log((i + 1) + "行目のtext:" + entity.sheets[0].list[i].text);
                Debug.Log(entity.sheets[0].list[i].number + "番目のtext:" + entity.sheets[0].list[i].text);
            }
        }
        catch { }
    }
}
