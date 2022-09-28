using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;

namespace RestaurantChainApp.Integration.Tests
{
    public class ApiCaller
    {
        private readonly JsonSerializerSettings jsonSerializerSettings;

        public ApiCaller()
        {
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };
            this.jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            };
        }

        private RestRequest CreateRequest(Uri uri, Method methodVerb)
        {
            RestRequest Request = new RestRequest(uri, methodVerb);

            Request.AddHeader("cache-control", "no-cache");
            Request.AddHeader("content-type", "application/json");
            return Request;
        }

        private string MakeParametersPartOfUrl(object[] parameters)
        {
            string result = string.Empty;

            foreach (var parameter in parameters)
            {
                result = $"{result}/{parameter.ToString()}";
            }

            return result;
        }


        public string Get(string url, params object[] parameters)
        {
            string parametersPart = MakeParametersPartOfUrl(parameters);
            url = $"{url}{parametersPart}";
            RestClient client = new RestClient(url);
            RestRequest request = CreateRequest(new Uri(url), Method.Get);

            RestResponse response = client.Execute(request);

            return response.Content;
        }

        public RestResponse Post<T>(string url, T parameter)
        {
            RestClient client = new RestClient(url);
            RestRequest request = CreateRequest(new Uri(url), Method.Post);

            var json = JsonConvert.SerializeObject(parameter, this.jsonSerializerSettings);
            request.AddParameter("application/json", json, ParameterType.RequestBody);

            RestResponse response = client.Execute(request);

            return response;
        }
        public RestResponse Put<T>(string url, T parameter)
        {
            RestClient client = new RestClient(url);
            RestRequest request = CreateRequest(new Uri(url), Method.Put);

            var json = JsonConvert.SerializeObject(parameter, this.jsonSerializerSettings);
            request.AddParameter("application/json", json, ParameterType.RequestBody);

            RestResponse response = client.Execute(request);

            return response;
        }
    }
}
