using Newtonsoft.Json;
using Test.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Test.Services.ClientHTTP
{
    public class NewsClientHTTP : IServiceBaseHTTP<News>
    {
        public Task<ResultObject<News>> Add()
        {
            throw new NotImplementedException();
        }

        public Task<ResultObject<News>> Delete()
        {
            throw new NotImplementedException();
        }

        public Task<ResultObject<News>> Edit()
        {
            throw new NotImplementedException();
        }

        public  Task<ResultObject<News>> Get()
        {
            throw new NotImplementedException();
        }

        public async Task<ResultList<News>> GetList(string TipoSuscripcion,string Category,int Page)
        {
            ResultList<News> News = new ResultList<News>();
            try
            {
                string ApiKey = "pub_759ddc95e77750910f9f2a8c268cb13b3ac";
                string Uri = "";
                switch (TipoSuscripcion)
                {
                    case "Basic":
                        Uri = $"https://newsdata.io/api/1/news?apikey={ApiKey}&category={Category}&page={Page}&language=es";
                        break;
                    case "Premium":
                        Uri = $"https://newsdata.io/api/1/news?apikey={ApiKey}&category={Category}&page={Page}&language=es";
                        break;
                    case "Gold":
                        Uri = $"https://newsdata.io/api/1/news?apikey={ApiKey}&category={Category}&page={Page}&language=es";
                        break;
                    default:
                        break;
                }

                HttpClientHandler httpClientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; }
                };

                using (HttpClient client = new HttpClient(httpClientHandler))
                {
                    client.BaseAddress = new Uri("https://newsdata.io/api/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var uri = new Uri(Uri);
                    HttpResponseMessage response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        var tt = JsonConvert.DeserializeObject<object>(result.ToString());
                        var ttNews = JsonConvert.DeserializeObject<NewsResult>(result.ToString());

                        News.Success = true;
                        News.Data = ttNews.results;
                    }
                }
            }
            catch (Exception e)
            {
                News.Data = null ;
                News.Error= e;
                News.Success = false;
                News.Mensaje = e.Message.ToString(); 
            }
            return News;
        }
    }
}