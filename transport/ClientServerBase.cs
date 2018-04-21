using System;

using RCP.Protocol;
using System.Collections.Generic;
using System.Collections;

namespace RCP
{
    public abstract class ClientServerBase: IDisposable
	{
		//public ILogger Logger { get; set; }
		
		protected Packet Pack(RcpTypes.Command command, IParameter parameter)
		{
			var packet = new Packet(command);
			packet.Data = parameter;
			
			return packet;
		}
		
		protected Packet Pack(RcpTypes.Command command, uint id)
		{
			var packet = new Packet(command);
			//packet.Data = new Parameter<T>(id, null);
			
			return packet;
		}
		
		protected Packet Pack(RcpTypes.Command command)
		{
			var packet = new Packet(command);
			
			return packet;
		}
		
		public virtual void Dispose()
		{
			//Logger = null;
		}
	}
}