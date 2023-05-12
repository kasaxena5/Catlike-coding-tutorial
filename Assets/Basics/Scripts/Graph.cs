using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    [SerializeField] private Transform pointPrefab;
    [SerializeField, Range(10, 100)] private int resolution = 10;

    private Transform[] points;


    private void Awake()
    {
        points = new Transform[resolution];
        Vector2 position;
        float step = 2f / resolution;
        for (int i = 0; i < resolution; i++)
        {
            Transform point = Instantiate(pointPrefab);
            position.x = (i + 0.5f) * step - 1f;
            position.y = 0;
            point.localPosition = position;
            point.localScale *= step;
            point.SetParent(transform, false);
            points[i] = point;
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        for(int i = 0; i < points.Length; i++)
        {
            Transform point = points[i];
            Vector3 position = point.localPosition;
            position.y = Mathf.Sin(Mathf.PI * (position.x + Time.time));
            point.localPosition = position;
        }
    }
}
