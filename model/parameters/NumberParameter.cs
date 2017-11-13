using Kaitai;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RCP.Model
{
    public class NumberParameter<T> : ValueParameter<T> where T : struct
    {
        public new Nullable<T> Value { get; set; }
        public new INumberDefinition<T> TypeDefinition
        {
            get { return base.TypeDefinition as INumberDefinition<T>;}
        }

        public NumberParameter(uint id, INumberDefinition<T> definition): 
            base (id, definition)
        { }

        protected override bool HandleOption(KaitaiStream input, RcpTypes.ParameterOptions option)
        {
            switch (option)
            {
                case RcpTypes.ParameterOptions.Value:
                    Value = TypeDefinition.ReadValue(input);
                    return true;
            }

            return false;
        }

        //since we're reintroducing the value as nullable we also need to override WriteValue
        protected override void WriteValue(BinaryWriter writer)
        {
            if (Value != null)
            {
                writer.Write((byte)RcpTypes.ParameterOptions.Value);
                TypeDefinition.WriteValue(writer, (T)Value);
            }
        }
    }
}
