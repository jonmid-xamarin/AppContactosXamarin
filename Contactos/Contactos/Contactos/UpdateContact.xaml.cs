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
	public partial class UpdateContact : ContentPage
	{
        private const string UrlRoot = "https://apirestcontactos.herokuapp.com/";
        private const string UrlUpdateContact = UrlRoot + "contacts";
        private readonly HttpClient client = new HttpClient();
        private Contact contact;


        public UpdateContact (Contact contact)
		{
			InitializeComponent ();
            this.contact = contact;
            BindingContext = contact;
        }

        // Metodo para actualizar un contacto
        async public void ClickButtonUpdateContact(object sender, EventArgs e)
        {
            contact.Name = entryNameUc.Text;
            contact.Phone = entryPhoneUc.Text;
            contact.Email = entryEmailUc.Text;

            var json = JsonConvert.SerializeObject(contact);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            response = await client.PutAsync(UrlUpdateContact + "/" + contact.Id, content);

            await Navigation.PushModalAsync(new ListContacts());
        }
    }
}