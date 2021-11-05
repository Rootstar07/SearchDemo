using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using TMPro;

public class DataManager : MonoBehaviour
{
    public TextMeshProUGUI path;

    [Header("문서 데이터")]
    public GameData gameData;
    public DocData[] docDatas;
    public PWData[] pWDatas;
    public MemoData[] memoDatas;
    public ThemeData[] themeDatas;

    public enum PW
    {
        없음,
        구원받은_장소,
        토모히로의_사망날짜_YYMMDD,
        강정호의_아들_이름,
        유키코의_생일_MMDD,
        유키코의_두번째책_출간일_MMDD,
        보관물_756
    }

    [System.Serializable]
    public class DocData
    {
        public string 이름;
        public string 날짜;
        public string[] 태그;
        public bool 중요문서;
        public bool 확인한문서;
        public PW 암호데이터;
        [TextArea]
        public string 내용;
    }
    
    [System.Serializable]
    public class PWData
    {
        public PW 암호종류;
        public string 암호;
        public bool 해제여부;
    }

    [System.Serializable]
    public class GameData
    {
        public bool 컴퓨터잠금여부;
        public int 글자크기;
        public int 자간크기;
        public int 글자간격;
        public int 띄어쓰기;
        public int 문단간격;
        public int 현재테마;
        public bool 후원자;
    }

    [System.Serializable]
    public class MemoData
    {
        public string 내용;
    }

    [System.Serializable]
    public class ThemeData
    {
        public bool 활성가능;
        public Color basicColor;
        public Color anoColor;
        public Color shadowColor;
        public Color backgroundColor;
    }

    public static DataManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // 데이터 확인
        FileInfo fi = new FileInfo(Application.persistentDataPath + "/docDatas.json");
        if (fi.Exists)
        {
            Debug.Log("저장된 파일 있음");
            ImportData();
        }
        else
        {
            Debug.Log("저장된 파일 없음");
            LoadData();
        }
    }

    public void ExportData()
    {
        string jsonData0 = JsonConvert.SerializeObject(docDatas);
        File.WriteAllText(Application.persistentDataPath + "/docDatas.json", jsonData0);

        string jsonData1 = JsonConvert.SerializeObject(gameData);
        File.WriteAllText(Application.persistentDataPath + "/gameData.json", jsonData1);

        string jsonData2 = JsonConvert.SerializeObject(pWDatas);
        File.WriteAllText(Application.persistentDataPath + "/pWDatas.json", jsonData2);

        string jsonData3 = JsonConvert.SerializeObject(memoDatas);
        File.WriteAllText(Application.persistentDataPath + "/memoDatas.json", jsonData3);

        Debug.Log("저장 완료");

        path.text = Application.persistentDataPath;
    }

    // android/data 에서 접근하는 방법
    public void ImportData()
    {
        string data0 = File.ReadAllText(Application.persistentDataPath + "/docDatas.json");
        docDatas = JsonConvert.DeserializeObject<DocData[]>(data0);

        string data1 = File.ReadAllText(Application.persistentDataPath + "/gameData.json");
        gameData = JsonConvert.DeserializeObject<GameData>(data1);

        string data2 = File.ReadAllText(Application.persistentDataPath + "/pWDatas.json");
        pWDatas = JsonConvert.DeserializeObject<PWData[]>(data2);

        string data3 = File.ReadAllText(Application.persistentDataPath + "/memoDatas.json");
        memoDatas = JsonConvert.DeserializeObject<MemoData[]>(data3);

        Debug.Log("불러오기 완료");
    }

    // 유니티 파일 내부에서 처리
    public void LoadData()
    {
        // 초기 파일 불러오기
        TextAsset asset = Resources.Load<TextAsset>("json/DEMOdocDatas");
        docDatas = JsonConvert.DeserializeObject<DocData[]>(asset.ToString());

        Debug.Log("초기 데이터 불러오기 성공");
    }

    public void DeleteData()
    {
        File.Delete(Application.persistentDataPath + "/docDatas.json");
        File.Delete(Application.persistentDataPath + "/gameData.json");
        File.Delete(Application.persistentDataPath + "/pWDatas.json");
        File.Delete(Application.persistentDataPath + "/memoDatas.json");

        Debug.Log("초기화 완료");

        Application.Quit();

        //LoadData();
        //ExportData();
        //ImportData();
    }
}
