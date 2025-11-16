namespace ir_coding_task_csv_validator.Helpers
{
    public interface ICsvMapper
    {
        /// <summary>
        /// Read the CSV file and return the rows as a list of strings.
        /// </summary>
        /// <param name="file">csv file to read</param>
        /// <returns>csv rows</returns>
        Task<List<string>> ReadAsList(IFormFile file);

        /// <summary>
        /// Map the CSV rows to a list of objects of type T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="csvRows">csv rows</param>
        /// <param name="validateHeaders">true if need to validate headers</param>
        /// <param name="expectedHeaders">expected headers in csv</param>
        /// <returns></returns>
        List<T> ParseAsync<T>(List<string> csvRows, bool validateHeaders, List<string> expectedHeaders) where T : new();
    }
}