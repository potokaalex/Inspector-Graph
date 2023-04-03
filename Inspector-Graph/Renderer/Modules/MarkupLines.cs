using UnityEngine;
using System.Linq;
using System;

namespace InspectorGraph.Renderer
{
    public class MarkupLines
    {
        private Vector3[] _lines;
        private Rect _position;
        private Vector2 _divisionValue;
        private int _divisionsOfX;
        private int _divisionsOfY;

        public Vector3[] GetMarkupLines(Rect position, int divisionsOfX, int divisionsOfY, Vector2 divisionValue)
        {
            if (_position == position &&
                _divisionsOfX == divisionsOfX &&
                _divisionsOfY == divisionsOfY &&
                _divisionValue == divisionValue)
                return _lines;

            if (divisionsOfX < 0 || divisionsOfY < 0)
                return Array.Empty<Vector3>();

            _position = position;
            _divisionsOfX = divisionsOfX;
            _divisionsOfY = divisionsOfY;
            _divisionValue = divisionValue;

            var displacementForY = Vector2.right * position.width;
            var stepForY = Vector2.down * divisionValue.y;

            var displacementForX = Vector2.down * position.height;
            var stepForX = Vector2.right * divisionValue.x;

            return _lines = Markup(divisionsOfY, position, stepForY, displacementForY)
                .Concat(Markup(divisionsOfX, position, stepForX, displacementForX)).ToArray();
        }

        private Vector3[] Markup(int divisionsNumber, Rect position, Vector2 step, Vector2 displacement)
        {
            var lines = new Vector3[divisionsNumber * 2];
            var currentLineIndex = 0;

            for (var _currentDivision = 1; _currentDivision <= divisionsNumber; _currentDivision++, currentLineIndex += 2)
            {
                var startingPosition = position.position + Vector2.up * position.height + step * _currentDivision;
                var endPosition = startingPosition + displacement;

                lines[currentLineIndex] = startingPosition;
                lines[currentLineIndex + 1] = endPosition;
            }

            return lines;
        }
    }
}
