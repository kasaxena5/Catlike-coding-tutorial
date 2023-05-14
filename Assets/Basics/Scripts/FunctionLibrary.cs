using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;

public static class FunctionLibrary
{
    public delegate float Function(float x, float t);
    public delegate float Function3D(float x, float z, float t);
    public delegate Vector3 SurfaceFunction(float u, float v, float t);

    public enum FunctionName { Wave, MultiWave, Ripple }
    public enum Function3DName { Wave3D, MultiWave3D, Ripple3D }

    public enum SurfaceFunctionName { Wave, MultiWave, Ripple, Sphere, Torus }

    static Function[] functions = { Wave, MultiWave, Ripple };
    static Function3D[] function3Ds = { Wave3D, MultiWave3D, Ripple3D };
    static SurfaceFunction[] surfaceFunctions = { Wave, MultiWave, Ripple, Sphere, Torus };


    public static Function GetFunction(FunctionName name)
    {
        return functions[(int)name];
    }

    public static Function3D GetFunction3D(Function3DName name)
    {
        return function3Ds[(int)name];
    }

    public static SurfaceFunction GetSurfaceFunction(SurfaceFunctionName name)
    {
        return surfaceFunctions[(int)name];
    }


    // Sin(2PIx / T + kt)
    public static float Wave(float x, float t)
    {
        return Sin(PI * (x + t));
    }

    public static float Wave3D(float x, float z, float t)
    {
        return Sin(PI * (x + z + t));
    }

    public static Vector3 Wave(float u, float v, float t)
    {
        Vector3 p;
        p.x = u;
        p.y = Sin(PI * (u + v + t));
        p.z = v;
        return p;
    }

    public static float MultiWave(float x, float t)
    {
        float y = Sin(PI * (x + t)) + 0.5f * Sin(2 * PI * (x + t));
        return y / 1.5f;

    }

    public static float MultiWave3D(float x, float z, float t)
    {
        float y = Sin(PI * (x + 0.5f * t));
        y += 0.5f * Sin(2f * PI * (z + t));
        y += Sin(PI * (x + z + 0.25f * t));
        return y * (1f / 2.5f);
    }

    public static Vector3 MultiWave(float u, float v, float t)
    {
        Vector3 p;
        p.x = u;
        p.y = Sin(PI * (u + 0.5f * t));
        p.y += 0.5f * Sin(2f * PI * (v + t));
        p.y += Sin(PI * (u + v + 0.25f * t));
        p.y *= 1f / 2.5f;
        p.z = v;
        return p;
    }

    public static float Ripple(float x, float t)
    {
        float d = Abs(x);
        float y = Sin(PI * (4f * d - t));
        return y / (1f + 10f * d);
    }

    public static float Ripple3D(float x, float z, float t)
    {
        float d = Sqrt(x * x + z * z);
        float y = Sin(PI * (4f * d - t));
        return y / (1f + 10f * d);
    }

    public static Vector3 Ripple(float u, float v, float t)
    {
        float d = Sqrt(u * u + v * v);
        Vector3 p;
        p.x = u;
        p.y = Sin(PI * (4f * d - t));
        p.y /= 1f + 10f * d;
        p.z = v;
        return p;
    }

    public static Vector3 Sphere(float u, float v, float t)
    {
        Vector3 p;
        float r = 0.9f + 0.1f * Sin(PI * (6f * u + 4f * v + t));
        float s = r * Cos(PI * v * 0.5f);
        p.x = s * Sin(PI * u);
        p.y = r * Sin(PI * v * 0.5f);
        p.z = s * Cos(PI * u);
        return p;
    }


    public static Vector3 Torus(float u, float v, float t)
    {
        float r1 = 0.7f + 0.1f * Sin(PI * (6f * u + 0.5f * t));
        float r2 = 0.15f + 0.05f * Sin(PI * (8f * u + 4f * v + 2f * t));
        float s = r1 + r2 * Cos(PI * v);
        Vector3 p;
        p.x = s * Sin(PI * u);
        p.y = r2 * Sin(PI * v);
        p.z = s * Cos(PI * u);
        return p;
    }
}
