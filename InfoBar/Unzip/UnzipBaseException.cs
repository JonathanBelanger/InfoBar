using System;
using System.Runtime.Serialization;

namespace NightIguana.Unzip
{
	[Serializable]
	public class UnzipBaseException : ApplicationException
	{
		protected UnzipBaseException(SerializationInfo info, StreamingContext context )
			: base( info, context )
		{
		}
	
		public UnzipBaseException()
		{
		}
		
		public UnzipBaseException(string msg)
			: base(msg)
		{
		}

		public UnzipBaseException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
