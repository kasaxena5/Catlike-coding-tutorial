using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private Transform pointPrefab;
    [SerializeField, Range(10, 100)] private int resolution = 10;
    [SerializeField] private FunctionLibrary.Function3DName function;

    private Transform[,] points;


    private void Awake()
    {
        points = new Transform[resolution, resolution];
        Vector3 position;
        float step = 2f / resolution;
        for (int i = 0; i < resolution; i++)
        {
            for (int j = 0; j < resolution; j++)
            {
                Transform point = Instantiate(pointPrefab);
                position.x = (i + 0.5f) * step - 1f;
                position.z = (j + 0.5f) * step - 1f;
                position.y = 0;
                point.localPosition = position;
                point.localScale *= step;
                point.SetParent(transform, false);
                points[i,j] = point;
            }
        }
    }

    void Start()
    {

    }

    void Update()
    {
        FunctionLibrary.Function3D f = FunctionLibrary.GetFunction3D(function);
        for (int i = 0; i < resolution; i++)
        {
            for (int j = 0; j < resolution; j++)
            {
                Transform point = points[i, j];
                Vector3 position = point.localPosition;
                position.y = f(position.x, position.z, Time.time);
                point.localPosition = position;
            }
        }
    }
}
