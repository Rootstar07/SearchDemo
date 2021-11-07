using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingManager : MonoBehaviour
{
    public GameObject[] endingList;
    public GameObject rightBut;
    public GameObject leftBut;
    public int 페이즈;
    public GameObject endingScene;
    public GameObject endingA;
    public GameObject endingB;
    public GameObject thanks;
    public GameObject nhk문서화면;
    public Color color;

    public void CheckButton()
    {
        if (페이즈 == 0)
        {
            leftBut.SetActive(false);
        }
        else if (페이즈 == 8)
        {
            rightBut.SetActive(false);
        }
        else
        {
            rightBut.SetActive(true);
            leftBut.SetActive(true);
        }
    }

    public void PressRight()
    {
        페이즈++;
        CheckButton();
        ShowText();
    }

    public void PressLeft()
    {
        페이즈--;
        CheckButton();
        ShowText();
    }

    public void ShowText()
    {
        for (int i =0; i < endingList.Length; i++)
        {
            endingList[i].SetActive(false);
        }

        endingList[페이즈].SetActive(true);
    }

    public void BacktoSearch()
    {
        thanks.GetComponent<Image>().color = color;
        thanks.transform.GetChild(0).gameObject.SetActive(false);
        thanks.transform.GetChild(1).gameObject.SetActive(false);

        페이즈 = 0;      
        endingScene.SetActive(false);
        nhk문서화면.SetActive(false);
        endingList[0].SetActive(true);
        endingList[8].SetActive(false);
        rightBut.SetActive(true);
        leftBut.SetActive(true);
    }

}
