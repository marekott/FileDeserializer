using System;
using System.IO;
using Xunit;
using FileDeserializer.CSV;
using FileDeserializerTests.CSV.MockFiles;

namespace FileDeserializerTests.CSV
{
	public class CsvTests
	{
		[Fact()]
		public void DeserializeCsvWithOneRowToInt()
		{
			int[] expected = {1, 1, 1, 1};

			Csv csv = new Csv(@"F:\Informatyka\C#\Projekty1.0\FileDeserializer\FileDeserializer\FileDeserializerTests\CSV\MockFiles\CsvWithOneRow.csv", ';');

			var actual = csv.Deserialize<int>();

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void DeserializeCsvWithOneRowToString()
		{
			string[] expected = { "1", "1", "1", "1" };

			Csv csv = new Csv(@"F:\Informatyka\C#\Projekty1.0\FileDeserializer\FileDeserializer\FileDeserializerTests\CSV\MockFiles\CsvWithOneRow.csv", ';');

			var actual = csv.Deserialize<string>();

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void DeserializeCsvWithTwoRows()
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

			Csv csv = new Csv(@"F:\Informatyka\C#\Projekty1.0\FileDeserializer\FileDeserializer\FileDeserializerTests\CSV\MockFiles\CsvWithTwoRows.csv", ';');

			var actual = csv.DeserializeByRows<int>();

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void DeserializeCsvWithHeaders()
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

			Csv csv = new Csv(@"F:\Informatyka\C#\Projekty1.0\FileDeserializer\FileDeserializer\FileDeserializerTests\CSV\MockFiles\CsvWithTwoRowsAndHeaders.csv", ';');

			var actual = csv.DeserializeByRows<string>();

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void DeserializeCsvWithoutHeaders()
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

			Csv csv = new Csv(@"F:\Informatyka\C#\Projekty1.0\FileDeserializer\FileDeserializer\FileDeserializerTests\CSV\MockFiles\CsvWithTwoRowsAndHeaders.csv", ';');

			var actual = csv.DeserializeByRows<int>(true);

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void DeserializeCsvWasFormatExceptionThrown()
		{
			Csv csv = new Csv(@"F:\Informatyka\C#\Projekty1.0\FileDeserializer\FileDeserializer\FileDeserializerTests\CSV\MockFiles\CsvWithString.csv", ';');

			Assert.Throws<FormatException>(() => csv.Deserialize<int>());
		}

		[Fact()]
		public void DeserializeCsvWithHeaderToIntWasFormatExceptionThrown()
		{
			Csv csv = new Csv(@"F:\Informatyka\C#\Projekty1.0\FileDeserializer\FileDeserializer\FileDeserializerTests\CSV\MockFiles\CsvWithTwoRowsAndHeaders.csv", ';');

			Assert.Throws<FormatException>(() => csv.DeserializeByRows<int>());
		}

		[Fact()]
		public void WasFileNotFoundExceptionThrown()
		{
			Csv csv = new Csv(@"F:\Informatyka\C#\Projekty1.0\FileDeserializer\FileDeserializer\FileDeserializerTests\CSV\MockFiles\CsvWithStringiiixdxdxd.csv", ';');

			Assert.Throws<FileNotFoundException>(() => csv.Deserialize<int>());
		}

		[Fact()]
		public void ConstructorWithOnlyFileNameTest()
		{
			Csv csv = new Csv(new MokFileLocator(@"CSV\MockFiles\CsvWithString.csv"), ';');

			csv.Deserialize<string>();
		}
	}
}