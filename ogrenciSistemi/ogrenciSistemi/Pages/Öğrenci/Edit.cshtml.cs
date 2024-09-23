using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ogrenciSistemi.Pages.Öğrenci
{
    public class EditModel : PageModel
    {
        public OgrenciInfo ogrenciInfo = new OgrenciInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];
            try
            {
                String connectionString = "Data Source=DESKTOP-1OMDOOA;Initial Catalog=tablolar;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Ögrenci WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                ogrenciInfo.id = "" + reader.GetInt32(0);
                                ogrenciInfo.ÖğrenciNo = "" + reader.GetInt32(1);
                                ogrenciInfo.İsim = reader.GetString(2);
                                ogrenciInfo.Cinsiyet = reader.GetString(3);
                                ogrenciInfo.Bölüm = reader.GetString(4);

                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            ogrenciInfo.id = Request.Form["id"];
            ogrenciInfo.ÖğrenciNo = Request.Form["öğrencino"];
            ogrenciInfo.İsim = Request.Form["isim"];
            ogrenciInfo.Cinsiyet = Request.Form["cinsiyet"];
            ogrenciInfo.Bölüm = Request.Form["bölüm"];

            if (ogrenciInfo.id.Length == 0 || ogrenciInfo.ÖğrenciNo.Length == 0 || ogrenciInfo.İsim.Length == 0 || ogrenciInfo.Cinsiyet.Length == 0 || ogrenciInfo.Bölüm.Length == 0)
            {
                errorMessage = "All the field are required";
                return;
            }

            try
            {
                String connectionString = "Data Source=DESKTOP-1OMDOOA;Initial Catalog=tablolar;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE Ögrenci" + "SET ÖgrenciNo=@ÖgrenciNo, İsim=@İsim, Cinsiyet=@Cinsiyet, Bölüm=@Bölüm" + "WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ÖgrenciNo", ogrenciInfo.ÖğrenciNo);
                        command.Parameters.AddWithValue("@İsim", ogrenciInfo.İsim);
                        command.Parameters.AddWithValue("@Cinsiyet", ogrenciInfo.Cinsiyet);
                        command.Parameters.AddWithValue("@Bölüm", ogrenciInfo.Bölüm);
                        command.Parameters.AddWithValue("@id", ogrenciInfo.id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("http://localhost:5039/%C3%96%C4%9Frenci/IndexOgrenci");

        }
    }
}
