using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Savchin.Core;
using Savchin.Text;

namespace TVSeriesTracker.Imdb
{
    internal interface IImdbManager
    {
        ImdbResult Search(ImdbRequest request);
        ImdbResult GetByIds(string[] ids);
        Task<ImdbResult> GetByIdsAsync(string[] ids);
    }

    class ImdbManager : IImdbManager
    {
        HttpClient client = new HttpClient { BaseAddress = new Uri("http://imdbapi.org/") };
        JsonSerializer ser = new JsonSerializer();
        public ImdbManager()
        {
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

        }

        public ImdbResult Search(ImdbRequest request)
        {

            var builder = new StringBuilder();
            builder.Append("?type=json&title=" + HttpUtility.UrlEncode(request.Text));
            if (request.Limit.HasValue)
                builder.Append("&limit=" + request.Limit.Value);
            if (request.Year.HasValue)
                builder.Append("&yg=1&year=" + request.Year.Value);
            if (request.Type != MovieType.None)
                builder.Append("&mt=" + request.Type.GetDescription());
            var query = builder.ToString();
            return ProcessQuery(query).Result;
        }

        public ImdbResult GetByIds(string[] ids)
        {
            return GetByIdsAsync(ids).Result;
        }

        public Task<ImdbResult> GetByIdsAsync(string[] ids)
        {
            return ProcessQuery("?type=json&ids=" + ids.Join(","));
        }

        private Task<ImdbResult> ProcessQuery(string query)
        {

            return client.GetAsync(query).ContinueWith(t =>
                     {
                         var response = t.Result;
                      

                         var result = new ImdbResult
                          {
                              IsSuccess = response.IsSuccessStatusCode,
                              StatusCode = response.StatusCode,
                              ReasonPhrase = response.ReasonPhrase,
                          };
                         if (!response.IsSuccessStatusCode)
                             return result;

                         return response.Content.ReadAsStringAsync().ContinueWith(t1 =>
                             {
                                 var text = t1.Result;
                                 try
                                 {
                                     result.Items = (MovieInfo[])ser.Deserialize(new StringReader(text), typeof(MovieInfo[]));
                                     return result;
                                 }
                                 catch
                                 {
                                     var error = (ErrorInfo)ser.Deserialize(new StringReader(text), typeof(ErrorInfo));
                                     return new ImdbResult
                                     {
                                         IsSuccess = false,
                                         StatusCode = (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), error.Code),
                                         ReasonPhrase = error.Error
                                     };
                                 }
                             }).Result;
                        


                     });





        }


    }
}
