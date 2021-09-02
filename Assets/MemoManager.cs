using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MemoManager : MonoBehaviour
{
    public GameObject[] memoList;
    public GameObject memoListTarget;

    public void Start()
    {
        for (int i = 0; i < memoListTarget.transform.childCount; i++)
        {
            memoList[i] = memoListTarget.transform.GetChild(i).gameObject;
        }
    }

    public void UpdateMemo()
    {
        for (int i = 0; i < memoList.Length; i++)
        {
            memoList[i].GetComponent<TMP_InputField>().text =
                DataManager.instance.memoDatas[i].내용;
        }
    }

    public void SaveMemo()
    {
        for (int i = 0; i < memoList.Length; i++)
        {
            DataManager.instance.memoDatas[i].내용 =
                memoList[i].GetComponent<TMP_InputField>().text;
        }
    }

}
