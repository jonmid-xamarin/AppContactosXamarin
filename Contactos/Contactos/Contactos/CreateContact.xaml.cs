using Contactos.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contactos
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CreateContact : ContentPage
	{
        private const string UrlRoot = "https://apirestcontactos.herokuapp.com/";
        private const string UrlCreateContact = UrlRoot + "contacts";
        private readonly HttpClient client = new HttpClient();


        public CreateContact ()
		{
			InitializeComponent ();
		}

        public void ClickButtonCreateContact(object sender, EventArgs e)
        {
            CreateNewContact();
        }

        async public void CreateNewContact()
        {
            Contact contact = new Contact()
            {
                Name = entryNameCc.Text,
                Phone = entryPhoneCc.Text,
                Email = entryEmailCc.Text
            };

            var json = JsonConvert.SerializeObject(contact);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(UrlCreateContact, content);
            string message = await response.Content.ReadAsStringAsync();

            await Navigation.PushModalAsync(new ListContacts());
        }
    }
}