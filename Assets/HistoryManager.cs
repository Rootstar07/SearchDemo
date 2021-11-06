using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryManager : MonoBehaviour
{
    public GameObject[] historyList;

    public void SaveHistory()
    {
        for (int i = 0; i < DataManager.instance.gameData.검색기록.Length; i++)
        {
            DataManager.instance.gameData.검색기록[i] = historyList[i].GetComponent<ForHistory>().기록;
        }
    }

    public void LoadHistory()
    {
        for (int i = 0; i < DataManager.instance.gameData.검색기록.Length; i++)
        {
            historyList[i].GetComponent<ForHistory>().기록 = DataManager.instance.gameData.검색기록[i];
            historyList[i].GetComponent<ForHistory>().targetText.text = DataManager.instance.gameData.검색기록[i];
        }

        Debug.Log("히스토리 불러오기 완료");
    }
}
