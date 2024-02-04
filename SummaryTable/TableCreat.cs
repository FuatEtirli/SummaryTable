using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummaryTable
{
    public static class TableCreat
    {
        public static List<Product> GetCategories(List<DataTable>AllData ,List<DataTable>Tavsiye,List<DataTable>Markets)
        {
            List<Product> Products = new List<Product>();
            DataTable Cats = new DataTable();
            DataTable TavsiyeFiyat = Tavsiye[0].Copy();
            DataTable markets = Markets[0].Copy();
            Cats = AllData[0].Copy();
         
            var pen = Cats.AsEnumerable().Select(x => x.ItemArray[13]).ToList().Distinct().Count();

            var pen2 = markets.AsEnumerable().Select(x => x.ItemArray[4]).ToList().Count();

            var grups = Cats.AsEnumerable().OrderBy(x => x.ItemArray[2]).ToList().AsEnumerable().OrderBy(x => x.ItemArray[1]).ToList().AsEnumerable().OrderBy(x => x.ItemArray[0]).ToList();

            var grup = grups.AsEnumerable().GroupBy(x =>x.ItemArray[6]).ToList();

            foreach (var item in grup)
            {
                Product pr = new Product();
                var modmarkets =item.AsEnumerable().Select(x=>x.ItemArray[13]).ToList();
                var ort = item.AsEnumerable().Select(x => x.ItemArray[9]).ToList();
                var fiyatnokta = item.AsEnumerable().Select(x => x.ItemArray[13]).ToList();
                List<double> sayi = new List<double>();
                foreach (var item2 in ort)
                {

                    sayi.Add(Convert.ToDouble(item2));
                }
                pr.OrtFiyat = sayi.Average();
                                 
                pr.MinFiyat = sayi.Min();
                                 
                pr.MaxFiyat = sayi.Max();

                var mod = sayi.GroupBy(x => x).OrderByDescending(g => g.Count()).First();
                pr.ModFiyat = mod.Key;

                pr.ModFiyatPenetrasyon = (mod.Count() * 100)/modmarkets.Count();

                pr.Penetrasyon = (ort.Count() * 100) / pen2;

                double kg = 0;
                try
                {
                    var miktar = item.AsEnumerable().Select(x => x.ItemArray[8]).ToList()[1];
                    pr.Miktar = Convert.ToDouble(miktar);
                    kg = 1000 / Convert.ToDouble(miktar);
                }
                catch (Exception)
                {
                    pr.Miktar = 0;
                }
                 
                pr.OrtKgFiyat = pr.OrtFiyat * kg;

                pr.ModFiyatKg = pr.ModFiyat * kg;

                pr.MarketSayisi = fiyatnokta.Count();

                pr.Sku = item.Key.ToString();

                var kat = item.AsEnumerable().Select(x => x.ItemArray[0].ToString());

                pr.Segment = kat.FirstOrDefault();

                var altkat = item.AsEnumerable().Select(x => x.ItemArray[1].ToString());

                pr.AltKategori = altkat.FirstOrDefault();

                var altkat2 = item.AsEnumerable().Select(x => x.ItemArray[2].ToString());

                pr.AltKategori2 = altkat2.FirstOrDefault();

                var eslesme = item.AsEnumerable().Select(x => x.ItemArray[5]).ToList();

               
                foreach (DataRow t in TavsiyeFiyat.Rows)
                {
                    if (t.ItemArray[0].Equals(eslesme[0]))
                    {
                      pr.TavsiyeFiyat = Convert.ToInt32(t.ItemArray[1]);
                    }
               
                }
                List<double> ust = new List<double>();
                List<double> alt = new List<double>();
                List<double> esit = new List<double>();
                foreach (var price in sayi)
                {
                    if (price < pr.TavsiyeFiyat)
                    {
                        alt.Add(price);
                    }
                    else if (price > pr.TavsiyeFiyat)
                    {
                        ust.Add(price);
                    }

                    else
                    {
                        esit.Add(price);
                    }
                }
                if (pr.TavsiyeFiyat > 0)
                {
                    pr.TavsiyeFiyatAlt = alt.Count();

                    pr.TavsiyeFiyatUst = ust.Count();

                    pr.TavsiyeFiyatUyum = ((pr.OrtFiyat - pr.TavsiyeFiyat)/pr.TavsiyeFiyat) * 100;
                }

                double sap = 0;

                foreach (double i in sayi)
                {
                    sap += Math.Pow(i - pr.OrtFiyat , 2);
                }
           

                double  sap1 = Math.Sqrt(sap / sayi.Count());

                double sap2 = Math.Round((sap1 / pr.OrtFiyat) * 100, 2);

                pr.FiyatSagligi = sap2;

                if (sap2 >= 0 && sap2 <= 6)
                {
                    pr.Degerlendirme = "Üst-Üst Sağlık";
                }

                else if (sap2 >= 6.01 && sap2 <= 12)
                {
                    pr.Degerlendirme = "Üst-Alt Sağlık";
                }

                else if (sap2 >= 12.01 && sap2 <= 18)
                {
                    pr.Degerlendirme = "Orta-Üst Sağlık";
                }

                else if (sap2 >= 18.01 && sap2 <= 24)
                {
                    pr.Degerlendirme = "Orta-Alt Sağlık";
                }

                else if (sap2 >= 24.01 && sap2 <= 30)
                {
                    pr.Degerlendirme = "Alt-Üst Sağlık";
                }

                else if (sap2 >= 30.01)
                {
                    pr.Degerlendirme = "Alt-Alt Sağlık";
                }

                Products.Add(pr);
            }
        

            return Products;
        }
    }
}
       