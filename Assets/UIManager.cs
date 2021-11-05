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
    public GameObject 검색기록;
    [Header("설정 피드백")]
    public TextMeshProUGUI 글자크기;
    public TextMeshProUGUI 글자간격;
    public TextMeshProUGUI 띄어쓰기;
    public TextMeshProUGUI 줄간격;
    public TextMeshProUGUI 문단간격;


    private void Start()
    {
        시작화면.SetActive(true);
        //CloseAll();

        UIPos();

        UpdateUIFeedBack();
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
        if (!DataManager.instance.gameData.후원자)
        {
            하단바.transform.localPosition =
                new Vector2(0, 하단바.transform.localPosition.y + DPToPixel(2280, 50) * 2.3f);
            search.transform.localPosition =
                new Vector2(0, search.transform.localPosition.y + DPToPixel(2280, 50) * 1.5f);
            star.transform.localPosition =
                new Vector2(0, star.transform.localPosition.y + DPToPixel(2280, 50) * 1.5f);
            memo.transform.localPosition =
                new Vector2(0, memo.transform.localPosition.y + DPToPixel(2280, 50) * 1.5f);
            setting.transform.localPosition =
                new Vector2(0, setting.transform.localPosition.y + DPToPixel(2280, 50) * 1.5f);
            doc.transform.localPosition =
                new Vector2(0, doc.transform.localPosition.y + DPToPixel(2280, 50) * 2.3f);
            검색기록.transform.localPosition =
                new Vector2(0, 검색기록.transform.localPosition.y + DPToPixel(2280, 50) * 1.5f);
        }
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
        UpdateUIFeedBack();

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
                DataManager.instance.gameData.글자크기 + 10;
        }
        //글자 줄임
        else if (num == 1)
        {
            if (DataManager.instance.gameData.글자크기 > 40)
                DataManager.instance.gameData.글자크기 =
                DataManager.instance.gameData.글자크기 - 10;
        }
        //줄간격 크게
        else if (num == 2)
        {
            if (DataManager.instance.gameData.자간크기 < 160)
                DataManager.instance.gameData.자간크기 =
                DataManager.instance.gameData.자간크기 + 10;
        }
        //줄간격 줄임
        else if (num == 3)
        {
            if (DataManager.instance.gameData.자간크기 > 40)
                DataManager.instance.gameData.자간크기 =
                DataManager.instance.gameData.자간크기 - 10;
        }
        // 초기화
        else if (num == 5)
        {
            DataManager.instance.gameData.글자크기 = 100;
            DataManager.instance.gameData.자간크기 = 120;
            DataManager.instance.gameData.글자간격 = 5;
            DataManager.instance.gameData.띄어쓰기 = 20;
            DataManager.instance.gameData.문단간격 = 200;
        }
        // 글자간격
        else if (num == 6)
        {
            if (DataManager.instance.gameData.글자간격 < 10)
                DataManager.instance.gameData.글자간격 =
                DataManager.instance.gameData.글자간격 + 1;
        }
        else if (num == 7)
        {
            if (DataManager.instance.gameData.글자간격 > 0)
                DataManager.instance.gameData.글자간격 =
                DataManager.instance.gameData.글자간격 - 1;
        }
        // 띄어쓰기
        else if (num == 8)
        {
            if (DataManager.instance.gameData.띄어쓰기 < 40)
                DataManager.instance.gameData.띄어쓰기 =
                DataManager.instance.gameData.띄어쓰기 + 5;
        }
        else if (num == 9)
        {
            if (DataManager.instance.gameData.띄어쓰기 > 0)
                DataManager.instance.gameData.띄어쓰기 =
                DataManager.instance.gameData.띄어쓰기 - 5;
        }
        // 문단간격
        else if (num == 10)
        {
            if (DataManager.instance.gameData.문단간격 < 400)
                DataManager.instance.gameData.문단간격 =
                DataManager.instance.gameData.문단간격 + 20;
        }
        else if (num == 11)
        {
            if (DataManager.instance.gameData.문단간격 > 0)
                DataManager.instance.gameData.문단간격 =
                DataManager.instance.gameData.문단간격 - 20;
        }

        UpdateUIFeedBack();

        // 글자 크기 변화
        exampleText.fontSize =
            DataManager.instance.gameData.글자크기;

        exampleText.lineSpacing =
            DataManager.instance.gameData.자간크기;

        exampleText.characterSpacing =
            DataManager.instance.gameData.글자간격;

        exampleText.wordSpacing =
            DataManager.instance.gameData.띄어쓰기;

        exampleText.paragraphSpacing =
            DataManager.instance.gameData.문단간격;
    }

    public void UpdateUIFeedBack()
    {
        글자크기.text = DataManager.instance.gameData.글자크기.ToString();
        글자간격.text = DataManager.instance.gameData.글자간격.ToString();
        띄어쓰기.text = DataManager.instance.gameData.띄어쓰기.ToString();
        줄간격.text = DataManager.instance.gameData.자간크기.ToString();
        문단간격.text = DataManager.instance.gameData.문단간격.ToString();
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
