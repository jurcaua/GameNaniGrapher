using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class KeyDataManager {

    public static List<KeyDataSession> sessions = new List<KeyDataSession>();

    public static void AddSession(string gameName, string dateTime, List<KeyCode> keys, List<Keydata> values) {
        Debug.Log(string.Format("Added session: {0} at {1}, with {2} values", gameName, dateTime, keys.Count));
        sessions.Add(new KeyDataSession(gameName, dateTime, keys, values));
    }
}

public class KeyDataSession {

    public string gameName;
    public string dateTime;
    public Dictionary<KeyCode, Keydata> dictionary = new Dictionary<KeyCode, Keydata>();

    public KeyDataSession(string _gameName, string _dateTime, List<KeyCode> keys, List<Keydata> values) {
        gameName = _gameName;
        dateTime = _dateTime;

        if (!(keys == null) && !(values == null)) {
            if (keys.Count != values.Count) {
                Debug.LogWarning("Different sized arrays passed in!");
            } else {
                for (int i = 0; i < keys.Count; i++) {
                    dictionary.Add(keys[i], values[i]);
                }
            }
        }
    }

    public override string ToString() {
        string toReturn = string.Format("Game: {0}, DateTime: {1}\n", gameName, dateTime);
        foreach (Keydata data in dictionary.Values) {
            toReturn += data.ToString() + "\n";
        }
        return toReturn;
    }
}

[System.Serializable]
public class Keydata {

    // the regular data
    public KeyCode keycode;
    public int count;

    // longest hold data
    public float longestHold;
    float[] holdTime = new float[2];
    bool held = false;

    public override string ToString() {
        return string.Format("{0}: {1} times, Longest Hold: {2}", keycode.ToString(), count, longestHold);
    }

}
