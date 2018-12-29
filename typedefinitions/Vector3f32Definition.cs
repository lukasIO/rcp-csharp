using System.IO;
using UnityEngine;

using Kaitai;
using RCP.IO;

namespace RCP.Types
{
    public class Vector3f32Definition : NumberDefinition<Vector3>
    {
        protected override Vector3 DefaultMinimum => new Vector3(float.MinValue,float.MinValue,float.MinValue);
        protected override Vector3 DefaultMaximum => new Vector3(float.MaxValue,float.MaxValue,float.MaxValue);
        protected override Vector3 DefaultMulitpleOf => new Vector3(0.01f,0.01f,0.01f);

        public override Vector3 ReadValue(KaitaiStream input)
        {
            return new Vector3(input.ReadF4be(), input.ReadF4be(), input.ReadF4be());
        }

        public override void WriteValue(BinaryWriter writer, Vector3 value)
        {
            writer.Write(value.x, ByteOrder.BigEndian);
            writer.Write(value.y, ByteOrder.BigEndian);
            writer.Write(value.z, ByteOrder.BigEndian);
        }
    }
}