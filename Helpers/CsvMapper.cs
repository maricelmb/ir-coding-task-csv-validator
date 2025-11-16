using System.Globalization;
using System.Runtime.CompilerServices;

namespace ir_coding_task_csv_validator.Helpers
{
    public class CsvMapper : ICsvMapper
    {
        public async IAsyncEnumerable<T> ParseAsync<T>(Stream stream,
        [EnumeratorCancellation] CancellationToken cancellationToken = default) where T : new()
        {
            //Todo: validate parameters for public functions

            using var reader = new StreamReader(stream);
            //Todo: validation for header
            var header = (await reader.ReadLineAsync())?.Split(',') ?? [];
            var result = new List<T>();

            while (!reader.EndOfStream)
            {
                cancellationToken.ThrowIfCancellationRequested();
                var line = await reader.ReadLineAsync().ConfigureAwait(false);
                var values = line?.Split(',');
                if (values == null) continue;

                var obj = new T();
                var props = typeof(T).GetProperties();

                for (int i = 0; i < header.Length && i < values.Length; i++)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    //trim all the spaces from the header to match property names
                    string trimmedHeader = RemoveSpace(header[i]);
                    var prop = props.FirstOrDefault(p => p.Name.Equals(trimmedHeader, StringComparison.OrdinalIgnoreCase));

                    try
                    {
                        prop?.SetValue(obj, ConvertValue(values[i], prop.PropertyType, prop.Name));
                    }
                    catch (Exception ex)
                    {
                        //Todo: log something and continue with the rest
                    }
                }

                //result.Add(obj);
                yield return obj;
            }


        }

        private object? ConvertValue(string raw, Type type, string name)
        {
            if (string.IsNullOrWhiteSpace(raw))
                return type.IsValueType ? Activator.CreateInstance(type) : null;

            if (type == typeof(int))
            {
                return int.TryParse(RemoveSpace(raw), out var v) ? v : 0;
            }

            if (type == typeof(long))
            {

                return long.TryParse(RemoveSpace(raw), out var v) ? v : 0;
            }

            if (type == typeof(decimal))
            {
                if (name.Equals("Salary", StringComparison.OrdinalIgnoreCase))
                {
                    return decimal.TryParse(raw, NumberStyles.Currency, CultureInfo.CurrentCulture, out var value) ? value : 0m;
                }

                return decimal.TryParse(raw, out var v) ? v : 0m;
            }

            if (type == typeof(DateTime))
            {
                //log something for parsing error or add a validation in Validation Service
                return DateTime.TryParse(raw, out var v) ? v : DateTime.MinValue;
            }

            return Convert.ChangeType(raw, type);
        }

        private string RemoveSpace(string rawValue)
        {
            return rawValue.Replace(" ", "");
        }

        public async Task<List<T>> ParseAsync<T>(Stream stream) where T : new()
        {
            //Todo: validate parameters for public functions

            using var reader = new StreamReader(stream);
            //Todo: validation for header
            var header = (await reader.ReadLineAsync())?.Split(',') ?? [];
            var result = new List<T>();

            while (!reader.EndOfStream)
            {
                var values = (await reader.ReadLineAsync())?.Split(',');
                if (values == null) continue;

                var obj = new T();
                var props = typeof(T).GetProperties();

                for (int i = 0; i < header.Length && i < values.Length; i++)
                {
                    //trim all the spaces from the header to match property names
                    string trimmedHeader = RemoveSpace(header[i]);
                    var prop = props.FirstOrDefault(p => p.Name.Equals(trimmedHeader, StringComparison.OrdinalIgnoreCase));

                    try
                    {
                        prop?.SetValue(obj, ConvertValue(values[i], prop.PropertyType, prop.Name));
                    }
                    catch (Exception ex)
                    {
                        //Todo: log something and continue with the rest
                    }
                }

                result.Add(obj);
            }

            return result;
        }
    }
}
