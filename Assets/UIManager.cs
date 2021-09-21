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
    public GameObject 하단바;
    [Header("디바이스 로그인")]
    public Text devicePW;
    public GameObject 로그인영역;
    public GameObject 환영영역;
    [Header("시작")]
    public GameObject 시작화면;

    private void Start()
    {
        시작화면.SetActive(true);
        //CloseAll();

        UIPos();
    }

    public void StartClick()
    {
        시작화면.SetActive(false);
        // 데이터 로딩
    }

    //로딩
    public void GameStart()
    {
        animator.SetBool("Start", true);
    }

    // 배너 광고 UI 위치 조정
    public void UIPos()
    {
        하단바.transform.localPosition = 
            new Vector2(0, 하단바.transform.localPosition.y + DPToPixel(2280, 50)*2.3f);
        search.transform.localPosition =
            new Vector2(0, search.transform.localPosition.y + DPToPixel(2280, 50) * 1.5f);
        star.transform.localPosition =
            new Vector2(0, star.transform.localPosition.y + DPToPixel(2280, 50) * 1.5f);
        memo.transform.localPosition =
            new Vector2(0, memo.transform.localPosition.y + DPToPixel(2280, 50) * 1.5f);
        setting.transform.localPosition =
            new Vector2(0, setting.transform.localPosition.y + DPToPixel(2280, 50) * 1.5f);
        doc.transform.localPosition =
            new Vector2(0, doc.transform.localPosition.y + DPToPixel(2280, 50) * 1.5f);

    }

    public float DPToPixel(float fFixedResoulutionHeight, float fdpHeight)
    {
        float fNowDpi = (Screen.dpi * fFixedResoulutionHeight) / Screen.height;
        float scale = fNowDpi / 160;
        float pixel = fdpHeight * scale;
        return pixel;
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
           if (DataManager.instance.gameData.글자크기 < 160)
                DataManager.instance.gameData.글자크기 = 
                DataManager.instance.gameData.글자크기 + 20;
        }
        //글자 줄임
        else if (num == 1)
        {
            if (DataManager.instance.gameData.글자크기 > 40)
                DataManager.instance.gameData.글자크기 =
                DataManager.instance.gameData.글자크기 - 20;
        }
        //자간 크게
        else if (num == 2)
        {
            if (DataManager.instance.gameData.자간크기 < 150)
                DataManager.instance.gameData.자간크기 =
                DataManager.instance.gameData.자간크기 + 10;
        }
        //자간 줄임
        else if (num == 3)
        {
            if (DataManager.instance.gameData.자간크기 > 50)
                DataManager.instance.gameData.자간크기 =
                DataManager.instance.gameData.자간크기 - 10;
        }
        else if (num == 5)
        {
            DataManager.instance.gameData.글자크기 = 100;
            DataManager.instance.gameData.자간크기 = 100;
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
