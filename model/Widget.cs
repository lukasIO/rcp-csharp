using System;
using System.Linq;
using RCP.Protocol;
using System.IO;
using Kaitai;
using RCP.Exceptions;

namespace RCP.Protocol
{
    [Flags]
    public enum WidgetChangedFlags : int
    {
        Enabled = 1 << 0,
        LabelVisible = 1 << 1,
        ValueVisible = 1 << 2,
        NeedsConfirmation = 1 << 3,
    }

    public class Widget: RCPObject
    {
        int FChangedFlags;
        public RcpTypes.Widgettype Type { get; set; }

        public Widget(RcpTypes.Widgettype type)
        {
            Type = type;
        }

        protected void SetChanged(WidgetChangedFlags flags) => FChangedFlags |= (int)flags;
        protected bool IsChanged(WidgetChangedFlags flags) => ((WidgetChangedFlags)FChangedFlags).HasFlag(flags);

        private bool FEnabled;
        public bool Enabled
        {
            get => FEnabled;
            set
            {
                if (SetProperty(ref FEnabled, value))
                    SetChanged(WidgetChangedFlags.Enabled);
            }
        }

        private bool FLabelVisible;
        public bool LabelVisible
        {
            get => FLabelVisible;
            set
            {
                if (SetProperty(ref FLabelVisible, value))
                    SetChanged(WidgetChangedFlags.LabelVisible);
            }
        }

        private bool FValueVisible;
        public bool ValueVisible
        {
            get => FValueVisible;
            set
            {
                if (SetProperty(ref FValueVisible, value))
                    SetChanged(WidgetChangedFlags.ValueVisible);
            }
        }

        private bool FNeedsConfirmation;
        public bool NeedsConfirmation
        {
            get => FNeedsConfirmation;
            set
            {
                if (SetProperty(ref FNeedsConfirmation, value))
                    SetChanged(WidgetChangedFlags.NeedsConfirmation);
            }
        }

        public virtual void Write(BinaryWriter writer)
        {
            writer.Write((byte)Type);

            //write type specific stuff
            WriteOptions(writer);

            //terminate
            writer.Write((byte)0);
        }

        protected virtual void WriteOptions(BinaryWriter writer)
        {
            if (IsChanged(WidgetChangedFlags.Enabled))
            {
                writer.Write((byte)RcpTypes.WidgetOptions.Enabled);
                writer.Write(Enabled);
            }

            if (IsChanged(WidgetChangedFlags.LabelVisible))
            {
                writer.Write((byte)RcpTypes.WidgetOptions.LabelVisible);
                writer.Write(LabelVisible);
            }

            if (IsChanged(WidgetChangedFlags.ValueVisible))
            {
                writer.Write((byte)RcpTypes.WidgetOptions.ValueVisible);
                writer.Write(ValueVisible);
            }

            if (IsChanged(WidgetChangedFlags.NeedsConfirmation))
            {
                writer.Write((byte)RcpTypes.WidgetOptions.NeedsConfirmation);
                writer.Write(NeedsConfirmation);
            }
        }

        protected virtual void ParseOptions(KaitaiStream input)
        {
            while (true)
            {
                var code = input.ReadU1();
                if (code == 0)
                    break;

                var option = (RcpTypes.WidgetOptions)input.ReadU1();
                if (!Enum.IsDefined(typeof(RcpTypes.WidgetOptions), option))
                    throw new RCPDataErrorException("Widget parsing: Unknown widget option: " + option.ToString());

                switch (option)
                {
                    case RcpTypes.WidgetOptions.Enabled:
                        Enabled = input.ReadBoolean();
                        break;

                    case RcpTypes.WidgetOptions.LabelVisible:
                        Enabled = input.ReadBoolean();
                        break;

                    case RcpTypes.WidgetOptions.ValueVisible:
                        Enabled = input.ReadBoolean();
                        break;

                    case RcpTypes.WidgetOptions.NeedsConfirmation:
                        Enabled = input.ReadBoolean();
                        break;

                    default:
                        if (!HandleOption(input, option))
                        {
                            throw new RCPUnsupportedFeatureException();
                        }
                        break;
                }
            }
        }

        public static Widget Parse(KaitaiStream input)
        {
            var widgettype = (RcpTypes.Widgettype)input.ReadU1();
            if (!Enum.IsDefined(typeof(RcpTypes.Widgettype), widgettype))
                throw new RCPDataErrorException("Widget parsing: Unknown widget!");

            Widget widget = null;

            switch (widgettype)
            {
                case RcpTypes.Widgettype.Bang:
                    {
                        widget = new BangWidget();
                        break;
                    }

                case RcpTypes.Widgettype.Press:
                    {
                        widget = new PressWidget();
                        break;
                    }

                case RcpTypes.Widgettype.Toggle:
                    {
                        widget = new ToggleWidget();
                        break;
                    }

                case RcpTypes.Widgettype.Slider:
                    {
                        widget = new SliderWidget();
                        break;
                    }

                case RcpTypes.Widgettype.Numberbox:
                    {
                        widget = new NumberboxWidget();
                        break;
                    }
            }

            widget.ParseOptions(input);
            return widget;
        }

        protected virtual bool HandleOption(KaitaiStream input, RcpTypes.WidgetOptions code)
        {
            return false;
        }
    }

    public class BangWidget: Widget
    {
        public BangWidget()
        : base(RcpTypes.Widgettype.Bang)
        { }
    }

    public class PressWidget : Widget
    {
        public PressWidget()
        : base(RcpTypes.Widgettype.Press)
        { }
    }

    public class ToggleWidget : Widget
    {
        public ToggleWidget()
        : base(RcpTypes.Widgettype.Toggle)
        { }
    }

    public class SliderWidget : Widget
    {
        public SliderWidget()
        : base(RcpTypes.Widgettype.Slider)
        { }
    }

    public class NumberboxWidget : Widget
    {
        public NumberboxWidget()
        : base(RcpTypes.Widgettype.Numberbox)
        { }
    }
}