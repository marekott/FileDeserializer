using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileDeserializer.CSV
{
	public class Csv
	{
		private readonly string _path;
		private readonly char _separator;

		//TODO zabezpieczenie przed błędną lokalizacją pliku
		//TODO inne konstruktory
		public Csv(string path, char separator)
		{
			_path = path;
			_separator = separator;
		}

		//TODO refaktor metody
		public T[] Deserialize<T>()
		{
			var stringArray = ReadWholeFile().Split(_separator);
			T[] oneDimensionalArray = new T[stringArray.Length];

			var providedType = typeof(T);

			try
			{
				for (int i = 0; i < oneDimensionalArray.Length; i++)
				{
					oneDimensionalArray[i] = (T)Convert.ChangeType(stringArray[i], providedType);
				}

				return oneDimensionalArray;
			}

			catch (FormatException)
			{
				throw new FormatException("Unable to cast value from file for pointed type. Check if you are not trying to cast string for number type.");
			}
		}

		private string ReadWholeFile()
		{
			using (var reader = new StreamReader(_path))
			{
				var wholeFile = reader.ReadToEnd();

				return wholeFile;
			}
		}

		//TODO czy na pewno musi zwracać zawsze string[,] a nie może być generyczna[,]
		public string[,] DeserializeByRows(bool skipHeaders = false)
		{
			var listWithRows = ReadWholeFileByRows();
			var listWithValues = listWithRows.SplitBy(_separator);
			if (skipHeaders)
			{
				listWithValues.SkipHeaders();
			}

			var twoDimensionalArray = listWithValues.ConvertToTwoDimensionalArray();

			return twoDimensionalArray;
		}

		private List<string> ReadWholeFileByRows()
		{
			var listWithRows = new List<string>();

			using (var reader = new StreamReader(_path))
			{
				while (!reader.EndOfStream)
				{
					listWithRows.Add(reader.ReadLine());
				}

				return listWithRows;
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

		public static T[,] ConvertToTwoDimensionalArray<T>(this List<List<T>> list)
		{
			var twoDimensionalArray = new T[list.Count, list[0].Count];

			for (int i = 0; i < twoDimensionalArray.GetLength(0); i++)
			{
				for (int j = 0; j < twoDimensionalArray.GetLength(1); j++)
				{
					twoDimensionalArray[i, j] = list[i][j];
				}
			}

			return twoDimensionalArray;
		}

	}
}
