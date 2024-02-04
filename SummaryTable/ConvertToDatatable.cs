using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummaryTable
{
    public static class ConvertToDatatable
    {
        public static string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        public static string path = string.Empty;
        public static string PROJECT_NAME = "Özet Tablo";
        public static List<Product> ConvertToDatatables(List<Product>tables)
        {
            try
            {
                DataTable dt = new DataTable();

                string date = DateTime.Now.ToString("yyyy-MM-dd");
                if (DateTime.Now.Hour >= 0 && DateTime.Now.Hour <= 3)
                {
                    date = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                }

                dt.Columns.Add("SEGMENT");
                dt.Columns.Add("KATEGORİ");
                dt.Columns.Add("ALT KATEGORİ");
                dt.Columns.Add("SKU");
                dt.Columns.Add("MİKTAR(KG/LT)");
                dt.Columns.Add("TAVSİYE RAF FİYATI");
                dt.Columns.Add("FİYAT GÖRÜLEN NOKTA SAYISI");
                dt.Columns.Add("PENETRASYON ORANI");
                dt.Columns.Add("MİNUMUM FİYAT");
                dt.Columns.Add("ORTALAMA FİYAT");
                dt.Columns.Add("MAKSİMUM FİYAT");
                dt.Columns.Add("MOD FİYAT");
                dt.Columns.Add("MOD FİYAT PENETRASYONU");
                dt.Columns.Add("ORTALAMA KG FİYATI");
                dt.Columns.Add("MOD FİYAT KG FİYATI");
                dt.Columns.Add("TAVSİYE RAF FİYATININ ÜSTÜNDEKİ NOKTA SAYISI");
                dt.Columns.Add("TAVSİYE RAF FİYATININ ALTINDAKİ NOKTA SAYISI");
                dt.Columns.Add("TAVSİYE RAF FİYATINA UYUM ORANI");
                dt.Columns.Add("FİYAT SAĞLIĞI");
                dt.Columns.Add("DEĞERLENDİRME");


                string botName = "";


                foreach (var item in tables.Distinct().ToList())
                {
                    var row = dt.NewRow();

                    row["SEGMENT"] = item.Segment;
                    row["KATEGORİ"] = item.AltKategori;
                    row["ALT KATEGORİ"] = item.AltKategori2;
                    row["SKU"] = item.Sku;
                    row["MİKTAR(KG/LT)"] = item.Miktar;
                    if (item.TavsiyeFiyat > 0)
                    {
                        row["TAVSİYE RAF FİYATI"] = item.TavsiyeFiyat;
                    }
                    else
                    {
                        row["TAVSİYE RAF FİYATI"] = string.Empty;
                    }
                    row["FİYAT GÖRÜLEN NOKTA SAYISI"] = item.MarketSayisi;
                    row["PENETRASYON ORANI"] = item.Penetrasyon;
                    row["MİNUMUM FİYAT"] = item.MinFiyat;
                    row["ORTALAMA FİYAT"] = Math.Round(item.OrtFiyat,2);
                    row["MAKSİMUM FİYAT"] = item.MaxFiyat;
                    row["MOD FİYAT"] = item.ModFiyat;
                    row["MOD FİYAT PENETRASYONU"] = Math.Round(item.ModFiyatPenetrasyon,2);
                    row["ORTALAMA KG FİYATI"] = Math.Round(item.OrtKgFiyat,2);
                    row["MOD FİYAT KG FİYATI"] = Math.Round(item.ModFiyatKg,2);
                    if (item.TavsiyeFiyat > 0)
                    {
                        row["TAVSİYE RAF FİYATININ ÜSTÜNDEKİ NOKTA SAYISI"] = item.TavsiyeFiyatUst;
                    }
                    else
                    {
                        row["TAVSİYE RAF FİYATININ ÜSTÜNDEKİ NOKTA SAYISI"] = string.Empty;
                    }

                    if (item.TavsiyeFiyat > 0)
                    {
                        row["TAVSİYE RAF FİYATININ ALTINDAKİ NOKTA SAYISI"] = item.TavsiyeFiyatAlt;
                    }
                    else
                    {
                        row["TAVSİYE RAF FİYATININ ALTINDAKİ NOKTA SAYISI"] = string.Empty;
                    }
                    if (item.TavsiyeFiyat > 0)
                    {
                        row["TAVSİYE RAF FİYATINA UYUM ORANI"] = Math.Round(item.TavsiyeFiyatUyum,2);
                    }
                    else
                    {
                        row["TAVSİYE RAF FİYATINA UYUM ORANI"] = string.Empty;
                    }
                    row["FİYAT SAĞLIĞI"] = item.FiyatSagligi;
                    row["DEĞERLENDİRME"] = item.Degerlendirme;

                    dt.Rows.Add(row);
                }

                try
                {
                    Random r = new Random();

                    path = folderPath + PROJECT_NAME + " " + System.DateTime.Now.ToString("d MMMM yyyy dddd HH.mm.ss") + "-" + r.Next(1, 999999) + ".xlsx";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    ErrorHelper error = new ErrorHelper();
                    error.ErrorWriteFile(ex, folderPath, "excelPathError.txt", PROJECT_NAME);

                }

                bool isSuccess = false;

                #region Excel Çıkarma
                try
                {
                    GC.Collect();
                    Pum_Excel_Management.ExportToExcel(path, dt, true, false);
                    isSuccess = true;
                }
                catch (Exception ex)
                {
                    isSuccess = false;
                    ErrorHelper error = new ErrorHelper();
                    error.ErrorWriteFile(ex, folderPath, "excelError.txt", PROJECT_NAME);
                }
                #endregion

                System.Diagnostics.Process.Start(folderPath);
                Console.Clear();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return (tables);
        }
        
    }
}
