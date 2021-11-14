using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Firmas_Xamarin.Model;
using SQLite;

namespace Firmas_Xamarin.Controller
{
   public class DataBaseSQLite
    {

        readonly SQLiteAsyncConnection db;
 
        //constructor de la clase DataBaseSQLite
        public DataBaseSQLite(string pathdb)
        {
             db = new SQLiteAsyncConnection(pathdb);
            db.CreateTableAsync<Firmas_personal>().Wait();
        }

        //Operaciones crud de sqlite
        //Read List way
        public Task<List<Firmas_personal>> ObtenerListaFirmas()
        {
            return db.Table<Firmas_personal>().ToListAsync();
        }

        //read one by one 
        public Task<Firmas_personal> ObtenerFirmas(int pcodigo)
        {
            return db.Table<Firmas_personal>()
                .Where(i => i.codigo == pcodigo)
                .FirstOrDefaultAsync();
        }

        //Create o update personas
        public Task<int> GrabarFirmas(Firmas_personal firmas)
        {
            if (firmas.codigo != 0)
            {
               return db.UpdateAsync(firmas);
            }
            else
            {
                return db.InsertAsync(firmas);
            }
            
        }

  

        //delete
        public Task<int> EliminarFirmas(Firmas_personal firmas)
        {
            return db.DeleteAsync(firmas);
        }


    }
}
