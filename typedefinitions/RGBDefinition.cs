using System;
using System.IO;
using Kaitai;

using RCP.Protocol;
using UnityEngine;
using RCP.Parameters;

namespace RCP.Types
{                           
    public class RGBDefinition : DefaultDefinition<Color>
    {
        public RGBDefinition()
            : base(RcpTypes.Datatype.Rgb, Color.black)
        {
        }

        public override Parameter CreateParameter(short id, IParameterManager manager) => new ValueParameter<Color>(id, manager, this);

        public override Color ReadValue(KaitaiStream input)
        {
            var b = input.ReadU1();
            var g = input.ReadU1();
            var r = input.ReadU1();
            var a = input.ReadU1(); // Server send alpha?
            return new Color(r,g,b,255);
        }

        public override void WriteValue(BinaryWriter writer, Color value)
        {
            writer.Write((byte)value.b);
            writer.Write((byte)value.r);
            writer.Write((byte)value.g);
            writer.Write((byte)255);
        }
    }
}
