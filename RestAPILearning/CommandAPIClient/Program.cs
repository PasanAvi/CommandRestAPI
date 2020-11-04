using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace CommandAPIClient
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            await ProcessRepos();
        }

        private static async Task ProcessRepos()
        {

            client.DefaultRequestHeaders.Accept.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));

            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var StringTask = client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");

            //var msg = await StringTask;

            //Console.WriteLine(msg);

            var repos = await JsonSerializer.DeserializeAsync<List<CommandRepo>>(await StringTask);


            foreach (var repo in repos)
            {
                
                Console.WriteLine(repo.Name);
            }



        }
    }
}
