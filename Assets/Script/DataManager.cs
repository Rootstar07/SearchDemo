using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class DataManager : MonoBehaviour
{
    [Header("문서 데이터")]
    public GameData gameData;
    public DocData[] docDatas;
    public PWData[] pWDatas;

    public enum PW
    {
        없음,
        A코드,
        B코드,
        C코드
    }

    [System.Serializable]
    public class DocData
    {
        public string 이름;
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

    public static DataManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void ExportData()
    {
        string jsonData0 = JsonConvert.SerializeObject(docDatas);
        File.WriteAllText(Application.persistentDataPath + "/docDatas.json", jsonData0);

        Debug.Log("데이터 내보내기 완료");
    }

    public void ImportData()
    {
        string data0 = File.ReadAllText(Application.persistentDataPath + "/docDatas.json");
        docDatas = JsonConvert.DeserializeObject<DocData[]>(data0);

        Debug.Log("데이터 불러오기 완료");
    }
}
