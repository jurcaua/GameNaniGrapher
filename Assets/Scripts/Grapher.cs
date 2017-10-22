﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grapher : MonoBehaviour {
    
    public int resolution = 10;
    public Text xAxis;
    public Text yAxis;
    public Text yMin;
    public Text yMax;
    public float wideMult;
    public float xShift;

    private int currentResolution;
    private List<Vector3> points = new List<Vector3>();
    private LineRenderer[] r;

    void Start() {

        r = GetComponentsInChildren<LineRenderer>();

        //CreatePoints();


        //Graph("x", lookSessions.Count)
    }

    void Update() {
        /*
        if (currentResolution != resolution || points == null) {
            CreatePoints();
        }

        FunctionDelegate f = functionDelegates[(int)function];
        for (int i = 0; i < resolution; i++) {
            Vector3 p = points[i].position;
            p.y = f(p.x);
            points[i].position = p;
            Color c = points[i].startColor;
            c.g = p.y;
            points[i].startColor = c;
        }
        p.SetParticles(points, points.Length);
        */
    }
    
    public void CreatePoints() {

        for (int rendIndex = 0; rendIndex < r.Length; rendIndex++) {

            List<string> objects = new List<string>(DATA.sessions[0].lookData.dictionary.Keys);

            List<LookData> data = new List<LookData>();
            Debug.Log(objects.Count);
            if (objects.Count > 0) {
                string tempObj = objects[0];
                Debug.Log(tempObj);

                foreach (Session s in DATA.sessions) {
                    if (s.lookData.dictionary.ContainsKey(tempObj)) {
                        data.Add(s.lookData.dictionary[tempObj]);
                    }
                }
                Debug.Log(data.Count);

                float[] x = new float[data.Count];
                float[] y = new float[data.Count];

                for (int i = 0; i < data.Count; i++) {
                    x[i] = i;

                    if (rendIndex == 0) {
                        y[i] = data[i].averageTime;
                        Debug.Log(i + " -- " + r[rendIndex].name);
                    } else if (rendIndex == 1) {
                        y[i] = data[i].TotalTime;
                        Debug.Log(i + " -- " + r[rendIndex].name);
                    } else if (rendIndex == 2) {
                        y[i] = data[i].lookedAt;
                        Debug.Log(i + " -- " + r[rendIndex].name);
                    }
                }

                Graph(r[rendIndex], "Session", x, "Value", y, rendIndex);
            }
        }
    }

    public void CreateObjectPoints(string objectName) {
        for (int rendIndex = 0; rendIndex < r.Length; rendIndex++) {

            List<LookData> data = new List<LookData>();
            

            foreach (Session s in DATA.sessions) {
                if (s.lookData.dictionary.ContainsKey(objectName)) {
                    data.Add(s.lookData.dictionary[objectName]);
                }
            }

            float[] x = new float[data.Count];
            float[] y = new float[data.Count];

            Debug.Log(rendIndex + " -- " + r[rendIndex].name);
            for (int i = 0; i < data.Count; i++) {
                x[i] = i;

                if (rendIndex == 0) {
                    y[i] = data[i].averageTime;
                } else if (rendIndex == 1) {
                    y[i] = data[i].TotalTime;
                } else if (rendIndex == 2) {
                    y[i] = data[i].lookedAt;
                }
            }

            Graph(r[rendIndex], "Session", x, "", y, rendIndex);
        }
    }

    public void CreateKeyCodePoints(KeyCode keycode) {
        for (int rendIndex = 0; rendIndex < r.Length; rendIndex++) {

            List<Keydata> data = new List<Keydata>();


            foreach (Session s in DATA.sessions) {
                if (s.keyData.dictionary.ContainsKey(keycode)) {
                    data.Add(s.keyData.dictionary[keycode]);
                }
            }

            float[] x = new float[data.Count];
            float[] y = new float[data.Count];

            Debug.Log(rendIndex + " -- " + r[rendIndex].name);
            for (int i = 0; i < data.Count; i++) {
                x[i] = i;

                if (rendIndex == 0) {
                    y[i] = data[i].longestHold;
                } else if (rendIndex == 1) {
                    y[i] = data[i].count;
                }
            }

            Graph(r[rendIndex], "Session", x, "", y, rendIndex);
        }
    }

    private static float Linear(float x) {
        return x;
    }

    private static float Exponential(float x) {
        return x * x;
    }

    private static float Parabola(float x) {
        x = 2f * x - 1f;
        return x * x;
    }

    private static float Sine(float x) {
        return 0.5f + 0.5f * Mathf.Sin(2 * Mathf.PI * x + Time.timeSinceLevelLoad);
    }

    public void Graph(LineRenderer line, string xLabel, float[] x, string yLabel, float[] y, float z) {

        if (x.Length != y.Length) {
            Debug.Log("x and y lists should be the same size!");
            return;
        }

        resolution = x.Length;
        float minX = Min(x);
        float maxX = Max(x);
        float minY = Min(y);
        float maxY = Max(y);

        points.Clear();
        for (int i = 0; i < resolution; i++) {
            Vector3 newPoint = new Vector3();
            
            newPoint.x = Mathf.InverseLerp(minX, maxX, x[i]) * wideMult - xShift;
            newPoint.y = Mathf.InverseLerp(minY, maxY, y[i]);
            newPoint.z = z;
            //Debug.Log(newPoint);

            points.Add(newPoint);
        }
        line.positionCount = points.Count;
        line.SetPositions(points.ToArray());

        xAxis.text = xLabel;
        yAxis.text = yLabel;
        yMin.text = minY.ToString();
        yMax.text = maxY.ToString();

    }

    float Max(float[] list) {

        float max = float.MinValue;
        for (int i = 0; i < list.Length; i++) {
            if (list[i] > max) {
                max = list[i];
            }
        }
        return max;
    }

    float Min(float[] list) {

        float min = float.MaxValue;
        for (int i = 0; i < list.Length; i++) {
            if (list[i] < min) {
                min = list[i];
            }
        }
        return min;
    }
}
