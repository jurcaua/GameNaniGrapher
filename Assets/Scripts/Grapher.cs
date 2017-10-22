using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grapher : MonoBehaviour {
    
    public int resolution = 10;
    public Text xAxis;
    public Text yAxis;

    private int currentResolution;
    private List<Vector3> points = new List<Vector3>();
    private LineRenderer r;

    void Start() {

        r = GetComponent<LineRenderer>();

        //CreatePoints();

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
                y[i] = data[i].averageTime;
                x[i] = i;
            }

            Graph("Session", x, "Average Time", y);
        }



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
    /*
    private void CreatePoints() {
        if (resolution < 10 || resolution > 100) {
            Debug.LogWarning("Grapher resolution out of bounds, resetting to minimum.", this);
            resolution = 10;
        }
        currentResolution = resolution;
        points = new ParticleSystem.Particle[resolution];
        float increment = 1f / (resolution - 1);
        for (int i = 0; i < resolution; i++) {
            float x = i * increment;
            points[i].position = new Vector3(x, 0f, 0f);
            points[i].startColor = new Color(x, 0f, 0f);
            points[i].startSize = 0.1f;
        }
    }*/

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

    public void Graph(string xLabel, float[] x, string yLabel, float[] y) {

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
            
            newPoint.x = Mathf.InverseLerp(minX, maxX, x[i]);
            newPoint.y = Mathf.InverseLerp(minY, maxY, y[i]);
            Debug.Log(newPoint);

            points.Add(newPoint);
        }
        r.positionCount = points.Count;
        r.SetPositions(points.ToArray());

        xAxis.text = xLabel;
        yAxis.text = yLabel;
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
