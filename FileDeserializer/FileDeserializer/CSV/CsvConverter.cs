using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileDeserializer.CSV
{
	public class CsvConverter : ICsvConverter
    {
		/// <summary>
		/// Generic method which deserialize specific Csv for one-dimensional array. Use when file has only one row or you don't need data split by columns.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="path">Absolute path to file</param>
		/// <param name="separator">Separator by which file will be spitted</param>
		/// <returns></returns>
		public T[] Deserialize<T>(string path, char separator)
		{
			var stringArray = ReadWholeFile(path).Split(separator);	
			var oneDimensionalArray = ConvertToProvidedType<T>(stringArray, typeof(T));

			return oneDimensionalArray;
		}

		private string ReadWholeFile(string path)
		{
			using (var reader = new StreamReader(path))
			{
				var wholeFile = reader.ReadToEnd();

				return wholeFile;
			}
		}

		private static T[] ConvertToProvidedType<T>(string[] stringArray, Type type)
		{
			var oneDimensionalArray = new T[stringArray.Length];

			try
			{
				for (int i = 0; i < oneDimensionalArray.Length; i++)
				{
					oneDimensionalArray[i] = (T)Convert.ChangeType(stringArray[i], type);
				}

				return oneDimensionalArray;
			}

			catch (FormatException formatEx)
			{
				throw new FormatException($"Unable to cast value from file for pointed type. Check if you are not trying to cast string for numeral type. {formatEx.StackTrace}");
			}
		}

		/// <summary>
		/// Generic method which deserialize specific Csv for two-dimensional array. Deserialize to string if you would like to keep headers which are different than numeric.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="path">Absolute path to file</param>
		/// <param name="separator">Separator by which file will be spitted</param>
		/// <param name="skipHeaders">Choose if headers should be omitted.</param>
		/// <returns></returns>
		public T[,] DeserializeByRows<T>(string path, char separator, bool skipHeaders = false)
		{
			var listWithRows = ReadWholeFileByRows(path);
			var listWithValues = listWithRows.SplitBy(separator);
			if (skipHeaders)
			{
				listWithValues.SkipHeaders();
			}

			var twoDimensionalArray = ConvertToTwoDimensionalArray<T>(listWithValues, typeof(T));

			return twoDimensionalArray;
		}

		private List<string> ReadWholeFileByRows(string path)
		{
			var listWithRows = new List<string>();

			using (var reader = new StreamReader(path))
			{
				while (!reader.EndOfStream)
				{
					listWithRows.Add(reader.ReadLine());
				}

				return listWithRows;
			}
		}

		private T[,] ConvertToTwoDimensionalArray<T>(List<List<string>> list, Type type)
		{
			var twoDimensionalArray = new T[list.Count, list[0].Count];

			try
			{
				for (int i = 0; i < twoDimensionalArray.GetLength(0); i++)
				{
					for (int j = 0; j < twoDimensionalArray.GetLength(1); j++)
					{
						twoDimensionalArray[i, j] = (T)Convert.ChangeType(list[i][j], type);
					}
				}

				return twoDimensionalArray;
			}

			catch (FormatException formatEx)
			{
				throw new FormatException($"Unable to cast value from file for pointed type. Check if you are not trying to cast string for numeral type. {formatEx.StackTrace}");
			}
		}
	}

	public static class ListExtensions
	{
		public static void SkipHeaders<T>(this List<T> list)
		{
			list.RemoveAt(0);
		}

		public static List<List<string>> SplitBy(this List<string> list, char separator)
		{
			var listWithValues = new List<List<string>>();

			foreach (var row in list)
			{
				listWithValues.Add(row.Split(separator).ToList());
			}

			return listWithValues;
		}
	}
}
