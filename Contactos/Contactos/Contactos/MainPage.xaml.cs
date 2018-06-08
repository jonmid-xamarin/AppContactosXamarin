using Contactos.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Contactos
{
	public partial class MainPage : ContentPage
	{
        private const string UrlRoot = "https://apirestcontactos.herokuapp.com/";
        private const string UrlValidarUser = UrlRoot + "validateUser";
        private readonly HttpClient client = new HttpClient();


        public MainPage()
		{
			InitializeComponent();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (Application.Current.Properties.ContainsKey("id_user"))
            {
                ShowWindowListContacts();
            }
        }

        async private void ClickButtonSignIn(object sender, EventArgs e)
        {
            User user = new User(){Username = entryUser.Text, Password = entryPass.Text};

            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(UrlValidarUser, content);
            string message = await response.Content.ReadAsStringAsync();
            List<User> users = JsonConvert.DeserializeObject<List<User>>(message);

            if (users[0].Success)
            {
                Application.Current.Properties["id_user"] = users[0].Id;
                ShowWindowListContacts();
            }
            else
            {
                await DisplayAlert("Error", "El usuario no existe", "OK");
            }
        }

        async private void ClickButtonCreateUser(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new CreateUser());
        }

        async private void ShowWindowListContacts()
        {
            await Navigation.PushModalAsync(new ListContacts());
        }
    }
}
