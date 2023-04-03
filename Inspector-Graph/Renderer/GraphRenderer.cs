using UnityEditor;
using UnityEngine;
using System;

#if UNITY_EDITOR
namespace InspectorGraph.Renderer
{
    [CustomPropertyDrawer(typeof(Graph))]
    public class GraphRenderer : PropertyDrawer
    {
        private readonly AxisLines _axisLines = new();
        private readonly MarkupLines _markupLines = new();
        private readonly MarkupNumbers _markupNumbers = new();
        private readonly GraphLines _graphLines = new();

        private Graph _graph;
        private IFunction _function;
        private float _additionalHeight;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            InitializeFields(property);

            return EditorGUI.GetPropertyHeight(property, true) + _additionalHeight + 6f;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            _additionalHeight = 0f;

            DrawProperty(GetHeaderRectangle(position.position), property, label, false);

            if (!property.isExpanded)
                return;

            _additionalHeight = _graph.DisplayData.GraphHeight + 20f;

            var displayData = _graph.DisplayData;

            var graphRectangle = new Rect(position.position + new Vector2(10f, 25f),
                new Vector2Int(_graph.DisplayData.GraphWidth, _graph.DisplayData.GraphHeight));

            var displayDataRectangle = new Rect(position.position + new Vector2(10f, displayData.GraphHeight + 44f),
                new Vector2(GetPropertyWidth() - 10f, 18f));

            DrawAxis(graphRectangle, _axisLines, _graph.DisplayData.Axis);

            MarkupWithLines(graphRectangle, _markupLines, displayData.DivisionsOfX,
                displayData.DivisionsOfY, displayData.DivisionValue, displayData.MarkupLines);

            MarkupWithNumbers(graphRectangle, _markupNumbers, displayData.DivisionsOfX,
                displayData.DivisionsOfY, displayData.DivisionValue, displayData.MarkupNumbers);

            DrawProperty(displayDataRectangle, property.FindPropertyRelative("DisplayData"),
                new GUIContent("GraphDisplayData"), true);

            DrawGraph(graphRectangle, _function, displayData.DivisionsOfX, displayData.DivisionsOfY,
                displayData.GraphAccuracy, displayData.Graph);
        }

        private void InitializeFields(SerializedProperty property)
        {
            _graph ??= (Graph)fieldInfo.GetValue(property.serializedObject.targetObject);

            if (_function != null)
                return;

            if (property.serializedObject.targetObject is IFunction)
                _function = property.serializedObject.targetObject as IFunction;
            else
                throw new Exception(property.serializedObject.targetObject.GetType() + " script does not inherit from IFunction");
        }

        private float GetPropertyWidth()
            => Screen.width - 37f;

        private Rect GetHeaderRectangle(Vector2 position)
            => new(position, new Vector2(GetPropertyWidth(), 18f));

        private void DrawProperty(Rect position, SerializedProperty property, GUIContent label, bool isIncludeChildren = false)
            => EditorGUI.PropertyField(position, property, label, isIncludeChildren);

        private void DrawAxis(Rect position, AxisLines calculator, Color color)
        {
            var lines = calculator.GetAxisLines(position);
            var previousColor = Handles.color;

            Handles.color = color;

            for (var currentLineIndex = 0; currentLineIndex < lines.Length; currentLineIndex += 2)
                Handles.DrawLine(lines[currentLineIndex], lines[currentLineIndex + 1]);

            Handles.color = previousColor;
        }

        private void MarkupWithLines(Rect position, MarkupLines calculator, int divisionsOfX, int divisionsOfY, Vector2 divisionValue, Color color)
        {
            var lines = calculator.GetMarkupLines(position, divisionsOfX, divisionsOfY, divisionValue);
            var previousColor = Handles.color;

            Handles.color = color;

            for (var currentLineIndex = 0; currentLineIndex < lines.Length; currentLineIndex += 2)
                Handles.DrawLine(lines[currentLineIndex], lines[currentLineIndex + 1]);

            Handles.color = previousColor;
        }

        private void MarkupWithNumbers(Rect position, MarkupNumbers calculator, int divisionsOfX, int divisionsOfY, Vector2 divisionValue, Color color)
        {
            var numbers = calculator.GetMarkupNumbers(position, divisionsOfX, divisionsOfY, divisionValue);
            var numberStyle = new GUIStyle
            {
                normal =
                {
                    textColor = color
                }
            };

            foreach (var number in numbers)
                Handles.Label(number.Item1, number.Item2, numberStyle);
        }

        private void DrawGraph(Rect position, IFunction function, int divisionsOfX, int divisionsOfY, int graphAccuracy, Color color)
        {
            var lines = _graphLines.GetGraphLines(position, function, divisionsOfX, divisionsOfY, graphAccuracy);
            var previousColor = Handles.color;
            Handles.color = color;

            foreach (var line in lines)
                Handles.DrawAAPolyLine(line);

            Handles.color = previousColor;
        }
    }
}
#endif
