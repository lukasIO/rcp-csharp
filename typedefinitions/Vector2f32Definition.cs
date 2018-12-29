using System.IO;
using UnityEngine;

using Kaitai;
using RCP.IO;

namespace RCP.Types
{
    public class Vector2f32Definition : NumberDefinition<Vector2>
    {
        protected override Vector2 DefaultMinimum => new Vector2(float.MinValue, float.MinValue);
        protected override Vector2 DefaultMaximum => new Vector2(float.MaxValue, float.MaxValue);
        protected override Vector2 DefaultMulitpleOf => new Vector2(0.01f,0.01f);

        public override Vector2 ReadValue(KaitaiStream input)
        {
            return new Vector2(input.ReadF4be(), input.ReadF4be());
        }

        public override void WriteValue(BinaryWriter writer, Vector2 value)
        {
            writer.Write(value.x, ByteOrder.BigEndian);
            writer.Write(value.y, ByteOrder.BigEndian);
        }
    }
}