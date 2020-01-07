using System;

namespace Geeky.VanityRemover.Core
{
	public class CleaningDoneEventArgs : EventArgs
	{
		private readonly uint deleted;

		private readonly uint total;

		public uint Total => total;

		public uint Deleted => deleted;

		public CleaningDoneEventArgs(uint total, uint deleted)
		{
			this.deleted = deleted;
			this.total = total;
		}
	}
}
