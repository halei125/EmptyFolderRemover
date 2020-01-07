using System;
using System.IO;

namespace Geeky.VanityRemover.Core
{
	public interface ICleaner
	{
		event EventHandler<CleaningDoneEventArgs> CleaningDone;

		bool Clean(DirectoryInfo directory);

		void Cancel();
	}
}
