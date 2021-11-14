using Firmas_Xamarin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Firmas_Xamarin.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Listado : ContentPage
    {
        public Listado()
        {
            InitializeComponent();
            cargarListado();
        }

        public async void cargarListado()
        {
            var lista = await App.BaseDatos.ObtenerListaFirmas();
            listastfirmas.ItemsSource = lista;
        }

        private void lstfirmas_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            listastfirmas.SelectedItem = null;
        }

        private void lstfirmas_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }

        private async void VerDetalles(object sender, EventArgs e)
        {
            SwipeItem item = sender as SwipeItem;
            Firmas_personal model = item.BindingContext as Firmas_personal;
            //await DisplayAlert("Pais", model.nombre  , "ok");
            await Navigation.PushAsync(new DetallesFirmas(model));
        }
    }
}