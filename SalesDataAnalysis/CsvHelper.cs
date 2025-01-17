using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

public static class CsvHelper
{
    // Method to detect the delimiter by checking the first line
    private static char DetectDelimiter(string[] lines)
    {
        var firstLine = lines.FirstOrDefault();
        if (firstLine == null) return ';'; // Default fallback delimiter

        // Check for common delimiters in the first line
        if (firstLine.Contains(","))
            return ',';
        if (firstLine.Contains(";"))
            return ';';
        if (firstLine.Contains("\t"))
            return '\t';

        // Default to comma if nothing found
        return ',';
    }

    // Method to read CSV file and return a list of records
    public static List<T> ReadCsv<T>(string filePath, Func<string[], T> mapFunction, char? delimiter = null)
    {
        var records = new List<T>();

        if (!File.Exists(filePath))
        {
            Console.WriteLine($"Error: CSV file not found at {Path.GetFullPath(filePath)}");
            return records;
        }

        try
        {
            var lines = File.ReadAllLines(filePath);

            // Detect delimiter if not provided
            char actualDelimiter = delimiter ?? DetectDelimiter(lines);

            foreach (var line in lines.Skip(1)) // Skipping the header
            {
                var values = line.Split(actualDelimiter);
                records.Add(mapFunction(values));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading CSV file: {ex.Message}");
        }

        return records;
    }

    // Method to write a list of records to a CSV file
    public static void WriteCsv<T>(string filePath, List<T> records, Func<T, string[]> mapFunction, char delimiter = ';')
    {
        try
        {
            using (var writer = new StreamWriter(filePath))
            {
                foreach (var record in records)
                {
                    var line = string.Join(delimiter, mapFunction(record));
                    writer.WriteLine(line);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing to CSV file: {ex.Message}");
        }
    }
}
