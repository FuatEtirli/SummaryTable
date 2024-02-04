using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummaryTable
{
    public class DB
    {
        public static List<DataTable> GetAllData()
        {
            List<DataTable> readyDtList = new List<DataTable>();

            string connectionString = $"Data Source=;Initial Catalog=;User ID=;Password=;MultipleActiveResultSets=True;Connection Timeout=0";

            DataTable readyDt = new DataTable();
                   
            string mainQuery = $"SELECT * FROM ProducerData";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(mainQuery, connection))
                {
                    command.CommandTimeout = 0;
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    da.Fill(readyDt);
                }

                connection.Close();
            }

            readyDtList.Add(readyDt);

            return readyDtList;
        }
        public static List<DataTable> GetTavsiye() 
        {
            List<DataTable> readyDtList2 = new List<DataTable>();

            string connectionString = $"Data Source=;Initial Catalog=;User ID=;Password=;MultipleActiveResultSets=True;Connection Timeout=0";
            DataTable tavsiye = new DataTable();


            string mainQuery2 = "SELECT * FROM Tavsiye WHERE TavsiyeFiyat is not null";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(mainQuery2, connection))
                {
                    command.CommandTimeout = 0;
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    da.Fill(tavsiye);
                }

                connection.Close();
            }

           
            readyDtList2.Add(tavsiye);


            return readyDtList2;
        }

        public static List<DataTable> GetMarkets() 
        {
            List<DataTable> readyDtList3 = new List<DataTable>();

            string connectionString = $"Data Source=;Initial Catalog=;User ID=;Password=;MultipleActiveResultSets=True;Connection Timeout=0";
            DataTable Markets = new DataTable();


            string mainQuery3 = "SELECT * FROM Markets";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(mainQuery3, connection))
                {
                    command.CommandTimeout = 0;
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    da.Fill(Markets);
                }

                connection.Close();
            }


            readyDtList3.Add(Markets);


            return readyDtList3;

        }

    }
}

