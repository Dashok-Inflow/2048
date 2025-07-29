using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace _2048WindowsFormsApp
{
    public class FileProvider
    {
        public static string Path = "2048, результаты.json";
        public static string GetValue(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                var value = sr.ReadToEnd();
                return value;
            }
        }

        public static void AddToFile(List<User> usersResult, string path)
        {
            var jsonData = JsonConvert.SerializeObject(usersResult, Formatting.Indented);
            using (StreamWriter sw = new StreamWriter(path, false, Encoding.UTF8))
            {
                sw.WriteLine(jsonData);
            }
        }
    }
}
