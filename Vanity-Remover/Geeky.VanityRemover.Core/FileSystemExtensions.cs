using System.IO;
using System.Linq;

namespace Geeky.VanityRemover.Core
{
	public static class FileSystemExtensions
	{
		public static bool IsNotEmpty(this DirectoryInfo directory)
		{
			return directory.GetFileSystemInfos().Any();
		}
	}
}
