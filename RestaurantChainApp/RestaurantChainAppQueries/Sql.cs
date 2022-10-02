using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;


namespace RestaurantChainAppQueries
{
    public static class Sql
    {
        public static Dictionary<string, string> Queries = new Dictionary<string, string>();

        public static void Load(bool debugMode = false)
        {


            List<Query> queries = new List<Query>();
            string queriesFilePath = debugMode ? "..\\..\\Sql\\queries.json" :
                                                 "..\\..\\..\\..\\..\\Sql\\queries.json";
            using (StreamReader streamReader = new StreamReader(queriesFilePath))
            {
                string json = streamReader.ReadToEnd();
                queries = JsonConvert.DeserializeObject<List<Query>>(json);
            }



            queries.ForEach(query => Queries.Add(query.Name, query.Value));
        }
    }
}
