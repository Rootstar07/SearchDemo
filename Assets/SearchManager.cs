using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SearchManager : MonoBehaviour
{
    TouchScreenKeyboard keyboard;
    public Text txt;
    string Fromis;
    int count;
    public Text inputfieldText;
    public TextMeshProUGUI feedBackText;


    private void Update()
    {
        if (keyboard != null && keyboard.status == TouchScreenKeyboard.Status.Done)
        {
            txt.text = keyboard.text;
            Fromis = txt.text;
            feedBackText.text = "검색감지";         
            Search();
            keyboard = null;
        }

        
        if (Input.GetKeyDown(KeyCode.Return))
        {
            txt.text = inputfieldText.text;
            Fromis = txt.text;
            Search();
        } 
    }

    public void Search()
    {
        count = 0;

        if (Fromis != null)
        {
            for (int i = 0; i < DataManager.instance.docDatas.Length; i++)
            {
                for (int j = 0; j < DataManager.instance.docDatas[i].태그.Length; j++)
                {
                    if (Fromis == DataManager.instance.docDatas[i].태그[j])
                    {
                        Debug.Log("검색 성공");
                        count++;
                    }
                }
            }
            Debug.Log("검색 종료");

            feedBackText.text = count.ToString();
        }
        else
        {
            Debug.Log("입력한 항목이 없음");

            feedBackText.text = "입력한 항목이 없음";
        }
    }

    public void SearchButtonClicked()
    {
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
    }

}
