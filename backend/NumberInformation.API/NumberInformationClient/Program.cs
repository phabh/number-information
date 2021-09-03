using NumberInformation.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;

namespace NumberInformationClient
{
    public class Program
    {
        const string BASE_URI = "https://localhost:5001/api/NumberInformation/";
        static void Main(string[] args)
        {

            Console.WriteLine("Olá!");
            var readKeys = "";
            long number = 0L;
            while (readKeys?.ToLowerInvariant() != "q")
            {
                Console.WriteLine("Digite um número para coletar as informações dele ou q para sair");
                readKeys = Console.ReadLine();

                if(readKeys?.ToLowerInvariant() == "q")
                {
                    break;
                }

                if (!long.TryParse(readKeys, out number))
                {
                    continue;
                }

                HttpResponseMessage response = ProcessNumber(number);

                readKeys = Console.ReadLine();
            }
            

        }

        private static HttpResponseMessage ProcessNumber(long number)
        {
            var response = Request(number.ToString());
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(content);
            }

            if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                var taskId = JsonSerializer.Deserialize<string>(response.Content.ReadAsStringAsync().Result);
                var code = HttpStatusCode.NoContent;
                var content = new NIResponse();
                while (code == HttpStatusCode.NoContent)
                {
                    using HttpResponseMessage responseTask = Request($"task/{taskId}");
                    code = responseTask.StatusCode;
                    Console.Clear();
                    Console.WriteLine("Processando");
                    Thread.Sleep(1000);
                    if (responseTask.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var contentTask = responseTask.Content.ReadAsStringAsync().Result;
                        Console.WriteLine(contentTask);
                    }
                }
            }

            return response;
        }

        private static HttpResponseMessage Request(string param)
        {
            using var client = new HttpClient();
            using var request = new HttpRequestMessage();
            request.Method = new HttpMethod("GET");
            request.RequestUri = new Uri(BASE_URI + param);
            var response = client.Send(request);
            return response;
        }
    }
}
