using Dataflow.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace Dataflow.Services
{
    public class HttpClientProvider : IHttpClientProvider
    {
        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("auth-token", "8e4c46fe-5e1d-4382-b7fc-19541f7bf3b0");
                var resp = await client.GetAsync(url);

                resp.EnsureSuccessStatusCode();

                return resp;
            }
        }
    }
}
