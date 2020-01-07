using System;
using System.Windows.Forms;

namespace Geeky.VanityRemover
{
	internal static class Program
	{
		[STAThread]
		private static void Main(string[] args)
		{
			string initialDirectory = (args.Length > 0) ? args[0] : "";
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(defaultValue: false);
			Application.Run(new Main(initialDirectory));
		}
	}
}
