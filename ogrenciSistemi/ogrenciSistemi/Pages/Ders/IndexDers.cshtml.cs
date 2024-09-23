using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ogrenciSistemi.Pages.Öðrenci;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;

namespace ogrenciSistemi.Pages.Ders
{
    public class IndexDersModel : PageModel
    {
        public List<DersInfo> listDers = new List<DersInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-1OMDOOA;Initial Catalog=tablolar;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Ders";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader readerr = command.ExecuteReader())
                        {
                            while (readerr.Read())
                            {
                                DersInfo dersInfo = new DersInfo();
                                dersInfo.id = "" + readerr.GetInt32(0);
                                dersInfo.DersKodu = "" + readerr.GetInt32(1);
                                dersInfo.DersAdý = readerr.GetString(2);
                                dersInfo.AKTS = "" + readerr.GetInt32(3);

                                listDers.Add(dersInfo);
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

    public class DersInfo
    {
        public String id;
        public String DersKodu;
        public String DersAdý;
        public String AKTS;

    }
}
