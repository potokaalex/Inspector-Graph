using UnityEngine;
using System;

namespace InspectorGraph
{
    [Serializable]
    public class GraphDisplayData
    {
        [Range(1, 500)] public int GraphHeight = 100;
        [Range(1, 500)] public int GraphWidth = 270;

        [Range(1, 100)] public int DivisionsOfY = 5;
        [Range(1, 100)] public int DivisionsOfX = 10;

        [Tooltip("Number of chart points")][Range(1, 500)] public int GraphAccuracy = 100;

        public Color Axis = Color.white;
        public Color MarkupLines = Color.gray;
        public Color MarkupNumbers = Color.gray;
        public Color Graph = Color.white;

        public Vector2 DivisionValue 
            => new(GraphWidth / (float)DivisionsOfX, GraphHeight / (float)DivisionsOfY);
    }
}
