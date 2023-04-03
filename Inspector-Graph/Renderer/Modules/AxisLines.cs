using UnityEngine;

namespace InspectorGraph.Renderer
{
    public class AxisLines
    {
        private Vector3[] _lines;
        private Rect _rectangle;

        public Vector3[] GetAxisLines(Rect rectangle)
        {
            if (_rectangle == rectangle)
                return _lines;

            _rectangle = rectangle;

            var endPositionForY = rectangle.position + Vector2.up * rectangle.height;
            var endPositionForX = endPositionForY + Vector2.right * rectangle.width;

            return _lines = new Vector3[]
            {
                rectangle.position, endPositionForY,
                endPositionForY,endPositionForX
            };
        }
    }
}
