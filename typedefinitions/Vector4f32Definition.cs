using Kaitai;
using System.IO;

using RCP.Protocol;
using System.Numerics;

namespace RCP.Parameter
{
    public class Vector4f32Definition : NumberDefinition<Vector4>
    {
        public Vector4f32Definition()
        : base(RcpTypes.Datatype.Vector4f32)
        {
            FMinimum = new Vector4(float.MinValue, float.MinValue, float.MinValue, float.MinValue);
            FMaximum = new Vector4(float.MaxValue, float.MaxValue, float.MaxValue, float.MaxValue);
            FMultipleOf = new Vector4(0.01f, 0.01f, 0.01f, 0.01f);
        }

        public override void ResetForInitialize()
        {
            base.ResetForInitialize();

            DefaultChanged = Default != new Vector4(0, 0, 0, 0);

            MinimumChanged = Minimum != new Vector4(float.MinValue, float.MinValue, float.MinValue, float.MinValue);
            MaximumChanged = Maximum != new Vector4(float.MaxValue, float.MaxValue, float.MaxValue, float.MaxValue);
            MultipleOfChanged = MultipleOf != new Vector4(0.01f, 0.01f, 0.01f, 0.01f);
        }

        public override Vector4 ReadValue(KaitaiStream input)
        {
            return new Vector4(input.ReadF4be(), input.ReadF4be(), input.ReadF4be(), input.ReadF4be());
        }

        public override void WriteValue(BinaryWriter writer, Vector4 value)
        {
            writer.Write(value.X, ByteOrder.BigEndian);
            writer.Write(value.Y, ByteOrder.BigEndian);
            writer.Write(value.Z, ByteOrder.BigEndian);
            writer.Write(value.W, ByteOrder.BigEndian);
        }
    }
}