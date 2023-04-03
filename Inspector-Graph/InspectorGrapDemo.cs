using InspectorGraph;
using UnityEngine;
using System;

public class InspectorGrapDemo : MonoBehaviour, IFunction
{
    [SerializeField] private FunctionTypes type;
    [SerializeField] private float heightShift;
    [SerializeField] private float widthShift;

    [SerializeField] private Graph inspectorGraph;

    private enum FunctionTypes
    {
        Sine,
        Cosine,
        Tangent,
        Cotangent,
        StraightLine,
    }

    public float GetFunctionValue(float argument)
        => type switch
        {
            FunctionTypes.Sine => (float)Math.Sin(argument + widthShift) + heightShift,
            FunctionTypes.Cosine => (float)Math.Cos(argument + widthShift) + heightShift,
            FunctionTypes.Tangent => (float)Math.Tan(argument + widthShift) + heightShift,
            FunctionTypes.Cotangent => 1 / (float)Math.Tan(argument + widthShift) + heightShift,
            _ => heightShift,
        };
}