using System;
using System.Collections;

namespace Transit
{
	public class ConsoleWriter : Component
	{
		[InputPort("In")]
		StandardInputPort data;

		public override IEnumerator Execute ()
		{
			InformationPacket packet;
			while(!data.Receive(out packet))
			{
				yield return WaitForPacketOn (data);
			}

			if (packet.Content is Exception)
			{
				var exception = packet.Content as Exception;
				Console.WriteLine (string.Format("Error ({0}): {1}\n{2}", exception.GetType(), exception.Message, exception.StackTrace));
			}
			else
			{
				Console.WriteLine (packet.Content);
			}
		}
	}
}