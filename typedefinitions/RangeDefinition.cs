using System;
using System.IO;
using Kaitai;

using RCP.Protocol;

namespace RCP.Parameter
{
    public class RangeDefinition<T> : DefaultDefinition<Range<T>>, IRangeDefinition where T : struct
    {
        public readonly NumberDefinition<T> ElementType;

        public RangeDefinition(NumberDefinition<T> elementType)
        : base(RcpTypes.Datatype.Range)
        {
            FDefault = new Range<T>(elementType.Default, elementType.Default);
            ElementType = elementType;
        }

        public override Parameter CreateParameter(short id, IParameterManager manager) => new RangeParameter<T>(id, manager, this);

        public override TypeDefinition CreateRange()
        {
            throw new NotSupportedException();
        }

        public override void ParseOptions(KaitaiStream input)
        {
            ElementType.ParseOptions(input);
            base.ParseOptions(input);
        }

        protected override bool HandleOption(KaitaiStream input, byte code)
        {
            var result = base.HandleOption(input, code);
            if (result)
                return true;

            var option = (RcpTypes.RangeOptions)code;

            switch (option)
            {
                case RcpTypes.RangeOptions.Default:
                    Default = ReadValue(input);
                    return true;
                default:
                    break;
            }

            return false;
        }

        public override void ResetForInitialize()
        {
            base.ResetForInitialize();

            DefaultChanged = Default != new Range<T>(ElementType.Default, ElementType.Default);
        }

        public override Range<T> ReadValue(KaitaiStream input)
        {
            return new Range<T>(ElementType.ReadValue(input), ElementType.ReadValue(input));
        }

        public override void WriteValue(BinaryWriter writer, Range<T> value)
        {
            ElementType.WriteValue(writer, value.Lower);
            ElementType.WriteValue(writer, value.Upper);
        }

        INumberDefinition IRangeDefinition.ElementType => ElementType;
    }
}
