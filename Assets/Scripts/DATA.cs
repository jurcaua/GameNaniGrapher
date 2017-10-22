using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DATA {

    public static List<Session> sessions = new List<Session>();

    public static void AddSession(string gameName, string dateTime, List<string> lookDataKeys, List<LookData> lookDataValues, List<KeyCode> keyDataKeys, List<Keydata> keyDataValues) {
        sessions.Add(new Session(gameName, dateTime, lookDataKeys, lookDataValues, keyDataKeys, keyDataValues));
    }
    
}

public class Session {

    public string gameName;
    public string dateTime;

    public LookDataManager LookData = new LookDataManager();
    public KeyDataManager KeyData = new KeyDataManager();

    public Session(string _gameName, string _dateTime, List<string> lookDataKeys, List<LookData> lookDataValues, List<KeyCode> keyDataKeys, List<Keydata> keyDataValues) {
        gameName = _gameName;
        dateTime = _dateTime;
        
        AddLookDataSessions(lookDataKeys, lookDataValues);
        AddKeyDataSessions(keyDataKeys, keyDataValues);
    }

    public void AddLookDataSessions(List<string> keys, List<LookData> values) {
        LookData.AddSession(keys, values);
    }

    public void AddKeyDataSessions(List<KeyCode> keys, List<Keydata> values) {
        KeyData.AddSession(keys, values);
    }

}
