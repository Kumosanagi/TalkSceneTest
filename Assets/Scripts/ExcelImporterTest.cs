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
                //Debug.Log((i + 1) + "�s�ڂ�number:" + entity.sheets[0].list[i].number);
                //Debug.Log((i + 1) + "�s�ڂ�text:" + entity.sheets[0].list[i].text);
                Debug.Log(entity.sheets[0].list[i].number + "�Ԗڂ�text:" + entity.sheets[0].list[i].text);
            }
        }
        catch { }
    }
}
