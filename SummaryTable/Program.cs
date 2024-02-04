using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummaryTable
{
    class Program
    {
       
        static void Main(string[] args)
        {
            Console.WriteLine("Özet Tablo Botu Başladı Database'den Veri Çekliyor.");
            List<DataTable> AllData = DB.GetAllData();
            List<DataTable> Tavsiye = DB.GetTavsiye();
            List<DataTable> Markets = DB.GetMarkets();
            Console.WriteLine("Databaseden " + AllData[0].Rows.Count +" Adet Veri Çekildi.");
            List<Product> tables = TableCreat.GetCategories(AllData ,Tavsiye,Markets);
            Console.WriteLine("Excel Hazırlanıyor.");
            ConvertToDatatable.ConvertToDatatables(tables);
        }
       
    }
}
