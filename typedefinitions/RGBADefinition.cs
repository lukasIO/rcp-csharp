using System;
using System.IO;
using Kaitai;

using RCP.Protocol;
using UnityEngine;
using RCP.Parameters;

namespace RCP.Types
{                           
    public class RGBADefinition : DefaultDefinition<Color>, IRGBADefinition
    {
        public RGBADefinition()
        : base(RcpTypes.Datatype.Rgba, Color.black)
        {
        }

        public override Parameter CreateParameter(short id, IParameterManager manager) => new ValueParameter<Color>(id, manager, this);

        public override Color ReadValue(KaitaiStream input)
        {
            var a = input.ReadU1();
            var b = input.ReadU1();
            var g = input.ReadU1();
            var r = input.ReadU1();
            return new Color(r, g, b, a);
        }

        public override void WriteValue(BinaryWriter writer, Color value)
        {
            writer.Write((byte)value.a);
            writer.Write((byte)value.b);
            writer.Write((byte)value.g);
            writer.Write((byte)value.r);
        }
    }
}
