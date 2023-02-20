using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CsvHelper;
using Newtonsoft.Json;

namespace Dte.Common.Helpers
{
    public static class FileHelper
    {
        public static async Task<string> GetEmbeddedResource(Assembly assembly, string fileName, string resourceFolder = "Resources")
        {
            var specifiedAssembly = assembly ?? Assembly.GetExecutingAssembly();
            var filePath = $"{specifiedAssembly.GetName().Name}.{resourceFolder}.{fileName}";
            var stream = specifiedAssembly.GetManifestResourceStream(filePath);

            if (stream == null)
            {
                throw new Exception($"{filePath} - Not Found in assembly {specifiedAssembly.FullName}");    
            }

            using var reader = new StreamReader(stream);
            return await reader.ReadToEndAsync();
        }

        public static IReadOnlyCollection<T> LoadCsvFile<T>(string filePath) where T: class
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            //csv.Configuration.PrepareHeaderForMatch = (header, index) => header.ToLower();
            // csv.Configuration.BadDataFound = null;
            return csv.GetRecords<T>().ToList();
        }

        public static T LoadJson<T>(string filePath) where T : class
        {
            using var reader = new StreamReader(filePath);
            return JsonConvert.DeserializeObject<T>(reader.ReadToEnd());
        }

        public static string LoadString(string filePath)
        {
            return File.ReadAllText(filePath);
        }

        public static async Task<string> LoadStringAsync(string filePath)
        {
            return await File.ReadAllTextAsync(filePath);
        }
    }
}