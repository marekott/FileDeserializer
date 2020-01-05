using System;
using System.IO;
using Xunit;
using FileDeserializer.CSV;

namespace FileDeserializerTests.CSV
{
	public class CsvTests
    {
        private static readonly string CurrentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory()).FullName;
        private static readonly string CsvOneRowPath = CurrentDirectory + @"\CSV\MockFiles\CsvWithOneRow.csv";
        private static readonly string CsvTwoRowsPath = CurrentDirectory + @"\CSV\MockFiles\CsvWithTwoRows.csv";
        private static readonly string CsvTwoRowsAndHeadersPath = CurrentDirectory + @"\CSV\MockFiles\CsvWithTwoRowsAndHeaders.csv";
        private static readonly string CsvWithStringPath = CurrentDirectory + @"\CSV\MockFiles\CsvWithString.csv";
        private const char PolishCsvSeparator = ';';

		[Fact]
		public void DeserializeCsvWithOneRowToOneDimensionalArrayOfIntTest()
		{
			// Arrange
			int[] expected = {1, 1, 1, 1};
			var csvConverter = new CsvConverter();

			// Act
			var actual = csvConverter.Deserialize<int>(CsvOneRowPath, PolishCsvSeparator);

			// Assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void DeserializeCsvWithOneRowToOneDimensionalArrayOfStringTest()
		{
            // Arrange
			string[] expected = { "1", "1", "1", "1" };
            var csvConverter = new CsvConverter();

			// Act
			var actual = csvConverter.Deserialize<string>(CsvOneRowPath, PolishCsvSeparator);

            // Assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void DeserializeCsvWithTwoRowsToTwoDimensionalArrayOfIntTest()
		{
            // Arrange
			int[,] expected = new int[2,4];
			expected[0, 0] = 1;
			expected[0, 1] = 1;
			expected[0, 2] = 1;
			expected[0, 3] = 1;
			expected[1, 0] = 2;
			expected[1, 1] = 2;
			expected[1, 2] = 2;
			expected[1, 3] = 2;
            var csvConverter = new CsvConverter();

			// Act
			var actual = csvConverter.DeserializeByRows<int>(CsvTwoRowsPath, PolishCsvSeparator);

            // Assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void DeserializeCsvWithTwoRowsAndHeadersToTwoDimensionalArrayOfStringsTest()
		{
            // Arrange
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
            var csvConverter = new CsvConverter();

			// Act
			var actual = csvConverter.DeserializeByRows<string>(CsvTwoRowsAndHeadersPath, PolishCsvSeparator);

            // Assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void DeserializeCsvWithTwoRowsAndHeadersToTwoDimensionalArrayOfStringsAndSkipHeadersTest()
		{
            // Arrange
			int[,] expected = new int[2, 4];
			expected[0, 0] = 1;
			expected[0, 1] = 1;
			expected[0, 2] = 1;
			expected[0, 3] = 1;
			expected[1, 0] = 2;
			expected[1, 1] = 2;
			expected[1, 2] = 2;
			expected[1, 3] = 2;
            var csvConverter = new CsvConverter();

			// Act
			var actual = csvConverter.DeserializeByRows<int>(CsvTwoRowsAndHeadersPath, PolishCsvSeparator, true);

            // Assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void DeserializeCsvOfStringToIntHasFormatExceptionBeenThrownTest()
		{
			// Arrange
			var csvConverter = new CsvConverter();

			// Act
			// Assert
			Assert.Throws<FormatException>(() => csvConverter.Deserialize<int>(CsvWithStringPath, PolishCsvSeparator));
		}

		[Fact]
		public void DeserializeCsvWithHeaderToIntHasFormatExceptionBeenThrownTest()
		{
            // Arrange
			var csvConverter = new CsvConverter();

			// Act
			// Assert
			Assert.Throws<FormatException>(() => csvConverter.DeserializeByRows<int>(CsvTwoRowsAndHeadersPath, PolishCsvSeparator));
		}

		[Fact]
		public void DeserializeCsvWrongFilePathHasFileNotFoundExceptionBeenThrownTest()
		{
            // Arrange
			var csvConverter = new CsvConverter();

			// Act
			// Assert
			Assert.Throws<FileNotFoundException>(() => csvConverter.Deserialize<int>(CurrentDirectory + @"\CSV\MockFiles\CsvWithStringiiixdxdxd.csv", PolishCsvSeparator));
		}
    }
}