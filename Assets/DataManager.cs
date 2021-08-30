using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class DataManager : MonoBehaviour
{
    [Header("문서 데이터")]
    public DocData[] docDatas;

    [System.Serializable]
    public class DocData
    {
        public int 문서코드;
        public string 이름;
        public string[] 태그;
        [TextArea]
        public string 내용;
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
