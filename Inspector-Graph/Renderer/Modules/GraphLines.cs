using System.Collections.Generic;
using UnityEngine;

namespace InspectorGraph.Renderer
{
    public class GraphLines
    {
        public List<Vector3[]> GetGraphLines(Rect position, IFunction function, int divisionsOfX, int divisionsOfY, int accuracy)
        {
            if (accuracy < 1 ||
                divisionsOfX < 0 ||
                divisionsOfY < 0)
                return new List<Vector3[]>();

            var divisionValue = new Vector2(position.width / divisionsOfX, position.height / divisionsOfY);
            var startingPosition = position.position + new Vector2(0, position.height);
            var lines = new List<Vector3[]>(accuracy);
            var points = new List<Vector3>(accuracy);
            var step = 1f / accuracy;
            var upperBound = divisionsOfY;
            var lowerBound = 0;

            for (var _xPoint = 0f; _xPoint < divisionsOfX; _xPoint += step)
            {
                var _yPoint = function.GetFunctionValue(_xPoint);

                if (_yPoint > upperBound || _yPoint < lowerBound)
                {
                    if (points.Count > 0)
                    {
                        lines.Add(points.ToArray());
                        points.Clear();
                    }

                    continue;
                }

                points.Add(startingPosition + new Vector2(_xPoint, -_yPoint) * divisionValue);
            }

            lines.Add(points.ToArray());

            return lines;
        }

    }
}
