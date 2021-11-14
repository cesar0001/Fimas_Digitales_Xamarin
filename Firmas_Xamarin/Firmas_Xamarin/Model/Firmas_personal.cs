using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Firmas_Xamarin.Model
{
    public class Firmas_personal
    {

        [PrimaryKey, AutoIncrement]
        public int codigo { get; set; }

        public string base_64 { get; set; }

        [MaxLength(200)]
        public string nombre { get; set; }

        [MaxLength(200)]
        public string descripcion { get; set; }

    }
}
