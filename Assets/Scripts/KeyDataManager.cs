﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDataManager {

    public List<KeyDataSession> sessions = new List<KeyDataSession>();

    public void AddSession(List<KeyCode> keys, List<Keydata> values) {
        Debug.Log(string.Format("Added key session with {0} values", keys.Count));
        sessions.Add(new KeyDataSession(keys, values));
    }
}

public class KeyDataSession {
    
    public Dictionary<KeyCode, Keydata> dictionary = new Dictionary<KeyCode, Keydata>();

    public KeyDataSession(List<KeyCode> keys, List<Keydata> values) {

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
        string toReturn = "";
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
