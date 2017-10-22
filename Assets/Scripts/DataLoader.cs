using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataLoader : MonoBehaviour {

    public string folderPath;
    
    void Awake() {
        folderPath = Application.persistentDataPath + "/../GameNani/GameNani/LoadData/";
        Debug.Log(folderPath);
    }

	void Start () {
		if (Directory.Exists(folderPath)) {
            string[] files = Directory.GetFiles(folderPath);
            foreach (string file in files) {
                if (Path.GetExtension(file) == ".JSON") {
                    Debug.Log("Found file:" + file);
                    OpenFile(file);
                }
            }
        } else {
            Debug.Log("Directory does not exist!");
            Directory.CreateDirectory(folderPath);
        }
	}
	
	void OpenFile(string filePath) {
        string json = File.ReadAllText(filePath);
        PrintableData data = JsonUtility.FromJson<PrintableData>(json);

        ProcessData(data);
    }

    void ProcessData(PrintableData data) {
        LookDataManager.AddSession(data.gameName, data.dateTime, data.keys, data.lookDatas);
        KeyDataManager.AddSession(data.gameName, data.dateTime, data.keyPressData.keycodes, data.keyPressData.keydatas);
    }
}

[System.Serializable]
public class PrintableData {

    // session data
    public string gameName;
    public string dateTime;

    // ordered pairs of keys and lookdata objects
    public List<string> keys;
    public List<LookData> lookDatas;

    // key data
    public PrintableKeyPressData keyPressData;

}

[System.Serializable]
public class PrintableKeyPressData {
    public List<KeyCode> keycodes;
    public List<Keydata> keydatas;
}
