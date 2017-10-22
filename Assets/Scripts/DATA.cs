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

    public LookDataManager lookData;
	public KeyDataManager keyData;

    public Session(string _gameName, string _dateTime, List<string> lookDataKeys, List<LookData> lookDataValues, List<KeyCode> keyDataKeys, List<Keydata> keyDataValues) {
        gameName = _gameName;
        dateTime = _dateTime;
       
		lookData = new LookDataManager (lookDataKeys, lookDataValues);
		keyData = new KeyDataManager(keyDataKeys, keyDataValues);
    }
}
