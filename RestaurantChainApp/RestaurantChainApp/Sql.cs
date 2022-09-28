using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace RestaurantChainApp
{
    public static class Sql
    {
        public static Dictionary<string, string> Queries = new Dictionary<string, string>();

        public static void Load() 
        {
            List<Query> queries = new List<Query>();

            using (StreamReader streamReader = new StreamReader("..\\..\\Sql\\queries.json"))
            {
                string json = streamReader.ReadToEnd();
                queries = JsonConvert.DeserializeObject<List<Query>>(json);
            }

            queries.ForEach(query => Queries.Add(query.Name, query.Value));
        }
    }
}
