using System;
using Xunit;
using FileDeserializer.CSV;

namespace FileDeserializerTests.CSV
{
	public class CsvTests
	{
		[Fact()]
		public void DeserializeCsvWithOneRowToInt()
		{
			int[] expected = {1, 1, 1, 1};

			Csv csv = new Csv(@"F:\Informatyka\C#\Projekty1.0\FileDeserializer\FileDeserializer\FileDeserializerTests\CSV\MokFiles\CsvWithOneRow.csv", ';');

			var actual = csv.Deserialize<int>();

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void DeserializeCsvWithOneRowToString()
		{
			string[] expected = { "1", "1", "1", "1" };

			Csv csv = new Csv(@"F:\Informatyka\C#\Projekty1.0\FileDeserializer\FileDeserializer\FileDeserializerTests\CSV\MokFiles\CsvWithOneRow.csv", ';');

			var actual = csv.Deserialize<string>();

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void DeserializeCsvWasFormatExceptionThrown()
		{
			Csv csv = new Csv(@"F:\Informatyka\C#\Projekty1.0\FileDeserializer\FileDeserializer\FileDeserializerTests\CSV\MokFiles\CsvWithString.csv", ';');

			Assert.Throws<FormatException>(() => csv.Deserialize<int>());
		}

		[Fact()]
		public void DeserializeCsvWithTwoRows()
		{
			string[,] expected = new string[2,4];
			expected[0, 0] = "1";
			expected[0, 1] = "1";
			expected[0, 2] = "1";
			expected[0, 3] = "1";
			expected[1, 0] = "2";
			expected[1, 1] = "2";
			expected[1, 2] = "2";
			expected[1, 3] = "2";

			Csv csv = new Csv(@"F:\Informatyka\C#\Projekty1.0\FileDeserializer\FileDeserializer\FileDeserializerTests\CSV\MokFiles\CsvWithTwoRows.csv", ';');

			var actual = csv.DeserializeByRows();

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

			Csv csv = new Csv(@"F:\Informatyka\C#\Projekty1.0\FileDeserializer\FileDeserializer\FileDeserializerTests\CSV\MokFiles\CsvWithTwoRowsAndHeaders.csv", ';');

			var actual = csv.DeserializeByRows();

			Assert.Equal(expected, actual);
		}

		[Fact()]
		public void DeserializeCsvWithoutHeaders()
		{
			string[,] expected = new string[2, 4];
			expected[0, 0] = "1";
			expected[0, 1] = "1";
			expected[0, 2] = "1";
			expected[0, 3] = "1";
			expected[1, 0] = "2";
			expected[1, 1] = "2";
			expected[1, 2] = "2";
			expected[1, 3] = "2";

			Csv csv = new Csv(@"F:\Informatyka\C#\Projekty1.0\FileDeserializer\FileDeserializer\FileDeserializerTests\CSV\MokFiles\CsvWithTwoRowsAndHeaders.csv", ';');

			var actual = csv.DeserializeByRows(true);

			Assert.Equal(expected, actual);
		}
	}
}