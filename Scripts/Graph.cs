﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour {

    public List<Vector3> LinePoints = new List<Vector3>();
    public Color lineColor;
    public float lineWidth;
    public Texture dotTexture;

    private LineRenderer line;

	// Use this for initialization
	void Awake () {
        this.gameObject.layer = 8;
        this.gameObject.AddComponent<LineRenderer> ();
        line = this.gameObject.GetComponent<LineRenderer>();
	}

    void Start ()
    {
        DrawLine();
    }

    void DrawLine()
    {
        line.numPositions = LinePoints.Count;
        line.widthMultiplier = lineWidth;
        Material mat = new Material(Shader.Find("VertexLit"));
        mat.color = lineColor;
        line.material = mat;
        GraphController gControl = GameObject.Find("GraphController").GetComponent<GraphController>();
        float cameraSize = gControl.renderCamera.orthographicSize;

        for (int i = 0; i < LinePoints.Count; i++)
        {
            if (LinePoints[i].y > 0)
            {
                LinePoints[i] = new Vector3(LinePoints[i].x, (LinePoints[i].y / 100) * cameraSize, LinePoints[i].z);
            }
        }

        line.SetPositions(LinePoints.ToArray());

        if (dotTexture)
        {
            DrawDots(lineWidth);
        }
    }

    void DrawDots(float scaleMultiplier)
    {
        foreach (var item in LinePoints)
        {
            GameObject linePointGo = GameObject.CreatePrimitive(PrimitiveType.Quad);
            Material pointMat = new Material(Shader.Find("Sprites/Default"));
            pointMat.mainTexture = dotTexture;
            pointMat.color = lineColor;
            linePointGo.GetComponent<MeshRenderer>().material = pointMat;
            linePointGo.transform.position = item + new Vector3(0, 0, -0.25f);
            linePointGo.layer = 8;
            linePointGo.name = "Dot" + item.ToString();
            linePointGo.transform.localScale = new Vector3(7 * scaleMultiplier, 7 * scaleMultiplier, 7 * scaleMultiplier);
            linePointGo.transform.SetParent(this.transform);
        }
    }
}
