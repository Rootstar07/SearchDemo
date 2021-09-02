using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Animator animator;
    [Header("디바이스")]
    public GameObject search;
    public SearchManager searchManager;
    public GameObject star;
    public StarManager starManager;
    public GameObject doc;
    public GameObject memo;
    public MemoManager memoManager;
    public GameObject device;
    public GameObject[] app;
    public GameObject DeviceMenu;
    public GameObject setting;
    public TextMeshProUGUI exampleText;
    [Header("디바이스 로그인")]
    public Text devicePW;
    public GameObject 로그인영역;
    public GameObject 환영영역;

    [Header("방")]
    public GameObject room;

    private void Start()
    {
        CloseAll();
    }

    public void GameStart()
    {
        animator.SetBool("Start", true);
    }

    public void Search()
    {
        CloseAll();
        search.SetActive(true);
        searchManager.SearchActive();

        app[0].GetComponent<ForApp>().누른배경.SetActive(true);
        app[0].GetComponent<ForApp>().기본배경.SetActive(false);
    }

    public void Star()
    {
        CloseAll();
        star.SetActive(true);
        starManager.StarActive();

        app[1].GetComponent<ForApp>().누른배경.SetActive(true);
        app[1].GetComponent<ForApp>().기본배경.SetActive(false);
    }

    public void Memo()
    {
        CloseAll();
        memo.SetActive(true);
        memoManager.UpdateMemo();

        app[2].GetComponent<ForApp>().누른배경.SetActive(true);
        app[2].GetComponent<ForApp>().기본배경.SetActive(false);
    }

    public void Setting()
    {
        CloseAll();
        setting.SetActive(true);

        app[3].GetComponent<ForApp>().누른배경.SetActive(true);
        app[3].GetComponent<ForApp>().기본배경.SetActive(false);

        // 글자 크기 변화
        exampleText.fontSize =
            DataManager.instance.gameData.글자크기;

        exampleText.lineSpacing =
            DataManager.instance.gameData.자간크기;
    }

    public void SettingChange(int num)
    {
        // 글자 크게
        if (num == 0)
        {
           if (DataManager.instance.gameData.글자크기 < 140)
                DataManager.instance.gameData.글자크기 = 
                DataManager.instance.gameData.글자크기 + 20;
        }
        //글자 줄임
        else if (num == 1)
        {
            if (DataManager.instance.gameData.글자크기 > 60)
                DataManager.instance.gameData.글자크기 =
                DataManager.instance.gameData.글자크기 - 20;
        }
        //자간 크게
        else if (num == 2)
        {
            if (DataManager.instance.gameData.자간크기 < 50)
                DataManager.instance.gameData.자간크기 =
                DataManager.instance.gameData.자간크기 + 10;
        }
        //자간 줄임
        else if (num == 3)
        {
            if (DataManager.instance.gameData.자간크기 > 10)
                DataManager.instance.gameData.자간크기 =
                DataManager.instance.gameData.자간크기 - 10;
        }
        else if (num == 5)
        {
            DataManager.instance.gameData.글자크기 = 100;
            DataManager.instance.gameData.자간크기 = 30;
        }

        // 글자 크기 변화
        exampleText.fontSize =
            DataManager.instance.gameData.글자크기;

        exampleText.lineSpacing =
            DataManager.instance.gameData.자간크기;
    }

    public void CloseAll()
    {
        search.SetActive(false);
        star.SetActive(false);
        doc.SetActive(false);
        memo.SetActive(false);
        setting.SetActive(false);

        for (int i = 0; i < app.Length; i++)
        {
            app[i].GetComponent<ForApp>().누른배경.SetActive(false);
            app[i].GetComponent<ForApp>().기본배경.SetActive(true);
        }

        memoManager.SaveMemo();
    }

    public void Settings()
    {
        CloseAll();
        memo.SetActive(true);
    }

    public void DeviceClicked()
    {
        room.SetActive(false);
        device.SetActive(true);
        CloseAll();

        // 컴퓨터 잠금관리
        if (!DataManager.instance.gameData.컴퓨터잠금여부)
        {
            DeviceMenu.SetActive(false);
            로그인영역.SetActive(true);
            환영영역.SetActive(false);
        }
        else
        {
            UnlockDevice();
        }
    }

    public void DevicePWCheck()
    {
        if (devicePW.text == "1234")
        {
            Debug.Log("로그인 성공");
            DataManager.instance.gameData.컴퓨터잠금여부 = true;
            UnlockDevice();
        }
        else
        {
            Debug.Log("로그인 실패");
        }
    }

    public void UnlockDevice()
    {
        로그인영역.SetActive(false);
        환영영역.SetActive(true);
        DeviceMenu.SetActive(true);
    }

}
