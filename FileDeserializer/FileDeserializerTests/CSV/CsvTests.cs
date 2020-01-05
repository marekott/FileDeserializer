using System;
using System.IO;
using Xunit;
using FileDeserializer.CSV;
using FileDeserializerTests.CSV.MockFiles;

namespace FileDeserializerTests.CSV
{
	public class CsvTests
	{
		private const string CsvOneRowPath = @"CSV\MockFiles\CsvWithOneRow.csv";
		private const string CsvTwoRowsPath = @"CSV\MockFiles\CsvWithTwoRows.csv";
		private const string CsvTwoRowsAndHeadersPath = @"CSV\MockFiles\CsvWithTwoRowsAndHeaders.csv";
		private const string CsvWithStringPath = @"CSV\MockFiles\CsvWithString.csv";
		private const char PolishCsvSeparator = ';';

		[Fact]
		public void DeserializeCsvWithOneRowToOneDimensionalArrayOfIntTest()
		{
			int[] expected = {1, 1, 1, 1};

			Csv csv = new Csv(new MokFileLocator(CsvOneRowPath), PolishCsvSeparator);

			var actual = csv.Deserialize<int>();

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void DeserializeCsvWithOneRowToOneDimensionalArrayOfStringTest()
		{
			string[] expected = { "1", "1", "1", "1" };

			Csv csv = new Csv(new MokFileLocator(CsvOneRowPath), PolishCsvSeparator);

			var actual = csv.Deserialize<string>();

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void DeserializeCsvWithTwoRowsToTwoDimensionalArrayOfIntTest()
		{
			int[,] expected = new int[2,4];
			expected[0, 0] = 1;
			expected[0, 1] = 1;
			expected[0, 2] = 1;
			expected[0, 3] = 1;
			expected[1, 0] = 2;
			expected[1, 1] = 2;
			expected[1, 2] = 2;
			expected[1, 3] = 2;

			Csv csv = new Csv(new MokFileLocator(CsvTwoRowsPath), PolishCsvSeparator);

			var actual = csv.DeserializeByRows<int>();

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void DeserializeCsvWithTwoRowsAndHeadersToTwoDimensionalArrayOfStringsTest()
		{
			string[,] expected = new string[3, 4];
			expected[0, 0] = "x1";
			expected[0, 1] = "x2";
			expected[0, 2] = "x3";
			expected[0, 3] = "x4";
			expected[1, 0] = "1";
			expected[1, 1] = "1";
			expected[1, 2] = "1";
			expected[1, 3] = "1";
			expected[2, 0] = "2";
			expected[2, 1] = "2";
			expected[2, 2] = "2";
			expected[2, 3] = "2";

			Csv csv = new Csv(new MokFileLocator(CsvTwoRowsAndHeadersPath), PolishCsvSeparator);

			var actual = csv.DeserializeByRows<string>();

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void DeserializeCsvWithTwoRowsAndHeadersToTwoDimensionalArrayOfStringsAndSkipHeadersTest()
		{
			int[,] expected = new int[2, 4];
			expected[0, 0] = 1;
			expected[0, 1] = 1;
			expected[0, 2] = 1;
			expected[0, 3] = 1;
			expected[1, 0] = 2;
			expected[1, 1] = 2;
			expected[1, 2] = 2;
			expected[1, 3] = 2;

			Csv csv = new Csv(new MokFileLocator(CsvTwoRowsAndHeadersPath), PolishCsvSeparator);

			var actual = csv.DeserializeByRows<int>(true);

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void DeserializeCsvOfStringToIntHasFormatExceptionBeenThrownTest()
		{
			Csv csv = new Csv(new MokFileLocator(CsvWithStringPath), PolishCsvSeparator);

			Assert.Throws<FormatException>(() => csv.Deserialize<int>());
		}

		[Fact]
		public void DeserializeCsvWithHeaderToIntHasFormatExceptionBeenThrownTest()
		{
			Csv csv = new Csv(new MokFileLocator(CsvTwoRowsAndHeadersPath), PolishCsvSeparator);

			Assert.Throws<FormatException>(() => csv.DeserializeByRows<int>());
		}

		[Fact]
		public void DeserializeCsvWrongFilePathHasFileNotFoundExceptionBeenThrownTest()
		{
			Csv csv = new Csv(new MokFileLocator(@"CSV\MockFiles\CsvWithStringiiixdxdxd.csv"), PolishCsvSeparator);

			Assert.Throws<FileNotFoundException>(() => csv.Deserialize<int>());
		}

        [Fact]
        public void ConstructorWithAbsolutePathTest()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var directory = new DirectoryInfo(currentDirectory);
            var absolutePath = directory.FullName + @"\CSV\MockFiles\CsvWithString.csv";

			Csv csv = new Csv(absolutePath, PolishCsvSeparator);

            csv.Deserialize<string>();
        }

	}
}