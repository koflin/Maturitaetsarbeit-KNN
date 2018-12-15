using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GraphicManager : MonoBehaviour {

    [Header("Prefabs")]
    public GameObject xMarkPrefab;
    public GameObject yMarkPrefab;
    public GameObject pointPrefab;

    [Header("Limits")]
    public double maxX;
    public double maxY;
    public double xScale;
    public double yScale;
    public double xSteps;
    public double ySteps;

    [Header("Objects")]
    public GameObject xAxis;
    public GameObject yAxis;
    public List<GameObject> xMarks;
    public List<GameObject> yMarks;
    public GameObject background;
    public List<GameObject> points = new List<GameObject>();

    public string xLabel;
    public string yLabel;
    public IDictionary<double, double> data;
    public int currentPoint;

    public void Start()
    {
    }

    public void ShowData(string xLabel, string yLabel, IDictionary<double, double> data)
    {
        this.xLabel = xLabel;
        this.yLabel = yLabel;
        this.data = data;

        currentPoint = -1;

        maxX = 0;
        maxY = 0;

        foreach (KeyValuePair<double, double> point in data)
        {
            Debug.Log(point.Key + " | " + point.Value);
            if (point.Key >= maxX)
            {
                maxX = point.Key;
            }

            if (point.Value >= maxY)
            {
                maxY = point.Value;
            }
        }

        ClearAxisAndPoints();

        GenerateAxisMarks();

        GeneratePoints();
    }

    public void PointClicked(int index)
    {
        if (currentPoint != -1)
        {
            points[currentPoint].transform.GetChild(0).gameObject.SetActive(false);
        }

        GameObject point = points[index];
        point.transform.GetChild(0).gameObject.SetActive(true);

        currentPoint = index;
    }

    private void GenerateAxisMarks()
    {
        xAxis.transform.GetChild(0).GetComponent<Text>().text = xLabel;
        yAxis.transform.GetChild(0).GetComponent<Text>().text = yLabel;

        xMarks = new List<GameObject>();
        yMarks = new List<GameObject>();

        xScale = xAxis.GetComponent<RectTransform>().rect.width / (Mathf.CeilToInt((float)maxX) + 1d);
        yScale = yAxis.GetComponent<RectTransform>().rect.height / ((Mathf.CeilToInt((float)maxY) / 10d) + 1d);

        int xSteps = 1;
        while (xScale < (double)xAxis.GetComponent<RectTransform>().rect.width / 23)
        {
            xScale /= xSteps;

            xSteps += 1;

            xScale *= xSteps;
        }

        this.xSteps = xSteps;

        int ySteps = 10;
        while (yScale < (double)yAxis.GetComponent<RectTransform>().rect.height / 11)
        {
            yScale /= ySteps;

            ySteps += 10;

            yScale *= ySteps;
        }
        this.ySteps = ySteps;

        for (int x = 0; x < Mathf.FloorToInt((float)(xAxis.GetComponent<RectTransform>().rect.width / xScale)); x++)
        {
            xMarks.Add(Instantiate(xMarkPrefab, xAxis.transform));
            xMarks[x].transform.localPosition = new Vector3((float)((x + 1) * xScale), 0, 0);
            xMarks[x].transform.GetChild(0).GetComponent<Text>().text = ((x + 1) * xSteps).ToString();
        }

        for (int y = 0; y < Mathf.FloorToInt((float)(yAxis.GetComponent<RectTransform>().rect.height / yScale)); y++)
        {
            yMarks.Add(Instantiate(yMarkPrefab, yAxis.transform));
            yMarks[y].transform.localPosition = new Vector3(0, (float)((y + 1) * yScale), 0);
            yMarks[y].transform.GetChild(0).GetComponent<Text>().text = ((y + 1) * ySteps).ToString();
        }
    }

    private void GeneratePoints()
    {
        int index = 0;
        foreach (KeyValuePair<double, double> point in data)
        {
            GameObject pointObject = Instantiate(pointPrefab, transform);

            RectTransform pointRect = pointObject.GetComponent<RectTransform>();
            pointRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, (float)(xScale / xSteps * point.Key - pointRect.rect.width / 2),pointRect.rect.width);
            pointRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, (float)(yScale / ySteps * point.Value - pointRect.rect.height / 2), pointRect.rect.height);

            Debug.Log("Scale: " + yScale + " Steps: " + ySteps);

            int indexCopy = index;
            pointObject.GetComponent<Button>().onClick.AddListener(() => PointClicked(indexCopy));
            pointObject.transform.GetChild(0).GetComponent<Text>().text = point.Key + "/" + point.Value;

            points.Add(pointObject);
            index++;
        }
    }

    private void ClearAxisAndPoints()
    {
        foreach (GameObject xMark in xMarks)
        {
            Destroy(xMark);
        }
        xMarks.Clear();

        foreach (GameObject yMark in yMarks)
        {
            Destroy(yMark);
        }
        yMarks.Clear();

        foreach (GameObject pointObject in points)
        {
            Destroy(pointObject);
        }
        points.Clear();
    }
}
