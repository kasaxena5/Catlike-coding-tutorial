using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surface : MonoBehaviour
{
    [SerializeField] private Transform pointPrefab;
    [SerializeField, Range(10, 100)] private int resolution = 10;
    [SerializeField] private FunctionLibrary.SurfaceFunctionName function;

    private Transform[,] points;


    private void Awake()
    {
        points = new Transform[resolution, resolution];
        float step = 2f / resolution;
        for (int i = 0; i < resolution; i++)
        {
            for (int j = 0; j < resolution; j++)
            {
                Transform point = Instantiate(pointPrefab);
                point.localScale *= step;
                point.SetParent(transform, false);
                points[i, j] = point;
            }
        }
    }

    void Start()
    {

    }

    void Update()
    {
        FunctionLibrary.SurfaceFunction f = FunctionLibrary.GetSurfaceFunction(function);
        float step = 2f / resolution;
        for (int i = 0; i < resolution; i++)
        {
            float u = (i + 0.5f) * step - 1f;
            for (int j = 0; j < resolution; j++)
            {
                Transform point = points[i, j];
                float v = (j + 0.5f) * step - 1f;
                point.localPosition = f(u, v, Time.time);
            }
        }
    }
}
