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

    public enum PW
    {
        없음,
        어머니의_생일,
        토모히로의_사망날짜,
        C코드
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
    }

    [System.Serializable]
    public class MemoData
    {
        public string 내용;
    }

    public static DataManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //ImportData();
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

        Debug.Log("데이터 내보내기 완료");

        path.text = Application.persistentDataPath;
    }

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

        Debug.Log("데이터 불러오기 완료");
    }
}
