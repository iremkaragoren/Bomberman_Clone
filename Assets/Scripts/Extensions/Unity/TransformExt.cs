using UnityEngine;

namespace Extensions.Unity
{
    public static class TransformExt
    {
        public static void SetX(this Transform transform, float x)
        {
            Vector3 pos = transform.position;
            pos.x = x;
            transform.position = pos;
        }

        public static void SetPosAxis(this Transform transform, int axisIndex, float val)
        {
            Vector3 pos = transform.position;
            pos[axisIndex] = val;
            transform.position = pos;
        }
    }
}