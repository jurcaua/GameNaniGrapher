﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LookDataManager {

    public static List<LookDataSession> sessions = new List<LookDataSession>();

    public static void AddSession(string gameName, string dateTime, List<string> keys, List<LookData> values) {
        Debug.Log(string.Format("Added session: {0} at {1}, with {2} values", gameName, dateTime, keys.Count));
        sessions.Add(new LookDataSession(gameName, dateTime, keys, values));
    }
}

[System.Serializable]
public class LookDataSession {

    public string gameName;
    public string dateTime;
    public Dictionary<string, LookData> dictionary = new Dictionary<string, LookData>();

    public LookDataSession(string _gameName, string _dateTime, List<string> keys, List<LookData> values) {
        gameName = _gameName;
        dateTime = _dateTime;

        if (!(keys == null) && !(values == null)){
            if (keys.Count != values.Count) {
                Debug.LogWarning("Different sized arrays passed in!");
            } else {
                for (int i = 0; i < keys.Count; i++) {
                    dictionary.Add(keys[i], values[i]);
                }
            }
        }
    }
}

[System.Serializable]
public class LookData {

    public float TotalTime;
    public float averageTime;
    public List<LookSession> list;
    public int lookedAt;

    public override string ToString() {
        string toReturn = string.Format("Total: {0}, Avg: {1}, #Times: {2}\n", TotalTime, averageTime, lookedAt);
        foreach (LookSession session in list) {
            toReturn += session.ToString() + "\n";
        } 
        return toReturn;
    }
}

[System.Serializable]
public class LookSession {
    public float start;
    public float end;
    public float duration;
    public float attention;

    public LookSession(float d) {
        end = Time.time;
        duration = d;
        start = end - duration;
        attention = 0;
    }

    public override string ToString() {
        return string.Format("({2}:({0} --> {1}), attention: {3}", start, end, duration, attention);
    }

}