using UnityEngine;

namespace Extensions.Unity
{
    public static class GameObjectExt
    {
        public static void Destroy(this GameObject gameObject)
        {
            Object.Destroy(gameObject);
        }
    }
}