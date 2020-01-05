namespace FileDeserializer.CSV
{
    public interface ICsvConverter
    {
        /// <summary>
        /// Generic method which deserialize specific Csv for one-dimensional array. Use when file has only one row or you don't need data split by columns.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path">Absolute path to file</param>
        /// <param name="separator">Separator by which file will be spitted</param>
        /// <returns></returns>
        T[] Deserialize<T>(string path, char separator);

        /// <summary>
        /// Generic method which deserialize specific Csv for two-dimensional array. Deserialize to string if you would like to keep headers which are different than numeric.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path">Absolute path to file</param>
        /// <param name="separator">Separator by which file will be spitted</param>
        /// <param name="skipHeaders">Choose if headers should be omitted.</param>
        /// <returns></returns>
        T[,] DeserializeByRows<T>(string path, char separator, bool skipHeaders = false);
    }
}