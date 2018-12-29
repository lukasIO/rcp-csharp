using System.IO;
using UnityEngine;

using Kaitai;
using RCP.IO;

namespace RCP.Types
{
    public class Vector4f32Definition : NumberDefinition<Vector4>
    {
        protected override Vector4 DefaultMinimum => new Vector4(float.MinValue,float.MinValue,float.MinValue,float.MinValue);
        protected override Vector4 DefaultMaximum => new Vector4(float.MaxValue,float.MaxValue,float.MaxValue,float.MaxValue);
        protected override Vector4 DefaultMulitpleOf => new Vector4(0.01f,0.01f,0.01f,0.01f);

        public override Vector4 ReadValue(KaitaiStream input)
        {
            return new Vector4(input.ReadF4be(), input.ReadF4be(), input.ReadF4be(), input.ReadF4be());
        }

        public override void WriteValue(BinaryWriter writer, Vector4 value)
        {
            writer.Write(value.x, ByteOrder.BigEndian);
            writer.Write(value.y, ByteOrder.BigEndian);
            writer.Write(value.z, ByteOrder.BigEndian);
            writer.Write(value.w, ByteOrder.BigEndian);
        }
    }
}