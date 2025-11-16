using System.Runtime.CompilerServices;

namespace ir_coding_task_csv_validator.Helpers
{
    public interface ICsvMapper
    {
        //Todo: add cancellation token
        //Todo: add custom mapping options

        /// <summary>
        /// Parses the CSV file into a list of objects of type T
        /// CSV header fields must match the property names of T (case insensitive, white space are ignored as well)
        /// </summary>
        /// <typeparam name="T">Target object to map the csv fields</typeparam>
        /// <param name="file"></param>
        /// <returns>List of mapped objects</returns>
        IAsyncEnumerable<T> ParseAsync<T>(Stream stream,
        [EnumeratorCancellation] CancellationToken cancellationToken = default) where T : new();

        Task<List<T>> ParseAsync<T>(Stream stream) where T : new();
    }
}