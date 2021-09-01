using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarManager : MonoBehaviour
{
    public GameObject[] starList;
    public GameObject starListPar;
    int count;

    private void Start()
    {
        for (int i = 0; i < starListPar.transform.childCount; i++)
        {
            starList[i] = starListPar.transform.GetChild(i).gameObject;
        }
    }

    public void StarActive()
    {
        DeleteStarList();
        CheckStarList();
    }

    public void DeleteStarList()
    {
        for (int i = 0; i < starList.Length; i++)
        {
            starList[i].SetActive(false);
        }

        count = 0;
    }

    public void CheckStarList()
    {
        for (int i = 0; i < DataManager.instance.docDatas.Length; i++)
        {
            if (DataManager.instance.docDatas[i].중요문서 == true)
            {
                AddStarList(count, i);
            }
        }
    }

    public void AddStarList(int index, int i)
    {
        starList[index].SetActive(true);
        starList[index].GetComponent<ForResult>().fileName.text = DataManager.instance.docDatas[i].이름;
        starList[index].GetComponent<ForResult>().index = i;

        if (DataManager.instance.docDatas[i].중요문서)
        {
            starList[index].GetComponent<ForResult>().중요테두리.SetActive(true);
        }
        else
        {
            starList[index].GetComponent<ForResult>().중요테두리.SetActive(false);
        }

        starList[index].GetComponent<ForResult>().확인한문서테두리.SetActive(false);

        if (DataManager.instance.docDatas[i].암호데이터 != 0)
        {
            starList[index].GetComponent<ForResult>().잠금배경.SetActive(true);
            starList[index].GetComponent<ForResult>().해제아이콘.SetActive(true);
        }
        else
        {
            starList[index].GetComponent<ForResult>().잠금배경.SetActive(false);
        }

        count++;


    }

}
