using UnityEngine;

namespace HECSFramework.Core
{
    public partial struct Vector2Serialize
    {
        public static implicit operator Vector2Serialize(Vector2 data) { return new Vector2Serialize(data); }
        public static implicit operator Vector2(Vector2Serialize data) { return data.AsVector(); }

        public Vector2Serialize(Vector2 source)
        {
            X = source.x;
            Y = source.y;
        }
        
        public Vector2 AsVector()
            => new Vector2(X, Y);
    }
}