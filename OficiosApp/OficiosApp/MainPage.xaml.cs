using Newtonsoft.Json;
using OficiosApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OficiosApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            
            BtnAgregar.Clicked += (sender, e) =>
            {
                Navigation.PushAsync(new AddOficioPage());
            };

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();


            LoadOficios();
            
        }

        private void BtnAgregar_Clicked(object sender, EventArgs e)
        {

        }

        private async void LoadOficios()
        {
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://portaloficios.ddns.net/api/Oficios");
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");

            var client = new HttpClient();

            HttpResponseMessage response = await client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<GetOficios>>(content);

                ListaOficios.ItemsSource = resultado;
            }
        }

        private async void BtnOficios_Clicked(object sender, EventArgs e)
        {
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://portaloficios.ddns.net/api/Oficios");
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");

            var client = new HttpClient();

            HttpResponseMessage response=await client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<GetOficios>>(content);

                ListaOficios.ItemsSource = resultado;
            }

        }
    }
}
