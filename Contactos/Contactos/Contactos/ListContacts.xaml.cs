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
using Xamarin.Forms.Xaml;

namespace Contactos
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListContacts : ContentPage
	{
        private const string UrlRoot = "https://apirestcontactos.herokuapp.com/";
        private const string UrlListContact = UrlRoot + "contacts";
        private const string UrlDeleteContact = UrlRoot + "contacts";
        private readonly HttpClient client = new HttpClient();
        private ObservableCollection<Contact> _contact;


        public ListContacts ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ListDataContacts();
        }

        // Metodo para listar todos los contactos
        async public void ListDataContacts()
        {
            string content = await client.GetStringAsync(UrlListContact);
            List<Contact> contacts = JsonConvert.DeserializeObject<List<Contact>>(content);
            _contact = new ObservableCollection<Contact>(contacts);
            listViewContacts.ItemsSource = _contact;
        }

        public void ClickUpdateContact(object sender, EventArgs e)
        {
            var mi = sender as MenuItem;
            var item = mi.BindingContext as Contact;

            Contact contact = new Contact()
            {
                Id = item.Id,
                Name = item.Name,
                Phone = item.Phone,
                Email = item.Email,
                Image = item.Image
            };

            showWindowUpdateContact(contact);
        }

        async public void showWindowUpdateContact(Contact contact)
        {
            await Navigation.PushModalAsync(new UpdateContact(contact));
        }

        public void ClickDeleteContact(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            DeleteContact(mi.CommandParameter.ToString());
        }

        // Metodo para eliminar un contacto
        async public void DeleteContact(string position)
        {
            HttpResponseMessage response = null;
            response = await client.DeleteAsync(UrlDeleteContact + "/" + position);

            ListDataContacts();
        }

        async public void ClickButtonShowWindowNewContact(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new CreateContact());
        }
    }
}