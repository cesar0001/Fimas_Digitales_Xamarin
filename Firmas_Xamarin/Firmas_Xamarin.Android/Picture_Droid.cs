using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firmas_Xamarin.Droid;
using Firmas_Xamarin.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

[assembly: Xamarin.Forms.Dependency(typeof(Picture_Droid))]
namespace Firmas_Xamarin.Droid
{
    public class Picture_Droid : IPicture
    {

        public bool GuardarImagen( Stream data)
        {
            if (MainActivity.Instance.CheckSelfPermission(Manifest.Permission.WriteExternalStorage) == Android.Content.PM.Permission.Granted)
            {
                DateTime date = DateTime.Now;
                string  filename= "Imagen" + date.Year.ToString() + date.Month.ToString() + date.Day.ToString() + date.Second.ToString() + date.Millisecond.ToString() + ".jpg";

            
                //System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                //con el codigo de arriba en teoria deberia de guardar la imagen en esa ruta
                //var documentsPath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads).AbsolutePath;
                var documentsPath = Android.App.Application.Context.GetExternalFilesDir("").AbsolutePath;
                //con documentsPath guarda la imagen en donde esta la app

                documentsPath = Path.Combine(documentsPath, "Imagenes_Firmas");
                Directory.CreateDirectory(documentsPath);

                string filePath = Path.Combine(documentsPath, filename);

                byte[] bArray = new byte[data.Length];
                using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
                {
                    using (data)
                    {
                        data.Read(bArray, 0, (int)data.Length);
                    }
                    int length = bArray.Length;
                    fs.Write(bArray, 0, length);
                }
                return true;
            }
            else
            {
                MainActivity.Instance.RequestPermissions(new string[] { Manifest.Permission.WriteExternalStorage }, 0);
                return false;
            }

        }
    }
}