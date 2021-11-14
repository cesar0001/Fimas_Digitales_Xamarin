using Firmas_Xamarin.Interface;
using Firmas_Xamarin.View;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace Firmas_Xamarin
{
    //informacion sacada de 
    //signatured
    //https://www.youtube.com/watch?v=Erwd0sX8JQE&ab_channel=ParallelCodes

    // guardar imagen en el diapositivo
    //https://stackoverflow.com/questions/51038251/how-to-download-image-and-save-it-in-local-storage-using-xamarin-forms

    public partial class MainPage : ContentPage
    {
        string base64Val;
        //static string DEFAULTPATH = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public MainPage()
        {
            InitializeComponent();
        }

        private async void BtnSubmit_Clicked(object sender, EventArgs e)
        {
            try
            {
                var image = await signature.GetImageStreamAsync(SignaturePad.Forms.SignatureImageFormat.Png);
                var mStream = (MemoryStream)image;
                byte[] data = mStream.ToArray();
                base64Val = Convert.ToBase64String(data);
                //lblBase64Value.Text = base64Val;
                //imgSignature.Source = ImageSource.FromStream(() => mStream);

                // imgSignature.Source = Xamarin.Forms.ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(base64Val)));
               

                //data:image/png;base64,
                //Guardar_Datos();

                /*
                DateTime date = DateTime.Now;
                string nString = "Pic" + date.Year.ToString() + date.Month.ToString() + date.Day.ToString() + date.Second.ToString() + date.Millisecond.ToString() + ".jpg";
                var absolutePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                var filePath = System.IO.Path.Combine(absolutePath, nString);

                Stream imagea = await signature.GetImageStreamAsync(SignaturePad.Forms.SignatureImageFormat.Png);


                using (FileStream file = new FileStream(filePath, FileMode.Create, System.IO.FileAccess.Write))
                {
                    imagea.CopyTo(file);
                }
                Console.WriteLine(filePath);
                */
                //DependencyService.Get<IPicture>().SavePicture("ImageName.jpg", mStream, "imagesFolder");

                
                var SeGuardoImagen = DependencyService.Get<IPicture>().GuardarImagen(mStream);
                if (SeGuardoImagen == true)
                {
                    bool ver = false;
                    if(String.IsNullOrWhiteSpace(txtDescripcion.Text)==true || String.IsNullOrWhiteSpace(txtNombre.Text) == true)
                    {
                        await DisplayAlert("Error", "Campos Vacios", "Ok");
                    }
                    else
                    {
                        Informacion();
                    }
                     
                }
                else
                {
                    await DisplayAlert("Alerta", "No se Pudo Guardar, Asegurese de tener los permisos para escribir imagen en este celular", "ok");
                }

            }
            catch (Exception ex)
            {

                await DisplayAlert("Error", "Porfavor Digite su Firma", "Ok");
            }
        }

        public async void Informacion()
        {
            var firma = new Model.Firmas_personal
            {
                base_64 = base64Val,
                nombre = txtNombre.Text,
                descripcion = txtDescripcion.Text 
            };

            var resultado = await App.BaseDatos.GrabarFirmas(firma);

            if (resultado == 1)
            {
                await DisplayAlert("Mensaje", "Registro exitoso!!!", "ok");
                txtDescripcion.Text = txtNombre.Text = base64Val = String.Empty;
                signature.Clear();
            }
            else
            {
                await DisplayAlert("Error", "No se pudo Guardar", "ok");
            }
        }

        public static async Task WriteStreamAsync(string path, Stream contents)
        {
            using (var writer = File.Create(path))
            {
                await contents.CopyToAsync(writer).ConfigureAwait(false);
            }
        }

        /*
        public static void SaveBytes(string fileName, byte[] data)
        {
            var filePath = Path.Combine(DEFAULTPATH, fileName);
            if (!File.Exists(filePath))
                File.Delete(filePath);
            File.WriteAllBytes(filePath, data);
        }
        */
        private async void btnlist_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Listado());
        }

         

    }
}
