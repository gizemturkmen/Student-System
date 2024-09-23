using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ogrenciSistemi.Pages.Öğrenci;
using System.Data.SqlClient;

namespace ogrenciSistemi.Pages.Fakülte
{
    public class IndexFakulteModel : PageModel
    {
        public List<FakulteInfo> listFakulte = new List<FakulteInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-1OMDOOA;Initial Catalog=tablolar;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Fakülte";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                FakulteInfo fakulteInfo = new FakulteInfo();
                                fakulteInfo.id = "" + reader.GetInt32(0);
                                fakulteInfo.FakülteKodu = "" + reader.GetInt32(1);
                                fakulteInfo.FakülteAdı = reader.GetString(2);
                                fakulteInfo.BölümSayısı = "" + reader.GetInt32(3);

                                listFakulte.Add(fakulteInfo);
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

    public class FakulteInfo
    {
        public String id;
        public String FakülteKodu;
        public String FakülteAdı;
        public String BölümSayısı;
    }
}
