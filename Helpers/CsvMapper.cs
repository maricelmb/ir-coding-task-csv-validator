using System.Globalization;
using System.Reflection.PortableExecutable;

namespace ir_coding_task_csv_validator.Helpers
{
    public class CsvMapper : ICsvMapper
    {
        private const char Delimiter = ',';

        public async Task<List<string>> ReadAsList(IFormFile file)
        {
            try
            {
                List<string> result = new();
                using StreamReader reader = new StreamReader(file.OpenReadStream());
                while (reader.Peek() > 0)
                {
                    var line = await reader.ReadLineAsync();
                    if (line != null)
                    {
                        result.Add(line);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                return new List<string>();
            }
        }

        public List<T> ParseAsync<T>(List<string> csvRows, bool validateHeaders, List<string> expectedHeaders) where T : new()
        {
            var props = typeof(T).GetProperties();
            var result = new List<T>();
            var header = new string[] { };

            //process each row
            for (int x = 0; x < csvRows.Count; x++)
            {
                //handle header row
                if (x == 0)
                {
                    header = csvRows[x].Split(Delimiter, StringSplitOptions.TrimEntries);

                    if (validateHeaders && !expectedHeaders.SequenceEqual(header))
                    {
                        throw new InvalidDataException("CSV headers do not match expected format.");
                    }
                    
                    continue;
                }

                var obj = new T();

                var values = csvRows[x].Split(Delimiter, StringSplitOptions.TrimEntries);
                if(values == null) continue;

                //process each column of each row
                //to do: log something if  header and values length are not equal
                for (int i = 0; i < header.Length && i < values.Length; i++)
                {
                    //remove the space in between words from the header to match property names
                    //to do: consider other options like using a mapping dictionary or custom attributes of the model properties
                    string trimmedHeader = RemoveSpace(header[i]);
                    var prop = props.FirstOrDefault(p => p.Name.Equals(trimmedHeader, StringComparison.OrdinalIgnoreCase));

                    //to do: log something if prop is null, why do we have an extra header that is not matched with the prop
                    try
                    {
                        prop?.SetValue(obj, ConvertValue(values[i], prop.PropertyType, prop.Name));
                    }
                    catch (Exception ex)
                    {
                        //Todo: log something, don't throw and continue with the rest
                    }
                }
                
                result.Add(obj);
            }

            return result;
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
}
}
