using UnityEngine;

namespace HECSFramework.Core
{
    public partial struct Vector2IntSerialize
    {
        public static implicit operator Vector2IntSerialize(Vector2Int data) { return new Vector2IntSerialize(data); }
        public static implicit operator Vector2Int(Vector2IntSerialize data) { return data.AsVector(); }

        public Vector2IntSerialize(Vector2Int source)
        {
            X = source.x;
            Y = source.y;
        }

        public Vector2Int AsVector()
            => new Vector2Int(X, Y);
    }
}