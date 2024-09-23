using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ogrenciSistemi.Pages.Öğrenci
{
    public class IndexOgrenciModel : PageModel
    {
        public List<OgrenciInfo> listOgrenci = new List<OgrenciInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-1OMDOOA;Initial Catalog=tablolar;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Ögrenci";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                OgrenciInfo ogrenciInfo = new OgrenciInfo();
                                ogrenciInfo.id = "" + reader.GetInt32(0);
                                ogrenciInfo.ÖğrenciNo = "" + reader.GetInt32(1);
                                ogrenciInfo.İsim = reader.GetString(2);
                                ogrenciInfo.Cinsiyet = reader.GetString(3);
                                ogrenciInfo.Bölüm = reader.GetString(4);

                                listOgrenci.Add(ogrenciInfo);
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

    public class OgrenciInfo
    {
        public String id;
        public String ÖğrenciNo;
        public String İsim;
        public String Cinsiyet;
        public String Bölüm;
    }
}
