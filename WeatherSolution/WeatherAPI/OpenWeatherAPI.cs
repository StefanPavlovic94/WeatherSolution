using RestSharp;
using System;



namespace WeatherAPI
{
    public static class OpenWeatherAPI
    {
        public static IRestResponse Get(string baseAddress, string resource, string query, string APIKey) { 

            IRestClient restClient = new RestClient(baseAddress);
            IRestRequest request = new RestRequest(baseAddress + resource + query + APIKey,Method.GET);
            request.RequestFormat = DataFormat.Json;
            IRestResponse response = restClient.Execute(request);
            return response;
        }
    }
}
