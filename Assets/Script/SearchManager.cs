using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SearchManager : MonoBehaviour
{
    TouchScreenKeyboard keyboard;
    public Text txt;
    public Text inputfieldText;
    public TextMeshProUGUI feedBackText;
    public TextMeshProUGUI feedBackText2;
    string Fromis;
    int count;
    public GameObject[] resultList;
    public GameObject 문서화면;
    public TextMeshProUGUI 문서내용;
    public TextMeshProUGUI 문서제목;
    public GameObject 날짜;
    public int 현재문서인덱스;
    public GameObject 별;
    public GameObject 검색화면;
    public GameObject 추가정보버튼;
    public TextMeshProUGUI Info;
    public GameObject 추가정보창;
    [Header("비밀번호 체크")]
    public TextMeshProUGUI 암호종류Text;
    public GameObject 비밀번호경고창;
    public Text 입력한비밀번호;
    public GameObject 해제UI;
    public TextMeshProUGUI 비밀번호feedBackText;
    [Header("스크롤 관리")]
    public RectTransform scrollContent;

    // 암호 잠금해제 여부
    int lockCode;
    // 변하지 않는 암호정보
    int lockInfo;

    // 별관리
    GameObject 현재열린결과리스트;

    private void Start()
    {
        DeleteList();
        feedBackText.text = "";
        feedBackText2.text = "";
    }

    private void Update()
    {
        if (!비밀번호경고창.activeSelf)
        {
            if (검색화면.activeSelf)
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
                    Debug.Log("검색 실행");
                    Search();
                }
            }
        }
    }

    public void SearchActive()
    {
        문서화면.SetActive(false);
    }

    public void SearchButtonClicked()
    {
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
    }

    public void Search()
    {
        DeleteList();
        feedBackText2.text = "  개의 항목이 있습니다.";

        bool countMax = false;

        if (Fromis != null)
        {
            for (int i = 0; i < DataManager.instance.docDatas.Length; i++)
            {
                if (countMax == true)
                    break;

                for (int j = 0; j < DataManager.instance.docDatas[i].태그.Length; j++)
                {
                    if (countMax == true)
                        break;

                    if (Fromis == DataManager.instance.docDatas[i].태그[j])
                    {
                        ShowList(count, i);
                        count++;

                        if (count == 4)
                        {
                            countMax = true;
                            break;
                        }
                    }
                }
            }
            // 검색 결과 숫자 피드백
            feedBackText.text = count.ToString();
        }
        else
        {
            feedBackText.text = "입력한 항목이 없음";
        }
    }

    public void DeleteList()
    {
        for (int i = 0; i < resultList.Length; i++)
        {
            resultList[i].SetActive(false);
        }
        count = 0;
    }

    public void ShowList(int index, int i)
    {
        resultList[index].SetActive(true);

        resultList[index].GetComponent<ForResult>().fileName.text = DataManager.instance.docDatas[i].이름;

        // 문서 new 체크
        if (DataManager.instance.docDatas[i].확인한문서)
        {
            resultList[index].GetComponent<ForResult>().확인한문서테두리.SetActive(false);
        }
        else
        {
            resultList[index].GetComponent<ForResult>().확인한문서테두리.SetActive(true);
        }

        resultList[index].GetComponent<ForResult>().index = i;


        // 테두리 체크
        if (DataManager.instance.docDatas[i].중요문서)
        {
            resultList[index].GetComponent<ForResult>().중요테두리.SetActive(true);
        }
        else
        {
            resultList[index].GetComponent<ForResult>().중요테두리.SetActive(false);
        }


        // 문서 잠금여부 체크
        if (DataManager.instance.docDatas[i].암호데이터 == 0)
        {
            resultList[index].GetComponent<ForResult>().잠금배경.SetActive(false);
        }
        else if (DataManager.instance.pWDatas[(int)DataManager.instance.docDatas[i].암호데이터 - 1].해제여부)
        {
            resultList[index].GetComponent<ForResult>().잠금배경.SetActive(true);
            resultList[index].GetComponent<ForResult>().잠금아이콘.SetActive(false);
            resultList[index].GetComponent<ForResult>().해제아이콘.SetActive(true);
        }
        else
        {
            resultList[index].GetComponent<ForResult>().잠금배경.SetActive(true);
            resultList[index].GetComponent<ForResult>().잠금아이콘.SetActive(true);
            resultList[index].GetComponent<ForResult>().해제아이콘.SetActive(false);
        }
    }

    public void ShowDoc(GameObject list)
    {
        // 스크롤 위로
        scrollContent.transform.position = new Vector3(0, 0, 0);

        // 암호체크
        CheckPW(DataManager.instance.docDatas[list.GetComponent<ForResult>().index].암호데이터);
        현재열린결과리스트 = list;

        if (lockCode == 0)
        {         
            문서화면.SetActive(true);
            문서제목.text = DataManager.instance.docDatas[list.GetComponent<ForResult>().index].이름;
            문서내용.text = DataManager.instance.docDatas[list.GetComponent<ForResult>().index].내용;
            DataManager.instance.docDatas[list.GetComponent<ForResult>().index].확인한문서 = true;
            현재문서인덱스 = list.GetComponent<ForResult>().index;
            CheckStar();

            // 날짜 관리
            if (DataManager.instance.docDatas[list.GetComponent<ForResult>().index].날짜 != "")
            {
                날짜.SetActive(true);
                날짜.GetComponent<TextMeshProUGUI>().text = DataManager.instance.docDatas[list.GetComponent<ForResult>().index].날짜;
            }
            else
            {
                날짜.SetActive(false);
            }
            

            // 글자 크기 변화
            문서내용.fontSize = DataManager.instance.gameData.글자크기;

            문서내용.lineSpacing = DataManager.instance.gameData.자간크기;
        }
        else
        {
            Debug.Log(lockCode);

            // 잠김 경고
            PWAlert(lockCode);
        }

        추가정보창.SetActive(false);

        // 추가정보 버튼 표시
        if (DataManager.instance.docDatas[list.GetComponent<ForResult>().index].암호데이터 != 0)
        {
            추가정보버튼.SetActive(true);
            lockInfo = (int)DataManager.instance.docDatas[list.GetComponent<ForResult>().index].암호데이터;
        }
        else
        {
            추가정보버튼.SetActive(false);
        }
    }

    public void ShowInfo()
    {
        추가정보창.SetActive(true);
        Info.text = DataManager.instance.pWDatas[lockInfo - 1].암호종류.ToString();
    }

    public void PWAlert(int code)
    {
        비밀번호경고창.SetActive(true);
        비밀번호feedBackText.text = "파일이 아래의 암호로 보호되고 있습니다";
        해제UI.SetActive(false);

        switch (code)
        {
            case 1:
                암호종류Text.text = DataManager.instance.pWDatas[0].암호종류.ToString();
                break;
            case 2:
                암호종류Text.text = DataManager.instance.pWDatas[1].암호종류.ToString();
                break;
            case 3:
                암호종류Text.text = DataManager.instance.pWDatas[2].암호종류.ToString();
                break;
        }
    }

    public void CheckPW(DataManager.PW data)
    {
        if (data == DataManager.PW.없음)
        {
            lockCode = 0;
        }
        else if (data == DataManager.PW.A코드)
        {
            if (DataManager.instance.pWDatas[0].해제여부 == true)
            {
                lockCode = 0;
            }
            else
            {
                lockCode = 1;
            }
        }
        else if (data == DataManager.PW.B코드)
        {
            if (DataManager.instance.pWDatas[1].해제여부 == true)
            {
                lockCode = 0;
            }
            else
            {
                lockCode = 2;
            }
        }
        else if (data == DataManager.PW.C코드)
        {
            if (DataManager.instance.pWDatas[2].해제여부 == true)
            {
                lockCode = 0;
            }
            else
            {
                lockCode = 3;
            }
        }
    }

    public void PWCheck()
    {
        if (입력한비밀번호.text == DataManager.instance.pWDatas[lockCode-1].암호)
        {
            Debug.Log("암호 해제");
            DataManager.instance.pWDatas[lockCode - 1].해제여부 = true;
            비밀번호feedBackText.text = "";
            해제UI.SetActive(true);
        }
        else
        {
            Debug.Log("해제 실패");
            비밀번호feedBackText.text = "올바른 암호가 아닙니다.";
        }
    }

    public void PWContinue()
    {
        비밀번호경고창.SetActive(false);
        ShowDoc(현재열린결과리스트);
    }

    public void OffDoc()
    {
        Search();
        문서화면.SetActive(false);
    }

    public void CheckStar()
    {
        if (DataManager.instance.docDatas[현재문서인덱스].중요문서)
        {
            별.SetActive(true);
        }
        else
        {
            별.SetActive(false);
        }
    }

    public void PressStar()
    {
        if (별.activeSelf)
        {
            별.SetActive(false);
            DataManager.instance.docDatas[현재문서인덱스].중요문서 = false;
        }
        else
        {
            별.SetActive(true);
            DataManager.instance.docDatas[현재문서인덱스].중요문서 = true;
        }
    }
}
