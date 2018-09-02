using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

using RCP.Protocol;

namespace RCP
{
    public interface IWriteable
    {
        void Write(BinaryWriter writer);
    }

    public interface ITypeDefinition : IWriteable
    {
        RcpTypes.Datatype Datatype { get; }
        Type ClrType { get; }
        void ParseOptions(Kaitai.KaitaiStream input);
        void ResetForInitialize();
    }

    public interface INumberDefinition : ITypeDefinition
    {
        object Minimum { get; }
        object Maximum { get; }
        object MultipleOf { get; }
    }

    public interface IDefaultDefinition<T> : ITypeDefinition
    {
        T Default { get; set; }
        T ReadValue(Kaitai.KaitaiStream input);
        void WriteValue(BinaryWriter writer, T value);
    }

    public interface IBoolDefinition : IDefaultDefinition<bool>
    {
    }

    public interface INumberDefinition<T> : IDefaultDefinition<T>, INumberDefinition where T : struct
    {
        new T Minimum { get; set; }
        new T Maximum { get; set; }
        new T MultipleOf { get; set; }
        RcpTypes.NumberScale Scale { get; set; }
        string Unit { get; set; }
    }

    public interface IStringDefinition : IDefaultDefinition<string>
    {
        string RegularExpression { get; set; }
    }

    public interface IRGBADefinition : IDefaultDefinition<Color>
    {
    }

    public interface IUriDefinition : IDefaultDefinition<string>
    {
        string Schema { get; set; }
        string Filter { get; set; }
    }

    public interface IEnumDefinition : IDefaultDefinition<string>
    {
        string[] Entries { get; set; }
        bool MultiSelect { get; set; }
    }

    public interface IArrayDefinition : ITypeDefinition
    {
        ITypeDefinition ElementType { get; }
        int[] Structure { get; set; }
    }

    public interface IRangeDefinition : ITypeDefinition
    {
        INumberDefinition ElementType { get; }
    }

    public interface IParameter : IWriteable
    {
        Int16 Id { get; }
        ITypeDefinition Type { get; }
        string Label { get; set; }
        string Description { get; set; }
        string Tags { get; set; }
        int Order { get; set; }
        Int16 ParentId { get; }
        Widget Widget { get; set; }
        byte[] Userdata { get; set; }
        string UserId { get; set; }

        event EventHandler Updated;
    }

    public interface IValueParameter : IParameter
    {
        object Value { get; set; }
        object Default { get; set; }
        event EventHandler ValueUpdated;
    }

    public interface IValueParameter<T>: IValueParameter
    {
        new T Value { get; set; }
        new T Default { get; set; }
    }

    public interface IGroupParameter: IParameter
    {
    }

    public interface IRangeParameter : IValueParameter
    {
        new IRangeDefinition Type { get; }
        object Lower { get; set; }
        object Upper { get; set; }
    }
}
