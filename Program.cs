using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ApiDGII1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a new HttpClient instance.
            HttpClient client = new HttpClient();

            // Set the Content-Type header to application/json.
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");

            // Set the Request URL.
            string requestUrl = "https://dgii.gob.do/ws/v2/rnc/consultar";

            // Create a new HttpRequestMessage instance.
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUrl);

            // Set the RNC number in the request body.
            request.Content = new StringContent("RNC=123456789-0", Encoding.UTF8, "application/json");

            // Send the request and get the response.
            HttpResponseMessage response = client.Send(request);

            // Check the response status code.
            if (response.StatusCode == HttpStatusCode.OK)
            {
                // The request was successful.
                string responseBody = response.Content.ReadAsStringAsync().Result;

                // Parse the response body as a JSON object.
                var responseData = JsonConvert.DeserializeObject<RNCResponse>(responseBody);

                // Print the RNC number and the name of the taxpayer.
                Console.WriteLine("RNC: {0}", responseData.RNC);
                Console.WriteLine("Name: {0}", responseData.Name);
            }
            else
            {
                // The request failed.
                Console.WriteLine("Request failed with status code {0}", response.StatusCode);
            }
        }
        public class RNCResponse
        {
            public string RNC { get; set; }
            public string Name { get; set; }
        }
    }
}
