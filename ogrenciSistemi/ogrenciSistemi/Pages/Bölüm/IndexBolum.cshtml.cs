using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ogrenciSistemi.Pages.Bölüm
{
    public class IndexBolumModel : PageModel
    {
        public List<BolumInfo> listBolum = new List<BolumInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-1OMDOOA;Initial Catalog=tablolar;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Bölüm";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                BolumInfo bolumInfo = new BolumInfo();
                                bolumInfo.id = "" + reader.GetInt32(0);
                                bolumInfo.BölümKodu = "" + reader.GetInt32(1);
                                bolumInfo.BölümAdı = reader.GetString(2);

                                listBolum.Add(bolumInfo);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());

            }
        }
    }

    public class BolumInfo
    {
        public String id;
        public String BölümKodu;
        public String BölümAdı;

    }
}
