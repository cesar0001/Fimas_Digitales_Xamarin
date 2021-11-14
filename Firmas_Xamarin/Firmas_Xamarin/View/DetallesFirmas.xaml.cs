using Firmas_Xamarin.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Firmas_Xamarin.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetallesFirmas : ContentPage
    {
        public DetallesFirmas(Firmas_personal firmas_personal)
        {
            InitializeComponent();
            lblnombre.Text = firmas_personal.nombre;
            lbldescripcion.Text = firmas_personal.descripcion;
             imgSignature.Source = Xamarin.Forms.ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(firmas_personal.base_64)));



        }
    }
}