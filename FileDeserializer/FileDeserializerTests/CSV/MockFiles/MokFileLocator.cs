using System.IO;
using FileDeserializer.CSV;

namespace FileDeserializerTests.CSV.MockFiles
{
	internal class MokFileLocator : IFileLocator
	{
		private static string _filePath;

		public MokFileLocator(string fileName)
		{
			var currentDirectory = Directory.GetCurrentDirectory();
			var directory = new DirectoryInfo(currentDirectory);
			_filePath = Path.Combine(directory.FullName, fileName);
		}

		public string GetFileLocation()
		{
			return _filePath;
		}
	}
}
