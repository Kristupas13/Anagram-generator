using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AnagramGenerator.HttpRequest
{
    class Program
    {
        static readonly HttpClient client = new HttpClient();

        static async Task Main()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:44300/api/GetAnagrams/jonukas%20suvalge%20daug%20ledu");

                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                

                Console.WriteLine(responseBody);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }
    }
}
