using InspectorGraph;
using UnityEngine;
using System;

public class InspectorGrapDemo : MonoBehaviour, IFunction
{
    [SerializeField] private FunctionTypes _type;
    [SerializeField] private float _heightShift;
    [SerializeField] private float _widthShift;

    [SerializeField] private Graph _inspectorGraph;

    private enum FunctionTypes
    {
        Sine,
        Cosine,
        Tangent,
        Cotangent,
        StraightLine,
    }

    public float GetFunctionValue(float argument)
        => _type switch
        {
            FunctionTypes.Sine => (float)Math.Sin(argument + _widthShift) + _heightShift,
            FunctionTypes.Cosine => (float)Math.Cos(argument + _widthShift) + _heightShift,
            FunctionTypes.Tangent => (float)Math.Tan(argument + _widthShift) + _heightShift,
            FunctionTypes.Cotangent => 1 / (float)Math.Tan(argument + _widthShift) + _heightShift,
            _ => _heightShift,
        };
}
