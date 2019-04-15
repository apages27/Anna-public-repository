using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoiceControlPlayground
{
	public class VoiceControlProcessor
	{
		public void ProcessRequest(string resultText)
		{
			switch (resultText)
			{
				case "m s teams":
					Process.Start(@"C:\Users\anna.pages\AppData\Local\Microsoft\Teams\current\Teams.exe");
					break;
			}
		}
	}
}
