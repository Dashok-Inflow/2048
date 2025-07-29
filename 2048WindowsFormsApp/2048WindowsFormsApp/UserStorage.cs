using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace _2048WindowsFormsApp
{
    public class UserStorage
    {
        public static string Path = "2048, результаты.json";

        public static void SaveResults(User user)
        {
            var usersResult = UserStorage.GetUsersResults();
            usersResult.Add(user);
            FileProvider.AddToFile(usersResult,Path);
        }
        public static List<User> GetUsersResults()
        {
            if (!File.Exists(Path))
            {
                var usersResult = new List<User>();
                return usersResult;
            }
            var value = FileProvider.GetValue(Path);
            var usersResults = JsonConvert.DeserializeObject<List<User>>(value);
            return usersResults;
        }
    }
}
