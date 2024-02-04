using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummaryTable
{
    public  class Product
    {
        public  string Segment { get; set; }
        public  string AltKategori { get; set; }

        public  string AltKategori2 { get; set; }

        public  string Sku { get; set; }

        public  double Miktar { get; set; }
              
        public  double TavsiyeFiyat { get; set; }
            
        public  double MarketSayisi { get; set; }
          
        public  double Penetrasyon { get; set; }
             
        public  double MinFiyat { get; set; }
               
        public  double MaxFiyat { get; set; }
              
        public  double OrtFiyat { get; set; }
              
        public  double ModFiyat { get; set; }
              
        public  double ModFiyatPenetrasyon { get; set; }
                
        public  double OrtKgFiyat { get; set; }
                
        public  double ModFiyatKg { get; set; }

        public  double TavsiyeFiyatAlt { get; set; }
              
        public  double TavsiyeFiyatUst { get; set; }
            
        public  double TavsiyeFiyatUyum { get; set; }
               
        public  double FiyatSagligi { get; set; }
               
        public  string Degerlendirme { get; set; }
    }          
}
