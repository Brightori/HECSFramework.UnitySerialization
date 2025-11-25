using MessagePack;
using UnityEngine;

namespace HECSFramework.Core
{
    public partial struct Vector3Serialize
    {
        public static implicit operator Vector3Serialize (Vector3 data) { return new Vector3Serialize (data); }
        public static implicit operator Vector3 (Vector3Serialize data) { return data.AsVector; }

        public Vector3Serialize(Vector3 source)
        {
            X = source.x;
            Y = source.y;
            Z = source.z;
        }
        
        [IgnoreMember]
#if JsonSerialize
        [Newtonsoft.Json.JsonIgnore]
#endif
        public Vector3 AsVector
            => new Vector3(X, Y, Z);

        public void SetVector3Serialize(Vector3 source)
        {
            X = source.x;
            Y = source.y;
            Z = source.z;
        }
    }
}