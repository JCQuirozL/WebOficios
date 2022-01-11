using Newtonsoft.Json;
using OficiosApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OficiosApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddOficioPage : ContentPage
    {
        public AddOficioPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();


            GetTipoOficios();
            GetDirecciones();

        }
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
                var resultado = JsonConvert.DeserializeObject<List<GetTipoOficio>>(content);

                pTipo.ItemsSource = resultado;
                
                //ListaOficios.ItemsSource = resultado;
            }
        }

        private async void GetDirecciones()
        {
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://portaloficios.ddns.net/api/Direcciones");
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");

            var client = new HttpClient();

            HttpResponseMessage response = await client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<GetDirecciones>>(content);

                pDireccion.ItemsSource = resultado;

                //ListaOficios.ItemsSource = resultado;
            }
        }
    }
}