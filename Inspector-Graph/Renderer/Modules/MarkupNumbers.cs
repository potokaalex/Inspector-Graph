using System.Collections.Generic;
using UnityEngine;
using System;

namespace InspectorGraph.Renderer
{
    public class MarkupNumbers
    {
        private (Vector3, string)[] _numbers;
        private Rect _position;
        private Vector2 _divisionValue;
        private int _divisionsOfX;
        private int _divisionsOfY;

        public (Vector3, string)[] GetMarkupNumbers(Rect position, int divisionsOfX, int divisionsOfY, Vector2 divisionValue)
        {
            if (_position == position &&
                _divisionsOfX == divisionsOfX &&
                _divisionsOfY == divisionsOfY &&
                _divisionValue == divisionValue)
                return this._numbers;

            if (divisionsOfX < 0 || divisionsOfY < 0)
                return Array.Empty<(Vector3, string)>();

            _position = position;
            _divisionsOfX = divisionsOfX;
            _divisionsOfY = divisionsOfY;
            _divisionValue = divisionValue;

            var numbers = new List<(Vector3, string)>(divisionsOfX + divisionsOfY + 1);

            var initialOffset = new Vector2(-18f, 5f);

            var markupStepForY = new Vector2(0f, -divisionValue.y);
            var markupOffsetForY = new Vector2(initialOffset.x, 0f);

            var markupStepForX = new Vector2(divisionValue.x, 0f);
            var markupOffsetForX = new Vector2(0f, initialOffset.y);

            Markup(numbers, divisionsOfY, markupStepForY, markupOffsetForY);
            numbers.Add((position.position + new Vector2(0f, position.height) + initialOffset / 2f, "0"));
            Markup(numbers,divisionsOfX, markupStepForX, markupOffsetForX);

            return _numbers = numbers.ToArray();
        }

        private void Markup(List<(Vector3, string)> numbers, int divisionsNumber, Vector2 step, Vector2 offset)
        {
            for (var currentDivision = 1f; currentDivision <= divisionsNumber; currentDivision++)
            {
                var numberPosition = _position.position + new Vector2(0f, _position.height) + offset + step * currentDivision;

                numbers.Add((numberPosition, currentDivision.ToString()));
            }
        }
    }
}
