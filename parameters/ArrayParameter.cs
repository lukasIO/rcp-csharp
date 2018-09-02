using System;
using RCP.Types;

namespace RCP.Parameters
{
    public sealed class ArrayParameter<T> : ValueParameter<T[]>
    {
        public new ArrayDefinition<T> Type => base.Type as ArrayDefinition<T>;

        public ArrayParameter(Int16 id, IParameterManager manager, ArrayDefinition<T> typeDefinition)
            : base(id, manager, typeDefinition)
        {
        }
    }
}
