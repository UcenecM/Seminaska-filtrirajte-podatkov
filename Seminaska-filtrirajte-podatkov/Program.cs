using System;
using System.Collections.Generic;
using System.Linq;

namespace DataFilterApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Primer podatkov
            var data = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object> { { "name", "Alice" }, { "age", 25 }, { "salary", 50000 } },
                new Dictionary<string, object> { { "name", "Bob" }, { "age", 30 }, { "salary", 60000 } },
                new Dictionary<string, object> { { "name", "Charlie" }, { "age", 35 }, { "salary", 70000 } }
            };

            // Primer kriterijev
            var criteria = new Dictionary<string, (string, object)>
            {
                { "age", (">", 28) },
                { "salary", ("<", 65000) }
            };

            // Filtriranje podatkov
            var filteredData = FilterRecords(data, criteria);

            // Izpis rezultatov
            foreach (var record in filteredData)
            {
                Console.WriteLine($"Name: {record["name"]}, Age: {record["age"]}, Salary: {record["salary"]}");
            }
        }

        static List<Dictionary<string, object>> FilterRecords(List<Dictionary<string, object>> data, Dictionary<string, (string, object)> criteria)
        {
            return data.Where(record => criteria.All(criterion =>
            {
                var (key, (op, value)) = criterion;
                if (!record.ContainsKey(key)) return false;

                var recordValue = record[key];

                return op switch
                {
                    ">" => Comparer<object>.Default.Compare(recordValue, value) > 0,
                    "<" => Comparer<object>.Default.Compare(recordValue, value) < 0,
                    "==" => Comparer<object>.Default.Compare(recordValue, value) == 0,
                    "!=" => Comparer<object>.Default.Compare(recordValue, value) != 0,
                    ">=" => Comparer<object>.Default.Compare(recordValue, value) >= 0,
                    "<=" => Comparer<object>.Default.Compare(recordValue, value) <= 0,
                    _ => throw new ArgumentException($"Unsupported operator: {op}")
                };
            })).ToList();
        }
    }
}