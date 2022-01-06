using Newtonsoft.Json;
using OficiosApp.Responses;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace OficiosApp.Services
{
    public class ApiService
    {
        private async void GetTipoOficios()
        {
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://portaloficios.ddns.net/api/TipoOficios");
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");

            var client = new HttpClient();

            HttpResponseMessage response = await client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<TipoOficioResponse>>(content);

                //ListaOficios.ItemsSource = resultado;
            }
        } 
    }
}
